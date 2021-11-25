using Flambee.Core;
using Flambee.Service.AppServiceProviders.Authentication;
using Flambee.Service.AppServiceProviders.Email;
using Flambee.Service.AppServiceProviders;
using Flambee.WebAPI.Factories.Auth;
using Microsoft.Extensions.DependencyInjection;
using Flambee.Service.AppServiceProviders.Image;
using Flambee.WebAPI.Factories.Image;
using Microsoft.AspNetCore.Http;
using Flambee.WebAPI.Factories;
using Flambee.Data;
using AspNetCore.Identity.MongoDbCore.Infrastructure;

namespace Flambee.WebAPI.Infrastructure.ServiceRegistrar
{
    public static class DependencyRegistrar
    {
        public static void RegisterDependencyInjection(this IServiceCollection service)
        {
            service.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            service.AddSingleton<IBaseEntity, BaseEntity>();
            //service.AddScoped(typeof(IRepository<>), typeof(EntityRepository<>));
            service.AddScoped(typeof(IMongoRepository<>), typeof(MongoRepository<>));

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