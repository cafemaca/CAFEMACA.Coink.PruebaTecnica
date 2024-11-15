// ****************************************************************
//  Assembly         : CAFEMACA.Coink.PruebaTecnica.Data
//  Author           :  Carlos Fernando Malagón Cano
//  Created          : 04-02-2024
//
//  Last Modified By : Carlos Fernando Malagón Cano
//  Last Modified On : 09-19-2024
//  ****************************************************************
//  <copyright file="EntityRepository.cs"
//      company="Cafemaca - CAFEMACA Colombia">
//      Cafemaca - CAFEMACA Colombia
//  </copyright>
//

using CAFEMACA.Coink.PruebaTecnica.Application.Common.Abstractions.Interfaces.Repository;
using CAFEMACA.Coink.PruebaTecnica.Application.Common.SpecificationQueries;
using CAFEMACA.Coink.PruebaTecnica.Domain.Common.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CAFEMACA.Coink.PruebaTecnica.Data.Repositories
{
    public abstract class EntityRepository<TEntity, TPrimaryKey, TContext> : IRepository<TEntity, TPrimaryKey>
        where TEntity : class, IEntity<TPrimaryKey>, new()
        where TPrimaryKey : IComparable, IEquatable<TPrimaryKey>
        where TContext : DbContext
    {
        protected readonly TContext _context;
        protected DbSet<TEntity> _dbSet;

        public EntityRepository(TContext context)
        {
            ArgumentNullException.ThrowIfNull(context);
            _context = context;
            this._dbSet = _context.Set<TEntity>();
        }

        /// <summary>
        /// Inserts a new entity.
        /// </summary>
        /// <param name="entity">Inserted entity</param>
        /// <param name="cancellationToken">Cancellation Token</param>
        /// <returns>New Entity</returns>
        public async Task<TEntity> InsertAsync(TEntity entity, CancellationToken cancellationToken)
        {
            ArgumentNullException.ThrowIfNull(entity);
            await _dbSet.AddAsync(entity, cancellationToken);
            return entity;
        }

        /// <summary>
        /// Used to get all entities.
        /// </summary>
        /// <param name="cancellationToken">Cancellation Token</param>
        /// <returns>List of all entities</returns>
        public async Task<IReadOnlyList<TEntity>> GetAllAsync(CancellationToken cancellationToken)
        {
            return await _dbSet
                .ToListAsync(cancellationToken);

            // AsNoTracking tells EF Core it doesn't need to track changes on listed entities. Disabling entity
            // tracking makes the code a little faster
        }

        /// <summary>
        /// Used to get all entities.
        /// </summary>
        /// <param name="specification">specification</param>
        /// <returns>List of all entities</returns>
        public async Task<IReadOnlyList<TEntity>> GetAllAsync(ISpecificationQuery<TEntity> specification, CancellationToken cancellationToken)
        {
            return (IReadOnlyList<TEntity>) await SpecificationQueryBuilder.GetQuery(_dbSet, specification).ToListAsync(cancellationToken).ConfigureAwait(false);
        }

        public async Task DeleteAsync(TPrimaryKey id, CancellationToken cancellationToken)
        {
            TEntity? entityToDelete = await _dbSet.FindAsync([id, cancellationToken], cancellationToken: cancellationToken);
            await DeleteAsync(entityToDelete, cancellationToken);

            return;
        }

        /// <summary>
        /// Deletes an entity by primary key.
        /// </summary>
        /// <param name="id">Primary key of the entity</param>
        /// <param name="cancellationToken">Cancellation Token</param>
        /// <returns></returns>
        public async Task DeleteAsync(TEntity entity, CancellationToken cancellationToken)
        {
            await Task.Run(() =>
            {
                _dbSet.Remove(entity);
            }
            );
        }

        /// <summary>
        /// Gets an entity with given primary key.
        /// </summary>
        /// <param name="id">Primary key of the entity to get</param>
        /// <param name="cancellationToken">Cancellation Token</param>
        /// <returns>Entity</returns>
        public async Task<TEntity?> GetAsync(TPrimaryKey id, CancellationToken cancellationToken)
        {
            TEntity? entity = await _dbSet.FindAsync([id, cancellationToken], cancellationToken: cancellationToken);
            return entity;


        }

        /// <summary>
        /// Updates an existing entity.
        /// </summary>
        /// <param name="entity">Entity</param>
        /// <param name="cancellationToken">Cancellation Token</param>
        /// <returns>Updated Entity</returns>
        public async Task UpdateAsync(TEntity entity, CancellationToken cancellationToken)
        {
            await Task.Run(() => _dbSet.Update(entity), cancellationToken);
        }

        public async Task<TEntity> FirstAsync(ISpecificationQuery<TEntity> specification, CancellationToken cancellationToken)
        {
            return await SpecificationQueryBuilder.GetQuery(_dbSet, specification).FirstAsync(cancellationToken);
        }

        public async Task<TEntity?> FirstOrDefaultAsync(ISpecificationQuery<TEntity> specification, CancellationToken cancellationToken) => await SpecificationQueryBuilder.GetQuery(_dbSet, specification).FirstOrDefaultAsync(cancellationToken);
    }
}
