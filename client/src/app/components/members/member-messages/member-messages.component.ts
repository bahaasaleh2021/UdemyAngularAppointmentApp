import { Component, Input, OnInit } from '@angular/core';
import { Message } from 'src/app/_models/message';
import { MessagesService } from 'src/app/_services/messages.service';

@Component({
  selector: 'app-member-messages',
  templateUrl: './member-messages.component.html',
  styleUrls: ['./member-messages.component.css']
})
export class MemberMessagesComponent implements OnInit {

  @Input() messages:Message[];
  @Input() userName:string;

  content:string;

  constructor(private msgService:MessagesService) { }

  ngOnInit(): void {
   
  }

sendMessage(){
  this.msgService.sendMessage(this.userName,this.content).subscribe(newMsg=>this.messages.push(newMsg))
}

}
