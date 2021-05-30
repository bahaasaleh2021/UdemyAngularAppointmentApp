import { Component, OnInit } from '@angular/core';
import {HttpClient} from '@angular/common/http';
import { Subscriber } from 'rxjs';
import { AccountService } from './_services/account.service';



@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit{
  title = 'client';
  welcomeMsg='Hello from the other side';
 users:any;
  /**
   *
   */
  constructor(private http:HttpClient,private accountService:AccountService) {
    
  }

  populateLocallySavedUser(){
    var user=localStorage.getItem('user');
    if(user)
       this.accountService.setCurrentUser(JSON.parse(user));
  }

  ngOnInit(): void {
    this.populateLocallySavedUser();
  }

  getUsers(){
    this.http.get('https://localhost:5005/api/Users').
    subscribe(response=>{
      this.users=response;
    },
    error=>{
      console.log(error);
    }
    )
  }
}
