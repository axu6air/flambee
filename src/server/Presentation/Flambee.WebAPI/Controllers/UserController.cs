using Flambee.Service.AppServiceProviders.User;
using Flambee.WebAPI.Attributes;
using Flambee.WebAPI.DataTransferModel;
using Flambee.WebAPI.DataTransferModel.User;
using Flambee.WebAPI.Factories.User;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Flambee.WebAPI.Controllers
{
    [Route("/[controller]")]
    [ApiController]
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

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var user = await _userService.GetUserInfoById(id);

            if (user == null)
                return BadRequest();

            var model = _userFactory.PrepareUserProfileResponseModel(user);

            return Ok(model);
        }

        [HttpPut]
        [ValidateModelState]
        public async Task<IActionResult> Update([FromBody] UserProfileSubmitModel model)
        {
            var user = await _userService.GetApplicationUser(model.UserId);

            if (user == null)
                return BadRequest();

            await _userFactory.UpdateUserProfile(model, user);

            return Ok(new BaseResponseModel
            {
                Message = "User successfully updated",
                Status = StatusCodes.Status200OK
            });
        }

    }
}
