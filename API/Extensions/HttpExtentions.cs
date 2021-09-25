using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using API.Helpers;
using Microsoft.AspNetCore.Http;

namespace API.Extensions
{
    public static class HttpExtentions
    {
        public static void AddPaginationHeader(this HttpResponse response,int pageNo,int pageSize,int totalPages,int totalCount){
            var header=new PaginationHeader(pageNo,pageSize,totalPages,totalCount);
           var options=new JsonSerializerOptions{
                PropertyNamingPolicy=JsonNamingPolicy.CamelCase
           };
            response.Headers.Add("Pagination",JsonSerializer.Serialize(header,options));
            response.Headers.Add("Access-Control-Expose-Headers","Pagination");
        }
    }
}