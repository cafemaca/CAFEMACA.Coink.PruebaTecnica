// ****************************************************************
//  Assembly         : CAFEMACA.Coink.PruebaTecnica.Data
//  Author           :  Carlos Fernando Malagón Cano
//  Created          : 10-16-2024
//
//  Last Modified By : Carlos Fernando Malagón Cano
//  Last Modified On : 10-16-2024
//  ****************************************************************
//  <copyright file="CachedMemoryPlayerRepository.cs"
//      company="Cafemaca - CAFEMACA Colombia">
//      Cafemaca - CAFEMACA Colombia
//  </copyright>
//

using CAFEMACA.Coink.PruebaTecnica.Application.Common.Abstractions.Interfaces.Repositories;
using CAFEMACA.Coink.PruebaTecnica.Application.Common.Abstractions.Interfaces.Repository;
using CAFEMACA.Coink.PruebaTecnica.Data.Common.Options;
using CAFEMACA.Coink.PruebaTecnica.Data.Common.Pagining;
using CAFEMACA.Coink.PruebaTecnica.Data.Context;
using CAFEMACA.Coink.PruebaTecnica.Data.Repositories.Players;
using CAFEMACA.Coink.PruebaTecnica.Domain.Entities.Player;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Options;

namespace CAFEMACA.Coink.PruebaTecnica.Data.Cache.Players
{
    public class CachedMemoryPlayerRepository : GenericCacheRepository<Player, int, CafemacaDbContext>, IPlayerRepository
    {
        PlayerRepository _decorated;
        IDistributedCache _memoryCache;

        public CachedMemoryPlayerRepository(PlayerRepository decorated, IDistributedCache memoryCache, IOptions<CacheOptions> options) : base(decorated, memoryCache, options)
        {
            _decorated = decorated;
            _memoryCache = memoryCache;
        }

        public async Task<PagedList<Player>> GetAllAsync(ISpecificationQuery<Player> specification, int pageIndex, int pageSize, CancellationToken cancellationToken)
        {
            return await _decorated.GetAllAsync(specification, pageIndex, pageSize, cancellationToken);
        }
    }
}
