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

using CAFEMACA.Coink.PruebaTecnica.Application.Common.Abstractions.Interfaces.Repository;
using System.Linq.Expressions;

namespace CAFEMACA.Coink.PruebaTecnica.Application.Common.SpecificationQueries
{
    public class SpecificationSort<T>
    {
        public Expression<Func<T, object>> PropertySort { get; set; }
        public bool Desc { get; set; }

        public SpecificationSort(Expression<Func<T, object>> propertySort, bool desc)
        {
            PropertySort = propertySort;
            Desc = desc;

        }
    }

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

        //public Expression<Func<T, object>> OrderBy { get; set; }
        public List<SpecificationSort<T>> OrderBy { get; set; }

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
            //OrderBy = orderByExpression;
            OrderBy.Add(orderByExpression);
        }

    }
}
