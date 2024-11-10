// ****************************************************************
//  Assembly         : CAFEMACA.Coink.PruebaTecnica.Application
//  Author           :  Carlos Fernando Malagón Cano
//  Created          : 04-02-2024
//
//  Last Modified By : Carlos Fernando Malagón Cano
//  Last Modified On : 04-12-2024
//  ****************************************************************
//  <copyright file="CustomExpressionFilter.cs"
//      company="Cafemaca - CAFEMACA Colombia">
//      Cafemaca - CAFEMACA Colombia
//  </copyright>
//

using CAFEMACA.Coink.PruebaTecnica.Application.Common.SpecificationQueries;
using System.Linq.Expressions;
using System.Text.Json;

namespace CAFEMACA.Coink.PruebaTecnica.Application.Common.Filtering
{
    public static class CustomExpressionFilter<T> where T : class
    {
        public static List<ColumnFilter> GetColumnFilters(string? columns)
        {
            List<ColumnFilter> columnFilters = new List<ColumnFilter>();

            if (!String.IsNullOrEmpty(columns))
            {
                try
                {
                    columnFilters.AddRange(JsonSerializer.Deserialize<List<ColumnFilter>>(columns));
                }
                catch (Exception)
                {
                    columnFilters = new List<ColumnFilter>();
                }
            }

            return columnFilters;
        }

        public static List<ColumnSorting> GetColumnSorting(string? columns)
        {
            List<ColumnSorting> columnSorting = new List<ColumnSorting>();

            if (!String.IsNullOrEmpty(columns))
            {
                try
                {
                    columnSorting.AddRange(JsonSerializer.Deserialize<List<ColumnSorting>>(columns));
                }
                catch (Exception)
                {
                    columnSorting = new List<ColumnSorting>();
                }
            }

            return columnSorting;
        }

        public static Expression<Func<T, bool>> CustomFilter(List<ColumnFilter> columnFilters)
        {
            try
            {
                // Create the parameter expression for the input data
                var parameterExpression = Expression.Parameter(typeof(T), nameof(T));


                // Build the filter expression dynamically
                Expression filterExpression = null;
                foreach (var filter in columnFilters)
                {
                    var memberExpression = Expression.PropertyOrField(parameterExpression, filter.ColumnName);

                    Expression comparison;

                    if (memberExpression.Type == typeof(string))
                    {
                        var constant = Expression.Constant(filter.Value);
                        comparison = Expression.Call(memberExpression, "Contains", Type.EmptyTypes, constant);
                    }
                    else if (memberExpression.Type == typeof(double))
                    {
                        var constant = Expression.Constant(Convert.ToDouble(filter.Value));
                        comparison = Expression.Equal(memberExpression, constant);
                    }
                    else if (memberExpression.Type == typeof(Guid))
                    {
                        var constant = Expression.Constant(Guid.Parse(filter.Value));
                        comparison = Expression.Equal(memberExpression, constant);
                    }
                    else if (memberExpression.Type == typeof(bool))
                    {
                        var constant = Expression.Constant(Convert.ToBoolean(filter.Value));
                        comparison = Expression.Equal(memberExpression, constant);
                    }
                    else
                    {
                        var constant = Expression.Constant(Convert.ToInt32(filter.Value));
                        comparison = Expression.Equal(memberExpression, constant);
                    }


                    filterExpression = filterExpression == null
                        ? comparison
                        : Expression.And(filterExpression, comparison);
                }

                // Create the lambda expression with the parameter and the filter expression
                return Expression.Lambda<Func<T, bool>>(filterExpression, parameterExpression);
            }
            catch (Exception)
            {
                return null;
            }

        }
        public static List<SpecificationSort<T>> CustomSort(List<ColumnSorting> columnSortings)
        {
            try
            {
                var expressionSorts = new List<SpecificationSort<T>>();

                foreach (ColumnSorting column in columnSortings)
                {
                    expressionSorts.Add(new SpecificationSort<T>(LambdaExpressionFactory.Lambda<T>(column.ColumnName), column.Desc));
                }

                // Create the lambda expression with the parameter and the filter expression
                return expressionSorts;
            }
            catch (Exception)
            {
                return null;
            }

        }
    }
}
