// ****************************************************************
//  Assembly         : CAFEMACA.Coink.PruebaTecnica.Application
//  Author           :  Carlos Fernando Malagón Cano
//  Created          : 11-10-2024
//
//  Last Modified By : Carlos Fernando Malagón Cano
//  Last Modified On : 11-10-2024
//  ****************************************************************
//  <copyright file="IDepartamentoServices.cs"
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
    public interface IDepartamentoServices
    {
        Task<Result<IEnumerable<DepartamentoResponse>, DomainError>> SelectAllDepartamentos(CancellationToken cancellationToken);
        Task<Result<PagedList<DepartamentoResponse>, DomainError>> SelectAllDepartamentos(SearchQueryParameters searchQueryParameters, CancellationToken cancellationToken);
        Task<Result<DepartamentoResponse?, DomainError>> SelectDepartamentoByIdAsync(string id, CancellationToken cancellationToken);

        Task<Result<DepartamentoResponse?, IEnumerable<DomainError>>> CreateDepartamentoAsync(DepartamentoRequest playerRequest, CancellationToken cancellationToken);

        Task<Result<bool, DomainError>> DeleteDepartamentoAsync(string id, CancellationToken cancellationToken);

        Task<Result<bool, IEnumerable<DomainError>>> UpdateAsync(string id, DepartamentoRequest playerRequest, CancellationToken cancellationToken);
    }
}
