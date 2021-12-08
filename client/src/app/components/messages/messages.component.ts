
import { Component, OnInit } from '@angular/core';
import { Message } from 'src/app/_models/message';
import { Pagination } from 'src/app/_models/pagination';
import { MessagesService } from 'src/app/_services/messages.service';

@Component({
  selector: 'app-messages',
  templateUrl: './messages.component.html',
  styleUrls: ['./messages.component.css']
})
export class MessagesComponent implements OnInit {
  messages:Message[];
pagination:Pagination;
pageNo=1;
pageSize=5;
container="Inbox";

  constructor(private messagesService:MessagesService) {
    
   }

  ngOnInit(): void {
    this.loadMessages();
  }

  loadMessages(){
    return this.messagesService.getMessages(this.pageNo,this.pageSize,this.container).subscribe(msgs=>{
      this.messages=msgs.items;
      this.pagination=msgs.pagination;
    });
  }

  pageChanged(event:any){
    if(this.pageNo!=event.page){
      this.pageNo=event.page;
      this.loadMessages();
    }
   

  }

  deleteMessage(id:number){
    this.messagesService.deletedMessage(id).subscribe(()=>this.loadMessages())
  }
}
