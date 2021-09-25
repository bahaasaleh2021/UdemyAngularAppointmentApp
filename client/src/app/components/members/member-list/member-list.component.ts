import { Component, OnInit } from '@angular/core';
import { Member } from 'src/app/_models/member';
import { Pagination } from 'src/app/_models/pagination';
import { MembersService } from 'src/app/_services/members.service';

@Component({
  selector: 'app-member-list',
  templateUrl: './member-list.component.html',
  styleUrls: ['./member-list.component.css']
})
export class MemberListComponent implements OnInit {
members:Member[];
pagination:Pagination;
pageNo=1;
pageSize=5;

  constructor(private service:MembersService) { }

  
  ngOnInit(): void {
    this.getMembers();
  }

  getMembers(){
    this.service.getMembers(this.pageNo,this.pageSize).subscribe(response=>{
      
      this.members=response.items;
      this.pagination=response.pagination;
      console.log(response.pagination);
    });
  }

  pageChanged(event:any){
    this.pageNo=event.page;
    this.getMembers();
  }

}
