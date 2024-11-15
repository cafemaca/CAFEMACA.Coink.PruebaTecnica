﻿// ****************************************************************
//  Assembly         : CAFEMACA.Coink.PruebaTecnica.Application
//  Author           :  Carlos Fernando Malagón Cano
//  Created          : 11-10-2024
//
//  Last Modified By : Carlos Fernando Malagón Cano
//  Last Modified On : 11-10-2024
//  ****************************************************************
//  <copyright file="MunicipioSpecificationQuery.cs"
//      company="Cafemaca - CAFEMACA Colombia">
//      Cafemaca - CAFEMACA Colombia
//  </copyright>
//

using CAFEMACA.Coink.PruebaTecnica.Domain.Entities.Location;
using System.Linq.Expressions;

namespace CAFEMACA.Coink.PruebaTecnica.Application.Common.SpecificationQueries.Location
{
    public class MunicipioSpecificationQuery : BaseSpecificationQuery<Municipio>
    {
        public MunicipioSpecificationQuery() : base()
        {
            AddInclude(m => m.Departamento.Pais);
        }
        public MunicipioSpecificationQuery(string Id)
            : base(b => b.Id == Id)
        {
            AddInclude(b => b.Departamento.Pais);
        }

        public MunicipioSpecificationQuery(Expression<Func<Municipio, bool>> criteria, List<SpecificationSort<Municipio>> orderby) : base(criteria)
        {
            AddInclude(b => b.Departamento.Pais);
            OrderBy = orderby;
        }
    }
}
