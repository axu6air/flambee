using Flambee.Core.Configuration.Email;
using Flambee.Core.Domain.Authentication;
using Flambee.Service.AppServiceProviders.Authentication;
using Flambee.Service.AppServiceProviders.Email;
using Flambee.Service.AppServiceProviders.User;
using Flambee.WebAPI.DataTransferModel;
using Flambee.WebAPI.DataTransferModel.Auth;
using Flambee.WebAPI.Factories.Auth;
//using Flambee.WebAPI.Models;
using Flambee.WebAPI.Models.Authentication;
using Flambee.WebAPI.Models.User;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using Flambee.Core.Configuration.User;

namespace Flambee.WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : BaseController
    {
        private readonly IAuthService _authService;
        private readonly IConfiguration _configuration;
        private readonly IAuthFactory _authFactory;
        private readonly IUserInfoService _userDetailsService;
        private readonly IEmailService _emailService;
        private readonly IMapper _mapper;

        public AuthController(IAuthService authService,
            IConfiguration configuration,
            IAuthFactory authFactory,
            IUserInfoService userDetailsService,
            IEmailService emailService,
            IMapper mapper)
        {
            _authService = authService;
            _configuration = configuration;
            _authFactory = authFactory;
            _userDetailsService = userDetailsService;
            _emailService = emailService;
            _mapper = mapper;
        }

        [HttpPost]
        [Route("/Login")]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            var user = await _authService.FindByNameAsync(model.Username);
            if (user != null && await _authService.CheckPasswordAsync(user, model.Password))
            {
                await _authService.SignInAsync(user, true);
                var userRoles = await _authService.GetRolesAsync(user);

                var token = GenerateJwt(user, userRoles);

                var response = new LoginResponseModel
                {
                    Status = StatusCodes.Status200OK,
                    Token = new JwtSecurityTokenHandler().WriteToken(token),
                    UserInfoModel = await GetUserInfo(user),
                    Message = "Successfully logged in"
                };

                return Ok(response);
            }
            return BadRequest(new LoginResponseModel
            {
                Status = StatusCodes.Status401Unauthorized,
                Token = String.Empty,
                UserInfoModel = null,
                Errors = new List<string> { "Username or password is incorrect" },
                
            });
        }

        [HttpPost]
        [Route("/Register")]
        public async Task<IActionResult> Register([FromBody] RegistrationModel model)
        {
            var userExists = await _authService.FindByNameAsync(model.Username);
            if (userExists != null)
                return BadRequest(_authFactory.PrepareRegistrationResponseModel());

            ApplicationUser user = new ApplicationUser()
            {
                Email = model.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = model.Username
            };

            var result = await _authService.CreateAsync(user, model.Password);
            var response = _authFactory.PrepareRegistrationResponseModel(result, user);

            if (!result.Succeeded)
                return BadRequest(response);

            await AssignRole(user, model);

            return Ok(response);
        }

        [HttpPost]
        [Route("/Register-Admin")]
        public async Task<IActionResult> RegisterAdmin([FromBody] RegistrationModel model)
        {
            var userExists = await _authService.FindByNameAsync(model.Username);
            if (userExists != null)
                return StatusCode(StatusCodes.Status500InternalServerError, new BaseResponseModel { Status = StatusCodes.Status400BadRequest, Message = "User already exists!" });

            ApplicationUser user = new ApplicationUser()
            {
                Email = model.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = model.Username
            };
            var result = await _authService.CreateAsync(user, model.Password);
            if (!result.Succeeded)
                return StatusCode(StatusCodes.Status500InternalServerError, new BaseResponseModel { Status = StatusCodes.Status400BadRequest, Message = "User creation failed! Please check user details and try again." });

            await AssignRole(user, model, isAdmin: true);

            return Ok(new BaseResponseModel { Status = StatusCodes.Status200OK, Message = "User created successfully!" });
        }

        [HttpPost]
        [Route("/Logout")]
        public async Task<IActionResult> Logout()
        {
            await _authService.SignOutAsync();
            return Ok();
        }

        [HttpGet]
        [Route("/CheckUsernameAvailability")]
        public async Task<IActionResult> CheckUsernameAvailability(string username)
        {
            if (string.IsNullOrEmpty(username) || string.IsNullOrWhiteSpace(username))
                return Ok(false);

            username = username.Trim();

            Regex regex = new Regex(UserRules.Username);
            if (!regex.IsMatch(username))
                return Ok(new UsernameAvailabilityResponseModel(false));

            var userExists = await _authService.FindByNameAsync(username);

            if (userExists != null)
                return Ok(new UsernameAvailabilityResponseModel(false));

            return Ok(new UsernameAvailabilityResponseModel(true));
        }

        [HttpPost]
        [Route("/RecoveryPasswordRequest")]
        public async Task<IActionResult> RecoveryPassword(RecoveryPasswordRequestModel requestModel)
        {
            if (string.IsNullOrEmpty(requestModel.Email) || string.IsNullOrWhiteSpace(requestModel.Email))
                return BadRequest("");

            var applicationUser = await _userDetailsService.GetApplicationUserAsync(requestModel.Email);

            if (applicationUser == null)
                return BadRequest("");

            StringBuilder resetPasswordLink = new StringBuilder();
            resetPasswordLink.Append(requestModel.ClientRecoveryPasswordUrl);
            resetPasswordLink.Append("?email=" + applicationUser.Email + "&token=");
            resetPasswordLink.Append(await _authService.GeneratePasswordResetTokenAsync(applicationUser));

            var emailRequest = _authFactory.PrepareRecoveryPasswordEmailRequest(requestModel.Email, resetPasswordLink.ToString());

            if (emailRequest == null)
                return Problem(
                       detail: "Internal server error occured",
                       statusCode: StatusCodes.Status500InternalServerError
                   );

            var res = await _emailService.SendEmailAsync(emailRequest);

            if (!res)
                return Problem(
                    detail: "Internal server error occured",
                    statusCode: StatusCodes.Status500InternalServerError
                );

            return Ok(new BaseResponseModel
            {
                Status = StatusCodes.Status200OK,
                Message = "Check your email to reset your password"
            });
        }

        [HttpPost]
        [Route("/RecoveryPassword")]
        public async Task<IActionResult> RecoveryPassword(RecoveryPasswordConfirmationModel confirmationModel)
        {
            if (!ModelState.IsValid)
                return BadRequest(new BaseErrorModel
                {
                    Status = StatusCodes.Status400BadRequest,
                    Errors = new List<string> { "Please enter the fields correctly."}
                });

            var applicationUser = await _userDetailsService.GetApplicationUserAsync(confirmationModel.Email);

            if (applicationUser == null)
                return BadRequest(new BaseErrorModel
                {
                    Status = StatusCodes.Status400BadRequest,
                    Errors = new List<string> { "No such user." }
                });

            var result = await _authService.ResetPasswordAsync(applicationUser, confirmationModel.Token, confirmationModel.Password);

            if (!result.Succeeded)
                return BadRequest(new BaseErrorModel
                {
                    Status = StatusCodes.Status400BadRequest,
                    Errors = result.Errors.Select(res => res.Description).ToList()
                });

            return Ok(new BaseResponseModel
            {
                Status = StatusCodes.Status200OK,
                Message = "Password successfully changed"
            });
        }

        [HttpGet]
        [Route("/GetFormRules")]
        public IActionResult GetFormRules()
        {
            FormRulesModel rulesModel = new();
            return Ok(rulesModel);
        } 

        private async Task AssignRole(ApplicationUser user, RegistrationModel model, bool isAdmin = false)
        {
            if (!await _authService.RoleExistsAsync(UserRoles.User))
                await _authService.CreateAsync(new ApplicationRole(UserRoles.User));

            if (isAdmin)
            {
                if (!await _authService.RoleExistsAsync(UserRoles.Admin))
                    await _authService.CreateAsync(new ApplicationRole(UserRoles.Admin));

                if (await _authService.RoleExistsAsync(UserRoles.Admin))
                    await _authService.AddToRoleAsync(user, UserRoles.Admin);
            }
            else
                await _authService.AddToRoleAsync(user, UserRoles.User);



            var userInfo = _authFactory.PrepareUserInfo(model, user);
            await _userDetailsService.CreateUserInfo(userInfo);
        }

        private async Task<UserInfoModel> GetUserInfo(ApplicationUser user)
        {
            if (user == null)
                return new UserInfoModel();
            var userInfo = await _userDetailsService.GetUserInfoById(user.Id);
            var userInfoModel = _mapper.Map<UserInfoModel>(userInfo);
            userInfoModel.UserModel = _mapper.Map<UserModel>(user);

            return userInfoModel;
        }

        private JwtSecurityToken GenerateJwt(ApplicationUser user, IList<string> userRoles)
        {
            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));

            var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim("UserID", user.Id.ToString()),
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                };

            foreach (var userRole in userRoles)
            {
                authClaims.Add(new Claim(ClaimTypes.Role, userRole));
            }

            var token = new JwtSecurityToken(
                issuer: _configuration["JWT:ValidIssuer"],
                audience: _configuration["JWT:ValidAudience"],
                expires: DateTime.Now.AddDays(1), //null,
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256));

            return token;

        }

    }
}
