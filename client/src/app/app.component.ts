import { Component, OnInit } from '@angular/core';
import {HttpClient} from '@angular/common/http';
import { Subscriber } from 'rxjs';


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
  constructor(private http:HttpClient) {}

  ngOnInit(): void {
     this.getUsers();
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
