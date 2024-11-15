// ****************************************************************
//  Assembly         : CAFEMACA.Coink.PruebaTecnica.Application
//  Author           :  Carlos Fernando Malagón Cano
//  Created          : 11-10-2024
//
//  Last Modified By : Carlos Fernando Malagón Cano
//  Last Modified On : 11-10-2024
//  ****************************************************************
//  <copyright file="UsuarioSpecificationQuery.cs"
//      company="Cafemaca - CAFEMACA Colombia">
//      Cafemaca - CAFEMACA Colombia
//  </copyright>
//

using CAFEMACA.Coink.PruebaTecnica.Domain.Entities.User;
using System.Linq.Expressions;

namespace CAFEMACA.Coink.PruebaTecnica.Application.Common.SpecificationQueries.User
{
    public class UsuarioSpecificationQuery : BaseSpecificationQuery<Usuario>
    {
        public UsuarioSpecificationQuery() : base()
        {
            AddInclude(u => u.Direccion.Municipio.Departamento.Pais);
        }

        public UsuarioSpecificationQuery(string id) : base()
        {
            AddInclude(u => u.Direccion.Municipio.Departamento.Pais);
        }

        public UsuarioSpecificationQuery(Expression<Func<Usuario, bool>> criteria, List<SpecificationSort<Usuario>> orderby) : base(criteria)
        {
            AddInclude(u => u.Direccion.Municipio.Departamento.Pais);
            OrderBy = orderby;
        }
    }
}
