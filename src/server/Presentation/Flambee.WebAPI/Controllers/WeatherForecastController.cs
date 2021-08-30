using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Flambee.Service.AppServiceProviders.Authentication;
using Flambee.Service.AppServiceProviders.User;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Flambee.WebAPI.Controllers
{
    //[Authorize]
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : BaseController
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly IAuthService _authService;
        private readonly IUserService _userDetailsService;
        private readonly ILogger<WeatherForecastController> _logger;
        private readonly IMapper _mapper;


        public WeatherForecastController(ILogger<WeatherForecastController> logger,
            IAuthService authService,
            IUserService userDetailsService,
            IMapper mapper)
        {
            _logger = logger;
            _authService = authService;
            _userDetailsService = userDetailsService;
            _mapper = mapper;
        }

        [Authorize]
        [HttpGet]
        public IEnumerable<WeatherForecast> Get()
        {
            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            })
            .ToArray();
        }

        [HttpGet]
        [Route("/UserInfo")]
        public async Task<IActionResult> Get(int range = 5)
        {
            var userInfo = UserInfo;

            var u = new
            {
                a = "A",
                b = "B",
                c = "C",
                d = "D",
            };

            return Ok(u);
        }
    }
}
