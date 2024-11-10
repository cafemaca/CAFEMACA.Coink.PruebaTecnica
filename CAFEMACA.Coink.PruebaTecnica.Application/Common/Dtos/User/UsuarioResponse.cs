// ****************************************************************
//  Assembly         : CAFEMACA.Coink.PruebaTecnica.Application
//  Author           :  Carlos Fernando Malagón Cano
//  Created          : 11-10-2024
//
//  Last Modified By : Carlos Fernando Malagón Cano
//  Last Modified On : 11-10-2024
//  ****************************************************************
//  <copyright file="UsuarioResponse.cs"
//      company="Cafemaca - CAFEMACA Colombia">
//      Cafemaca - CAFEMACA Colombia
//  </copyright>
//

namespace CAFEMACA.Coink.PruebaTecnica.Application.Common.Dtos.User
{
    public class UsuarioResponse
    {
        public required string Id { get; set; } = string.Empty;
        public required string Nombre { get; set; } = string.Empty;
        public required string Telefono { get; set; } = string.Empty;

        public required DireccionResponse Direccion { get; set; }
    }
}
