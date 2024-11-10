// ****************************************************************
//  Assembly         : CAFEMACA.Coink.PruebaTecnica.Api
//  Author           :  Carlos Fernando Malagón Cano
//  Created          : 07-23-2024
//
//  Last Modified By : Carlos Fernando Malagón Cano
//  Last Modified On : 07-23-2024
//  ****************************************************************
//  <copyright file="AddPaginationMetadataExtension.cs"
//      company="Cafemaca - CAFEMACA Colombia">
//      Cafemaca - CAFEMACA Colombia
//  </copyright>
//

using CAFEMACA.Coink.PruebaTecnica.Application.Common.Filtering;
using CAFEMACA.Coink.PruebaTecnica.Data.Common.Pagining;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace CAFEMACA.Coink.PruebaTecnica.Api.Extensions
{
    /// <summary>
    /// Add data parameters to the metadata file
    /// </summary>
    public static class AddPaginationMetadataExtension
    {
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="controller"></param>
        /// <param name="pagedItems"></param>
        /// <param name="queryParameters"></param>
        public static void AddPaginationMetadata<T>(this ControllerBase controller, PagedList<T> pagedItems,
            SearchQueryParameters queryParameters)
        {
            string? previousPageUrl = null;
            string? nextPageUrl = null;

            HttpGetAttribute? paginationAttribute = controller.ControllerContext.ActionDescriptor.MethodInfo
                .GetCustomAttributes(false).First(obj => obj is HttpGetAttribute) as HttpGetAttribute;

            var routeName = paginationAttribute.Name;

            if (pagedItems.HasPrevious)
            {
                // If we have previous page, include link to that page
                previousPageUrl = controller.Url.Link(routeName, queryParameters with
                {
                    PageIndex = queryParameters.PageIndex - 1
                });
            }

            if (pagedItems.HasNext)
            {
                // If we have next page include, link to that page
                nextPageUrl = controller.Url.Link(routeName, queryParameters with
                {
                    PageIndex = queryParameters.PageIndex + 1
                });
            }

            // Add header
            controller.Response.Headers.Append("X-Pagination", JsonSerializer.Serialize(
                new
                {
                    queryParameters.SearchTerm,
                    queryParameters.ColumnFilters,
                    queryParameters.OrderBy,

                    pagedItems.HasNext,
                    pagedItems.HasPrevious,
                    pagedItems.TotalPageCount,
                    pagedItems.TotalItemCount,
                    pagedItems.CurrentPage,
                    pagedItems.PageSize,
                    previousPageUrl,
                    nextPageUrl
                }));
        }
    }
}
