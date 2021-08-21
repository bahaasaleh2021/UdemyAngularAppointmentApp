import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { NgxGalleryAnimation, NgxGalleryImage } from '@kolkov/ngx-gallery';
import { Member } from 'src/app/_models/member';
import { MembersService } from 'src/app/_services/members.service';

@Component({
  selector: 'app-member-details',
  templateUrl: './member-details.component.html',
  styleUrls: ['./member-details.component.css']
})
export class MemberDetailsComponent implements OnInit {

  member:Member|null=null;
  galleryOptions:any;
  galleryImages:NgxGalleryImage[]|null=null;


  constructor(private memService:MembersService,private route:ActivatedRoute) { }

  ngOnInit(): void {
    

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

    this.loadMember();
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

  loadMember(){
    this.memService.getMember(this.route.snapshot.paramMap.get("username")!).subscribe(mem=>{
      this.member=mem;
      this.galleryImages=this.getImages();
    });
  }

}
