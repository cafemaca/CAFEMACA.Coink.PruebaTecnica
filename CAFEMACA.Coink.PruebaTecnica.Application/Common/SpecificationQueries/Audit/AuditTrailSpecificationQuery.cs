using CAFEMACA.Coink.PruebaTecnica.Domain.Entities.Audit;
using System.Linq.Expressions;

namespace CAFEMACA.Coink.PruebaTecnica.Application.Common.SpecificationQueries.Audit
{
    public class AuditTrailSpecificationQuery : BaseSpecificationQuery<AuditTrail>
    {
        public AuditTrailSpecificationQuery() : base()
        {
        }
        public AuditTrailSpecificationQuery(Expression<Func<AuditTrail, bool>> criteria, List<SpecificationSort<AuditTrail>> orderby) : base(criteria)
        {
            OrderBy = orderby;
        }
    }
}
