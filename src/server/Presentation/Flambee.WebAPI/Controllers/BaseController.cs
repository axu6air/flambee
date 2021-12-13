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
using Flambee.Core.Domain.UserDetails;

namespace Flambee.WebAPI.Controllers
{
    [Controller]
    public abstract class BaseController : ControllerBase
    {
        public UserLoginModel LoggedInUserModel
        {
            get
            {
                //var mapper = HttpContext.RequestServices.GetService<IMapper>();
                return new UserLoginModel
                {
                    UserId = ObjectId.TryParse(base.User.FindFirstValue(ClaimTypes.NameIdentifier), out var userObjectId) ? userObjectId : ObjectId.Empty,
                    Username = base.User.FindFirstValue(ClaimTypes.Name),
                    Email = base.User.FindFirstValue(ClaimTypes.Email),
                };
            }
        }

        protected async Task<User> GetUser()
        {
            var userService = HttpContext.RequestServices.GetService<IUserService>();

            return await userService.FindById(LoggedInUserModel.UserId);
        }
    }
}
