import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HomeComponent } from './components/home/home.component';
import { ListsComponent } from './components/lists/lists.component';
import { EditMemberComponent } from './components/members/edit-member/edit-member.component';
import { MemberDetailsComponent } from './components/members/member-details/member-details.component';
import { MemberListComponent } from './components/members/member-list/member-list.component';
import { MessagesComponent } from './components/messages/messages.component';
import { NotFoundComponent } from './components/not-found/not-found.component';
import { ServerErrorComponent } from './components/server-error/server-error.component';
import { AuthGuard } from './_guards/auth.guard';
import { PreventLeaveChangedFormGuard } from './_guards/prevent-leave-changed-form.guard';

const routes: Routes = [
{
  path:'',
  canActivate:[AuthGuard],
  children:[
    {path:'',component:HomeComponent},
    {path:'members',component:MemberListComponent},
    {path:'members/:username',component:MemberDetailsComponent},
    {path:'member/edit',component:EditMemberComponent,canDeactivate:[PreventLeaveChangedFormGuard]},
    {path:'lists',component:ListsComponent},
    {path:'messages',component:MessagesComponent}
  ]
},
{path:'not-found',component:NotFoundComponent},
{path:'server-error',component:ServerErrorComponent},
{path:'**',component:NotFoundComponent,pathMatch:'full'}

];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
