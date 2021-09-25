import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map } from 'rxjs/operators';
import { environment } from 'src/environments/environment';
import { Member } from '../_models/member';
import { PagedList } from '../_models/pagination';

@Injectable({
  providedIn: 'root'
})
export class MembersService {
  baseUrl=environment.apiUrl;
  paginatedResult:PagedList<Member[]>=new PagedList<Member[]>();

  
  constructor(private http:HttpClient) { }

  getMembers(pageNo?:number,pageSize?:number){
    let params=new HttpParams();
    if(pageNo!==null && pageSize!==null){
    params=params.append("PageNo",pageNo.toString());
    params=params.append("PageSize",pageSize.toString());

    }
    return this.http.get<Member[]>(this.baseUrl+"Users",{observe:"response",params}).pipe(
      map(response=>{
        this.paginatedResult.items=response.body;
        if(response.headers.get('Pagination')!==null)
            this.paginatedResult.pagination=JSON.parse(response.headers.get('Pagination'));
            
        return this.paginatedResult;
      })
    );
  }

  getMember(userName:string){
    return this.http.get<Member>(this.baseUrl+"Users/"+userName);
  }

  updateMember(member:Member){
    return this.http.put(this.baseUrl+"Users",member);
  }

  setMainPhoto(photoId:number){
    return this.http.put(`${this.baseUrl}users/set-main-photo/${photoId}`,{});
  }

  removePhoto(photoId:number){
    return this.http.delete(`${this.baseUrl}users/delete-photo/${photoId}`,{});
  }
}
