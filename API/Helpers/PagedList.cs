using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace API.Helpers
{
    public class PagedList<T> : List<T>
    {
        

        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public int TotalPages { get; set; }
        public int TotalCount { get; set; }

        public PagedList(IEnumerable<T> items, int pageNo, int pageSize,int count)
        {
            CurrentPage = pageNo;
            TotalCount=count;
            PageSize = pageSize;
            TotalPages = (int) Math.Ceiling(count / (double)pageSize);
            AddRange(items);
            
        }

        public static async Task<PagedList<T>> CreateAsync(IQueryable<T> query,int pageNo,int pageSize)
        {
              var count=await query.CountAsync();
              var items=await query.Skip((pageNo-1)*pageSize).Take(pageSize).ToListAsync();
              return new PagedList<T>(items,pageNo,pageSize,count);
        }
       
    }
}