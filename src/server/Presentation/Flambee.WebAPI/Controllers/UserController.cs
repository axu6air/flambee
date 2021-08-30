using Flambee.Service.AppServiceProviders.User;
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

        public UserController(IUserService userService, IUserFactory userFactory)
        {
            _userService = userService;
            _userFactory = userFactory;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var user = await _userService.GetUserInfoById(id);
            var model = _userFactory.PrepareUserProfileModel(user);

            return Ok(model);
        }

    }
}
