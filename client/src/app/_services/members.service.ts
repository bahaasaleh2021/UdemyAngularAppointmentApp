import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { of } from 'rxjs';
import { map, take } from 'rxjs/operators';
import { environment } from 'src/environments/environment';
import { Member } from '../_models/member';
import { PagedList } from '../_models/pagination';
import { User } from '../_models/User';
import { UserParams } from '../_models/user-params';
import { AccountService } from './account.service';

@Injectable({
  providedIn: 'root'
})
export class MembersService {
  baseUrl=environment.apiUrl;
  memberCache=new Map();
  userPArams:UserParams;
  user:User;
  
  constructor(private http:HttpClient,private accountService:AccountService) { 
    this.accountService.CurrentUser$.pipe(take(1)).subscribe(user=>{
      this.user=user;
      this.resetUserParams();
    })
  }

  setUserPArams(params:UserParams){
    this.userPArams=params;
  }

  getUserParamas(){
    return this.userPArams;
  }

  resetUserParams(){
    this.userPArams=new UserParams(this.user);
    return this.userPArams;
  }

  
  getMembers(userParams:UserParams){
  const key=userParams.getValuesAsString();
  let cachedResult=this.memberCache.get(key);
  console.log('chached members:',cachedResult);
 if(cachedResult)
    return of(cachedResult);

    var params=this.getPaginationHeaders(userParams.pageNo,userParams.pageSize);

    params=params.append("minage",userParams.minAge.toString());
    params=params.append("maxAge",userParams.maxAge.toString());
    params=params.append("gender",userParams.gender);
    params=params.append("orderBy",userParams.orderBy);

    return this.getPaginatedResult<Member[]>(this.baseUrl + "Users",params).pipe(
      map(response=>{
        this.memberCache.set(key,response);
        return response;
      })
    );
  }

  private getPaginatedResult<T>(url:string, params: HttpParams) {
    const   paginatedResult:PagedList<T>=new PagedList<T>();

    return this.http.get<T>(url, { observe: "response", params }).pipe(
      map(response => {
       paginatedResult.items = response.body;
        if (response.headers.get('Pagination') !== null)
          paginatedResult.pagination = JSON.parse(response.headers.get('Pagination'));

        return paginatedResult;
      })
    );
  }

  getPaginationHeaders(pageNo:number,pageSize:number){
    let params=new HttpParams();
    
    params=params.append("PageNo",pageNo.toString());
    params=params.append("PageSize",pageSize.toString());

    return params;
    
  }

  getMember(userName:string){
    //get from cached result
    let cachedMember=[...this.memberCache.values()].reduce((mems,member)=>mems.concat(member.items),[])
    .find((mem:Member)=>mem.userName===userName);

    if(cachedMember)
    return of(cachedMember);

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
