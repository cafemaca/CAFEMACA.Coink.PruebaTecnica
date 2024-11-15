﻿// ****************************************************************
//  Assembly         : CAFEMACA.Coink.PruebaTecnica.Application
//  Author           :  Carlos Fernando Malagón Cano
//  Created          : 11-10-2024
//
//  Last Modified By : Carlos Fernando Malagón Cano
//  Last Modified On : 11-10-2024
//  ****************************************************************
//  <copyright file="UsuarioServices.cs"
//      company="Cafemaca - CAFEMACA Colombia">
//      Cafemaca - CAFEMACA Colombia
//  </copyright>
//

using AutoMapper;
using CAFEMACA.Coink.PruebaTecnica.Application.Common.Abstractions.Interfaces.Repositories.User;
using CAFEMACA.Coink.PruebaTecnica.Application.Common.Abstractions.Interfaces.Repository;
using CAFEMACA.Coink.PruebaTecnica.Application.Common.Abstractions.Interfaces.Services.User;
using CAFEMACA.Coink.PruebaTecnica.Application.Common.Dtos.User;
using CAFEMACA.Coink.PruebaTecnica.Application.Common.Filtering;
using CAFEMACA.Coink.PruebaTecnica.Application.Common.Result;
using CAFEMACA.Coink.PruebaTecnica.Application.Common.SpecificationQueries;
using CAFEMACA.Coink.PruebaTecnica.Application.Common.SpecificationQueries.User;
using CAFEMACA.Coink.PruebaTecnica.Data.Common.Pagining;
using CAFEMACA.Coink.PruebaTecnica.Domain.Common.Abstractions;
using CAFEMACA.Coink.PruebaTecnica.Domain.Common.Errors.User;
using CAFEMACA.Coink.PruebaTecnica.Domain.Entities.User;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.Extensions.Logging;
using System.Linq.Expressions;

namespace CAFEMACA.Coink.PruebaTecnica.Application.UseCases.Location
{
    public class UsuarioServices : IUsuarioServices
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IValidator<UsuarioCreateRequest> _createValidator;
        private readonly IValidator<UsuarioUpdateRequest> _updateValidator;

        public UsuarioServices(ILogger<UsuarioServices> logger
            , IMapper mapper
            , IUnitOfWork unitOfWork
            , IUsuarioRepository usuarioRepository
            , IValidator<UsuarioCreateRequest> createValidator
            , IValidator<UsuarioUpdateRequest> updateValidator)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _usuarioRepository = usuarioRepository ?? throw new ArgumentNullException(nameof(usuarioRepository));
            _createValidator = createValidator;
            _updateValidator = updateValidator;
        }

        public async Task<Result<UsuarioResponse?, IEnumerable<DomainError>>> CreateUsuarioAsync(UsuarioCreateRequest usuarioRequest, CancellationToken cancellationToken)
        {
            ValidationResult result = await _createValidator.ValidateAsync(usuarioRequest, cancellationToken).ConfigureAwait(false);
            if (result.IsValid)
            {
                Usuario usuario = _mapper.Map<Usuario>(usuarioRequest);

                await _usuarioRepository.InsertAsync(usuario, cancellationToken).ConfigureAwait(false);
                await _unitOfWork.SaveChangesAsync(cancellationToken).ConfigureAwait(false); // save the changes to the database

                return _mapper.Map<UsuarioResponse>(usuario);
            }
            else
            {
                return _mapper.Map<List<DomainError>>(result.Errors);
            }
        }

        public async Task<Result<bool, DomainError>> DeleteUsuarioAsync(string id, CancellationToken cancellationToken)
        {
            Usuario? usuario = await _usuarioRepository.GetAsync(id, cancellationToken).ConfigureAwait(false);
            if (usuario != null)
            {
                await _usuarioRepository.DeleteAsync(id, cancellationToken).ConfigureAwait(false);
                await _unitOfWork.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

                return true;
            }
            else
            {
                return UsuarioErrors.NotFound(id);
            }
        }

        public async Task<Result<IEnumerable<UsuarioResponse>, DomainError>> SelectAllUsuarios(CancellationToken cancellationToken)
        {
            var usuarios = (await _usuarioRepository.GetAllAsync(cancellationToken).ConfigureAwait(false)).ToList();
            return _mapper.Map<List<UsuarioResponse>>(usuarios);
        }

        public async Task<Result<PagedList<UsuarioResponse>, DomainError>> SelectAllUsuarios(SearchQueryParameters searchQueryParameters, CancellationToken cancellationToken)
        {
            #region Search filters
            List<ColumnFilter> columnFilters = CustomExpressionFilter<Usuario>.GetColumnFilters(searchQueryParameters.ColumnFilters);

            List<ColumnSorting> columnSorting = CustomExpressionFilter<Usuario>.GetColumnSorting(searchQueryParameters.OrderBy);
            #endregion

            Expression<Func<Usuario, bool>> filters = null;
            List<SpecificationSort<Usuario>> sorts = null;
            //First, we are checking our SearchTerm. If it contains information we are creating a filter.
            var searchTerm = "";
            if (!string.IsNullOrEmpty(searchQueryParameters.SearchTerm))
            {
                searchTerm = searchQueryParameters.SearchTerm.Trim().ToLower();
                filters = x => x.Nombre.ToLower().Contains(searchTerm);
            }
            // Then we are overwriting a filter if columnFilters has data.
            if (columnFilters.Count > 0)
            {
                filters = CustomExpressionFilter<Usuario>.CustomFilter(columnFilters);
            }

            if (columnSorting.Count > 0)
            {
                sorts = CustomExpressionFilter<Usuario>.CustomSort(columnSorting);
            }

            PagedList<Usuario> usuarios = (await _usuarioRepository.GetAllAsync(new UsuarioSpecificationQuery(filters, sorts), searchQueryParameters.PageIndex, searchQueryParameters.PageSize, cancellationToken));
            return _mapper.Map<PagedList<UsuarioResponse>>(usuarios);
        }

        public async Task<Result<UsuarioResponse?, DomainError>> SelectUsuarioByIdAsync(string id, CancellationToken cancellationToken)
        {
            Usuario? usuario = await _usuarioRepository.GetAsync(id, cancellationToken).ConfigureAwait(false);
            if (usuario != null)
            {
                return _mapper.Map<UsuarioResponse>(usuario);
            }
            else
            {
                return UsuarioErrors.NotFound(id);
            }
        }

        public async Task<Result<bool, IEnumerable<DomainError>>> UpdateAsync(string id, UsuarioUpdateRequest usuarioRequest, CancellationToken cancellationToken)
        {
            ValidationResult result = await _updateValidator.ValidateAsync(usuarioRequest, cancellationToken).ConfigureAwait(false);
            if (result.IsValid)
            {
                Usuario? usuario = await _usuarioRepository.GetAsync(id, cancellationToken).ConfigureAwait(false);
                if (usuario != null)
                {
                    _mapper.Map<UsuarioUpdateRequest, Usuario>(usuarioRequest, usuario);
                    usuario.Id = id;

                    await _usuarioRepository.UpdateAsync(usuario, cancellationToken).ConfigureAwait(false);
                    await _unitOfWork.SaveChangesAsync(cancellationToken);

                    return true;
                }
                else
                {
                    List<DomainError> errors = new List<DomainError>();
                    errors.Add(UsuarioErrors.NotFound(id));
                    return errors;
                }
            }
            else
            {
                return _mapper.Map<List<DomainError>>(result.Errors);
            }
        }
    }
}
