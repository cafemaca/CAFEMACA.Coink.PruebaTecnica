
// ****************************************************************
//  Assembly         : CAFEMACA.Coink.PruebaTecnica.Data
//  Author           :  Carlos Fernando Malagón Cano
//  Created          : 04-02-2024
//
//  Last Modified By : Carlos Fernando Malagón Cano
//  Last Modified On : 10-09-2024
//  ****************************************************************
//  <copyright file="PlayerServices.cs"
//      company="Cafemaca - CAFEMACA Colombia">
//      Cafemaca - CAFEMACA Colombia
//  </copyright>
//

using CAFEMACA.Coink.PruebaTecnica.Application.Common.Abstractions.Interfaces.Repositories;
using CAFEMACA.Coink.PruebaTecnica.Application.Common.Abstractions.Interfaces.Repository;
using CAFEMACA.Coink.PruebaTecnica.Application.Common.SpecificationQueries;
using CAFEMACA.Coink.PruebaTecnica.Data.Common.Pagining;
using CAFEMACA.Coink.PruebaTecnica.Data.Context;
using CAFEMACA.Coink.PruebaTecnica.Domain.Entities.Player;

namespace CAFEMACA.Coink.PruebaTecnica.Data.Repositories.Players
{
    public class PlayerRepository : EntityRepository<Player, int, CafemacaDbContext>, IPlayerRepository
    {
        public PlayerRepository(CafemacaDbContext context) : base(context)
        {
        }

        public async Task<PagedList<Player>> GetAllAsync(ISpecificationQuery<Player> specification, int pageIndex, int pageSize, CancellationToken cancellationToken)
        {

            return await PagedList<Player>.CreateAsync(SpecificationQueryBuilder.GetQuery(_dbSet, specification).AsQueryable<Player>(), pageIndex, pageSize);
        }
    }
}
