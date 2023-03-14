import { Component, OnInit } from '@angular/core';
import {TracksterService} from "../services/trackster.service";
import {Media} from "../models/media";
import {Person} from "../models/person";
import {MediaPersonRole} from "../models/media-person-role";
import {environment} from "../../environments/environment";
import {ActivatedRoute, Router} from "@angular/router";
import {GenreMedia} from "../models/genre-media";
import {Movie} from "../models/movie";
import {TvShow} from "../models/tv-show";

@Component({
  selector: 'app-media',
  templateUrl: './media.component.html',
  styleUrls: ['./media.component.css']
})
export class MediaComponent implements OnInit {

  id: number;
  selectedMedia: Media;
  isDataLoaded: boolean = false;
  mediaPerson: MediaPersonRole[];
  genreMedia: GenreMedia[];
  people:any[];
  movie:Movie;
  tvShow:TvShow;

  searchedMedia : Media[];
  searchText= '';
  allMedia : Media[];
  constructor(private tracksterService: TracksterService, private route: ActivatedRoute, private router: Router) {
  }

  ngOnInit(): void {
    this.id = this.route.snapshot.params['id'];
    this.getMediaByName();
    this.getMediaById(this.id);
    this.getMediaPersonByMediaId(this.id);
    this.getGenreByMediaId(this.id);
    this.getMovieByMediaId(this.id);
    this.getTVShowByMediaId(this.id);
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
  getMediaById(id: number) {
    this.tracksterService.getMediaById(id)
      .subscribe(response => {
        this.selectedMedia = response;
        this.isDataLoaded = true;
      })
  }
  getMediaPersonByMediaId(id : number){
     fetch(environment.apiBaseUrl + "MediaPerson/GetAll?MediaId=" + id)
       .then(
         r=> {
           if(r.status !==200){
             alert("greska" + r.status);
             return;
           }
           r.json().then(x=>{
             this.mediaPerson=x;
           });
         }
       )
       .catch(
         err=>{
            alert('Greska' + err);
         }
       )
  }

  getGenreByMediaId(id : number){
    fetch(environment.apiBaseUrl + "GenreMedia/GetAll?mediaId=" + id)
      .then(
        r=> {
          if(r.status !==200){
            alert("greska" + r.status);
            return;
          }
          r.json().then(x=>{
            this.genreMedia=x;
          });
        }
      )
      .catch(
        err=>{
          alert('Greska' + err);
        }
      )
  }

  getMovieByMediaId(id : number){
    this.tracksterService.getMovieByMediaId(id)
      .subscribe(response => {
        this.movie = response;
        this.isDataLoaded = true;
      })
  }
  getTVShowByMediaId(id : number){
    this.tracksterService.getTVShowByMediaId(id)
      .subscribe(response => {
        this.tvShow = response;
        this.isDataLoaded = true;
        console.log(this.tvShow);
      })
  }

  searchMedia(text: string){
    this.searchedMedia=this.allMedia.filter((val) =>
      val.name.toLowerCase().includes(text));
    console.log(this.searchedMedia);
  }

  redirectToMedia(media: Media) {
    this.id=media.mediaId;
    this.router.navigate(['/media', this.id]);
    this.getMediaById(this.id);
    this.searchText='';
  }
}

//ovaj dio ide unutar ngOnInit()
  /*this.id=1;
  console.log('testiram log');
  this.getMediaById(this.id);*/

//ovo je funkcija ispod ngOnInit()
  /*getMediaById(id: number) {
    this.tracksterService.getMediaById(id)
      .subscribe(response => {
        this.selectedMedia = response;
        this.isDataLoaded = true;
      });
  }
*/
