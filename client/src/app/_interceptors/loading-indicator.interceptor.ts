import { Injectable } from '@angular/core';
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor
} from '@angular/common/http';
import { Observable } from 'rxjs';
import { BusyService } from '../_services/busy.service';
import { delay, finalize } from 'rxjs/operators';

@Injectable()
export class LoadingIndicatorInterceptor implements HttpInterceptor {

  constructor(private busyService:BusyService) {}

  intercept(request: HttpRequest<unknown>, next: HttpHandler): Observable<HttpEvent<unknown>> {
    console.log('starting indicator');
    this.busyService.busy();

    return next.handle(request).pipe(
      finalize(()=>{
        this.busyService.idle();
      })
    );
  }
}
