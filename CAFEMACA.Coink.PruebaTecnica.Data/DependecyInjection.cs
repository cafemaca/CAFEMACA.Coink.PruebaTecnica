// ****************************************************************
//  Assembly         : CAFEMACA.Coink.PruebaTecnica.Data
//  Author           :  cmalagoncmalagon
//  Created          : 04-02-2024
//
//  Last Modified By : Carlos Fernando Malagón Cano
//  Last Modified On : 09-20-2024
//  ****************************************************************
//  <copyright file="DependecyInjection.cs"
//      company="Cafemaca - CAFEMACA Colombia">
//      Cafemaca - CAFEMACA Colombia
//  </copyright>
//

using CAFEMACA.Coink.PruebaTecnica.Application.Common.Abstractions.Interfaces.Repositories;
using CAFEMACA.Coink.PruebaTecnica.Application.Common.Abstractions.Interfaces.Repository;
using CAFEMACA.Coink.PruebaTecnica.Data.Cache.Players;
using CAFEMACA.Coink.PruebaTecnica.Data.Repositories;
using CAFEMACA.Coink.PruebaTecnica.Data.Repositories.Audit;
using CAFEMACA.Coink.PruebaTecnica.Data.Repositories.Players;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CAFEMACA.Coink.PruebaTecnica.Data
{
    /// <summary>
    /// 
    /// </summary>
    public static class DependecyInjection
    {
        public static IServiceCollection AddDataRepositories(this IServiceCollection services, IConfiguration Configuration)
        {
            //services.AddMemoryCache();
            services.AddDistributedMemoryCache(); //Change provider based on the need 
                                                  //One can use Redis, SQL or Custom Supported Cache

            services.AddScoped<IUnitOfWork, UnitOfWork>();

            //Decorate pattern for MemoryCache.
            services.AddScoped<PlayerRepository>();
            services.AddTransient<IPlayerRepository, CachedMemoryPlayerRepository>();

            services.AddScoped<IAuditTrailRepository, AuditTrailRepository>();
            return services;
        }
    }
}
