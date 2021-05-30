import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { User } from 'src/app/_models/User';
import { AccountService } from 'src/app/_services/account.service';

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.css']
})
export class NavComponent implements OnInit {
  loginModel:any={}


  constructor(public accountService:AccountService,private router:Router) { }

  ngOnInit(): void {
  }

  login(){
    this.accountService.login(this.loginModel).subscribe(res=>this.router.navigateByUrl('/members'))
  }

  logOut(){
    this.accountService.logout();
    this.router.navigateByUrl('/')
  }

}
