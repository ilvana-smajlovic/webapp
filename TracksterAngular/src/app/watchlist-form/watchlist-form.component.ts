import {Component, Inject, OnInit} from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {TracksterService} from "../services/trackster.service";
import { MatDialog } from '@angular/material/dialog';
import { MAT_DIALOG_DATA } from '@angular/material/dialog';
import {ActivatedRoute} from "@angular/router";
import {AuthHelper} from "../helper/auth-helper";
import {environment} from "../../environments/environment";
import {MatDialogModule} from "@angular/material/dialog";
import {coerceStringArray} from "@angular/cdk/coercion";
import {WatchlistMovie} from "../models/watchlist-movie";

declare function messageSuccess(a: string):any;
declare function messageError(a: string):any;

@Component({
  selector: 'app-watchlist-form',
  templateUrl: './watchlist-form.component.html',
  styleUrls: ['./watchlist-form.component.css']
})
export class WatchlistFormComponent implements OnInit {

  constructor(@Inject(MAT_DIALOG_DATA) public data: any, private tracksterService: TracksterService, private httpClient: HttpClient,
              private route: ActivatedRoute) {
    this.mediaType = this.data.mediaType;
    this.media = this.data.media;
  }

  id:any;
  movieStates:any;
  showStates:any;
  ratings:any;
  mediaType:any;
  media:any;
  token:any;
  user:any;
  selectedState: any;
  selectedRating: any;
  getMovies:any[];
  getShows:any[];

  ngOnInit(): void {

    this.token = AuthHelper.getLoginInfo();
    this.user=this.token.authenticationToken.registeredUser;
    this.GetAllStates();
    this.GetAllRatings();
    this.GetWatchlistMovies();
    this.GetWatchlistTVShows();
    console.log('type', this.mediaType);
    console.log('type', this.media);
  }


  GetAllStates(){
    this.tracksterService.getAllStates()
      .subscribe(response => {
        // @ts-ignore
        this.showStates = response;
        this.movieStates = [...this.showStates];

        this.removeState();
      });
  }
  private removeState() {
    this.movieStates.splice(1, 1);
    console.log(this.movieStates);
    console.log(this.showStates);
  }

  GetAllRatings(){
    this.tracksterService.getAllRatings()
      .subscribe(response => {
        // @ts-ignore
        this.ratings = response
      });
  }

  AddToWatchlist() {
    if(this.mediaType == 1){

      if(this.getMovies.some(m=>m.movie.movieID == this.media.movieID)){
        this.UpdateMovieWatchlist();
      }
      else {
        this.addToMovieWatchlist();
      }
    }
    else if(this.mediaType == 2){
      if(this.getShows.some(s=>s.tvShow.tvShowID == this.media.tvShowID))
        this.UpdateShowWatchlist();
      else{
        this.addToShowWatchlist();
      }
    }
    else {
      messageError("Failed to add to watchlist");
    }
  }

  private addToMovieWatchlist() {
    let movie={
      UserRegisteredUserId: this.user.registeredUserId,
      MediaID: this.media.movieID,
      StateID: parseInt(this.selectedState, 10),
      RatingID: parseInt(this.selectedRating, 10)
    }
    console.log(movie);

    this.httpClient.post(environment.apiBaseUrl + 'WatchlistMovie/Add', movie).subscribe((x:any) =>{
        console.log(movie);
      messageSuccess("Successfully added to watchlist");
    });
  }
  private addToShowWatchlist() {
    let show={
      UserRegisteredUserId: this.user.registeredUserId,
      MediaID: this.media.tvShowID,
      StateID: parseInt(this.selectedState, 10),
      RatingID: parseInt(this.selectedRating, 10)
    }
    this.httpClient.post(environment.apiBaseUrl + 'WatchlistTVSeries/Add', show).subscribe((x:any) =>{
      console.log(show);
      messageSuccess("Successfully added to watchlist");
    });
  }

  public GetWatchlistMovies() {
    this.tracksterService.getWatchlistMovies(this.user.registeredUserId)
      .subscribe(response => {
        // @ts-ignore
        this.getMovies = response;
      });
  }

  public GetWatchlistTVShows() {
    this.tracksterService.getWatchlistTVShows(this.user.registeredUserId)
      .subscribe(response => {
        // @ts-ignore
        this.getShows = response;
      });
  }

  private UpdateMovieWatchlist() {
    let movie={
      UserRegisteredUserId: this.user.registeredUserId,
      MediaID: this.media.movieID,
      StateID: parseInt(this.selectedState, 10),
      RatingID: parseInt(this.selectedRating, 10)
    }
    console.log(movie);
    this.httpClient.post(environment.apiBaseUrl + "WatchlistMovie/Update/" + movie.MediaID, movie).subscribe(x=>{
      messageSuccess("Successfully updated to watchlist");
      location.reload();
    });
  }

  private UpdateShowWatchlist() {
    let show={
      UserRegisteredUserId: this.user.registeredUserId,
      MediaID: this.media.tvShowID,
      StateID: parseInt(this.selectedState, 10),
      RatingID: parseInt(this.selectedRating, 10)
    }
    console.log(show);
    this.httpClient.post(environment.apiBaseUrl + "WatchlistTVSeries/Update/" + show.MediaID, show).subscribe(x=>{
      messageSuccess("Successfully updated to watchlist");
      location.reload();
    });
  }
}


