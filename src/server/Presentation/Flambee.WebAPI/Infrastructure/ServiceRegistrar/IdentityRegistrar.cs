using Flambee.Core.Configuration;
using Flambee.Core.Domain.Authentication;
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

namespace Flambee.WebAPI.Infrastructure.ServiceRegistrar
{
    public static class IdentityRegistrar
    {
        public static void RegisterIdentity(this IServiceCollection service, IConfiguration configuration)
        {
            //var mongoDbSettings = configuration.GetSection(nameof(MongoDbConfig)).Get<MongoDbConfig>();
            //service.AddIdentity<ApplicationUser, ApplicationRole>()
            //   .AddMongoDbStores<ApplicationUser, ApplicationRole, Guid>
            //    (
            //        mongoDbSettings.ConnectionString, mongoDbSettings.Name
            //    );
             
        }
    }
}
