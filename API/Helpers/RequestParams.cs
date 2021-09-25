using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Helpers
{
    public class RequestParams
    {
        private int MaxPageSize{get;set;}=50;
        private int _pageSize=10;
        public int PageSize { 
            get=> _pageSize;
            set=> _pageSize=value>MaxPageSize?MaxPageSize:value==0?_pageSize:value;
         }

         public int PageNo { get; set; }=1;
    }
}