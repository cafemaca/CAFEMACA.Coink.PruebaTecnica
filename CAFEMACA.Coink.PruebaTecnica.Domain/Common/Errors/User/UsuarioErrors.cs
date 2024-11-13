// ****************************************************************
//  Assembly         : CAFEMACA.Coink.PruebaTecnica.Domain
//  Author           : Carlos Fernando Malagón Cano
//  Created          : 11-10-2024 3:43:14 AM
//
//  Last Modified By : Carlos Fernando Malagón Cano
//  Last Modified On : 11-10-2024 3:43:14 AM
//  ****************************************************************
//  <copyright file="UsuarioErrors.cs" company="CAFEMACA Inc. Colombia">
//      CAFEMACA Inc. Colombia
//  </copyright>
//

using CAFEMACA.Coink.PruebaTecnica.Domain.Common.Abstractions;

namespace CAFEMACA.Coink.PruebaTecnica.Domain.Common.Errors.User
{
    public class UsuarioErrors
    {
        #region Bussines Validator Errors
        public static readonly DomainError RequiredId = new("Usuario.RequiredId", "Required Id");
        public static readonly DomainError RequiredName = new("Usuario.RequiredName", "Required Name");
        public static readonly DomainError ValidId = new("Usuario.ValidId", "Id length should between 6 to 10");
        public static readonly DomainError ValidName = new("Usuario.ValidName", "Name length should between 3 to 100");
        public static readonly DomainError RequiredIdMunicipio = new("Usuario.RequiredIdMunicipio", "Required Id Municipio");
        public static readonly DomainError ValidIdMunicipio = new("Usuario.ValidIdMunicipio", "Id Municipio length should between 2 to 6");
        #endregion

        #region Bussines Errors
        public static DomainError NotFound(string id) => new("Usuario.NotFound", $"The Usuario with Id '{id}' was not found");
        #endregion
    }
}
