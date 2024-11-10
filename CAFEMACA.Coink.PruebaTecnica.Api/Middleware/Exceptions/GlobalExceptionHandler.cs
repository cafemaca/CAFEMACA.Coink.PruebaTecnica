// ****************************************************************
//  Assembly         : CAFEMACA.Coink.PruebaTecnica.Api
//  Author           :  Carlos Fernando Malagón Cano
//  Created          : 07-23-2024
//
//  Last Modified By : Carlos Fernando Malagón Cano
//  Last Modified On : 07-23-2024
//  ****************************************************************
//  <copyright file="GlobalExceptionHandler.cs"
//      company="Cafemaca - CAFEMACA Colombia">
//      Cafemaca - CAFEMACA Colombia
//  </copyright>
//

using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;

namespace CAFEMACA.Coink.PruebaTecnica.Api.Middleware.Exceptions
{
    /// <summary>
    /// 
    /// </summary>
    public class GlobalExceptionHandler : IExceptionHandler
    {
        private const string UnhandledExceptionMsg = "An unhandled exception has occurred while executing the request.";
        private const string AppName = "CAFEMACA.Coink.PruebaTecnica.";

        private readonly IHostEnvironment _env;
        private readonly ILogger<GlobalExceptionHandler> logger;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="env"></param>
        /// <param name="logger"></param>
        public GlobalExceptionHandler(IHostEnvironment env, ILogger<GlobalExceptionHandler> logger)
        {
            this._env = env;
            this.logger = logger;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="httpContext"></param>
        /// <param name="exception"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async ValueTask<bool> TryHandleAsync(
            HttpContext httpContext,
            Exception exception,
            CancellationToken cancellationToken)
        {
            this.logger.LogError(exception, UnhandledExceptionMsg);

            ProblemDetails problemDetails = CreateProblemDetails(httpContext, exception);
            /*
            await httpContext.Response.WriteAsJsonAsync(new ProblemDetails
            {
                Status = StatusCodes.Status500InternalServerError,
                Title = $"{AppName} Internal Exception. {UnhandledExceptionMsg}",
                Detail = exception.Message,
            }, cancellationToken: cancellationToken);
            */
            await httpContext.Response.WriteAsJsonAsync(problemDetails, cancellationToken: cancellationToken);

            return true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <param name="exception"></param>
        /// <returns></returns>
        private ProblemDetails CreateProblemDetails(in HttpContext context, in Exception exception)
        {
            var statusCode = StatusCodes.Status500InternalServerError;
            var reasonPhrase = ReasonPhrases.GetReasonPhrase(statusCode);
            if (string.IsNullOrEmpty(reasonPhrase))
            {
                reasonPhrase = $"{AppName} Internal Exception. {UnhandledExceptionMsg}";
            }
            else
            {
                reasonPhrase = $"{AppName} {reasonPhrase}";
            }

            var problemDetails = new ProblemDetails
            {
                Status = statusCode,
                Title = reasonPhrase
            };

            if (!_env.IsDevelopment())
            {
                return problemDetails;
            }

            problemDetails.Detail = exception.ToString();
            problemDetails.Extensions["traceId"] = context.TraceIdentifier;
            problemDetails.Extensions["data"] = exception.Data;

            return problemDetails;
        }
    }
}
