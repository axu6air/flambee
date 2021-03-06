using Flambee.Service.AppServiceProviders.User;
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

namespace Flambee.WebAPI.Controllers
{
    [Controller]
    public abstract class BaseController : ControllerBase
    {
        public Task<UserInfoModel> UserInfo => GetUserInfo();

        private async Task<UserInfoModel> GetUserInfo()
        {
            var mapper = HttpContext.RequestServices.GetService<IMapper>();
            var userDetailsService = HttpContext.RequestServices.GetService<IUserService>();
            var token = await HttpContext.GetTokenAsync("access_token");
            var user = await userDetailsService.GetLoggedInApplicationUserAsync();
            if (user == null)
                return new UserInfoModel(); 

            var userInfoModel = mapper.Map<UserInfoModel>(user.UserInfo);
            userInfoModel.UserModel = mapper.Map<UserModel>(user);

            return userInfoModel;
        }



    }
}
