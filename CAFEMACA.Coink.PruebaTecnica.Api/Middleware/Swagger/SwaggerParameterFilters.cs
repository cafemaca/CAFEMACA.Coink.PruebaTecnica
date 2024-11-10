// ****************************************************************
//  Assembly         : Assembly Name
//  Author           :  Carlos Fernando Malagón Cano
//  Created          : mm-dd-yyyy 
//
//  Last Modified By : 
//  Last Modified On : mm-dd-yyyy
//  ****************************************************************
//  <copyright file="SwaggerParameterFilters.cs"
//      company="Cafemaca - CAFEMACA Colombia">
//      Cafemaca - CAFEMACA Colombia
//  </copyright>
//

using Asp.Versioning;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;

using Swashbuckle.AspNetCore.SwaggerGen;

using static CAFEMACA.Coink.PruebaTecnica.Api.Middleware.Swagger.SwaggerConfig;

namespace CAFEMACA.Coink.PruebaTecnica.Api.Middleware.Swagger
{
    /// <summary>
    /// Clase que define los filtros que se aplican a la configuraciòn de Swagger
    /// </summary>
    /// <seealso cref="IOperationFilter" />
    public class SwaggerParameterFilters : IOperationFilter
    {
        /// <summary>
        /// Applies the specified operation.
        /// </summary>
        /// <param name="operation">The operation.</param>
        /// <param name="context">The context.</param>
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            try
            {
                var maps = context.MethodInfo.GetCustomAttributes(true).OfType<MapToApiVersionAttribute>().SelectMany(attr => attr.Versions).ToList();
                if (maps.Any())
                {
                    var version = maps[0].MajorVersion;
                    if (CurrentVersioningMethod == VersioningType.CustomHeader && !context.ApiDescription.RelativePath.Contains("{version}"))
                    {
                        operation.Parameters.Add(new OpenApiParameter { Name = CustomHeaderParam, In = ParameterLocation.Header, Required = false, Schema = new OpenApiSchema { Type = "String", Default = new OpenApiString(version.ToString()) } });
                    }
                    else if (CurrentVersioningMethod == VersioningType.QueryString && !context.ApiDescription.RelativePath.Contains("{version}"))
                    {
                        operation.Parameters.Add(new OpenApiParameter { Name = QueryStringParam, In = ParameterLocation.Query, Schema = new OpenApiSchema { Type = "String", Default = new OpenApiString(version.ToString()) } });
                    }
                    else if (CurrentVersioningMethod == VersioningType.AcceptHeader && !context.ApiDescription.RelativePath.Contains("{version}"))
                    {

                        operation.Parameters.Add(new OpenApiParameter { Name = "Accept", In = ParameterLocation.Header, Required = false, Schema = new OpenApiSchema { Type = "String", Default = new OpenApiString($"application/json;{AcceptHeaderParam}=" + version.ToString()) } });

                    }

                    var versionParameter = operation.Parameters.Single(p => p.Name == "version");

                    if (versionParameter != null)
                    {
                        operation.Parameters.Remove(versionParameter);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
