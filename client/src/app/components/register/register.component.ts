import { Component, OnInit,EventEmitter, Output } from '@angular/core';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { AccountService } from 'src/app/_services/account.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {
registerModel:any={};
@Output() cancel=new EventEmitter();

  constructor(public accountSerivce:AccountService,private toastr :ToastrService,private router:Router) { }

  ngOnInit(): void {
  }

  register(){
    this.accountSerivce.register(this.registerModel).subscribe(res=>{
      if(res!=null){
        this.toastr.success("User Added Successfully");
        this.router.navigateByUrl('/')
      }
      
    } );
  }

  cancelRegister(){
    this.cancel.emit(false);
  }

}
