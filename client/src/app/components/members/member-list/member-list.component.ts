import { Component, OnInit } from '@angular/core';
import { take } from 'rxjs/operators';
import { Member } from 'src/app/_models/member';
import { Pagination } from 'src/app/_models/pagination';
import { User } from 'src/app/_models/User';
import { UserParams } from 'src/app/_models/user-params';
import { AccountService } from 'src/app/_services/account.service';
import { MembersService } from 'src/app/_services/members.service';

@Component({
  selector: 'app-member-list',
  templateUrl: './member-list.component.html',
  styleUrls: ['./member-list.component.css']
})
export class MemberListComponent implements OnInit {
members:Member[];
pagination:Pagination;
user:User;
userParams:UserParams;
genderList=[{display:'Male',value:'male'},{display:'Female',value:'female'}];

  constructor(private service:MembersService) { 
   
  }

  
  ngOnInit(): void {
    this.getMembers();
  }

  getMembers(){
    this.userParams=this.service.getUserParamas();
    this.service.getMembers(this.userParams).subscribe(response=>{
    
      this.members=response.items;
      this.pagination=response.pagination;
    });
  }

  reset(){
    this.userParams = this.service.resetUserParams();
    this.getMembers();
  }

  pageChanged(event:any){
    let params=this.service.getUserParamas();
    params.pageNo=event.page;
    this.service.setUserPArams(params);
    this.getMembers();
  }

}
