import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {
registerMode=false;
  constructor() { }

  ngOnInit(): void {
  }

  register(){
    this.registerMode=true;
  }

  cancel(event:boolean){
     this.registerMode=event;
  }

}
