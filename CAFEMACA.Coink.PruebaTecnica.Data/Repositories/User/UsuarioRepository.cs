// ****************************************************************
//  Assembly         : CAFEMACA.Coink.PruebaTecnica.Data
//  Author           :  Carlos Fernando Malagón Cano
//  Created          : 11-10-2024
//
//  Last Modified By : Carlos Fernando Malagón Cano
//  Last Modified On : 11-10-2024
//  ****************************************************************
//  <copyright file="ApplicationDbContext.cs"
//      company="Cafemaca - CAFEMACA Colombia">
//      Cafemaca - CAFEMACA Colombia
//  </copyright>
//

using CAFEMACA.Coink.PruebaTecnica.Application.Common.Abstractions.Interfaces.Repositories.User;
using CAFEMACA.Coink.PruebaTecnica.Application.Common.Abstractions.Interfaces.Repository;
using CAFEMACA.Coink.PruebaTecnica.Application.Common.SpecificationQueries;
using CAFEMACA.Coink.PruebaTecnica.Data.Common.Pagining;
using CAFEMACA.Coink.PruebaTecnica.Data.Context;
using CAFEMACA.Coink.PruebaTecnica.Domain.Entities.User;

namespace CAFEMACA.Coink.PruebaTecnica.Data.Repositories.User
{
    public class UsuarioRepository : EntityRepository<Usuario, string, CafemacaDbContext>, IUsuarioRepository
    {
        public UsuarioRepository(CafemacaDbContext context) : base(context)
        {
        }

        public async Task<PagedList<Usuario>> GetAllAsync(ISpecificationQuery<Usuario> specification, int pageIndex, int pageSize, CancellationToken cancellationToken)
        {

            return await PagedList<Usuario>.CreateAsync(SpecificationQueryBuilder.GetQuery(_dbSet, specification).AsQueryable<Usuario>(), pageIndex, pageSize);
        }
    }
}
