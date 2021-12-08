import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { Message } from '../_models/message';
import { getPaginatedResult, getPaginationHeaders } from './paginationHelper';

@Injectable({
  providedIn: 'root'
})
export class MessagesService {
  baseUrl=environment.apiUrl;
  
  constructor(private http:HttpClient) { }

  getMessages(pageNo:number,pageSize:number,container:string){
    let params=getPaginationHeaders(pageNo,pageSize);
    params.append('Container',container);

    return getPaginatedResult<Message[]>(this.baseUrl+'messages',params,this.http);
  }

  getMessageThread(userName:string){
    return this.http.get<Message[]>(this.baseUrl+'messages/thread/'+ userName);

  }

  sendMessage(userName:string,content:string){
    return this.http.post<Message>(this.baseUrl+'messages',{'recepientUserName':userName,content});

  }

  deletedMessage(id:number){
    return this.http.delete(this.baseUrl+'messages/'+ id);

  }
}
