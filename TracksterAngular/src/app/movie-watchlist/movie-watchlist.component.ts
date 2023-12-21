import { Component, OnInit } from '@angular/core';
import {RegisteredUser} from "../models/registered-user";
import {resolve} from "@angular/compiler-cli";
import {HttpClient} from "@angular/common/http";
import {environment} from "../../environments/environment";
import {Router} from "@angular/router";
import {AuthHelper} from "../helper/auth-helper";
import {WatchlistMovie} from "../models/watchlist-movie";
import {TracksterService} from "../services/trackster.service";
import {DialogService} from "../dialog.service";

@Component({
  selector: 'app-movie-watchlist',
  templateUrl: './movie-watchlist.component.html',
  styleUrls: ['./movie-watchlist.component.css']
})
export class MovieWatchlistComponent implements OnInit {
  list1: boolean=true;
  list2:boolean=false;
  bgColorP:boolean=true;
  bgColorF:boolean=false;
  token : any;
  user:any;
  movies:any[];
  isDataLoaded:boolean=false;

  constructor(private tracksterService: TracksterService, private httpClient: HttpClient, private router: Router,
              private dialogService: DialogService) { }

  ngOnInit(): void {
    this.token = AuthHelper.getLoginInfo();
    this.user=this.token.authenticationToken.registeredUser;
    this.GetWatchlistMovies();
  }

  showPlanning() {
    this.list1=true;
    this.list2=false;
    this.bgColorP=true;
    this.bgColorF=false;
  }

  showFinished() {
    this.list1=false;
    this.list2=true;
    this.bgColorP=false;
    this.bgColorF=true;
  }


  public GetWatchlistMovies() {
    this.tracksterService.getWatchlistMovies(this.user.registeredUserId)
      .subscribe(response => {
        // @ts-ignore
        this.movies = response;
        this.isDataLoaded = true;
      });
  }

  Edit(movie:any) {
    console.log(movie);
    this.dialogService.openFormDialog(1, movie);
  }

  Delete(movie: any) {
    this.httpClient.delete(environment.apiBaseUrl + "WatchlistMovie/Delete/"+ movie.movieID).subscribe(x=>{
      location.reload();
    });
  }
}
