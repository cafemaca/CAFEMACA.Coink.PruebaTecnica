// ****************************************************************
//  Assembly         : CAFEMACA.Coink.PruebaTecnica.Application
//  Author           :  Carlos Fernando Malagón Cano
//  Created          : 04-18-2024 
//
//  Last Modified By : Carlos Fernando Malagón Cano
//  Last Modified On : 04-18-2024
//  ****************************************************************
//  <copyright file="IPlayerServices.cs"
//      company="Cafemaca - CAFEMACA Colombia">
//      Cafemaca - CAFEMACA Colombia
//  </copyright>
//

using CAFEMACA.Coink.PruebaTecnica.Application.Common.Dtos.Players;
using CAFEMACA.Coink.PruebaTecnica.Application.Common.Filtering;
using CAFEMACA.Coink.PruebaTecnica.Application.Common.Result;
using CAFEMACA.Coink.PruebaTecnica.Data.Common.Pagining;
using CAFEMACA.Coink.PruebaTecnica.Domain.Common.Abstractions;

namespace CAFEMACA.Coink.PruebaTecnica.Application.Common.Abstractions.Interfaces.Services
{
    public interface IPlayerServices
    {
        Task<Result<IEnumerable<PlayerResponse>, DomainError>> SelectAllPlayers(CancellationToken cancellationToken);
        Task<Result<PagedList<PlayerResponse>, DomainError>> SelectAllPlayers(SearchQueryParameters searchQueryParameters, CancellationToken cancellationToken);
        Task<Result<PlayerResponse?, DomainError>> SelectPlayerByIdAsync(int id, CancellationToken cancellationToken);

        Task<Result<PlayerResponse?, IEnumerable<DomainError>>> CreatePlayerAsync(PlayerRequest playerRequest, CancellationToken cancellationToken);

        Task<Result<bool, DomainError>> DeletePlayerAsync(int id, CancellationToken cancellationToken);

        Task<Result<bool, IEnumerable<DomainError>>> UpdateAsync(int id, PlayerRequest playerRequest, CancellationToken cancellationToken);
    }
}
