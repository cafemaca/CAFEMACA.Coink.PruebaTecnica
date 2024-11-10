// ****************************************************************
//  Assembly         : CAFEMACA.Coink.PruebaTecnica.Application
//  Author           :  Carlos Fernando Malagón Cano
//  Created          : 04-02-2024
//
//  Last Modified By : Carlos Fernando Malagón Cano
//  Last Modified On : 04-12-2024
//  ****************************************************************
//  <copyright file="PlayerSpecificationQuery.cs"
//      company="Cafemaca - CAFEMACA Colombia">
//      Cafemaca - CAFEMACA Colombia
//  </copyright>
//

using CAFEMACA.Coink.PruebaTecnica.Domain.Entities.Player;
using System.Linq.Expressions;

namespace CAFEMACA.Coink.PruebaTecnica.Application.Common.SpecificationQueries.Players
{
    public class PlayerSpecificationQuery : BaseSpecificationQuery<Player>
    {
        public PlayerSpecificationQuery() : base()
        {
        }
        public PlayerSpecificationQuery(Expression<Func<Player, bool>> criteria, List<SpecificationSort<Player>> orderby) : base(criteria)
        {
            OrderBy = orderby;
        }
    }
}
