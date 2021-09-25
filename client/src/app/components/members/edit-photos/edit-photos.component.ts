
import { Component, Input, OnInit } from '@angular/core';
import { FileUploader } from 'ng2-file-upload';
import { take } from 'rxjs/operators';
import { Member } from 'src/app/_models/member';
import { Photo } from 'src/app/_models/photo';
import { User } from 'src/app/_models/User';
import { AccountService } from 'src/app/_services/account.service';
import { MembersService } from 'src/app/_services/members.service';
import { environment } from 'src/environments/environment';

@Component({
  selector: 'app-edit-photos',
  templateUrl: './edit-photos.component.html',
  styleUrls: ['./edit-photos.component.css']
})
export class EditPhotosComponent implements OnInit {
@Input() member:Member;
uploader:FileUploader;
hasBaseDropzoneOver=false;
baseUrl=environment.apiUrl;
user:User;

  constructor(private accountService:AccountService,private membersService:MembersService) {
    this.accountService.CurrentUser$.pipe(take(1)).subscribe(usr=>this.user=usr);
   }

  ngOnInit(): void {
    this.initializeUploader();
  }
  

  fileOverBase(e:any){
    this.hasBaseDropzoneOver=e;
  }

  initializeUploader(){
    this.uploader=new FileUploader({
       url:`${this.baseUrl}users/add-photo`,
       authToken:`Bearer ${this.user.token}`,
       isHTML5:true,
       allowedFileType:['image'],
       removeAfterUpload:true,
       autoUpload:false,
       maxFileSize:10 * 1024 * 1024
    });

    this.uploader.onAfterAddingFile=(file)=>{
      file.withCredentials=false;
    }

    this.uploader.onSuccessItem=(item,response,status,header)=>{
       if(response){
         const photo:Photo=JSON.parse(response);
         this.member.photos.push(photo);
         if(photo.isMain){
           this.user.photoUrl=photo.url;
           this.member.photoUrl=photo.url;
           this.accountService.setCurrentUser(this.user);
         }
       }
         
    }
  }

  setMainPhoto(photo:Photo){
    this.membersService.setMainPhoto(photo.id).subscribe(()=>{
        this.user.photoUrl=photo.url;
        this.accountService.setCurrentUser(this.user);
        this.member.photoUrl=photo.url;

        this.member.photos.forEach(p=>{
          if(p.isMain===true)
             p.isMain=false;

          if(p.id===photo.id)
            p.isMain=true;

          
        })
    })
  }

  removePhoto(photoId:number){
    this.membersService.removePhoto(photoId).subscribe(()=>{
      this.member.photos=this.member.photos.filter(x=>x.id!==photoId);
    })
  }



}
