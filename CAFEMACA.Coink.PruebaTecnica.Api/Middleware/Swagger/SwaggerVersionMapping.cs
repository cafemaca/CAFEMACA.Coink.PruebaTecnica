// ****************************************************************
//  Assembly         : Assembly Name
//  Author           :  Carlos Fernando Malagón Cano
//  Created          : mm-dd-yyyy 
//
//  Last Modified By : 
//  Last Modified On : mm-dd-yyyy
//  ****************************************************************
//  <copyright file="SwaggerVersionMapping.cs"
//      company="Cafemaca - CAFEMACA Colombia">
//      Cafemaca - CAFEMACA Colombia
//  </copyright>
//

using Microsoft.OpenApi.Models;

using Swashbuckle.AspNetCore.SwaggerGen;

namespace CAFEMACA.Coink.PruebaTecnica.Api.Middleware.Swagger
{
    /// <summary>
    /// Clase que define el mapeo entre el manejo de versiones de los servicios.
    /// </summary>
    /// <seealso cref="IDocumentFilter" />
    public class SwaggerVersionMapping : IDocumentFilter
    {
        /// <summary>
        /// Método de aplicación de la forma en que se maneja el versionamiento.
        /// </summary>
        /// <param name="swaggerDoc"></param>
        /// <param name="context"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public void Apply(OpenApiDocument swaggerDoc, DocumentFilterContext context)
        {
            if (swaggerDoc == null)
            {
                throw new ArgumentNullException(nameof(swaggerDoc));
            }

            var replacements = new OpenApiPaths();

            foreach (var (key, value) in swaggerDoc.Paths)
            {
                replacements.Add(key.Replace("{version}", swaggerDoc.Info.Version,
                        StringComparison.InvariantCulture), value);
            }

            swaggerDoc.Paths = replacements;
        }
    }
}
