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

using CAFEMACA.Coink.PruebaTecnica.Application.Common.Abstractions.Interfaces.Repositories.Location;
using CAFEMACA.Coink.PruebaTecnica.Application.Common.Abstractions.Interfaces.Repository;
using CAFEMACA.Coink.PruebaTecnica.Data.Common.Options;
using CAFEMACA.Coink.PruebaTecnica.Data.Common.Pagining;
using CAFEMACA.Coink.PruebaTecnica.Data.Context;
using CAFEMACA.Coink.PruebaTecnica.Data.Repositories.Location;
using CAFEMACA.Coink.PruebaTecnica.Domain.Entities.Location;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Options;

namespace CAFEMACA.Coink.PruebaTecnica.Data.Cache.Location
{
    public class CachedMemoryDepartamentoRepository : GenericCacheRepository<Departamento, string, CafemacaDbContext>, IDepartamentoRepository
    {
        DepartamentoRepository _decorated;
        IDistributedCache _memoryCache;

        public CachedMemoryDepartamentoRepository(DepartamentoRepository decorated, IDistributedCache memoryCache, IOptions<CacheOptions> options) : base(decorated, memoryCache, options)
        {
            _decorated = decorated;
            _memoryCache = memoryCache;
        }

        public async Task<PagedList<Departamento>> GetAllAsync(ISpecificationQuery<Departamento> specification, int pageIndex, int pageSize, CancellationToken cancellationToken)
        {
            return await _decorated.GetAllAsync(specification, pageIndex, pageSize, cancellationToken);
        }
    }
}
