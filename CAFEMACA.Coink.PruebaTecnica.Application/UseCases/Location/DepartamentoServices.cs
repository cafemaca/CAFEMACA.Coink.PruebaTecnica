// ****************************************************************
//  Assembly         : CAFEMACA.Coink.PruebaTecnica.Application
//  Author           :  Carlos Fernando Malagón Cano
//  Created          : 11-10-2024
//
//  Last Modified By : Carlos Fernando Malagón Cano
//  Last Modified On : 11-10-2024
//  ****************************************************************
//  <copyright file="DepartamentoServices.cs"
//      company="Cafemaca - CAFEMACA Colombia">
//      Cafemaca - CAFEMACA Colombia
//  </copyright>
//

using AutoMapper;
using CAFEMACA.Coink.PruebaTecnica.Application.Common.Abstractions.Interfaces.Repositories.Location;
using CAFEMACA.Coink.PruebaTecnica.Application.Common.Abstractions.Interfaces.Repository;
using CAFEMACA.Coink.PruebaTecnica.Application.Common.Abstractions.Interfaces.Services.Location;
using CAFEMACA.Coink.PruebaTecnica.Application.Common.Dtos.Location;
using CAFEMACA.Coink.PruebaTecnica.Application.Common.Filtering;
using CAFEMACA.Coink.PruebaTecnica.Application.Common.Result;
using CAFEMACA.Coink.PruebaTecnica.Application.Common.SpecificationQueries.Location;
using CAFEMACA.Coink.PruebaTecnica.Application.Common.SpecificationQueries;
using CAFEMACA.Coink.PruebaTecnica.Data.Common.Pagining;
using CAFEMACA.Coink.PruebaTecnica.Domain.Common.Abstractions;
using CAFEMACA.Coink.PruebaTecnica.Domain.Common.Errors.Location;
using CAFEMACA.Coink.PruebaTecnica.Domain.Entities.Location;
using FluentValidation;
using Microsoft.Extensions.Logging;
using System.Linq.Expressions;
using FluentValidation.Results;

namespace CAFEMACA.Coink.PruebaTecnica.Application.UseCases.Location
{
    public class DepartamentoServices : IDepartamentoServices
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IDepartamentoRepository _departamentoRepository;
        private readonly IValidator<DepartamentoCreateRequest> _createValidator;
        private readonly IValidator<DepartamentoUpdateRequest> _updateValidator;

        public DepartamentoServices(ILogger<DepartamentoServices> logger
            , IMapper mapper
            , IUnitOfWork unitOfWork
            , IDepartamentoRepository paisRepository
            , IValidator<DepartamentoCreateRequest> createValidator
            , IValidator<DepartamentoUpdateRequest> updateValidator
            )
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _departamentoRepository = paisRepository ?? throw new ArgumentNullException(nameof(paisRepository));
            _createValidator = createValidator;
            _updateValidator = updateValidator;
        }

        public async Task<Result<DepartamentoCreateResponse?, IEnumerable<DomainError>>> CreateDepartamentoAsync(DepartamentoCreateRequest departamentoRequest, CancellationToken cancellationToken)
        {
            ValidationResult result = await _createValidator.ValidateAsync(departamentoRequest, cancellationToken).ConfigureAwait(false);
            if (result.IsValid)
            {
                Departamento departamento = _mapper.Map<Departamento>(departamentoRequest);

                await _departamentoRepository.InsertAsync(departamento, cancellationToken).ConfigureAwait(false);
                await _unitOfWork.SaveChangesAsync(cancellationToken).ConfigureAwait(false); // save the changes to the database

                return _mapper.Map<DepartamentoCreateResponse>(departamento);
            }
            else
            {
                return _mapper.Map<List<DomainError>>(result.Errors);
            }
        }

        public async Task<Result<bool, DomainError>> DeleteDepartamentoAsync(string id, CancellationToken cancellationToken)
        {
            Departamento? departamento = await _departamentoRepository.GetAsync(id, cancellationToken).ConfigureAwait(false);
            if (departamento != null)
            {
                await _departamentoRepository.DeleteAsync(id, cancellationToken).ConfigureAwait(false);
                await _unitOfWork.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

                return true;
            }
            else
            {
                return DepartamentoErrors.NotFound(id);
            }
        }

        public async Task<Result<IEnumerable<DepartamentoResponse>, DomainError>> SelectAllDepartamentos(CancellationToken cancellationToken)
        {
            //var departamentos = (await _departamentoRepository.GetAllAsync(cancellationToken).ConfigureAwait(false)).ToList();
            var departamentos = (await _departamentoRepository.GetAllAsync(new DepartamentoSpecificationQuery(), cancellationToken).ConfigureAwait(false)).ToList();
            return _mapper.Map<List<DepartamentoResponse>>(departamentos);
        }

        public async Task<Result<PagedList<DepartamentoResponse>, DomainError>> SelectAllDepartamentos(SearchQueryParameters searchQueryParameters, CancellationToken cancellationToken)
        {
            #region Search filters
            List<ColumnFilter> columnFilters = CustomExpressionFilter<Departamento>.GetColumnFilters(searchQueryParameters.ColumnFilters);

            List<ColumnSorting> columnSorting = CustomExpressionFilter<Departamento>.GetColumnSorting(searchQueryParameters.OrderBy);
            #endregion

            Expression<Func<Departamento, bool>> filters = null;
            List<SpecificationSort<Departamento>> sorts = null;
            //First, we are checking our SearchTerm. If it contains information we are creating a filter.
            var searchTerm = "";
            if (!string.IsNullOrEmpty(searchQueryParameters.SearchTerm))
            {
                searchTerm = searchQueryParameters.SearchTerm.Trim().ToLower();
                filters = x => x.Name.ToLower().Contains(searchTerm);
            }
            // Then we are overwriting a filter if columnFilters has data.
            if (columnFilters.Count > 0)
            {
                filters = CustomExpressionFilter<Departamento>.CustomFilter(columnFilters);
            }

            if (columnSorting.Count > 0)
            {
                sorts = CustomExpressionFilter<Departamento>.CustomSort(columnSorting);
            }

            PagedList<Departamento> departamentos = (await _departamentoRepository.GetAllAsync(new DepartamentoSpecificationQuery(filters, sorts), searchQueryParameters.PageIndex, searchQueryParameters.PageSize, cancellationToken));
            return _mapper.Map<PagedList<DepartamentoResponse>>(departamentos);
        }

        public async Task<Result<DepartamentoResponse?, DomainError>> SelectDepartamentoByIdAsync(string id, CancellationToken cancellationToken)
        {
            DepartamentoSpecificationQuery departamentoSpecificationQuery = new DepartamentoSpecificationQuery(id);
            Departamento? departamento = await _departamentoRepository.FirstOrDefaultAsync(departamentoSpecificationQuery, cancellationToken).ConfigureAwait(false);
            if (departamento != null)
            {
                return _mapper.Map<DepartamentoResponse>(departamento);
            }
            else
            {
                return DepartamentoErrors.NotFound(id);
            }
        }

        public async Task<Result<bool, IEnumerable<DomainError>>> UpdateAsync(string id, DepartamentoUpdateRequest departamentoRequest, CancellationToken cancellationToken)
        {
            ValidationResult result = await _updateValidator.ValidateAsync(departamentoRequest, cancellationToken).ConfigureAwait(false);
            if (result.IsValid)
            {
                Departamento? departamento = await _departamentoRepository.GetAsync(id, cancellationToken).ConfigureAwait(false);
                if (departamento != null)
                {
                    _mapper.Map<DepartamentoUpdateRequest, Departamento>(departamentoRequest, departamento);
                    departamento.Id = id;

                    await _departamentoRepository.UpdateAsync(departamento, cancellationToken).ConfigureAwait(false);
                    await _unitOfWork.SaveChangesAsync(cancellationToken);

                    return true;
                }
                else
                {
                    List<DomainError> errors = new List<DomainError>();
                    errors.Add(DepartamentoErrors.NotFound(id));
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
