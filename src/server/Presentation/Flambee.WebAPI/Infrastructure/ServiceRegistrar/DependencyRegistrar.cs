using Flambee.Core;
using Flambee.Service.AppServiceProviders.Authentication;
using Flambee.Service.AppServiceProviders.Email;
using Flambee.Service.AppServiceProviders.User;
using Flambee.WebAPI.Factories.Auth;
using Microsoft.Extensions.DependencyInjection;
using Flambee.Service.AppServiceProviders.Image;
using Flambee.WebAPI.Factories.Image;
using Microsoft.AspNetCore.Http;
using Flambee.WebAPI.Factories.User;
using Flambee.Data;

namespace Flambee.WebAPI.Infrastructure.ServiceRegistrar
{
    public static class DependencyRegistrar
    {
        public static void RegisterDependencyInjection(this IServiceCollection service)
        {
            service.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            service.AddScoped(typeof(IRepository<>), typeof(EntityRepository<>));

            service.AddTransient<IUserService, UserService>();
            service.AddScoped<IAuthService, AuthService>();
            service.AddScoped<IEmailService, EmailService>();
            service.AddScoped<IImageService, ImageService>();

            service.AddScoped<IAuthFactory, AuthFactory>();
            service.AddScoped<IImageFactory, ImageFactory>();
            service.AddScoped<IUserFactory,  UserFactory>();
        }
    }
}