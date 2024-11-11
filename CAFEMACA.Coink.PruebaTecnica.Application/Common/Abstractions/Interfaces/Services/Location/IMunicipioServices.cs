// ****************************************************************
//  Assembly         : CAFEMACA.Coink.PruebaTecnica.Application
//  Author           :  Carlos Fernando Malagón Cano
//  Created          : 11-10-2024
//
//  Last Modified By : Carlos Fernando Malagón Cano
//  Last Modified On : 11-10-2024
//  ****************************************************************
//  <copyright file="IMunicipioServices.cs"
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
    public interface IMunicipioServices
    {
        Task<Result<IEnumerable<MunicipioResponse>, DomainError>> SelectAllMunicipios(CancellationToken cancellationToken);
        Task<Result<PagedList<MunicipioResponse>, DomainError>> SelectAllMunicipios(SearchQueryParameters searchQueryParameters, CancellationToken cancellationToken);
        Task<Result<MunicipioResponse?, DomainError>> SelectMunicipioByIdAsync(string id, CancellationToken cancellationToken);

        Task<Result<MunicipioResponse?, IEnumerable<DomainError>>> CreateMunicipioAsync(MunicipioCreateRequest municipioRequest, CancellationToken cancellationToken);

        Task<Result<bool, DomainError>> DeleteMunicipioAsync(string id, CancellationToken cancellationToken);

        Task<Result<bool, IEnumerable<DomainError>>> UpdateAsync(string id, MunicipioCreateRequest municipioRequest, CancellationToken cancellationToken);
    }
}
