import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { Member } from '../_models/member';

@Injectable({
  providedIn: 'root'
})
export class MembersService {
  baseUrl=environment.apiUrl;
  

  
  constructor(private http:HttpClient) { }

  getMembers(){
    return this.http.get<Member[]>(this.baseUrl+"Users");
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
