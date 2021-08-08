using Flambee.Core;
using Flambee.Core.Data;
using Flambee.Service.AppServiceProviders.Authentication;
using Flambee.Service.AppServiceProviders.Email;
using Flambee.Service.AppServiceProviders.User;
using Flambee.WebAPI.Factories.Auth;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Reflection;

namespace Flambee.WebAPI.Infrastructure.ServiceRegistrar
{
    public static class DependencyRegistrar
    {
        public static void RegisterDependencyInjection(this IServiceCollection service)
        {
            service.AddScoped(typeof(IRepository<>), typeof(EntityRepository<>));
            service.AddScoped<IAuthService, AuthService>();
            service.AddTransient<IUserInfoService, UserInfoService>();
            service.AddScoped<IAuthFactory, AuthFactory>();
            service.AddScoped<IEmailService, EmailService>();
        }
    }
}