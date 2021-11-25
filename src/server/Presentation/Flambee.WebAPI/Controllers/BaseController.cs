using Flambee.Service.AppServiceProviders;
using Flambee.WebAPI.Models.User;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using MongoDB.Bson;

namespace Flambee.WebAPI.Controllers
{
    [Controller]
    public abstract class BaseController : ControllerBase
    {
        public UserLoginModel UserLoginInfo
        {
            get
            {
                //var mapper = HttpContext.RequestServices.GetService<IMapper>();
                return new UserLoginModel
                {
                    UserId = ObjectId.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out var userObjectId) ? userObjectId : ObjectId.Empty,
                    Username = User.FindFirstValue(ClaimTypes.Name),
                    Email = User.FindFirstValue(ClaimTypes.Email),
                };
            }
        }
    }
}
