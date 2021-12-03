using Flambee.Core.Configuration;
using Flambee.Core.Domain.Authentication;
using Flambee.Core.Domain.UserDetails;
using Flambee.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Flambee.WebAPI.Infrastructure.ServiceRegistrar
{
    public static class IdentityRegistrar
    {
        public static void RegisterIdentity(this IServiceCollection service, string connectionString)
        {
            //var mongoDbSettings = configuration.GetSection(nameof(MongoDbConfig)).Get<MongoDbConfig>();
            //service.AddIdentity<ApplicationUser, ApplicationRole>()
            //   .AddMongoDbStores<ApplicationUser, ApplicationRole, Guid>
            //    (
            //        mongoDbSettings.ConnectionString, mongoDbSettings.Name
            //    );

            service.AddIdentityMongoDbProvider<User>(identity =>
            {
                identity.Password.RequireDigit = false;
                identity.Password.RequireLowercase = false;
                identity.Password.RequireNonAlphanumeric = false;
                identity.Password.RequireUppercase = false;
                identity.Password.RequiredLength = 1;
                identity.Password.RequiredUniqueChars = 0;
            },
                mongo =>
                {
                    mongo.ConnectionString = connectionString;
                }
            );
        }
    }
}
