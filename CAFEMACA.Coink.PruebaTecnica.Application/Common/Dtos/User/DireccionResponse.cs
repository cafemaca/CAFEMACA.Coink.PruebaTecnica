// ****************************************************************
//  Assembly         : CAFEMACA.Coink.PruebaTecnica.Application
//  Author           :  Carlos Fernando Malagón Cano
//  Created          : 11-10-2024
//
//  Last Modified By : Carlos Fernando Malagón Cano
//  Last Modified On : 11-10-2024
//  ****************************************************************
//  <copyright file="DireccionResponse.cs"
//      company="Cafemaca - CAFEMACA Colombia">
//      Cafemaca - CAFEMACA Colombia
//  </copyright>
//

using CAFEMACA.Coink.PruebaTecnica.Application.Common.Dtos.Location;

namespace CAFEMACA.Coink.PruebaTecnica.Application.Common.Dtos.User
{
    public class DireccionResponse
    {
        public string DireccionName { get; set; } = string.Empty;
        public MunicipioResponse Municipio { get; set; } = default;
    }
}
