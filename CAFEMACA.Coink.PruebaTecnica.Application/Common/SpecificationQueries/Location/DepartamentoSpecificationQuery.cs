// ****************************************************************
//  Assembly         : CAFEMACA.Coink.PruebaTecnica.Application
//  Author           :  Carlos Fernando Malagón Cano
//  Created          : 11-10-2024
//
//  Last Modified By : Carlos Fernando Malagón Cano
//  Last Modified On : 11-10-2024
//  ****************************************************************
//  <copyright file="DepartamentoSpecificationQuery.cs"
//      company="Cafemaca - CAFEMACA Colombia">
//      Cafemaca - CAFEMACA Colombia
//  </copyright>
//

using CAFEMACA.Coink.PruebaTecnica.Domain.Entities.Location;
using System.Linq.Expressions;

namespace CAFEMACA.Coink.PruebaTecnica.Application.Common.SpecificationQueries.Location
{
    public class DepartamentoSpecificationQuery : BaseSpecificationQuery<Departamento>
    {
        public DepartamentoSpecificationQuery() : base()
        {
        }
        public DepartamentoSpecificationQuery(Expression<Func<Departamento, bool>> criteria, List<SpecificationSort<Departamento>> orderby) : base(criteria)
        {
            OrderBy = orderby;
        }
    }
}
