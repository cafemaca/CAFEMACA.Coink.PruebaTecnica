// ****************************************************************
//  Assembly         : CAFEMACA.Coink.PruebaTecnica.Application
//  Author           :  Carlos Fernando Malagón Cano
//  Created          : 04-23-2024
//
//  Last Modified By : Carlos Fernando Malagón Cano
//  Last Modified On : 04-23-2024
//  ****************************************************************
//  <copyright file="ISpecificationQuery.cs"
//      company="Cafemaca - CAFEMACA Colombia">
//      Cafemaca - CAFEMACA Colombia
//  </copyright>
//

using CAFEMACA.Coink.PruebaTecnica.Application.Common.SpecificationQueries;
using System.Linq.Expressions;

namespace CAFEMACA.Coink.PruebaTecnica.Application.Common.Abstractions.Interfaces.Repository
{
    /// <summary>
    /// Interface para la definición de criterios de búsqueda sobre un rRepositorio de determinada entidad.
    /// GENERIC SPECIFICATION INTERFACE
    /// https://github.com/dotnet-architecture/eShopOnWeb
    /// <typeparam name="T">Entidad sobre la cual se determina la especificación</typeparam>
    public interface ISpecificationQuery<T>
    {
        Expression<Func<T, bool>> Criteria { get; }
        List<Expression<Func<T, object>>> Includes { get; }
        List<SpecificationSort<T>> OrderBy { get; set; }
        List<string> IncludeStrings { get; }
    }
}
