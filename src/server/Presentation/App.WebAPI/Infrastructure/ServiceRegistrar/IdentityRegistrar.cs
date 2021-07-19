using Flambee.Core.Data;
using Flambee.Core.Domain.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
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

            service.Configure<IdentityOptions>(opts => {
                opts.Password.RequiredLength = 6;
                opts.Password.RequireNonAlphanumeric = false;
                opts.Password.RequireLowercase = false;
                opts.Password.RequireUppercase = false;
                opts.Password.RequireDigit = true;
                opts.User.RequireUniqueEmail = true;
            });

        }
    }
}
