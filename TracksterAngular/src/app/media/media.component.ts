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
import {HttpClient} from "@angular/common/http";
import {UserFavourites} from "../models/user-favourites";
import {RegisteredUser} from "../models/registered-user";
import {DialogService} from "../dialog.service";
import {MatDialog} from "@angular/material/dialog";
import {MatDialogModule} from "@angular/material/dialog";

declare function messageError(a: string):any;
declare function messageSuccess(a: string):any;


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
  addMedia:any;

  searchedMedia : Media[];
  searchText= '';
  allMedia : Media[];
  user:any;
  token:any;
  tokenString:any;
  registeredUserId:any;
  constructor(private tracksterService: TracksterService, private httpClient: HttpClient, private route: ActivatedRoute, private router: Router,
              private dialogService: DialogService) {
  }

  ngOnInit(): void {
    this.id = this.route.snapshot.params['id'];
    this.getMediaByName();
    this.getMediaById(this.id);
    this.getMediaPersonByMediaId(this.id);
    this.getGenreByMediaId(this.id);
    this.getMovieByMediaId(this.id);
    this.getTVShowByMediaId(this.id);


    this.tokenString = localStorage.getItem('authentication-token');
    const token = JSON.parse(this.tokenString);
    this.registeredUserId = token._user.registeredUserId;


  }
  getMediaByName(){
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
       );
     console.log(id);
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
      });
  }
  getTVShowByMediaId(id : number){
    this.tracksterService.getTVShowByMediaId(id)
      .subscribe(response => {
        this.tvShow = response;
        this.isDataLoaded = true;
      });
  }

  searchMedia(text: string){
    this.searchedMedia=this.allMedia.filter((val) =>
      val.name.toLowerCase().includes(text));
  }

  redirectToMedia(media: Media) {
    this.id=media.mediaId;
    this.router.navigate(['/media', this.id]);
    this.getMediaById(this.id);
    this.searchText='';
  }

  AddToFavourites(selectedMedia: Media) {
    let userFavourite={
      MediaID:selectedMedia.mediaId,
      UserID:this.registeredUserId
    }
    console.log(userFavourite);
    this.httpClient.post(environment.apiBaseUrl + 'UserFavourites/Add', userFavourite).subscribe((x:any) =>{
      messageSuccess("Logout successful");

    });
  }

  AddToWatchlist() {
    if(this.movie != null){
      this.dialogService.openFormDialog(1, this.movie);
    }
    else if(this.tvShow != null){
      this.dialogService.openFormDialog(2, this.tvShow);
    }
    else{
      messageError("Failed to add to watchlist!");
    }
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
