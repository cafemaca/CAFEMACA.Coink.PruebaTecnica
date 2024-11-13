// ****************************************************************
//  Assembly         : CAFEMACA.Coink.PruebaTecnica.Api
//  Author           :  Carlos Fernando Malagón Cano
//  Created          : 04-18-2024 
//
//  Last Modified By : Carlos Fernando Malagón Cano
//  Last Modified On : 06-25-2024
//  ****************************************************************
//  <copyright file="ProgramHelpers.cs"
//      company="Cafemaca - CAFEMACA Colombia">
//      Cafemaca - CAFEMACA Colombia
//  </copyright>
//

using Asp.Versioning;
using CAFEMACA.Coink.PruebaTecnica.Api.Middleware.Exceptions;
using CAFEMACA.Coink.PruebaTecnica.Api.Middleware.HealthCheck;
using CAFEMACA.Coink.PruebaTecnica.Api.Middleware.Swagger;
using CAFEMACA.Coink.PruebaTecnica.Application;
using CAFEMACA.Coink.PruebaTecnica.Application.Common.Abstractions.Interfaces.Services;
using CAFEMACA.Coink.PruebaTecnica.Application.UseCases.Users;
using CAFEMACA.Coink.PruebaTecnica.Data;
using CAFEMACA.Coink.PruebaTecnica.Data.Common.Interceptors;
using CAFEMACA.Coink.PruebaTecnica.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.OpenApi.Models;
using Serilog;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Reflection;

namespace CAFEMACA.Coink.PruebaTecnica.Api.Extensions
{
    /// <summary>
    /// Static class that premit configurate the diferent services in the program.
    /// </summary>
    public static class ApplicationServiceExtensions
    {
        const string titleSolution = "CAFEMACA.Coink.PruebaTecnica API";
        const string connectionStringValue = "CAFEMACA.Coink.PruebaTecnicaDbConn";

        public static void RegisterMiddleware(this IServiceCollection services)
        {
            services.AddProblemDetails();
            services.AddExceptionHandler<ValidationExceptionHandler>();
            services.AddExceptionHandler<GlobalExceptionHandler>();
        }

        public static void RegisterDB(this IServiceCollection services, ConfigurationManager configuration)
        {
            services.AddDbContext<CafemacaDbContext>((provider, options) =>
            {
                var interceptor = provider.GetRequiredService<AuditableInterceptor>();

                options.UseNpgsql(configuration.GetConnectionString(connectionStringValue))
                .AddInterceptors(interceptor);
            }
            );

        }

        public static void RegisterDependency(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton(configuration);

            //Inyecciòn de dependencias de las capas de infraestructura y de aplicación.
            services.AddDataRepositories(configuration);
            services.AddApplication();

            services.AddHttpContextAccessor();
            services.AddScoped<ICurrentSessionProvider, CurrentSessionProvider>();
            services.AddSingleton<AuditableInterceptor>();
        }

        public static void RegisterSwagger(this IServiceCollection services)
        {
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            services.AddEndpointsApiExplorer();

            services.AddSwaggerGen(options =>
            {
                //Following code to avoid swagger generation error 
                //due to same method name in different versions.
                options.ResolveConflictingActions(descriptions => descriptions.First());

                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "1",
                    Title = titleSolution,
                    Description = "Web API REST para CAFEMACA.Coink.PruebaTecnica",
                    TermsOfService = new Uri(uriString: "https://example.com/terms"),
                    Contact = new OpenApiContact
                    {
                        Name = "Example Contact",
                        Url = new Uri("https://example.com/contact")
                    },
                    License = new OpenApiLicense
                    {
                        Name = "Example License",
                        Url = new Uri("https://example.com/license")
                    }
                });

                options.SwaggerDoc("v2", new OpenApiInfo
                {
                    Version = "2",
                    Title = "CAFEMACA.Coink.PruebaTecnica API",
                    Description = "Web API REST para CAFEMACA.Coink.PruebaTecnica",
                    TermsOfService = new Uri("https://example.com/terms"),
                    Contact = new OpenApiContact
                    {
                        Name = "Example Contact",
                        Url = new Uri("https://example.com/contact")
                    },
                    License = new OpenApiLicense
                    {
                        Name = "Example License",
                        Url = new Uri("https://example.com/license")
                    }
                });

                options.OperationFilter<SwaggerParameterFilters>();
                options.DocumentFilter<SwaggerVersionMapping>();

                // This on used to exclude endpoint mapped to not specified in swagger version.
                // In this particular example we exclude 'GET /api/v2/Values/otherget/three' endpoint,
                // because it was mapped to v3 with attribute: MapToApiVersion("3")
                options.DocInclusionPredicate((version, desc) =>
                {
                    if (!desc.TryGetMethodInfo(out var methodInfo))
                    {
                        return false;
                    }

                    var versions = methodInfo.DeclaringType.GetCustomAttributes(true)
                    .OfType<ApiVersionAttribute>()
                    .SelectMany(attr => attr.Versions);
                    var maps = methodInfo.GetCustomAttributes(true).OfType<MapToApiVersionAttribute>().SelectMany(attr => attr.Versions).ToArray();
                    version = version.Replace("v", "");
                    return maps.Length > 0 ? versions.Any(v => v.ToString() == version && maps.Any(v => v.ToString() == version)) : versions.Any(v => v.ToString() == version);
                });

                // Set the comments path for the Swagger JSON and UI.
                string xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                options.IncludeXmlComments(xmlPath);
            });
        }

        public static void RegisterCORS(this IServiceCollection services, IConfiguration configuration)
        {
            const string AllowSpecificOrigins = "_AllowSpecificOrigins";
            services.AddCors(options =>
            {
                string[] sites = configuration.GetSection("SitesCORSConfig").GetValue<string>("Sites").Split(",");
                options.AddPolicy(name: AllowSpecificOrigins,
                                  builder =>
                                  {
                                      builder.WithOrigins(sites)
                                                          .AllowAnyHeader()
                                                          .AllowAnyMethod();
                                  });
            });
        }

        /// <summary>
        /// The management of version control is configured in accordance with the mentioned article.
        /// Here’s the explanation for the ApiVersioningOptions properties:
        /// DefaultApiVersion - Sets the default API version.Typically, this will be v1.0.
        /// ReportApiVersions - Reports the supported API versions in the api-supported-versions response header.
        /// AssumeDefaultVersionWhenUnspecified - Uses the DefaultApiVersion when the client didn't provide an explicit version.
        /// ApiVersionReader - Configures how to read the API version specified by the client.The default value is QueryStringApiVersionReader.
        /// The AddApiExplorer method is helpful if you are using Swagger. It will fix the endpoint routes and substitute the API version route parameter.
        /// </summary>
        /// <see cref="https://medium.com/@mohammadbourm/api-versioning-in-asp-net-core-7ad07024c7dc"/>
        /// <param name="services">Services</param>
        public static void RegisterApiVersion(this IServiceCollection services)
        {
            services.AddApiVersioning(options =>
                {
                    // ReportApiVersions will return the "api-supported-versions" and "api-deprecated-versions" headers.
                    options.ReportApiVersions = true;

                    // Set a default version when it's not provided,
                    // e.g., for backward compatibility when applying versioning on existing APIs
                    options.AssumeDefaultVersionWhenUnspecified = true;
                    options.DefaultApiVersion = new ApiVersion(1, 0);
                    options.ApiVersionReader = ApiVersionReader.Combine(
                        //new HeaderApiVersionReader("X-Api-Version"),
                        new UrlSegmentApiVersionReader()
                    );
                }
            )
                .AddMvc()
                .AddApiExplorer(options =>
            {
                options.GroupNameFormat = "'v'VVV";
                options.SubstituteApiVersionInUrl = true;
            }
            );
        }

        public static void RegisterLogging(this IServiceCollection services, IConfiguration configuration)
        {
            var logger = new LoggerConfiguration()
                .ReadFrom.Configuration(configuration)
                .Enrich.FromLogContext()
                .CreateLogger();
            services.AddSerilog(logger);
        }

        public static void RegisterHealthCheck(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddHealthChecks()
            .AddNpgSql(configuration.GetConnectionString(connectionStringValue), healthQuery: "select 1", name: "COINK", failureStatus: HealthStatus.Unhealthy, tags: new[] { "Feedback", "Database" })
            .AddCheck<RemoteHealthCheck>("Remote endpoints Health Check", failureStatus: HealthStatus.Unhealthy)
            .AddCheck<MemoryHealthCheck>($"CAFEMACA Service Memory Check", failureStatus: HealthStatus.Unhealthy, tags: new[] { titleSolution })
            .AddUrlGroup(new Uri("https://www.google.com"), name: "google", failureStatus: HealthStatus.Unhealthy);

            services.AddHealthChecksUI(opt =>
            {
                opt.SetEvaluationTimeInSeconds(10); //time in seconds between check    
                opt.MaximumHistoryEntriesPerEndpoint(60); //maximum history of checks    
                opt.SetApiMaxActiveRequests(1); //api requests concurrency    
                opt.AddHealthCheckEndpoint(titleSolution, "/health"); //map health check api    
            }).AddInMemoryStorage();
        }
    }
}
