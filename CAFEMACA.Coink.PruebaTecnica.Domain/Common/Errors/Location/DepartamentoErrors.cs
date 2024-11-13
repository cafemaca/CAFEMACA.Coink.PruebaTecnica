// ****************************************************************
//  Assembly         : CAFEMACA.Coink.PruebaTecnica.Domain
//  Author           : Carlos Fernando Malagón Cano
//  Created          : 11-10-2024 3:43:14 AM
//
//  Last Modified By : Carlos Fernando Malagón Cano
//  Last Modified On : 11-10-2024 3:43:14 AM
//  ****************************************************************
//  <copyright file="DepartamentoErrors.cs" company="CAFEMACA Inc. Colombia">
//      CAFEMACA Inc. Colombia
//  </copyright>
//

using CAFEMACA.Coink.PruebaTecnica.Domain.Common.Abstractions;

namespace CAFEMACA.Coink.PruebaTecnica.Domain.Common.Errors.Location
{
    public class DepartamentoErrors
    {
        #region Bussines Validator Errors
        public static readonly DomainError RequiredId = new("Departamento.RequiredId", "Required Id");
        public static readonly DomainError RequiredName = new("Departamento.RequiredName", "Required Name");
        public static readonly DomainError ValidId = new("Departamento.ValidId", "Id length should between 4 to 6");
        public static readonly DomainError ValidName = new("Departamento.ValidName", "Name length should between 3 to 100");
        public static readonly DomainError RequiredPaisId = new("Departamento.RequiredPaisId", "Required Id Pais");
        public static readonly DomainError ValidPaisId = new("Departamento.ValidPaisId", "Id Pais length should 2");
        #endregion

        #region Bussines Errors
        public static DomainError NotFound(string id) => new("Departamento.NotFound", $"The Departamento with Id '{id}' was not found");
        #endregion
    }
}
