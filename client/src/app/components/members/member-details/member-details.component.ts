import { Component, OnInit, ViewChild } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { NgxGalleryAnimation, NgxGalleryImage } from '@kolkov/ngx-gallery';
import { TabDirective, TabsetComponent } from 'ngx-bootstrap/tabs';
import { Member } from 'src/app/_models/member';
import { Message } from 'src/app/_models/message';
import { MembersService } from 'src/app/_services/members.service';
import { MessagesService } from 'src/app/_services/messages.service';

@Component({
  selector: 'app-member-details',
  templateUrl: './member-details.component.html',
  styleUrls: ['./member-details.component.css']
})
export class MemberDetailsComponent implements OnInit {
@ViewChild('tabSet', { static: true })  tabSet:TabsetComponent;
activeTab:TabDirective;

  member:Member;
  galleryOptions:any;
  galleryImages:NgxGalleryImage[];
  messages:Message[]=[];

  constructor(private memService:MembersService,private route:ActivatedRoute,private msgService:MessagesService) { }

  ngOnInit(): void {
    this.route.data.subscribe(data=>this.member=data.member);
    
    
    this.route.queryParams.subscribe(params=>params.tab ?this.selectTab(params.tab):this.selectTab(0))

    this.galleryOptions = [
      {
        width: '600px',
        height: '400px',
        thumbnailsColumns: 4,
        imageAnimation: NgxGalleryAnimation.Slide
      },
      // max-width 800
      {
        breakpoint: 800,
        width: '100%',
        height: '600px',
        imagePercent: 80,
        thumbnailsPercent: 20,
        thumbnailsMargin: 20,
        thumbnailMargin: 20
      },
      // max-width 400
      {
        breakpoint: 400,
        preview: false
      }
    ];
    this.galleryImages=this.getImages();
  }

  onTabActivated(selected:TabDirective){
    this.activeTab=selected;
    if(selected.heading==="Messages" && this.messages.length===0){
   this.loadMessageThread();
    }
  }

  selectTab(tabId:number){
    this.tabSet.tabs[tabId].active=true;
  }

  loadMessageThread(){
    this.msgService.getMessageThread(this.member.userName).subscribe(msgs=>{
      this.messages=msgs;
      console.log(msgs);
    }
    );
  }

  getImages():NgxGalleryImage[]{
    const imagesUrls=[];
    for (const photo of this.member?.photos!) {
      imagesUrls.push({
        small:photo?.url,
        medium:photo?.url,
        big:photo?.url
      });
      
    }

    return imagesUrls;
  }



}
