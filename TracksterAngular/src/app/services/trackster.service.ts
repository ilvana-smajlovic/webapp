import { Injectable } from '@angular/core';
import {environment} from "../../environments/environment";
import {HttpClient} from "@angular/common/http";
import {Media} from "../models/media";

@Injectable({
  providedIn: 'root'
})
export class TracksterService {

  constructor(private httpClient: HttpClient) { }

  getMediaById(id: number) {
    // now returns an Observable of Config
    return this.httpClient.get<Media>(environment.apiBaseUrl + 'Media/GetById/'+ id);
  }
}
