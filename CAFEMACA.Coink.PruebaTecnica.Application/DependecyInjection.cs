// ****************************************************************
//  Assembly         : CAFEMACA.Coink.PruebaTecnica.Application
//  Author           :  Carlos Fernando Malagón Cano
//  Created          : 04-12-2024
//
//  Last Modified By : Carlos Fernando Malagón Cano
//  Last Modified On : 04-12-2024
//  ****************************************************************
//  <copyright file="DependecyInjection.cs"
//      company="Cafemaca - CAFEMACA Colombia">
//      Cafemaca - CAFEMACA Colombia
//  </copyright>
//

using CAFEMACA.Coink.PruebaTecnica.Application.Common.Abstractions.Interfaces.Services;
using CAFEMACA.Coink.PruebaTecnica.Application.Common.Abstractions.Interfaces.Services.Location;
using CAFEMACA.Coink.PruebaTecnica.Application.Common.Dtos.Location;
using CAFEMACA.Coink.PruebaTecnica.Application.Common.Dtos.User;
using CAFEMACA.Coink.PruebaTecnica.Application.Common.Profiles;
using CAFEMACA.Coink.PruebaTecnica.Application.Common.Validators.Location;
using CAFEMACA.Coink.PruebaTecnica.Application.Common.Validators.User;
using CAFEMACA.Coink.PruebaTecnica.Application.UseCases.Audit;
using CAFEMACA.Coink.PruebaTecnica.Application.UseCases.Location;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace CAFEMACA.Coink.PruebaTecnica.Application
{
    public static class DependecyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            #region Validators

            services.AddTransient<IValidator<PaisCreateRequest>, PaisCreateRequestValidator>();
            services.AddTransient<IValidator<PaisUpdateRequest>, PaisUpdateRequestValidator>();

            services.AddTransient<IValidator<DepartamentoCreateRequest>, DepartamentoCreateRequestValidator>();
            services.AddTransient<IValidator<DepartamentoUpdateRequest>, DepartamentoUpdateRequestValidator>();

            services.AddTransient<IValidator<MunicipioCreateRequest>, MunicipioCreateRequestValidator>();
            services.AddTransient<IValidator<MunicipioUpdateRequest>, MunicipioUpdateRequestValidator>();

            services.AddTransient<IValidator<UsuarioRequest>, UsuarioRequestValidator>();
            #endregion

            services.AddAutoMapper(Assembly.GetAssembly(typeof(MappingProfile)));

            #region Services
            services.AddScoped<IPaisServices, PaisServices>();
            services.AddScoped<IDepartamentoServices, DepartamentoServices>();
            services.AddScoped<IMunicipioServices, MunicipioServices>();

            services.AddScoped<IAuditTrailServices, AuditTrailServices>();

            #endregion

            return services;
        }
    }
}
