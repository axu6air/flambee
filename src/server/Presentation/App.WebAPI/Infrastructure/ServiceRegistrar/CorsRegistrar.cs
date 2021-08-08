using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace App.WebAPI.Infrastructure.ServiceRegistrar
{
    public static class CorsRegistrar
    {
        public static void RegisterCors(this IServiceCollection service)
        {
            service.AddCors();
        }
    }
}
