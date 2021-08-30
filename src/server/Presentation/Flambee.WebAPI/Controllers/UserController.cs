using Flambee.Service.AppServiceProviders.User;
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
        private readonly IUserInfoService _userService;

        public UserController(IUserInfoService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var userInfo = await _userService.GetUserInfoById(id);
            return Ok();
        }

    }
}
