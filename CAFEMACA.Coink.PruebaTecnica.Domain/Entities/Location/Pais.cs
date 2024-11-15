﻿// ****************************************************************
//  Assembly         : CAFEMACA.Coink.PruebaTecnica.Domain
//  Author           : Carlos Fernando Malagón Cano
//  Created          : 11/10/2024 3:39:58 AM
//
//  Last Modified By : Carlos Fernando Malagón Cano
//  Last Modified On : 11/10/2024 3:39:58 AM
//  ****************************************************************
//  <copyright file="Pais.cs" company="CAFEMACA Inc. Colombia">
//      CAFEMACA Inc. Colombia
//  </copyright>
//

using CAFEMACA.Coink.PruebaTecnica.Domain.Common.Abstractions;
using CAFEMACA.Coink.PruebaTecnica.Domain.Common.Interfaces;

namespace CAFEMACA.Coink.PruebaTecnica.Domain.Entities.Location
{
    /// <summary>
    /// Clase que identifica a un determinado Pais.
    /// </summary>
    public class Pais : Entity<string>, IAuditableEntity
    {
        public string Name { get; set; } = string.Empty;

        public ICollection<Departamento> Departamentos { get; set; } = new List<Departamento>();

        #region Auditable Entity
        public DateTime CreatedAtUtc { get; set; }
        public DateTime? UpdatedAtUtc { get; set; }
        public string CreatedBy { get; set; } = null!;
        public string? UpdatedBy { get; set; }
        #endregion
    }
}
