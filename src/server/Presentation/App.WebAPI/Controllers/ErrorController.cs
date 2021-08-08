using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace App.WebAPI.Controllers
{
    public class ErrorController : ControllerBase
    {
        [HttpGet]
        [Route("error")]
        public IActionResult Error()
        {
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }
}
