// ****************************************************************
//  Assembly         : CAFEMACA.Coink.PruebaTecnica.Application
//  Author           :  Carlos Fernando Malagón Cano
//  Created          : 11-10-2024
//
//  Last Modified By : Carlos Fernando Malagón Cano
//  Last Modified On : 11-10-2024
//  ****************************************************************
//  <copyright file="PaisSpecificationQuery.cs"
//      company="Cafemaca - CAFEMACA Colombia">
//      Cafemaca - CAFEMACA Colombia
//  </copyright>
//

using CAFEMACA.Coink.PruebaTecnica.Domain.Entities.Location;
using System.Linq.Expressions;

namespace CAFEMACA.Coink.PruebaTecnica.Application.Common.SpecificationQueries.Location
{
    public class PaisSpecificationQuery : BaseSpecificationQuery<Pais>
    {
        public PaisSpecificationQuery() : base()
        {
        }
        public PaisSpecificationQuery(Expression<Func<Pais, bool>> criteria, List<SpecificationSort<Pais>> orderby) : base(criteria)
        {
            OrderBy = orderby;
        }
    }
}
