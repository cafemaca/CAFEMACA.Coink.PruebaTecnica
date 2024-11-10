// ****************************************************************
//  Assembly         : CAFEMACA.Coink.PruebaTecnica.Application
//  Author           :  Carlos Fernando Malagón Cano
//  Created          : 04-02-2024
//
//  Last Modified By : Carlos Fernando Malagón Cano
//  Last Modified On : 04-12-2024
//  ****************************************************************
//  <copyright file="BaseSpecificationQuery.cs"
//      company="Cafemaca - CAFEMACA Colombia">
//      Cafemaca - CAFEMACA Colombia
//  </copyright>
//

using CAFEMACA.Coink.PruebaTecnica.Application.Common.SpecificationQueries;
using System.Linq.Expressions;

namespace CAFEMACA.Coink.PruebaTecnica.Application.Common.Abstractions.Interfaces.Repository
{
    /// <summary>
    /// Clas Base para la definición de clases que especifique criterios de búsqueda para determinada entidad en su repositorio.
    /// GENERIC SPECIFICATION IMPLEMENTATION (BASE CLASS)
    /// https://github.com/dotnet-architecture/eShopOnWeb    /// </summary>
    /// </summary>
    /// <typeparam name="T">Entidad sobre la cual se determina la especificación</typeparam>
    public abstract class BaseSpecificationQuery<T> : ISpecificationQuery<T>
    {
        protected BaseSpecificationQuery() { }

        protected BaseSpecificationQuery(Expression<Func<T, bool>> criteria)
        {
            Criteria = criteria;
        }

        public Expression<Func<T, bool>> Criteria { get; set; }

        public List<Expression<Func<T, object>>> Includes { get; set; } = new List<Expression<Func<T, object>>>();

        public List<string> IncludeStrings { get; set; } = new List<string>();

        public List<SpecificationSort<T>> OrderBy { get; set; } = new List<SpecificationSort<T>>();

        protected virtual void AddInclude(Expression<Func<T, object>> includeExpression)
        {
            Includes.Add(includeExpression);
        }

        // string-based includes allow for including children of children
        // e.g. Basket.Items.Product
        protected virtual void AddInclude(string includeString)
        {
            IncludeStrings.Add(includeString);
        }
        protected virtual void AddOrderBy(SpecificationSort<T> orderByExpression)
        {
            OrderBy.Add(orderByExpression);
        }

    }
}
