// ****************************************************************
//  Assembly         : CAFEMACA.Coink.PruebaTecnica.Domain
//  Author           :  Carlos Fernando Malagón Cano
//  Created          : 11-03-2024
//
//  Last Modified By : Carlos Fernando Malagón Cano
//  Last Modified On : 11-03-2024
//  ****************************************************************
//  <copyright file="Entity.cs"
//      company="Cafemaca - CAFEMACA Colombia">
//      Cafemaca - CAFEMACA Colombia
//  </copyright>
//

using CAFEMACA.Coink.PruebaTecnica.Domain.Common.Interfaces;

namespace CAFEMACA.Coink.PruebaTecnica.Domain.Common.Abstractions
{
    //
    // Resumen:
    //     Basic implementation of IEntity interface. An entity can inherit this class of
    //     directly implement to IEntity interface.
    //
    // Parámetros de tipo:
    //   TPrimaryKey:
    //     Type of the primary key of the entity
    public abstract class Entity<TPrimaryKey> : IEntity<TPrimaryKey>
        where TPrimaryKey : IComparable, IEquatable<TPrimaryKey>
    {

        //
        // Resumen:
        //     Unique identifier for this entity.
        public virtual TPrimaryKey Id { get; set; }

        /*
        public override bool Equals(object obj);
        public override int GetHashCode();
        */

        /*
        //
        // Resumen:
        //     Checks if this entity is transient (it has not an Id).
        //
        // Devuelve:
        //     True, if this entity is transient
        public virtual bool IsTransient();
        */

        public override string ToString()
        {
            return Id.ToString();
        }

        /*
        public static bool operator ==(Entity<TPrimaryKey> left, Entity<TPrimaryKey> right);
        public static bool operator !=(Entity<TPrimaryKey> left, Entity<TPrimaryKey> right);
        */
    }
}
