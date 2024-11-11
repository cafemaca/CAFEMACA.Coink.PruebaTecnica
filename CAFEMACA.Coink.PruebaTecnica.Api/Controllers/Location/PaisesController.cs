// ****************************************************************
//  Assembly         : CAFEMACA.Coink.PruebaTecnica.Api
//  Author           :  Carlos Fernando Malagón Cano
//  Created          : 11-10-2024
//
//  Last Modified By : Carlos Fernando Malagón Cano
//  Last Modified On : 11-10-2024
//  ****************************************************************
//  <copyright file="PaisController.cs"
//      company="Cafemaca - CAFEMACA Colombia">
//      Cafemaca - CAFEMACA Colombia
//  </copyright>
//

using Asp.Versioning;
using CAFEMACA.Coink.PruebaTecnica.Api.Common;
using CAFEMACA.Coink.PruebaTecnica.Api.Extensions;
using CAFEMACA.Coink.PruebaTecnica.Application.Common.Abstractions.Interfaces.Services.Location;
using CAFEMACA.Coink.PruebaTecnica.Application.Common.Dtos.Location;
using CAFEMACA.Coink.PruebaTecnica.Application.Common.Filtering;
using CAFEMACA.Coink.PruebaTecnica.Application.Common.Options;
using CAFEMACA.Coink.PruebaTecnica.Data.Common.Pagining;
using CAFEMACA.Coink.PruebaTecnica.Domain.Common.Abstractions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace CAFEMACA.Coink.PruebaTecnica.Api.Controllers.Location
{
    /// <summary>
    /// Controller de los endpoints para Paises
    /// </summary>
    [ApiVersion("1")]
    [ApiVersion("2")]
    [ApiController]
    [Route("api/v{v:apiVersion}/[controller]")]
    public class UsuariosController : ControllerBase // ControllerBase is a base class for MVC controller without view support.
    {
        private readonly IPaisServices _paisServices;
        private readonly IOptions<ApplicationOptions> _applicationOptions;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="paisServices"></param>
        public UsuariosController(IPaisServices paisServices, IOptions<ApplicationOptions> applicationOptions)
        {
            _paisServices = paisServices;
            _applicationOptions = applicationOptions;
        }

        /// <summary>
        /// Create a new pais 
        /// </summary>
        /// <param name="paisRequest">Pais</param>
        /// <param name="cancellationToken"></param>
        /// <returns>The new Pais Created.</returns>
        [MapToApiVersion("1")]
        [HttpPost]
        [ProducesResponseType(typeof(PaisResponse), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(IEnumerable<DomainError>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreatePais([FromBody] PaisCreateRequest paisRequest, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var result = await _paisServices.CreatePaisAsync(paisRequest, cancellationToken);

            return result.Match<IActionResult>(
                m => CreatedAtAction(nameof(GetPais), new { id = m.Id }, m),
                fail => BadRequest(fail)
                );
        }

        /// <summary>
        /// Get a single pais
        /// </summary>
        /// <param name="id">The id Pais</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [MapToApiVersion("1")]
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(PaisResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(DomainError), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetPais(string id, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var result = await _paisServices.SelectPaisByIdAsync(id, cancellationToken).ConfigureAwait(false);

            return result.Match<IActionResult>(
                m => Ok(m),
                fail => NotFound(fail)
                );
        }

        /// <summary>
        /// Get all paises
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [MapToApiVersion("1")]
        [HttpGet()]
        [ProducesResponseType(typeof(IEnumerable<PaisResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(DomainError), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetPais(CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var paisesResult = await _paisServices.SelectAllPaises(cancellationToken);

            return paisesResult.Match<IActionResult>(
                m => Ok(m),
                fail => NotFound(fail)
                );
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="searchQueryParameters"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [MapToApiVersion("2")]
        [HttpGet("Paging", Name = "PaisPaging")]
        [ProducesResponseType(typeof(IEnumerable<PaisResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(DomainError), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApplicationError), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetPagingPais([FromQuery] SearchQueryParameters searchQueryParameters, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();

            if (searchQueryParameters.PageIndex <= 0 || searchQueryParameters.PageSize <= 0)
                return BadRequest(ApplicationErrors.ValidPropertiesPage(searchQueryParameters.PageIndex, searchQueryParameters.PageSize));

            var paisesResult = await _paisServices.SelectAllPaises(searchQueryParameters, cancellationToken);

            // Add pagination metadata to headers
            var pagedItems = paisesResult.Match<PagedList<PaisResponse>>(
                m => m,
                fail => null
            );
            this.AddPaginationMetadata(pagedItems, searchQueryParameters);

            return paisesResult.Match<IActionResult>(
                m => Ok(m.Items),
                fail => NotFound(fail)
                );
        }

        /// <summary>
        /// Update a pais
        /// </summary>
        /// <param name="id">The Id Pais</param>
        /// <param name="paisRequest">The new Data Pais</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [MapToApiVersion("1")]
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(IEnumerable<DomainError>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdatePais(string id, [FromBody] PaisUpdateRequest paisRequest, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var result = await _paisServices.UpdateAsync(id, paisRequest, cancellationToken).ConfigureAwait(false);

            return result.Match<IActionResult>(
                m => Ok(m),
                fail => BadRequest(fail)
                );
        }

        /// <summary>
        /// Delete a pais
        /// </summary>
        /// <param name="id">The Ide Pais</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [MapToApiVersion("1")]
        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(bool), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeletePais(string id, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var result = await _paisServices.DeletePaisAsync(id, cancellationToken);

            return result.Match<IActionResult>(
                m => Ok(m),
                fail => NotFound(fail)
                );
        }
    }

}
