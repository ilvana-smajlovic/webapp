import { Injectable } from '@angular/core';
import {environment} from "../../environments/environment";
import {HttpClient} from "@angular/common/http";
import {Media} from "../models/media";
import {Person} from "../models/person";
import {MediaPersonRole} from "../models/media-person-role";
import {Movie} from "../models/movie";
import {TvShow} from "../models/tv-show";

@Injectable({
  providedIn: 'root'
})
export class TracksterService {

  constructor(private httpClient: HttpClient) { }

  getMediaById(id: number) {
    // now returns an Observable of Config
    return this.httpClient.get<Media>(environment.apiBaseUrl + 'Media/GetById/'+ id);
  }
  getMediaByName(){
    return this.httpClient.get<Media>(environment.apiBaseUrl + 'Media/GetAll');
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
  getPersonById(){
    return this.httpClient.get<Person>(environment.apiBaseUrl + 'Person/GetById/');
  }
  getMediaPersonByMediaId(id : number){
    return this.httpClient.get<MediaPersonRole>(environment.apiBaseUrl+ 'MediaPerson/GetByMediaId?MediaId=' + id);
  }
  getMediaGenreByMediaId(id : number){
    return this.httpClient.get<Media>(environment.apiBaseUrl + 'GenreMedia/GetAll?mediaId=' + id);
  }
}
