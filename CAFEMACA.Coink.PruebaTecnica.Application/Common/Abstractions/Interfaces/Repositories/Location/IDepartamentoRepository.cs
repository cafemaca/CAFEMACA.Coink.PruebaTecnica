﻿// ****************************************************************
//  Assembly         : CAFEMACA.Coink.PruebaTecnica.Application
//  Author           :  Carlos Fernando Malagón Cano
//  Created          : 11-10-2024
//
//  Last Modified By : Carlos Fernando Malagón Cano
//  Last Modified On : 11-10-2024
//  ****************************************************************
//  <copyright file="IDepartamento.cs"
//      company="Cafemaca - CAFEMACA Colombia">
//      Cafemaca - CAFEMACA Colombia
//  </copyright>
//

using CAFEMACA.Coink.PruebaTecnica.Application.Common.Abstractions.Interfaces.Repository;
using CAFEMACA.Coink.PruebaTecnica.Data.Common.Pagining;
using CAFEMACA.Coink.PruebaTecnica.Domain.Entities.Location;

namespace CAFEMACA.Coink.PruebaTecnica.Application.Common.Abstractions.Interfaces.Repositories.Location
{
    public interface IDepartamentoRepository : IRepository<Departamento, string>
    {
        // add methods that are specific to the Player entity
        // e.g Task<Player> GetByEmail(string email);
        // e.g Task<Player> GetByName(string name);
        // e.g Task<Player> GetByEmailAndPassword(string email, string password);
        //Task<PagedList<Player>> GetItems(int pageIndex, int pageSize);

        Task<PagedList<Departamento>> GetAllAsync(ISpecificationQuery<Departamento> specification, int pageIndex, int pageSize, CancellationToken cancellationToken);
    }
}