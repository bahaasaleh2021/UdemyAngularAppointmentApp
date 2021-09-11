import { Injectable } from '@angular/core';
import { NgxSpinnerService } from 'ngx-spinner';

@Injectable({
  providedIn: 'root'
})
export class BusyService {
busyRequestCount=0;

  constructor(private spinnerService:NgxSpinnerService) { }

  busy(){
    this.busyRequestCount++;
    console.log('showing:',this.busyRequestCount);
    this.spinnerService.show();
  }

  idle(){
    this.busyRequestCount--;
    console.log('hiding:',this.busyRequestCount);
    if(this.busyRequestCount<=0){
      this.busyRequestCount=0;
      this.spinnerService.hide();
    }
  }
}
