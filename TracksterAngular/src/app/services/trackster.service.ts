import { Injectable } from '@angular/core';
import {environment} from "../../environments/environment";
import {HttpClient} from "@angular/common/http";
import {Media} from "../models/media";
import {Person} from "../models/person";
import {MediaPersonRole} from "../models/media-person-role";
import {Movie} from "../models/movie";
import {TvShow} from "../models/tv-show";
import {Genre} from "../models/genre";
import {GenreMedia} from "../models/genre-media";
import {Status} from "../models/status";
import {UserFavourites} from "../models/user-favourites";
import {WatchlistMovie} from "../models/watchlist-movie";
import {State} from "../models/state";
import {Rating} from "../models/rating";
import {RtlScrollAxisType} from "@angular/cdk/platform";

@Injectable({
  providedIn: 'root'
})
export class TracksterService {

  constructor(private httpClient: HttpClient) { }

  getMediaById(id: number) {
    // now returns an Observable of Config
    return this.httpClient.get<Media>(environment.apiBaseUrl + 'Media/GetById/'+ id);
  }
  getMovieByMediaId(id:number){
    return this.httpClient.get<Movie>(environment.apiBaseUrl + 'Movie/GetByMediaId/'+ id);
  }
  getTVShowByMediaId(id:number){
    return this.httpClient.get<TvShow>(environment.apiBaseUrl + 'TVShow/GetByMediaId/'+ id);
  }
  getAllMedia(){
    return this.httpClient.get<Media>(environment.apiBaseUrl + 'Media/GetAll');
  }
  getMediaByName(name:string){
    return this.httpClient.get<Media>(environment.apiBaseUrl + 'Media/GetAll?name=' + name);
  }
  getPersonById(){
    return this.httpClient.get<Person>(environment.apiBaseUrl + 'Person/GetById/');
  }
  getMediaPersonByMediaId(id : number){
    return this.httpClient.get<MediaPersonRole>(environment.apiBaseUrl+ 'MediaPerson/GetByMediaId?MediaId=' + id);
  }
  getMediaGenreByMediaId(id : number){
    return this.httpClient.get<Media>(environment.apiBaseUrl + 'GenreMedia/GetAll?mediaId=' + id);
  }
  getMovie(){
    return this.httpClient.get<Movie>(environment.apiBaseUrl+'Movie/GetAll');
  }
  getTVShows(){
    return this.httpClient.get<TvShow>(environment.apiBaseUrl+'TVShow/GetAll');
  }
  getAllGenres(){
    return this.httpClient.get<Genre>(environment.apiBaseUrl + 'Genre/GetAll');
  }
  getAllGenreMedia(){
    return this.httpClient.get<GenreMedia>(environment.apiBaseUrl + 'GenreMedia/GetAll');
  }
  getAllStatus() {
    return this.httpClient.get<Status>(environment.apiBaseUrl + 'Status/GetAll')
  }
  getWatchlistMovies(id:number){
    return this.httpClient.get<WatchlistMovie>(environment.apiBaseUrl + "WatchlistMovie/GetAll?UserID=" + id);
  }
  getWatchlistTVShows(id:number){
    return this.httpClient.get<WatchlistMovie>(environment.apiBaseUrl + "WatchlistTVSeries/GetAll?UserID=" + id);
  }
  getAllStates(){
    return this.httpClient.get<State>(environment.apiBaseUrl + "State/GetAll");
  }
  getAllRatings(){
    return this.httpClient.get<Rating>(environment.apiBaseUrl + "Rating/GetAll");
  }
  getAllFavorites(userID:number){
    return this.httpClient.get<UserFavourites>(environment.apiBaseUrl + "UserFavourites/GetAll?UserID="+ userID);
  }
}
