// ****************************************************************
//  Assembly         : CAFEMACA.Coink.PruebaTecnica.Data
//  Author           :  cmalagoncmalagon
//  Created          : 11-10-2024
//
//  Last Modified By : Carlos Fernando Malagón Cano
//  Last Modified On : 11-10-2024
//  ****************************************************************
//  <copyright file="DependecyInjection.cs"
//      company="Cafemaca - CAFEMACA Colombia">
//      Cafemaca - CAFEMACA Colombia
//  </copyright>
//

using CAFEMACA.Coink.PruebaTecnica.Application.Common.Abstractions.Interfaces.Repositories;
using CAFEMACA.Coink.PruebaTecnica.Application.Common.Abstractions.Interfaces.Repositories.Location;
using CAFEMACA.Coink.PruebaTecnica.Application.Common.Abstractions.Interfaces.Repositories.User;
using CAFEMACA.Coink.PruebaTecnica.Application.Common.Abstractions.Interfaces.Repository;
using CAFEMACA.Coink.PruebaTecnica.Data.Cache.Location;
using CAFEMACA.Coink.PruebaTecnica.Data.Cache.User;
using CAFEMACA.Coink.PruebaTecnica.Data.Repositories;
using CAFEMACA.Coink.PruebaTecnica.Data.Repositories.Audit;
using CAFEMACA.Coink.PruebaTecnica.Data.Repositories.Location;
using CAFEMACA.Coink.PruebaTecnica.Data.Repositories.User;
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
            services.AddScoped<PaisRepository>();
            services.AddTransient<IPaisRepository, CachedMemoryPaisRepository>();

            services.AddScoped<DepartamentoRepository>();
            services.AddTransient<IDepartamentoRepository, CachedMemoryDepartamentoRepository>();

            services.AddScoped<MunicipioRepository>();
            services.AddTransient<IMunicipioRepository, CachedMemoryMunicipioRepository>();

            services.AddScoped<UsuarioRepository>();
            services.AddTransient<IUsuarioRepository, CachedMemoryUsuarioRepository>();

            services.AddScoped<IAuditTrailRepository, AuditTrailRepository>();
            return services;
        }
    }
}
