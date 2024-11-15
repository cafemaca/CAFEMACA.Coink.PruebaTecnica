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
        private readonly IMunicipioRepository _municipioRepository;
        private readonly IValidator<MunicipioCreateRequest> _createValidator;
        private readonly IValidator<MunicipioUpdateRequest> _updateValidator;

        public MunicipioServices(ILogger<MunicipioServices> logger
            , IMapper mapper
            , IUnitOfWork unitOfWork
            , IMunicipioRepository municipioRepository
            , IValidator<MunicipioCreateRequest> createValidator
            , IValidator<MunicipioUpdateRequest> updateValidator
            )
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _municipioRepository = municipioRepository ?? throw new ArgumentNullException(nameof(municipioRepository));
            _createValidator = createValidator;
            _updateValidator = updateValidator;
        }

        public async Task<Result<MunicipioResponse?, IEnumerable<DomainError>>> CreateMunicipioAsync(MunicipioCreateRequest municipioRequest, CancellationToken cancellationToken)
        {
            ValidationResult result = await _createValidator.ValidateAsync(municipioRequest, cancellationToken).ConfigureAwait(false);
            if (result.IsValid)
            {
                Municipio municipio = _mapper.Map<Municipio>(municipioRequest);

                await _municipioRepository.InsertAsync(municipio, cancellationToken).ConfigureAwait(false);
                await _unitOfWork.SaveChangesAsync(cancellationToken).ConfigureAwait(false); // save the changes to the database

                return _mapper.Map<MunicipioResponse>(municipio);
            }
            else
            {
                return _mapper.Map<List<DomainError>>(result.Errors);
            }
        }

        public async Task<Result<bool, DomainError>> DeleteMunicipioAsync(string id, CancellationToken cancellationToken)
        {
            Municipio? municipio = await _municipioRepository.GetAsync(id, cancellationToken).ConfigureAwait(false);
            if (municipio != null)
            {
                await _municipioRepository.DeleteAsync(id, cancellationToken).ConfigureAwait(false);
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
            //var municipioes = (await _municipioRepository.GetAllAsync(cancellationToken).ConfigureAwait(false)).ToList();
            var municipioes = (await _municipioRepository.GetAllAsync(new MunicipioSpecificationQuery(), cancellationToken).ConfigureAwait(false)).ToList();
            
            return _mapper.Map<List<MunicipioResponse>>(municipioes);
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

            PagedList<Municipio> municipioes = (await _municipioRepository.GetAllAsync(new MunicipioSpecificationQuery(filters, sorts), searchQueryParameters.PageIndex, searchQueryParameters.PageSize, cancellationToken));
            return _mapper.Map<PagedList<MunicipioResponse>>(municipioes);
        }

        public async Task<Result<MunicipioResponse?, DomainError>> SelectMunicipioByIdAsync(string id, CancellationToken cancellationToken)
        {
            Municipio? municipio = await _municipioRepository.FirstOrDefaultAsync(new MunicipioSpecificationQuery(id), cancellationToken).ConfigureAwait(false);
            if (municipio != null)
            {
                return _mapper.Map<MunicipioResponse>(municipio);
            }
            else
            {
                return MunicipioErrors.NotFound(id);
            }
        }

        public async Task<Result<bool, IEnumerable<DomainError>>> UpdateAsync(string id, MunicipioUpdateRequest municipioRequest, CancellationToken cancellationToken)
        {
            ValidationResult result = await _updateValidator.ValidateAsync(municipioRequest, cancellationToken).ConfigureAwait(false);
            if (result.IsValid)
            {
                Municipio? municipio = await _municipioRepository.GetAsync(id, cancellationToken).ConfigureAwait(false);
                if (municipio != null)
                {
                    _mapper.Map<MunicipioUpdateRequest, Municipio>(municipioRequest, municipio);
                    municipio.Id = id;

                    await _municipioRepository.UpdateAsync(municipio, cancellationToken).ConfigureAwait(false);
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
