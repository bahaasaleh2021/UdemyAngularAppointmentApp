using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Helpers
{
    public class PaginationHeader
    {
        public PaginationHeader(int cuurentPage, int pageSize, int totalPages, int totalCount)
        {
            CurrentPage = cuurentPage;
            PageSize = pageSize;
            TotalPages = totalPages;
            TotalCount = totalCount;
        }

        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public int TotalPages { get; set; }
        public int TotalCount { get; set; }
    }
}