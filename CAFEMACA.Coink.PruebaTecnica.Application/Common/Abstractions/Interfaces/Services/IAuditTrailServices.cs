using CAFEMACA.Coink.PruebaTecnica.Application.Common.Dtos.Audit;
using CAFEMACA.Coink.PruebaTecnica.Application.Common.Filtering;
using CAFEMACA.Coink.PruebaTecnica.Application.Common.Result;
using CAFEMACA.Coink.PruebaTecnica.Data.Common.Pagining;
using CAFEMACA.Coink.PruebaTecnica.Domain.Common.Abstractions;

namespace CAFEMACA.Coink.PruebaTecnica.Application.Common.Abstractions.Interfaces.Services
{
    public interface IAuditTrailServices
    {
        Task<Result<IEnumerable<AuditTrailResponse>, DomainError>> SelectAllAuditTrails(CancellationToken cancellationToken);
        Task<Result<PagedList<AuditTrailResponse>, DomainError>> SelectAllAuditTrails(SearchQueryParameters searchQueryParameters, CancellationToken cancellationToken);
        Task<Result<AuditTrailResponse?, DomainError>> SelectAuditTrailByIdAsync(Guid id, CancellationToken cancellationToken);

        //Task<Result<AuditTrailResponse?, IEnumerable<DomainError>>> CreateAuditTrailAsync(AuditTrailRequest AuditTrailRequest, CancellationToken cancellationToken);

        //Task<Result<bool, DomainError>> DeleteAuditTrailAsync(Guid id, CancellationToken cancellationToken);

        //Task<Result<bool, IEnumerable<DomainError>>> UpdateAsync(Guid id, AuditTrailRequest AuditTrailRequest, CancellationToken cancellationToken);
    }
}
