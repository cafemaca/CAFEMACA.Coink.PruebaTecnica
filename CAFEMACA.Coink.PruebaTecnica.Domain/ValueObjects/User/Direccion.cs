// ****************************************************************
//  Assembly         : CAFEMACA.Coink.PruebaTecnica.Domain
//  Author           : Carlos Fernando Malagón Camo
//  Created          : 11/10/2024 3:47:34 AM
//
//  Last Modified By : Carlos Fernando Malagón Camo
//  Last Modified On : 11/10/2024 3:47:34 AM
//  ****************************************************************
//  <copyright file="Direccion.cs" company="CAFEMACA Inc. Colombia">
//      CAFEMACA Inc. Colombia
//  </copyright>
//

using CAFEMACA.Coink.PruebaTecnica.Domain.Entities.Location;

namespace CAFEMACA.Coink.PruebaTecnica.Domain.ValueObjects.User
{
    /// <summary>
    /// Clase que identifica a un determinado Direccion.
    /// </summary>
    public class Direccion
    {
        public string DireccionName { get; set; } = string.Empty;
        public string Municipioid { get; set; } = default;
        public Municipio Municipio{ get; set; } = default;
    }
}
