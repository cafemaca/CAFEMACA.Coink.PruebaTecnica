// ****************************************************************
//  Assembly         : CAFEMACA.Coink.PruebaTecnica.Application
//  Author           :  Carlos Fernando Malagón Cano
//  Created          : 11-10-2024
//
//  Last Modified By : Carlos Fernando Malagón Cano
//  Last Modified On : 11-10-2024
//  ****************************************************************
//  <copyright file="IPaisServices.cs"
//      company="Cafemaca - CAFEMACA Colombia">
//      Cafemaca - CAFEMACA Colombia
//  </copyright>
//

using CAFEMACA.Coink.PruebaTecnica.Application.Common.Dtos.Location;
using CAFEMACA.Coink.PruebaTecnica.Application.Common.Filtering;
using CAFEMACA.Coink.PruebaTecnica.Application.Common.Result;
using CAFEMACA.Coink.PruebaTecnica.Data.Common.Pagining;
using CAFEMACA.Coink.PruebaTecnica.Domain.Common.Abstractions;

namespace CAFEMACA.Coink.PruebaTecnica.Application.Common.Abstractions.Interfaces.Services.Location
{
    public interface IPaisServices
    {
        Task<Result<IEnumerable<PaisResponse>, DomainError>> SelectAllPaises(CancellationToken cancellationToken);
        Task<Result<PagedList<PaisResponse>, DomainError>> SelectAllPaises(SearchQueryParameters searchQueryParameters, CancellationToken cancellationToken);
        Task<Result<PaisResponse?, DomainError>> SelectPaisByIdAsync(string id, CancellationToken cancellationToken);

        Task<Result<PaisResponse?, IEnumerable<DomainError>>> CreatePaisAsync(PaisCreateRequest playerRequest, CancellationToken cancellationToken);

        Task<Result<bool, DomainError>> DeletePaisAsync(string id, CancellationToken cancellationToken);

        Task<Result<bool, IEnumerable<DomainError>>> UpdateAsync(string id, PaisUpdateRequest playerRequest, CancellationToken cancellationToken);
    }
}
