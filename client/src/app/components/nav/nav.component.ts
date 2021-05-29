import { Component, OnInit } from '@angular/core';
import { AccountService } from 'src/app/_services/account.service';

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.css']
})
export class NavComponent implements OnInit {
  loginModel:any={}
  isLoggedIn=false;

  constructor(public accountService:AccountService) { }

  ngOnInit(): void {
  }

  login(){
    this.accountService.login(this.loginModel).subscribe(
      res=>{
        console.log(res);
        this.isLoggedIn=true;
      }
    )
  }

  logOut(){
    this.isLoggedIn=false;
  }

}
