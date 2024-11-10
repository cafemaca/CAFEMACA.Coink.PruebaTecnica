using CAFEMACA.Coink.PruebaTecnica.Application.Common.Abstractions.Interfaces.Repositories;
using CAFEMACA.Coink.PruebaTecnica.Application.Common.Abstractions.Interfaces.Repository;
using CAFEMACA.Coink.PruebaTecnica.Application.Common.SpecificationQueries;
using CAFEMACA.Coink.PruebaTecnica.Data.Common.Pagining;
using CAFEMACA.Coink.PruebaTecnica.Data.Context;
using CAFEMACA.Coink.PruebaTecnica.Domain.Entities.Audit;

namespace CAFEMACA.Coink.PruebaTecnica.Data.Repositories.Audit
{
    public class AuditTrailRepository : EntityRepository<AuditTrail, Guid, CafemacaDbContext>, IAuditTrailRepository
    {
        public AuditTrailRepository(CafemacaDbContext context) : base(context)
        {
        }

        public async Task<PagedList<AuditTrail>> GetAllAsync(ISpecificationQuery<AuditTrail> specification, int pageIndex, int pageSize, CancellationToken cancellationToken)
        {

            return await PagedList<AuditTrail>.CreateAsync(SpecificationQueryBuilder.GetQuery(_dbSet, specification).AsQueryable<AuditTrail>(), pageIndex, pageSize);
        }
    }
}
