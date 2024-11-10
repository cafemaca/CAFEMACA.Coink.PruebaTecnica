// ****************************************************************
//  Assembly         : CAFEMACA.Coink.PruebaTecnica.Domain
//  Author           : CArlos Fernando Malagón Cano
//  Created          : 11/10/2024 3:45:33 AM
//
//  Last Modified By : Carlos Fernando Malagón Cano
//  Last Modified On : 11/10/2024 3:45:33 AM
//  ****************************************************************
//  <copyright file="Usuario.cs" company="CAFEMACA Inc. Colombia">
//      CAFEMACA Inc. Colombia
//  </copyright>
//

using CAFEMACA.Coink.PruebaTecnica.Domain.Common.Abstractions;
using CAFEMACA.Coink.PruebaTecnica.Domain.Common.Interfaces;
using CAFEMACA.Coink.PruebaTecnica.Domain.ValueObjects.User;

namespace CAFEMACA.Coink.PruebaTecnica.Domain.Entities.User
{
    /// <summary>
    /// Clase que identifica a un determinado Usuario.
    /// </summary>
    public class Usuario : Entity<string>, IAuditableEntity
    {
        public required string Nombre { get; set; } = string.Empty;
        public required string Telefono { get; set; } = string.Empty;

        public required Direccion Direccion { get; set; }

        #region Auditable Entity
        public DateTime CreatedAtUtc { get; set; }
        public DateTime? UpdatedAtUtc { get; set; }
        public string CreatedBy { get; set; } = null!;
        public string? UpdatedBy { get; set; }
        #endregion
    }
}
