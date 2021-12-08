import { HttpClient, HttpParams } from "@angular/common/http";
import { map } from "rxjs/operators";
import { PagedList } from "../_models/pagination";

export function getPaginatedResult<T>(url:string, params: HttpParams,http:HttpClient) {
    const   paginatedResult:PagedList<T>=new PagedList<T>();

    return http.get<T>(url, { observe: "response", params }).pipe(
      map(response => {
       paginatedResult.items = response.body;
        if (response.headers.get('Pagination') !== null)
          paginatedResult.pagination = JSON.parse(response.headers.get('Pagination'));

        return paginatedResult;
      })
    );
  }

 export function getPaginationHeaders(pageNo:number,pageSize:number){
    let params=new HttpParams();
    
    params=params.append("PageNo",pageNo.toString());
    params=params.append("PageSize",pageSize.toString());

    return params;
    
  }