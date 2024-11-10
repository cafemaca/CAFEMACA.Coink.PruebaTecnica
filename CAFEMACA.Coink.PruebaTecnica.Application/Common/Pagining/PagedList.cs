// ****************************************************************
//  Assembly         : CAFEMACA.Coink.PruebaTecnica.Application
//  Author           :  Carlos Fernando Malagón Cano
//  Created          : 07-23-2024
//
//  Last Modified By : Carlos Fernando Malagón Cano
//  Last Modified On : 07-23-2024
//  ****************************************************************
//  <copyright file="PagedList.cs"
//      company="Cafemaca - CAFEMACA Colombia">
//      Cafemaca - CAFEMACA Colombia
//  </copyright>
//


namespace CAFEMACA.Coink.PruebaTecnica.Data.Common.Pagining
{
    public class PagedList<T>
    {
        public List<T> Items { get; private set; }
        public int PageSize { get; private set; }
        public int CurrentPage { get; private set; }
        public int TotalItemCount { get; private set; }
        public int TotalPageCount { get; private set; }
        public bool HasPrevious => (CurrentPage > 1);
        public bool HasNext => (CurrentPage < TotalPageCount);

        private PagedList(List<T> items, int pageSize, int currentPage, int totalItemCount)
        {
            PageSize = pageSize;
            CurrentPage = currentPage;
            TotalItemCount = totalItemCount;
            TotalPageCount = (int)Math.Ceiling(TotalItemCount / (double)PageSize);
            Items = items;
        }

        public static PagedList<T> Create(IEnumerable<T> items, int pageIndex, int pageSize)
        {
            var count = items.Count();
            var pagedItems = items.Skip(pageSize * (pageIndex - 1)).Take(pageSize).ToList();
            return new PagedList<T>(pagedItems, pageSize, pageIndex, count);
        }
        public static Task<PagedList<T>> CreateAsync(IEnumerable<T> items, int pageIndex, int pageSize)
        {
            Task<PagedList<T>> task = Task.Run(() =>
            {
                var count = items.Count();
                var pagedItems = items.Skip(pageSize * (pageIndex - 1)).Take(pageSize).ToList();
                return new PagedList<T>(pagedItems, pageSize, pageIndex, count);
            });

            return task;

        }
    }
}