// ****************************************************************
//  Assembly         : CAFEMACA.Coink.PruebaTecnica.Application
//  Author           :  Carlos Fernando Malagón Cano
//  Created          : 06-20-2024
//
//  Last Modified By : Carlos Fernando Malagón Cano
//  Last Modified On : 06-20-2024
//  ****************************************************************
//  <copyright file="PaginationQueryParameters.cs"
//      company="Cafemaca - CAFEMACA Colombia">
//      Cafemaca - CAFEMACA Colombia
//  </copyright>
//


namespace CAFEMACA.Coink.PruebaTecnica.Data.Common.AbstractionsPagination
{
    public record PaginationQueryParameters(int PageIndex = 1, int PageSize = 10);
}
