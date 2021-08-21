import { Component, OnInit } from '@angular/core';
import { Member } from 'src/app/_models/member';
import { MembersService } from 'src/app/_services/members.service';

@Component({
  selector: 'app-member-list',
  templateUrl: './member-list.component.html',
  styleUrls: ['./member-list.component.css']
})
export class MemberListComponent implements OnInit {
members:Member[]|null=null;

  constructor(private service:MembersService) { }

  
  ngOnInit(): void {
    this.getMembers();
  }

  getMembers(){
    this.service.getMembers().subscribe(mems=>this.members=mems);
  }

}
