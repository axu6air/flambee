using Flambee.Core.Domain.Authentication;
using Flambee.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Flambee.WebAPI.Infrastructure.ServiceRegistrar
{
    public static class IdentityRegistrar
    {
        public static void RegisterIdentity(this IServiceCollection service)
        {
            service.AddIdentity<ApplicationUser, ApplicationRole>(option => option.SignIn.RequireConfirmedEmail = true)
               .AddEntityFrameworkStores<ApplicationDbContext>()
               .AddDefaultTokenProviders()
               .AddUserStore<UserStore<ApplicationUser, ApplicationRole, ApplicationDbContext, Guid>>()
               .AddRoleStore<RoleStore<ApplicationRole, ApplicationDbContext, Guid>>();

            service.Configure<IdentityOptions>(option => {
                option.Password.RequiredLength = 6;
                option.Password.RequireNonAlphanumeric = false;
                option.Password.RequireLowercase = false;
                option.Password.RequireUppercase = false;
                option.Password.RequireDigit = true;
                option.User.RequireUniqueEmail = true;
            });
        }
    }
}
