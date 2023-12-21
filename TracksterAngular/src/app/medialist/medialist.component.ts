import {Component, Input, OnInit, Output} from '@angular/core';
import {ActivatedRoute, NavigationEnd, Route, Router} from "@angular/router";
import {Movie} from "../models/movie";
import {environment} from "../../environments/environment";
import {TracksterService} from "../services/trackster.service";
import {Media} from "../models/media";
import {TvShow} from "../models/tv-show";
import {HomeComponent} from "../home/home.component";
import {Genre} from "../models/genre";
import {GenreMedia} from "../models/genre-media";
import * as constants from "constants";
import {Status} from "../models/status";
import { MatPaginatorModule } from '@angular/material/paginator'

@Component({
  selector: 'app-medialist',
  templateUrl: './medialist.component.html',
  styleUrls: ['./medialist.component.css']
})
export class MedialistComponent implements OnInit {

  media:Media[];
  movies:Movie[]=[];
  _movies:Movie[]=[];
  shows:TvShow[]=[];
  _shows:TvShow[]=[];
  isDataLoaded: boolean=false;
  isMovie: boolean=false;
  isShow:boolean=false;
  _name:string='';
  genres: Genre[];
  genreMedia : GenreMedia[];
  searchText = '';
  searchedMedia: Media[];
  private show: TvShow;
  status:Status[];
  selectedGenre : Genre;
  filteredGenreMedia : GenreMedia[] = [];
  filteredMedia:Media[]=[];
  filteredMovies:Movie[]=[];
  filteredShows:TvShow[]=[];
  pickedGenre: string;
  pickedStatus: string;
  pickedOrder: string;
  id: number;
  hoveredMedia: any = null;
  pageSize=10;
  pageIndex=0;
  type:number;


  constructor(private tracksterService: TracksterService, private route : ActivatedRoute, private router : Router) { }

  ngOnInit(): void {
    this.getMedia();
    this.getGenres();
    this.getGenreMedia();
    this.getStatus();

    this._name = history.state.data;
    this.type=history.state.type;
    if(this.type==null)
      this.type=0;

    this.getShows();
    this.getMovies();
  }

  CheckType(){
    if(this.type==0) {
      if (this._name == 'shows') {
        this.isShow=true;
        this.isMovie=false;
        this.openShows();
      } else {
        this.isShow=false;
        this.isMovie=true;
        this.openMovies();
      }
    }
    else if(this.type==1){
      if (this._name == 'shows') {
        this.isShow=true;
        this.isMovie=false;
        this.openShows();
        this.orderByStatus("Upcoming");
      }
      else {
        this.isShow=false;
        this.isMovie=true;
        this.openMovies();
        this.orderByStatus("Upcoming");
      }
    }
    else if(this.type==2){
      if (this._name == 'shows') {
        this.isShow=true;
        this.isMovie=false;
        this.openShows();
        this.orderByRating();
      }
      else {
        this.isShow=false;
        this.isMovie=true;
        this.openMovies();
        this.orderByRating();
      }
    }
  }


  getMedia(){
    this.tracksterService.getAllMedia()
      .subscribe(response =>{
        // @ts-ignore
        this.media=response;
        this.isDataLoaded=true;
      });
  }
  getMovies(){
    this.tracksterService.getMovie()
      .subscribe(response => {
        // @ts-ignore
        this.movies = response;
        this._movies=[...this.movies];
        this.isDataLoaded = true;
        this.CheckType();
      });
  }
  getShows() {
    this.tracksterService.getTVShows()
      .subscribe(response => {
        // @ts-ignore
        this.shows = response;
        this._shows = [...this.shows];
        this.isDataLoaded = true;
        this.CheckType();
      });
  }
  getGenres(){
    this.tracksterService.getAllGenres()
      .subscribe(response => {
        // @ts-ignore
        this.genres=response;
        this.isDataLoaded = true;
      });
  }
  getGenreMedia(){
    this.tracksterService.getAllGenreMedia()
      .subscribe(response =>{
        // @ts-ignore
        this.genreMedia=response;
        this.isDataLoaded=true;
      });
  }
  getStatus(){
    this.tracksterService.getAllStatus()
      .subscribe((response =>{
        // @ts-ignore
        this.status=response;
        this.isDataLoaded=true;
      }))
  }

  orderByName() {
    this.movies=this.movies.sort((a, b) => a.media.name > b.media.name ? 1 : -1);
    this.shows=this.shows.sort((a, b) => a.media.name > b.media.name ? 1 : -1);
  }

  orderByRating() {
    this.movies = this.movies.sort((a, b) => b.media.rating - a.media.rating);
    this.shows = this.shows.sort((a, b) => b.media.rating - a.media.rating);
  }

  orderBy(value: string) {
    switch (value){
      case "Any":
        break;
      case "Title":
        this.orderByName();
        break;
      case "Rating":
        this.orderByRating();
        break;
    }
  }

  orderByStatus(value: string) {
    switch (value){
      case "Any":
        break;
      case "Upcoming":
        this.movies=this._movies.filter(m=>m.media.status.statusID==1);
        this.shows=this._shows.filter(m=>m.media.status.statusID==1);

        console.log('unutar orderby', this.movies)
        break;
      case "Airing":
        this.shows = this._shows.filter(a => a.media.status.statusID == 2);
        break;
      case "Completed":
        this.movies = this._movies.filter(a => a.media.status.statusID == 3);
        this.shows = this._shows.filter(a => a.media.status.statusID == 3);
        break;
    }
  }

  filterGenre(value:string){
    this.filteredGenreMedia=[];
    this.filteredMedia=[];
    this.filteredMovies=[];
    this.filteredShows=[];
    for (let gm of this.genreMedia) {
      if(gm.genre.genreName == value){
        this.filteredGenreMedia.push(gm);
      }
    }
    for (let fgm of this.filteredGenreMedia) {
      for (let m of this.media){
        if(fgm.media.mediaId==m.mediaId){
          this.filteredMedia.push(m);
        }
      }
    }
    for(let fm of this.filteredMedia) {
      console.log(this.filteredMedia);
      if (this._movies.find(x => x.media.mediaId == fm.mediaId)) {
        this.filteredMovies.push(this._movies.find(x => x.media.mediaId == fm.mediaId) as Movie);
      }
      if (this._shows.find(x => x.media.mediaId == fm.mediaId)) {
        this.filteredShows.push(this._shows.find(x => x.media.mediaId == fm.mediaId) as TvShow);
      }
      this.movies = this.filteredMovies;
      this.shows = this.filteredShows;
    }
  }

  orderByGenre(value: string) {
    if(value=="Any"){
      this.movies=this._movies;
      this.shows=this._shows;
    }
    else{
      this.filterGenre(value);
    }
  }

  openMovies() {
    this.isShow=false;
    this.isMovie=true;
  }

  openShows() {
    this.isShow=true;
    this.isMovie=false;
  }

  searchMedia(text: string) {
    this.searchedMedia=this.media.filter((val) =>
      val.name.toLowerCase().includes(text.toLowerCase()));
  }

  getByName(searched: Media) {
    this.tracksterService.getMediaByName(searched.name)
      .subscribe(response => {
        // @ts-ignore
        this.media=response;
        this.isDataLoaded = true;
      });
  }

  searchFilters() {
    console.log(this.shows);
    this.orderByGenre(this.pickedGenre);
    this.orderByStatus(this.pickedStatus);
    this.orderBy(this.pickedOrder);
    console.log(this.shows);
  }

  redirectToMedia(media: Media) {
   this.id=media.mediaId;
   this.router.navigate(['/media', this.id]);
  }

  onMouseEnter(media: any) {
    this.hoveredMedia=media;
  }

  onMouseLeave() {
    this.hoveredMedia=null;
  }

  onMoviePageChange(event: any) {
    this.pageIndex = event.pageIndex;
    this.getMovies();
  }

  onShowPageChange(event: any) {
    this.pageIndex = event.pageIndex;
    this.getShows();
  }
}
