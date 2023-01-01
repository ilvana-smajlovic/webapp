import {
  Component,
  ElementRef,
  EventEmitter,
  HostBinding,
  HostListener,
  Input,
  OnInit,
  Output, QueryList,
  ViewChild, ViewChildren
} from '@angular/core';
import {TracksterService} from "../services/trackster.service";
import {Media} from "../models/media";
import {resolve} from "@angular/compiler-cli";

import {ActiveDescendantKeyManager, Highlightable} from "@angular/cdk/a11y";
import {Router} from "@angular/router";
import {MediaComponent} from "../media/media.component";
import {environment} from "../../environments/environment";


@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {

  /*
  @viewchild('BtnToTop') private ButtonToTop! : ElementRef<HTMLButtonElement>//Da je div isao bih HTMLDivElement

  window.onscroll = function() {scrollFunction()};

  scrollFunction() {
    if (document.body.scrollTop > 20 || document.documentElement.scrollTop > 20) {
      this.ButtonToTop.nativeElement.style.display = "block"
    } else {
      this.ButtonToTop.nativeElement.style.display = "none";
    }
  }

// When the user clicks on the button, scroll to the top of the document
  function topFunction() {
    document.body.scrollTop = 0; // For Safari
    document.documentElement.scrollTop = 0; // For Chrome, Firefox, IE and Opera
  }
  */


  searchedMedia : Media[];
  isDataLoaded : boolean=false;
  searchText= '';
  allMedia : Media[];
  selectedMedia:Media;
  id:number;

  constructor(private tracksterService : TracksterService, private router: Router) { }

  logged_in=0;

  ngOnInit(): void {
    this.getMediaByName();
  }

  loggedin(){
    if(this.logged_in==0)
    {
      this.logged_in=1;
    }
    else {
      this.logged_in=0;
    }
  }

  BackToTop() {
    document.documentElement.scrollTop=0;
  }
  getMediaByName(){
    console.log('get media');
    this.tracksterService.getMediaByName()
      .subscribe(response => {
        // @ts-ignore
        this.allMedia=response;
        this.isDataLoaded = true;
        //this.selectedMedia=this.allMedia.map(item=>item.name);
      });
  }

  searchMedia(text: string){
      this.searchedMedia=this.allMedia.filter((val) =>
      val.name.toLowerCase().includes(text));
      console.log(this.searchedMedia);
  }
  redirectToMedia(media: Media) {
    this.id=media.mediaId;
    this.router.navigate(['/media', this.id]);
  }
}

