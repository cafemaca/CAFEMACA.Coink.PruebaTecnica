// ****************************************************************
//  Assembly         : CAFEMACA.Coink.PruebaTecnica.Data
//  Author           :  Carlos Fernando Malagón Cano
//  Created          : 10-16-2024
//
//  Last Modified By : Carlos Fernando Malagón Cano
//  Last Modified On : 10-23-2024
//  ****************************************************************
//  <copyright file="CachedMemoryEntityRepository.cs"
//      company="Cafemaca - CAFEMACA Colombia">
//      Cafemaca - CAFEMACA Colombia
//  </copyright>
//

using CAFEMACA.Coink.PruebaTecnica.Application.Common.Abstractions.Interfaces.Repository;
using CAFEMACA.Coink.PruebaTecnica.Data.Common.Interfaces;
using CAFEMACA.Coink.PruebaTecnica.Data.Common.Options;
using CAFEMACA.Coink.PruebaTecnica.Data.Repositories;
using CAFEMACA.Coink.PruebaTecnica.Domain.Common.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Options;
using System.Text;
using System.Text.Json;

namespace CAFEMACA.Coink.PruebaTecnica.Data.Cache
{
    public class GenericCacheRepository<TEntity, TPrimaryKey, TContext> : IRepository<TEntity, TPrimaryKey>, IGenericCache
        where TEntity : class, IEntity<TPrimaryKey>, new()
        where TPrimaryKey : IComparable, IEquatable<TPrimaryKey>
        where TContext : DbContext
    {

        private readonly string cacheKey = $"{typeof(TEntity).Name}";
        private readonly EntityRepository<TEntity, TPrimaryKey, TContext> _decorated;
        private readonly IDistributedCache _distributedCache;

        private readonly DistributedCacheEntryOptions _entryOptions;

        public GenericCacheRepository(EntityRepository<TEntity, TPrimaryKey, TContext> decorated, IDistributedCache distributedCache, IOptions<CacheOptions> options)
        {
            ArgumentNullException.ThrowIfNull(decorated);
            _decorated = decorated;
            _distributedCache = distributedCache;

            if (options != null)
            {
                _entryOptions = new()
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(options.Value.AbsoluteExpirationInHours), //Set default cache absolute expiration to 4 hour
                    SlidingExpiration = TimeSpan.FromMinutes(options.Value.SlidingExpirationInMinutes), //Set default cache sliding expiration to 2 hours
                };
            }
            else
            {
                //If cache entry options are not mentioned set default value
                _entryOptions = new DistributedCacheEntryOptions()
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(4), //Set default cache absolute expiration to 4 hour
                    SlidingExpiration = TimeSpan.FromHours(2), //Set default cache sliding expiration to 2 hours
                };
            }
        }

        private string GetKey(string id)
        {
            return $"{cacheKey}-{id}";
        }

        #region GenericCache Methods
        public T GetData<T>(string cacheKey)
        {
            var cachedDataBytes = _distributedCache.Get(cacheKey);
            if (cachedDataBytes == null)
            {
                return default;
            }
            else
            {
                var data = Encoding.UTF8.GetString(cachedDataBytes);
                return JsonSerializer.Deserialize<T>(data);
            }
        }

        public void SetData<T>(string cacheKey, T instance)
        {
            if (object.Equals(instance, default(T)))
                return;

            _distributedCache.Set(cacheKey, Encoding.UTF8.GetBytes(JsonSerializer.Serialize(instance)), _entryOptions);
        }

        public void Remove(string cachekey)
        {
            _distributedCache.Remove(cachekey);
        }
        #endregion

        public async Task DeleteAsync(TPrimaryKey id, CancellationToken cancellationToken)
        {
            Remove(GetKey("AllAsync"));
            Remove(GetKey(id.ToString()));
            await _decorated.DeleteAsync(id, cancellationToken);
        }

        public async Task DeleteAsync(TEntity entity, CancellationToken cancellationToken)
        {
            Remove(GetKey("AllAsync"));
            Remove(GetKey(entity.Id.ToString()));
            await _decorated.DeleteAsync(entity, cancellationToken);
        }

        public async Task<TEntity> FirstAsync(ISpecificationQuery<TEntity> specification, CancellationToken cancellationToken)
        {
            return await _decorated.FirstAsync(specification, cancellationToken);
        }

        public async Task<TEntity> FirstOrDefaultAsync(ISpecificationQuery<TEntity> specification, CancellationToken cancellationToken)
        {
            return await _decorated.FirstOrDefaultAsync(specification, cancellationToken);
        }

        public async Task<IReadOnlyList<TEntity>> GetAllAsync(CancellationToken cancellationToken)
        {
            string key = GetKey("AllAsync");
            IReadOnlyList<TEntity> list = GetData<IReadOnlyList<TEntity>>(key);
            if (list == null)
            {
                list = await _decorated.GetAllAsync(cancellationToken);
                SetData<IReadOnlyList<TEntity>>(key, list);
            }
            return list;
        }

        public async Task<IReadOnlyList<TEntity>> GetAllAsync(ISpecificationQuery<TEntity> specification, CancellationToken cancellationToken)
        {
            return await _decorated.GetAllAsync(specification, cancellationToken);
        }

        public async Task<TEntity?> GetAsync(TPrimaryKey id, CancellationToken cancellationToken)
        {
            string key = GetKey(id.ToString());
            TEntity entity = GetData<TEntity>(key);
            if (entity == null)
            {
                entity = await _decorated.GetAsync(id, cancellationToken);
                SetData<TEntity>(key, entity);
            }
            return entity;
        }

        public async Task<TEntity> InsertAsync(TEntity entity, CancellationToken cancellationToken)
        {
            Remove(GetKey("AllAsync"));
            Remove(GetKey(entity.Id.ToString()));

            return await _decorated.InsertAsync(entity, cancellationToken);
        }

        public async Task UpdateAsync(TEntity entity, CancellationToken cancellationToken)
        {
            Remove(GetKey("AllAsync"));
            Remove(GetKey(entity.Id.ToString()));

            await _decorated.UpdateAsync(entity, cancellationToken);
        }
    }
}
