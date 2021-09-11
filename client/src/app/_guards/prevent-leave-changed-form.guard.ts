import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, CanDeactivate, RouterStateSnapshot, UrlTree } from '@angular/router';
import { Observable } from 'rxjs';
import { EditMemberComponent } from '../components/members/edit-member/edit-member.component';

@Injectable({
  providedIn: 'root'
})
export class PreventLeaveChangedFormGuard implements CanDeactivate<unknown> {
  canDeactivate( component: EditMemberComponent): boolean {
    if(component.editForm.dirty)
       return confirm('you have unsaved changes , are you sure you want to leave page?');
       
    return true;
  }
  
}
