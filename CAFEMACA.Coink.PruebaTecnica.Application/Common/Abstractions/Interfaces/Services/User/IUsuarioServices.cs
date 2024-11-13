// ****************************************************************
//  Assembly         : CAFEMACA.Coink.PruebaTecnica.Application
//  Author           :  Carlos Fernando Malagón Cano
//  Created          : 11-10-2024
//
//  Last Modified By : Carlos Fernando Malagón Cano
//  Last Modified On : 11-10-2024
//  ****************************************************************
//  <copyright file="IUsuarioServices.cs"
//      company="Cafemaca - CAFEMACA Colombia">
//      Cafemaca - CAFEMACA Colombia
//  </copyright>
//

using CAFEMACA.Coink.PruebaTecnica.Application.Common.Dtos.User;
using CAFEMACA.Coink.PruebaTecnica.Application.Common.Filtering;
using CAFEMACA.Coink.PruebaTecnica.Application.Common.Result;
using CAFEMACA.Coink.PruebaTecnica.Data.Common.Pagining;
using CAFEMACA.Coink.PruebaTecnica.Domain.Common.Abstractions;

namespace CAFEMACA.Coink.PruebaTecnica.Application.Common.Abstractions.Interfaces.Services.User
{
    public interface IUsuarioServices
    {
        Task<Result<IEnumerable<UsuarioResponse>, DomainError>> SelectAllUsuarios(CancellationToken cancellationToken);
        Task<Result<PagedList<UsuarioResponse>, DomainError>> SelectAllUsuarios(SearchQueryParameters searchQueryParameters, CancellationToken cancellationToken);
        Task<Result<UsuarioResponse?, DomainError>> SelectUsuarioByIdAsync(string id, CancellationToken cancellationToken);

        Task<Result<UsuarioResponse?, IEnumerable<DomainError>>> CreateUsuarioAsync(UsuarioCreateRequest playerRequest, CancellationToken cancellationToken);

        Task<Result<bool, DomainError>> DeleteUsuarioAsync(string id, CancellationToken cancellationToken);

        Task<Result<bool, IEnumerable<DomainError>>> UpdateAsync(string id, UsuarioUpdateRequest playerRequest, CancellationToken cancellationToken);
    }
}
