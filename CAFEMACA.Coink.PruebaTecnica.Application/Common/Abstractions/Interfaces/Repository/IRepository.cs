// ****************************************************************
//  Assembly         : CAFEMACA.Coink.PruebaTecnica.Application
//  Author           :  Carlos Fernando Malagón Cano
//  Created          : 04-02-2024
//
//  Last Modified By : Carlos Fernando Malagón Cano
//  Last Modified On : 04-02-2024
//  ****************************************************************
//  <copyright file="IGenericRepository.cs"
//      company="Cafemaca - CAFEMACA Colombia">
//      Cafemaca - CAFEMACA Colombia
//  </copyright>
//

using CAFEMACA.Coink.PruebaTecnica.Domain.Common.Interfaces;

namespace CAFEMACA.Coink.PruebaTecnica.Application.Common.Abstractions.Interfaces.Repository
{
    /// <summary>
    /// This interface is implemented by all repositories to ensure implementation of fixed methods.
    /// </summary>
    /// <typeparam name="TEntity">Main Entity type this repository works on</typeparam>
    /// <typeparam name="TPrimaryKey">Primary key type of the entity</typeparam>
    public interface IRepository<TEntity, TPrimaryKey>
        where TEntity : class, IEntity<TPrimaryKey>
        where TPrimaryKey : IComparable, IEquatable<TPrimaryKey>
    {

        /// <summary>
        /// Used to get all entities.
        /// </summary>
        /// <param name="cancellationToken">Cancellation Token</param>
        /// <returns>List of all entities</returns>
        Task<IReadOnlyList<TEntity>> GetAllAsync(CancellationToken cancellationToken);

        /// <summary>
        /// Used to get all entities.
        /// </summary>
        /// <param name="specification">specification</param>
        /// <returns>List of all entities</returns>
        Task<IReadOnlyList<TEntity>> GetAllAsync(ISpecificationQuery<TEntity> specification, CancellationToken cancellationToken);

        /// <summary>
        /// Gets an entity with given primary key.
        /// </summary>
        /// <param name="id">Primary key of the entity to get</param>
        /// <param name="cancellationToken">Cancellation Token</param>
        /// <returns>Entity</returns>
        Task<TEntity?> GetAsync(TPrimaryKey id, CancellationToken cancellationToken);

        Task<TEntity> FirstAsync(ISpecificationQuery<TEntity> specification, CancellationToken cancellationToken);
        Task<TEntity> FirstOrDefaultAsync(ISpecificationQuery<TEntity> specification, CancellationToken cancellationToken);

        /// <summary>
        /// Inserts a new entity.
        /// </summary>
        /// <param name="entity">Inserted entity</param>
        /// <param name="cancellationToken">Cancellation Token</param>
        /// <returns>New Entity</returns>
        Task<TEntity> InsertAsync(TEntity entity, CancellationToken cancellationToken);

        /// <summary>
        /// Deletes an entity by primary key.
        /// </summary>
        /// <param name="id">Primary key of the entity</param>
        /// <param name="cancellationToken">Cancellation Token</param>
        /// <returns></returns>
        Task DeleteAsync(TPrimaryKey id, CancellationToken cancellationToken);

        /// <summary>
        /// Deletes an entity.
        /// </summary>
        /// <param name="entity">Entity to be deleted</param>
        /// <param name="cancellationToken">Cancellation Token</param>
        /// <returns></returns>
        Task DeleteAsync(TEntity entity, CancellationToken cancellationToken);

        /// <summary>
        /// Updates an existing entity.
        /// </summary>
        /// <param name="entity">Entity</param>
        /// <param name="cancellationToken">Cancellation Token</param>
        /// <returns>Updated Entity</returns>
        Task UpdateAsync(TEntity entity, CancellationToken cancellationToken);
    }
}
