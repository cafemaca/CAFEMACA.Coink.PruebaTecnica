// ****************************************************************
//  Assembly         : CAFEMACA.Coink.PruebaTecnica.Application
//  Author           :  Carlos Fernando Malagón Cano
//  Created          : 11-10-2024
//
//  Last Modified By : Carlos Fernando Malagón Cano
//  Last Modified On : 11-10-2024
//  ****************************************************************
//  <copyright file="MunicipioServices.cs"
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
    public class MunicipioServices : IMunicipioServices
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMunicipioRepository _paisRepository;
        private readonly IValidator<MunicipioCreateRequest> _validator;

        public MunicipioServices(ILogger<MunicipioServices> logger
            , IMapper mapper
            , IUnitOfWork unitOfWork
            , IMunicipioRepository paisRepository
            , IValidator<MunicipioCreateRequest> validator)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _paisRepository = paisRepository ?? throw new ArgumentNullException(nameof(paisRepository));
            _validator = validator;
        }

        public async Task<Result<MunicipioResponse?, IEnumerable<DomainError>>> CreateMunicipioAsync(MunicipioCreateRequest paisRequest, CancellationToken cancellationToken)
        {
            ValidationResult result = await _validator.ValidateAsync(paisRequest, cancellationToken).ConfigureAwait(false);
            if (result.IsValid)
            {
                Municipio pais = _mapper.Map<Municipio>(paisRequest);

                await _paisRepository.InsertAsync(pais, cancellationToken).ConfigureAwait(false);
                await _unitOfWork.SaveChangesAsync(cancellationToken).ConfigureAwait(false); // save the changes to the database

                return _mapper.Map<MunicipioResponse>(pais);
            }
            else
            {
                return _mapper.Map<List<DomainError>>(result.Errors);
            }
        }

        public async Task<Result<bool, DomainError>> DeleteMunicipioAsync(string id, CancellationToken cancellationToken)
        {
            Municipio? pais = await _paisRepository.GetAsync(id, cancellationToken).ConfigureAwait(false);
            if (pais != null)
            {
                await _paisRepository.DeleteAsync(id, cancellationToken).ConfigureAwait(false);
                await _unitOfWork.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

                return true;
            }
            else
            {
                return MunicipioErrors.NotFound(id);
            }
        }

        public async Task<Result<IEnumerable<MunicipioResponse>, DomainError>> SelectAllMunicipios(CancellationToken cancellationToken)
        {
            var paiss = (await _paisRepository.GetAllAsync(cancellationToken).ConfigureAwait(false)).ToList();
            return _mapper.Map<List<MunicipioResponse>>(paiss);
        }

        public async Task<Result<PagedList<MunicipioResponse>, DomainError>> SelectAllMunicipios(SearchQueryParameters searchQueryParameters, CancellationToken cancellationToken)
        {
            #region Search filters
            List<ColumnFilter> columnFilters = CustomExpressionFilter<Municipio>.GetColumnFilters(searchQueryParameters.ColumnFilters);

            List<ColumnSorting> columnSorting = CustomExpressionFilter<Municipio>.GetColumnSorting(searchQueryParameters.OrderBy);
            #endregion

            Expression<Func<Municipio, bool>> filters = null;
            List<SpecificationSort<Municipio>> sorts = null;
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
                filters = CustomExpressionFilter<Municipio>.CustomFilter(columnFilters);
            }

            if (columnSorting.Count > 0)
            {
                sorts = CustomExpressionFilter<Municipio>.CustomSort(columnSorting);
            }

            PagedList<Municipio> paiss = (await _paisRepository.GetAllAsync(new MunicipioSpecificationQuery(filters, sorts), searchQueryParameters.PageIndex, searchQueryParameters.PageSize, cancellationToken));
            return _mapper.Map<PagedList<MunicipioResponse>>(paiss);
        }

        public async Task<Result<MunicipioResponse?, DomainError>> SelectMunicipioByIdAsync(string id, CancellationToken cancellationToken)
        {
            Municipio? pais = await _paisRepository.GetAsync(id, cancellationToken).ConfigureAwait(false);
            if (pais != null)
            {
                return _mapper.Map<MunicipioResponse>(pais);
            }
            else
            {
                return MunicipioErrors.NotFound(id);
            }
        }

        public async Task<Result<bool, IEnumerable<DomainError>>> UpdateAsync(string id, MunicipioCreateRequest paisRequest, CancellationToken cancellationToken)
        {
            ValidationResult result = await _validator.ValidateAsync(paisRequest, cancellationToken).ConfigureAwait(false);
            if (result.IsValid)
            {
                Municipio? pais = await _paisRepository.GetAsync(id, cancellationToken).ConfigureAwait(false);
                if (pais != null)
                {
                    _mapper.Map<MunicipioCreateRequest, Municipio>(paisRequest, pais);
                    pais.Id = id;

                    await _paisRepository.UpdateAsync(pais, cancellationToken).ConfigureAwait(false);
                    await _unitOfWork.SaveChangesAsync(cancellationToken);

                    return true;
                }
                else
                {
                    List<DomainError> errors = new List<DomainError>();
                    errors.Add(MunicipioErrors.NotFound(id));
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
