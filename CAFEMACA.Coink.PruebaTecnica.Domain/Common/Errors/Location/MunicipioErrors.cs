// ****************************************************************
//  Assembly         : CAFEMACA.Coink.PruebaTecnica.Domain
//  Author           : Carlos Fernando Malagón Cano
//  Created          : 11-10-2024 3:43:14 AM
//
//  Last Modified By : Carlos Fernando Malagón Cano
//  Last Modified On : 11-10-2024 3:43:14 AM
//  ****************************************************************
//  <copyright file="MunicipioErrors.cs" company="CAFEMACA Inc. Colombia">
//      CAFEMACA Inc. Colombia
//  </copyright>
//

using CAFEMACA.Coink.PruebaTecnica.Domain.Common.Abstractions;

namespace CAFEMACA.Coink.PruebaTecnica.Domain.Common.Errors.Location
{
    public class MunicipioErrors
    {
        #region Bussines Validator Errors
        public static readonly DomainError RequiredId = new("Municipio.RequiredId", "Required Id");
        public static readonly DomainError RequiredName = new("Municipio.RequiredName", "Required Name");
        public static readonly DomainError ValidId = new("Municipio.ValidId", "Id length should between 6 to 10");
        public static readonly DomainError ValidName = new("Municipio.ValidName", "Name length should between 3 to 100");
        public static readonly DomainError RequiredIdDepartamento = new("Municipio.RequiredIdDepartamento", "Required Id Departamento");
        public static readonly DomainError ValidIdDepartamento = new("Municipio.ValidIdDepartamento", "Id Departamento length should between 2 to 6");
        #endregion

        #region Bussines Errors
        public static DomainError NotFound(int id) => new("Municipio.NotFound", $"The Municipio with Id '{id}' was not found");
        #endregion
    }
}
