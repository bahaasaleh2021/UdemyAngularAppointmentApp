import { Component, HostListener, OnInit, ViewChild } from '@angular/core';
import { NgForm } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { take } from 'rxjs/operators';
import { Member } from 'src/app/_models/member';
import { User } from 'src/app/_models/User';
import { AccountService } from 'src/app/_services/account.service';
import { MembersService } from 'src/app/_services/members.service';

@Component({
  selector: 'app-edit-member',
  templateUrl: './edit-member.component.html',
  styleUrls: ['./edit-member.component.css']
})
export class EditMemberComponent implements OnInit {
@ViewChild('editForm') editForm:NgForm;
@HostListener('window:beforeunload',['$event']) unloadNotification($event :any){
  if(this.editForm.dirty)
     $event.returnValue=true;
}
 member:Member;
 user:User;

  constructor(private accountService:AccountService,private memberService:MembersService,private toastr:ToastrService) {
     accountService.CurrentUser$.pipe(take(1)).subscribe(user=>this.user=user);
   }

  ngOnInit(): void {
    this.getMember();
  }

  getMember(){
    this.memberService.getMember(this.user.userName).pipe(take(1)).subscribe(member=>this.member=member);
  }

  saveChanges(){
    this.memberService.updateMember(this.member).subscribe(()=>{
      this.toastr.success("User Updated Successfully");
      this.editForm.reset(this.member);
    })
    
  }

}
