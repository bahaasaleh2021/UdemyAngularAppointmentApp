import { CUSTOM_ELEMENTS_SCHEMA, NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import {FormsModule, ReactiveFormsModule} from '@angular/forms';
import { AppRoutingModule } from './app-routing.module';
import {HttpClientModule, HTTP_INTERCEPTORS} from '@angular/common/http';
import {BrowserAnimationsModule} from '@angular/platform-browser/animations';
import { AppComponent } from './app.component';
import { NavComponent } from './components/nav/nav.component';
import { BsDropdownModule } from 'ngx-bootstrap/dropdown';
import { HomeComponent } from './components/home/home.component';
import { RegisterComponent } from './components/register/register.component';
import { MemberListComponent } from './components/members/member-list/member-list.component';
import { MemberDetailsComponent } from './components/members/member-details/member-details.component';
import { ListsComponent } from './components/lists/lists.component';
import { MessagesComponent } from './components/messages/messages.component';
import { ToastrModule } from 'ngx-toastr';
import { ErrorInterceptor } from './_interceptors/error.interceptor';
import { NotFoundComponent } from './components/not-found/not-found.component';
import { ServerErrorComponent } from './components/server-error/server-error.component';
import { MemberCardComponent } from './components/members/member-card/member-card.component';
import { JwtInterceptor } from './_interceptors/jwt.interceptor';
import {TabsModule} from 'ngx-bootstrap/tabs';
import { NgxGalleryModule } from '@kolkov/ngx-gallery';
import { NgxSpinnerModule } from "ngx-spinner";
import { EditMemberComponent } from './components/members/edit-member/edit-member.component';
import { LoadingIndicatorInterceptor } from './_interceptors/loading-indicator.interceptor';
import { EditPhotosComponent } from './components/members/edit-photos/edit-photos.component';
import { FileUploadModule } from 'ng2-file-upload';
import { TextInputComponent } from './components/_reusable/text-input/text-input.component';
import {BsDatepickerModule} from 'ngx-bootstrap/datepicker';
import { DateInputComponent } from './components/_reusable/date-input/date-input.component';
import { PaginationModule } from 'ngx-bootstrap/pagination';
import { ButtonsModule } from 'ngx-bootstrap/buttons';
import { TimeagoModule } from 'ngx-timeago';
import { MemberMessagesComponent } from './components/members/member-messages/member-messages.component';

@NgModule({
  declarations: [
    AppComponent,
    NavComponent,
    HomeComponent,
    RegisterComponent,
    MemberListComponent,
    MemberDetailsComponent,
    ListsComponent,
    MessagesComponent,
    NotFoundComponent,
    ServerErrorComponent,
    MemberCardComponent,
    EditMemberComponent,
    EditPhotosComponent,
    TextInputComponent,
    DateInputComponent,
    MemberMessagesComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    BrowserAnimationsModule,
    FormsModule,
    ReactiveFormsModule,
    NgxGalleryModule,
    NgxSpinnerModule,
    BsDropdownModule.forRoot(),
    ToastrModule.forRoot({
      positionClass:"toast-bottom-right"
    }),
    TabsModule.forRoot(),
    FileUploadModule,
    BsDatepickerModule.forRoot(),
    PaginationModule.forRoot(),
    ButtonsModule.forRoot(),
    TimeagoModule.forRoot()
  ],
  schemas: [CUSTOM_ELEMENTS_SCHEMA],
  providers: [
    {provide:HTTP_INTERCEPTORS,useClass:ErrorInterceptor,multi:true},
    {provide:HTTP_INTERCEPTORS,useClass:JwtInterceptor,multi:true},
    {provide:HTTP_INTERCEPTORS,useClass:LoadingIndicatorInterceptor,multi:true}
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
