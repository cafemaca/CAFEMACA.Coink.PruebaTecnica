using CAFEMACA.Coink.PruebaTecnica.Domain.ValueObjects.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAFEMACA.Coink.PruebaTecnica.Application.Common.Dtos.User
{
    public class UsuarioUpdateRequest
    {
        public required string Id { get; set; } = string.Empty;
        public required string Nombre { get; set; } = string.Empty;
        public required string Telefono { get; set; } = string.Empty;

        public required Direccion Direccion { get; set; }
    }
}
