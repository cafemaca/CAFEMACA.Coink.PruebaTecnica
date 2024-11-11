// ****************************************************************
//  Assembly         : CAFEMACA.Coink.PruebaTecnica.Api
//  Author           :  Carlos Fernando Malagón Cano
//  Created          : 11-10-2024
//
//  Last Modified By : Carlos Fernando Malagón Cano
//  Last Modified On : 11-10-2024
//  ****************************************************************
//  <copyright file="UsuarioController.cs"
//      company="Cafemaca - CAFEMACA Colombia">
//      Cafemaca - CAFEMACA Colombia
//  </copyright>
//

using Asp.Versioning;
using CAFEMACA.Coink.PruebaTecnica.Api.Common;
using CAFEMACA.Coink.PruebaTecnica.Api.Extensions;
using CAFEMACA.Coink.PruebaTecnica.Application.Common.Abstractions.Interfaces.Services.User;
using CAFEMACA.Coink.PruebaTecnica.Application.Common.Dtos.User;
using CAFEMACA.Coink.PruebaTecnica.Application.Common.Filtering;
using CAFEMACA.Coink.PruebaTecnica.Application.Common.Options;
using CAFEMACA.Coink.PruebaTecnica.Data.Common.Pagining;
using CAFEMACA.Coink.PruebaTecnica.Domain.Common.Abstractions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace CAFEMACA.Coink.PruebaTecnica.Api.Controllers.User
{
    /// <summary>
    /// Controller de los endpoints para Usuarios
    /// </summary>
    [ApiVersion("1")]
    [ApiVersion("2")]
    [ApiController]
    [Route("api/v{v:apiVersion}/[controller]")]
    public class UsuariosController : ControllerBase // ControllerBase is a base class for MVC controller without view support.
    {
        private readonly IUsuarioServices _usuarioServices;
        private readonly IOptions<ApplicationOptions> _applicationOptions;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="usuarioServices"></param>
        public UsuariosController(IUsuarioServices usuarioServices, IOptions<ApplicationOptions> applicationOptions)
        {
            _usuarioServices = usuarioServices;
            _applicationOptions = applicationOptions;
        }

        /// <summary>
        /// Create a new usuario 
        /// </summary>
        /// <param name="usuarioRequest">Usuario</param>
        /// <param name="cancellationToken"></param>
        /// <returns>The new Usuario Created.</returns>
        [MapToApiVersion("1")]
        [HttpPost]
        [ProducesResponseType(typeof(UsuarioResponse), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(IEnumerable<DomainError>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateUsuario([FromBody] UsuarioCreateRequest usuarioRequest, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var result = await _usuarioServices.CreateUsuarioAsync(usuarioRequest, cancellationToken);

            return result.Match<IActionResult>(
                m => CreatedAtAction(nameof(GetUsuario), new { id = m.Id }, m),
                fail => BadRequest(fail)
                );
        }

        /// <summary>
        /// Get a single usuario
        /// </summary>
        /// <param name="id">The id Usuario</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [MapToApiVersion("1")]
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(UsuarioResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(DomainError), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetUsuario(string id, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var result = await _usuarioServices.SelectUsuarioByIdAsync(id, cancellationToken).ConfigureAwait(false);

            return result.Match<IActionResult>(
                m => Ok(m),
                fail => NotFound(fail)
                );
        }

        /// <summary>
        /// Get all usuarioes
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [MapToApiVersion("1")]
        [HttpGet()]
        [ProducesResponseType(typeof(IEnumerable<UsuarioResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(DomainError), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetUsuario(CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var usuarioesResult = await _usuarioServices.SelectAllUsuarios(cancellationToken);

            return usuarioesResult.Match<IActionResult>(
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
        [HttpGet("Paging", Name = "UsuarioPaging")]
        [ProducesResponseType(typeof(IEnumerable<UsuarioResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(DomainError), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApplicationError), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetPagingUsuario([FromQuery] SearchQueryParameters searchQueryParameters, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();

            if (searchQueryParameters.PageIndex <= 0 || searchQueryParameters.PageSize <= 0)
                return BadRequest(ApplicationErrors.ValidPropertiesPage(searchQueryParameters.PageIndex, searchQueryParameters.PageSize));

            var usuarioesResult = await _usuarioServices.SelectAllUsuarios(searchQueryParameters, cancellationToken);

            // Add pagination metadata to headers
            var pagedItems = usuarioesResult.Match<PagedList<UsuarioResponse>>(
                m => m,
                fail => null
            );
            this.AddPaginationMetadata(pagedItems, searchQueryParameters);

            return usuarioesResult.Match<IActionResult>(
                m => Ok(m.Items),
                fail => NotFound(fail)
                );
        }

        /// <summary>
        /// Update a usuario
        /// </summary>
        /// <param name="id">The Id Usuario</param>
        /// <param name="usuarioRequest">The new Data Usuario</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [MapToApiVersion("1")]
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(IEnumerable<DomainError>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateUsuario(string id, [FromBody] UsuarioUpdateRequest usuarioRequest, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var result = await _usuarioServices.UpdateAsync(id, usuarioRequest, cancellationToken).ConfigureAwait(false);

            return result.Match<IActionResult>(
                m => Ok(m),
                fail => BadRequest(fail)
                );
        }

        /// <summary>
        /// Delete a usuario
        /// </summary>
        /// <param name="id">The Ide Usuario</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [MapToApiVersion("1")]
        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(bool), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteUsuario(string id, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var result = await _usuarioServices.DeleteUsuarioAsync(id, cancellationToken);

            return result.Match<IActionResult>(
                m => Ok(m),
                fail => NotFound(fail)
                );
        }
    }

}
