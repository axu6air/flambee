using Flambee.Service.AppServiceProviders;
using Flambee.WebAPI.Attributes;
using Flambee.WebAPI.DataTransferModel;
using Flambee.WebAPI.DataTransferModel.User;
using Flambee.WebAPI.Factories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Flambee.WebAPI.Controllers
{
    [Route("/[controller]")]
    [ApiController]
    [Authorize]
    public class UserController : BaseController
    {
        private readonly IUserService _userService;
        private readonly IUserFactory _userFactory;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserController(IUserService userService, IUserFactory userFactory, IHttpContextAccessor httpContextAccessor)
        {
            _userService = userService;
            _userFactory = userFactory;
            _httpContextAccessor = httpContextAccessor;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var id = LoggedInUserModel.UserId;
            var user = await _userService.GetUser(id);

            if (user == null)
                return BadRequest();

            var model = _userFactory.PrepareUserProfileResponseModel(user);

            return Ok(model);
        }

        [HttpPut]
        [ValidateModelState]
        public async Task<IActionResult> Update([FromBody] UserProfileSubmitModel model)
        {
            var user = await _userService.GetUser(model.UserId);

            if (user == null)
                return BadRequest();

            _userFactory.PrepareUserUpdate(model, user);
            await _userService.UpdateUser(user);

            return Ok(new BaseResponseModel
            {
                Message = "User successfully updated",
                Status = StatusCodes.Status200OK
            });
        }

    }
}
