// ****************************************************************
//  Assembly         : CAFEMACA.Coink.PruebaTecnica.Domain
//  Author           : Carlos Fernando Malagón Cano
//  Created          : 11-10-2024 3:43:14 AM
//
//  Last Modified By : Carlos Fernando Malagón Cano
//  Last Modified On : 11-10-2024 3:43:14 AM
//  ****************************************************************
//  <copyright file="PaisError.cs" company="CAFEMACA Inc. Colombia">
//      CAFEMACA Inc. Colombia
//  </copyright>
//

using CAFEMACA.Coink.PruebaTecnica.Domain.Common.Abstractions;

namespace CAFEMACA.Coink.PruebaTecnica.Domain.Common.Errors.Location
{
    public class PaisErrors
    {
        #region Bussines Validator Errors
        public static readonly DomainError RequiredId = new("Pais.RequiredId", "Required Id");
        public static readonly DomainError RequiredName = new("Pais.RequiredName", "Required Name");
        public static readonly DomainError ValidId = new("Pais.ValidId", "Name length must be 2");
        public static readonly DomainError ValidName = new("Pais.ValidName", "Name length should between 3 to 100");
        #endregion

        #region Bussines Errors
        public static DomainError NotFound(string id) => new("Pais.NotFound", $"The Pais with Id '{id}' was not found");
        #endregion
    }
}
