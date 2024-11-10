using AutoMapper;
using CAFEMACA.Coink.PruebaTecnica.Application.Common.Abstractions.Interfaces.Repositories;
using CAFEMACA.Coink.PruebaTecnica.Application.Common.Abstractions.Interfaces.Repository;
using CAFEMACA.Coink.PruebaTecnica.Application.Common.Abstractions.Interfaces.Services;
using CAFEMACA.Coink.PruebaTecnica.Application.Common.Dtos.Audit;
using CAFEMACA.Coink.PruebaTecnica.Application.Common.Filtering;
using CAFEMACA.Coink.PruebaTecnica.Application.Common.Result;
using CAFEMACA.Coink.PruebaTecnica.Application.Common.SpecificationQueries;
using CAFEMACA.Coink.PruebaTecnica.Application.Common.SpecificationQueries.Audit;
using CAFEMACA.Coink.PruebaTecnica.Data.Common.Pagining;
using CAFEMACA.Coink.PruebaTecnica.Domain.Common.Abstractions;
using CAFEMACA.Coink.PruebaTecnica.Domain.Common.Errors;
using CAFEMACA.Coink.PruebaTecnica.Domain.Entities.Audit;
using Microsoft.Extensions.Logging;
using System.Linq.Expressions;

namespace CAFEMACA.Coink.PruebaTecnica.Application.UseCases.Audit
{
    public class AuditTrailServices : IAuditTrailServices
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAuditTrailRepository _auditTrailRepository;

        public AuditTrailServices(ILogger<AuditTrailServices> logger
            , IMapper mapper
            , IUnitOfWork unitOfWork
            , IAuditTrailRepository auditTrailRepository)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _auditTrailRepository = auditTrailRepository;
        }

        public async Task<Result<IEnumerable<AuditTrailResponse>, DomainError>> SelectAllAuditTrails(CancellationToken cancellationToken)
        {
            var auditTrails = (await _auditTrailRepository.GetAllAsync(cancellationToken).ConfigureAwait(false)).ToList();
            return _mapper.Map<List<AuditTrailResponse>>(auditTrails);
        }

        public async Task<Result<PagedList<AuditTrailResponse>, DomainError>> SelectAllAuditTrails(SearchQueryParameters searchQueryParameters, CancellationToken cancellationToken)
        {
            #region Search filters
            List<ColumnFilter> columnFilters = CustomExpressionFilter<AuditTrail>.GetColumnFilters(searchQueryParameters.ColumnFilters);

            List<ColumnSorting> columnSorting = CustomExpressionFilter<AuditTrail>.GetColumnSorting(searchQueryParameters.OrderBy);
            #endregion

            Expression<Func<AuditTrail, bool>> filters = null;
            List<SpecificationSort<AuditTrail>> sorts = null;
            //First, we are checking our SearchTerm. If it contains information we are creating a filter.
            var searchTerm = "";
            if (!string.IsNullOrEmpty(searchQueryParameters.SearchTerm))
            {
                searchTerm = searchQueryParameters.SearchTerm.Trim().ToLower();
                filters = x => x.EntityName.ToLower().Contains(searchTerm);
            }
            // Then we are overwriting a filter if columnFilters has data.
            if (columnFilters.Count > 0)
            {
                filters = CustomExpressionFilter<AuditTrail>.CustomFilter(columnFilters);
            }

            if (columnSorting.Count > 0)
            {
                sorts = CustomExpressionFilter<AuditTrail>.CustomSort(columnSorting);
            }

            PagedList<AuditTrail> auditTrails = (await _auditTrailRepository.GetAllAsync(new AuditTrailSpecificationQuery(filters, sorts), searchQueryParameters.PageIndex, searchQueryParameters.PageSize, cancellationToken));
            return _mapper.Map<PagedList<AuditTrailResponse>>(auditTrails);
        }

        public async Task<Result<AuditTrailResponse?, DomainError>> SelectAuditTrailByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            AuditTrail? auditTrail = await _auditTrailRepository.GetAsync(id, cancellationToken).ConfigureAwait(false);
            if (auditTrail != null)
            {
                return _mapper.Map<AuditTrailResponse>(auditTrail);
            }
            else
            {
                return AuditTrailErrors.NotFound(id);
            }
        }

    }
}
