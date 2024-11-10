// ****************************************************************
//  Assembly         : Assembly Name
//  Author           :  Carlos Fernando Malagón Cano
//  Created          : mm-dd-yyyy 
//
//  Last Modified By : 
//  Last Modified On : mm-dd-yyyy
//  ****************************************************************
//  <copyright file="SwaggerConfig.cs"
//      company="Cafemaca - CAFEMACA Colombia">
//      Cafemaca - CAFEMACA Colombia
//  </copyright>
//

namespace CAFEMACA.Coink.PruebaTecnica.Api.Middleware.Swagger
{
    /// <summary>
    /// Clase que contiene los valores para configurar Swagger
    /// </summary>
    public class SwaggerConfig
    {
        /// <summary>
        /// Enumeraciòn que define los tipos de versionamiento que se tienen en el manejo de servicios
        /// </summary>
        public enum VersioningType
        {
            /// <summary>
            /// The none
            /// </summary>
            None,
            /// <summary>
            /// The custom header
            /// </summary>
            CustomHeader,
            /// <summary>
            /// The query string
            /// </summary>
            QueryString,
            /// <summary>
            /// The accept header
            /// </summary>
            AcceptHeader
        }

        /// <summary>
        /// Gets the query string parameter.
        /// </summary>
        /// <value>
        /// The query string parameter.
        /// </value>
        public static string? QueryStringParam { get; private set; }

        /// <summary>
        /// Gets the custom header parameter.
        /// </summary>
        /// <value>
        /// The custom header parameter.
        /// </value>
        public static string? CustomHeaderParam { get; private set; }

        /// <summary>
        /// Gets the accept header parameter.
        /// </summary>
        /// <value>
        /// The accept header parameter.
        /// </value>
        public static string? AcceptHeaderParam { get; private set; }
        /// <summary>
        /// Tipo de Manejo de Versión
        /// </summary>
        public static VersioningType CurrentVersioningMethod { get; set; } = VersioningType.None;

        /// <summary>
        /// Uses the custom header API version.
        /// </summary>
        /// <param name="parameterName">Name of the parameter.</param>
        /// <returns></returns>
        public static void UseCustomHeaderApiVersion(string parameterName)
        {
            CurrentVersioningMethod = VersioningType.CustomHeader;
            CustomHeaderParam = parameterName;
        }

        /// <summary>
        /// Uses the query string API version.
        /// </summary>
        /// <returns></returns>
        public static void UseQueryStringApiVersion()
        {
            QueryStringParam = "api-version";
            CurrentVersioningMethod = VersioningType.QueryString;
        }

        /// <summary>
        /// Uses the query string API version.
        /// </summary>
        /// <param name="parameterName">Name of the parameter.</param>
        /// <returns></returns>
        public static void UseQueryStringApiVersion(string parameterName)
        {
            CurrentVersioningMethod = VersioningType.QueryString;
            QueryStringParam = parameterName;
        }

        /// <summary>
        /// Uses the accept header API version.
        /// </summary>
        /// <param name="paramName">Name of the parameter.</param>
        /// <returns></returns>
        public static void UseAcceptHeaderApiVersion(string paramName)
        {
            CurrentVersioningMethod = VersioningType.AcceptHeader;
            AcceptHeaderParam = paramName;
        }
    }
}
