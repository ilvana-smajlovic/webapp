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
import {coerceNumberProperty} from "@angular/cdk/coercion";

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {

  searchedMedia : Media[];
  isDataLoaded : boolean=false;
  searchText = '';
  allMedia : Media[];
  selectedMedia:Media;
  id:number;
  name:string='';
  tokenString:any;
  user:any;
  token:any;
  hoveredMedia: any = null;
  type:number;

  constructor(private tracksterService : TracksterService, private router: Router) { }

  logged_in:boolean;

  ngOnInit(): void {
    this.getMediaByName();

    this.tokenString = localStorage.getItem('authentication-token');
    this.token = JSON.parse(this.tokenString);
    this.user=this.token._user;
    this.logged_in=this.token.isLogged;
  }


  getMediaByName(){
    console.log('get media');
    this.tracksterService.getAllMedia()
      .subscribe(response => {
        // @ts-ignore
        this.allMedia=response;
        this.isDataLoaded = true;
        //this.selectedMedia=this.allMedia.map(item=>item.name);
      });
  }

  searchMedia(text: string){
      this.searchedMedia=this.allMedia.filter((val) =>
      val.name.toLowerCase().includes(text.toLowerCase()));
  }
  redirectToMedia(media: Media) {
    this.id = media.mediaId;
    this.router.navigate(['/media', this.id]);
  }

  rediretToMovies() {
    this.name='movies';
    this.type=0;
    this.router.navigate(['medialist'],  {state: {data:this.name, type:this.type}});
  }

  rediretToShows() {
    this.name = 'shows';
    this.type=0;
    this.router.navigate(['medialist'], {state: {data: this.name, type: this.type}});
  }

  openSignUp() {
    this.router.navigateByUrl("sign-up");
  }
  openLogIn() {
    this.router.navigateByUrl("log-in");
  }

  onMouseEnter(media: any) {
    this.hoveredMedia=media;
  }

  onMouseLeave() {
    this.hoveredMedia=null;
  }


  rediretToUpcomingMovies() {
    this.name='movies';
    this.type=1;
    this.router.navigate(['medialist'],  {state: {data:this.name, type: this.type}});
  }

  rediretToUpcomingShows() {
    this.name = 'shows';
    this.type=1;
    this.router.navigate(['medialist'], {state: {data: this.name, type: this.type}});
  }

  rediretToTopMovies() {
    this.name='movies';
    this.type=2;
    this.router.navigate(['medialist'],  {state: {data:this.name, type: this.type}});
  }

  rediretToTopShows() {
    this.name = 'shows';
    this.type=2;
    this.router.navigate(['medialist'], {state: {data: this.name, type: this.type}});
  }
}

