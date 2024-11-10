// ****************************************************************
//  Assembly         : CAFEMACA.Coink.PruebaTecnica.Domain
//  Author           :  Carlos Fernando Malagón Cano
//  Created          : 04-18-2024 
//
//  Last Modified By : Carlos Fernando Malagón Cano
//  Last Modified On : 06-14-2024
//  ****************************************************************
//  <copyright file="DomainError.cs"
//      company="Cafemaca - CAFEMACA Colombia">
//      Cafemaca - CAFEMACA Colombia
//  </copyright>
//

namespace CAFEMACA.Coink.PruebaTecnica.Domain.Common.Abstractions
{
    public sealed record DomainError(string ErrorCode, string? ErrorMessage = null)
    {
        public static readonly DomainError None = new(string.Empty);
    }
}
