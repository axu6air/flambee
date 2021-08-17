using Flambee.Core;
using Flambee.Service.AppServiceProviders.Authentication;
using Flambee.Service.AppServiceProviders.Email;
using Flambee.Service.AppServiceProviders.User;
using Flambee.WebAPI.Factories.Auth;
using Microsoft.Extensions.DependencyInjection;
using Flambee.Service.AppServiceProviders.Image;
using Flambee.WebAPI.Factories.Image;

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
            service.AddScoped<IImageService, ImageService>();
            service.AddScoped<IImageFactory, ImageFactory>();
        }
    }
}