// ****************************************************************
//  Assembly         : CAFEMACA.Coink.PruebaTecnica.Api
//  Author           :  Carlos Fernando Malagón Cano
//  Created          : 07-23-2024
//
//  Last Modified By : Carlos Fernando Malagón Cano
//  Last Modified On : 07-23-2024
//  ****************************************************************
//  <copyright file="ApplicationErrors.cs"
//      company="Cafemaca - CAFEMACA Colombia">
//      Cafemaca - CAFEMACA Colombia
//  </copyright>
//

namespace CAFEMACA.Coink.PruebaTecnica.Api.Common
{
    /// <summary>
    /// Record that define the Error about the Application
    /// </summary>
    /// <param name="ErrorCode">Error Code</param>
    /// <param name="ErrorMessage">Error Message about the error.</param>
    public sealed record ApplicationError(string ErrorCode, string? ErrorMessage = null)
    {
        public static readonly ApplicationError None = new(string.Empty);
    }

    /// <summary>
    /// The differents Application Errors.
    /// </summary>
    public static class ApplicationErrors
    {
        #region Application Validator Errors
        #endregion

        #region Bussines Errors
        /// <summary>
        /// 
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public static ApplicationError ValidPropertiesPage(int pageIndex, int pageSize) => new("CAFEMACA.Coink.PruebaTecnica.Api.ValidPropertiesPage", $"{nameof(pageIndex)} and {nameof(pageSize)} size must be greater than 0.");
        #endregion
    }
}
