// ****************************************************************
//  Assembly         : CAFEMACA.Coink.PruebaTecnica.Application
//  Author           :  Carlos Fernando Malagón Cano
//  Created          : 07-29-2024
//
//  Last Modified By : Carlos Fernando Malagón Cano
//  Last Modified On : 07-29-2024
//  ****************************************************************
//  <copyright file="LambdaExpressionFactory.cs"
//      company="Cafemaca - CAFEMACA Colombia">
//      Cafemaca - CAFEMACA Colombia
//  </copyright>
//

using System.Linq.Expressions;

namespace CAFEMACA.Coink.PruebaTecnica.Application.Common.Filtering
{
    public static class LambdaExpressionFactory
    {
        // With member type
        public static Expression<Func<T, TProperty>> Lambda<T, TProperty>(string memberName)
        {
            var parameterExpression = Expression.Parameter(typeof(T), "x");
            var memberExpression = Expression.PropertyOrField(parameterExpression, memberName);
            var result = (Expression<Func<T, TProperty>>)Expression.Lambda(memberExpression, parameterExpression);
            return result;
        }

        // Without member type
        public static Expression<Func<T, object>> Lambda<T>(string memberName)
        {
            var parameterExpression = Expression.Parameter(typeof(T), "x");
            var memberExpression = Expression.PropertyOrField(parameterExpression, memberName);
            var unaryExpression = Expression.Convert(memberExpression, typeof(object));
            var result = (Expression<Func<T, object>>)Expression.Lambda(unaryExpression, parameterExpression);
            return result;
        }
    }
}
