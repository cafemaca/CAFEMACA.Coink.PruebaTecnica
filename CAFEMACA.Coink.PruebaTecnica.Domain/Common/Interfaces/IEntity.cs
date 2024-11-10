// ****************************************************************
//  Assembly         : CAFEMACA.Coink.PruebaTecnica.Domain
//  Author           :  Carlos Fernando Malagón Cano
//  Created          : 04-02-2024
//
//  Last Modified By : Carlos Fernando Malagón Cano
//  Last Modified On : 04-02-2024
//  ****************************************************************
//  <copyright file="IEntity.cs"
//      company="Cafemaca - CAFEMACA Colombia">
//      Cafemaca - CAFEMACA Colombia
//  </copyright>
//

namespace CAFEMACA.Coink.PruebaTecnica.Domain.Common.Interfaces
{
    /// <summary>
    // Define la interface base para una entidad. Todas las entidaddes del sistema deben implementar esta interface.
    /// </summary>
    /// <typeparam name="TPrimaryKey">Tipo para la llave primaria de la entidad</typeparam>
    public interface IEntity<TPrimaryKey>
        where TPrimaryKey : IComparable, IEquatable<TPrimaryKey>

    {
        /// <summary>
        /// Identificador único para la entidad.
        /// </summary>
        TPrimaryKey Id { get; set; }

        /*
        /// <summary>
        // Checks if this entity is transient (not persisted to database and it has not
        // an Abp.Domain.Entities.IEntity`1.Id).
        /// </summary>
        /// <returns>True, if this entity is transient</returns>
        bool IsTransient();
        */
    }
}
