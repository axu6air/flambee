using App.Core;
using App.Core.Data;
using App.Service.AppServiceProviders.Authentication;
using App.Service.AppServiceProviders.Email;
using App.Service.AppServiceProviders.User;
using App.WebAPI.Factories.Auth;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Reflection;

namespace App.WebAPI.Infrastructure.ServiceRegistrar
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