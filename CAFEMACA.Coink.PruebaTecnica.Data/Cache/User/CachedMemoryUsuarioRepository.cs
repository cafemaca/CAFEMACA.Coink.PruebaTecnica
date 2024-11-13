// ****************************************************************
//  Assembly         : CAFEMACA.Coink.PruebaTecnica.Data
//  Author           :  Carlos Fernando Malagón Cano
//  Created          : 11-10-2024
//
//  Last Modified By : Carlos Fernando Malagón Cano
//  Last Modified On : 11-10-2024
//  ****************************************************************
//  <copyright file="ApplicationDbContext.cs"
//      company="Cafemaca - CAFEMACA Colombia">
//      Cafemaca - CAFEMACA Colombia
//  </copyright>
//

using CAFEMACA.Coink.PruebaTecnica.Application.Common.Abstractions.Interfaces.Repositories.User;
using CAFEMACA.Coink.PruebaTecnica.Application.Common.Abstractions.Interfaces.Repository;
using CAFEMACA.Coink.PruebaTecnica.Data.Common.Options;
using CAFEMACA.Coink.PruebaTecnica.Data.Common.Pagining;
using CAFEMACA.Coink.PruebaTecnica.Data.Context;
using CAFEMACA.Coink.PruebaTecnica.Data.Repositories.User;
using CAFEMACA.Coink.PruebaTecnica.Domain.Entities.User;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Options;

namespace CAFEMACA.Coink.PruebaTecnica.Data.Cache.User
{
    public class CachedMemoryUsuarioRepository : GenericCacheRepository<Usuario, string, CafemacaDbContext>, IUsuarioRepository
    {
        UsuarioRepository _decorated;
        IDistributedCache _memoryCache;

        public CachedMemoryUsuarioRepository(UsuarioRepository decorated, IDistributedCache memoryCache, IOptions<CacheOptions> options) : base(decorated, memoryCache, options)
        {
            _decorated = decorated;
            _memoryCache = memoryCache;
        }

        public async Task<PagedList<Usuario>> GetAllAsync(ISpecificationQuery<Usuario> specification, int pageIndex, int pageSize, CancellationToken cancellationToken)
        {
            return await _decorated.GetAllAsync(specification, pageIndex, pageSize, cancellationToken);
        }
    }
}
