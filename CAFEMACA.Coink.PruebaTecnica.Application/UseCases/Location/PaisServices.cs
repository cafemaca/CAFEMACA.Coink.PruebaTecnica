// ****************************************************************
//  Assembly         : CAFEMACA.Coink.PruebaTecnica.Application
//  Author           :  Carlos Fernando Malagón Cano
//  Created          : 11-10-2024
//
//  Last Modified By : Carlos Fernando Malagón Cano
//  Last Modified On : 11-10-2024
//  ****************************************************************
//  <copyright file="PaisServices.cs"
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
using CAFEMACA.Coink.PruebaTecnica.Application.Common.SpecificationQueries;
using CAFEMACA.Coink.PruebaTecnica.Application.Common.SpecificationQueries.Location;
using CAFEMACA.Coink.PruebaTecnica.Data.Common.Pagining;
using CAFEMACA.Coink.PruebaTecnica.Domain.Common.Abstractions;
using CAFEMACA.Coink.PruebaTecnica.Domain.Common.Errors.Location;
using CAFEMACA.Coink.PruebaTecnica.Domain.Entities.Location;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.Extensions.Logging;
using System.Linq.Expressions;

namespace CAFEMACA.Coink.PruebaTecnica.Application.UseCases.Location
{
    public class PaisServices : IPaisServices
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPaisRepository _paisRepository;
        private readonly IValidator<PaisRequest> _validator;

        public PaisServices(ILogger<PaisServices> logger
            , IMapper mapper
            , IUnitOfWork unitOfWork
            , IPaisRepository paisRepository
            , IValidator<PaisRequest> validator)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _paisRepository = paisRepository ?? throw new ArgumentNullException(nameof(paisRepository));
            _validator = validator;
        }

        public async Task<Result<PaisResponse?, IEnumerable<DomainError>>> CreatePaisAsync(PaisRequest paisRequest, CancellationToken cancellationToken)
        {
            ValidationResult result = await _validator.ValidateAsync(paisRequest, cancellationToken).ConfigureAwait(false);
            if (result.IsValid)
            {
                Pais pais = _mapper.Map<Pais>(paisRequest);

                await _paisRepository.InsertAsync(pais, cancellationToken).ConfigureAwait(false);
                await _unitOfWork.SaveChangesAsync(cancellationToken).ConfigureAwait(false); // save the changes to the database

                return _mapper.Map<PaisResponse>(pais);
            }
            else
            {
                return _mapper.Map<List<DomainError>>(result.Errors);
            }
        }

        public async Task<Result<bool, DomainError>> DeletePaisAsync(string id, CancellationToken cancellationToken)
        {
            Pais? pais = await _paisRepository.GetAsync(id, cancellationToken).ConfigureAwait(false);
            if (pais != null)
            {
                await _paisRepository.DeleteAsync(id, cancellationToken).ConfigureAwait(false);
                await _unitOfWork.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

                return true;
            }
            else
            {
                return PaisErrors.NotFound(id);
            }
        }

        public async Task<Result<IEnumerable<PaisResponse>, DomainError>> SelectAllPaiss(CancellationToken cancellationToken)
        {
            var paiss = (await _paisRepository.GetAllAsync(cancellationToken).ConfigureAwait(false)).ToList();
            return _mapper.Map<List<PaisResponse>>(paiss);
        }

        public async Task<Result<PagedList<PaisResponse>, DomainError>> SelectAllPaiss(SearchQueryParameters searchQueryParameters, CancellationToken cancellationToken)
        {
            #region Search filters
            List<ColumnFilter> columnFilters = CustomExpressionFilter<Pais>.GetColumnFilters(searchQueryParameters.ColumnFilters);

            List<ColumnSorting> columnSorting = CustomExpressionFilter<Pais>.GetColumnSorting(searchQueryParameters.OrderBy);
            #endregion

            Expression<Func<Pais, bool>> filters = null;
            List<SpecificationSort<Pais>> sorts = null;
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
                filters = CustomExpressionFilter<Pais>.CustomFilter(columnFilters);
            }

            if (columnSorting.Count > 0)
            {
                sorts = CustomExpressionFilter<Pais>.CustomSort(columnSorting);
            }

            PagedList<Pais> paiss = (await _paisRepository.GetAllAsync(new PaisSpecificationQuery(filters, sorts), searchQueryParameters.PageIndex, searchQueryParameters.PageSize, cancellationToken));
            return _mapper.Map<PagedList<PaisResponse>>(paiss);
        }

        public async Task<Result<PaisResponse?, DomainError>> SelectPaisByIdAsync(string id, CancellationToken cancellationToken)
        {
            Pais? pais = await _paisRepository.GetAsync(id, cancellationToken).ConfigureAwait(false);
            if (pais != null)
            {
                return _mapper.Map<PaisResponse>(pais);
            }
            else
            {
                return PaisErrors.NotFound(id);
            }
        }

        public async Task<Result<bool, IEnumerable<DomainError>>> UpdateAsync(string id, PaisRequest paisRequest, CancellationToken cancellationToken)
        {
            ValidationResult result = await _validator.ValidateAsync(paisRequest, cancellationToken).ConfigureAwait(false);
            if (result.IsValid)
            {
                Pais? pais = await _paisRepository.GetAsync(id, cancellationToken).ConfigureAwait(false);
                if (pais != null)
                {
                    _mapper.Map<PaisRequest, Pais>(paisRequest, pais);
                    pais.Id = id;

                    await _paisRepository.UpdateAsync(pais, cancellationToken).ConfigureAwait(false);
                    await _unitOfWork.SaveChangesAsync(cancellationToken);

                    return true;
                }
                else
                {
                    List<DomainError> errors = new List<DomainError>();
                    errors.Add(PaisErrors.NotFound(id));
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
