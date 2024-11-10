// ****************************************************************
//  Assembly         : CAFEMACA.Coink.PruebaTecnica.Application
//  Author           :  Carlos Fernando Malagón Cano
//  Created          : 07-16-2024
//
//  Last Modified By : Carlos Fernando Malagón Cano
//  Last Modified On : 07-16-2024
//  ****************************************************************
//  <copyright file="SearchQueryParameters.cs"
//      company="Cafemaca - CAFEMACA Colombia">
//      Cafemaca - CAFEMACA Colombia
//  </copyright>
//

using CAFEMACA.Coink.PruebaTecnica.Data.Common.AbstractionsPagination;

namespace CAFEMACA.Coink.PruebaTecnica.Application.Common.Filtering
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="SearchTerm">This nullable string property is used to store a search term entered by the user.
    /// When this value is set, it can be used to filter the data based on the given search term, typically by matching the term with the content in one or more columns.
    /// </param>
    /// <param name="ColumnFilters">This nullable string property is designed to store additional filtering criteria based on specific columns.
    /// The format for this property is not defined in the code snippet, but it could be, for example, a JSON string representing column names and their corresponding filter values.
    /// {[{"ColumnName": "columnname","Value": "value"}]}
    /// </param>
    /// <param name="OrderBy">This property is a nullable string and is used to store the column name and sort order for sorting the data.
    /// For example, “user_name ASC” or “user_age DESC” could be values for this property, where “ASC” represents ascending order and “DESC” represents descending order.
    /// </param>
    /// <param name="PageIndex">An integer property representing the current page number. It is initialized to 1 by default.
    /// </param>
    /// <param name="PageSize">A public integer property with the Page Size.
    /// </param>
    public record SearchQueryParameters(string? SearchTerm, string? ColumnFilters, string? OrderBy, int PageIndex, int PageSize) : PaginationQueryParameters(PageIndex, PageSize);
}
