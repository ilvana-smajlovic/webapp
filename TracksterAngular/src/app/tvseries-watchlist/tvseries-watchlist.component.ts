import { Component, OnInit } from '@angular/core';
import {TracksterService} from "../services/trackster.service";
import {HttpClient} from "@angular/common/http";
import {Router} from "@angular/router";
import {AuthHelper} from "../helper/auth-helper";
import {DialogService} from "../dialog.service";
import {environment} from "../../environments/environment";

@Component({
  selector: 'app-tvseries-watchlist',
  templateUrl: './tvseries-watchlist.component.html',
  styleUrls: ['./tvseries-watchlist.component.css']
})
export class TvseriesWatchlistComponent implements OnInit {
  list1: boolean=true;
  list2:boolean=false;
  list3:boolean=false;
  bgColorP:boolean=true;
  bgColorW:boolean=false;
  bgColorF:boolean=false;
  token : any;
  user:any;
  shows:any[];
  isDataLoaded:boolean=false;

  constructor(private tracksterService: TracksterService, private httpClient: HttpClient, private router: Router,
              private dialogService: DialogService) { }

  ngOnInit(): void {
    this.token = AuthHelper.getLoginInfo();
    this.user=this.token.authenticationToken.registeredUser;
    this.GetWatchlistTVShows();
  }


  showPlanning() {
    this.list1=true;
    this.list2=false;
    this.list3=false;
    this.bgColorP=true;
    this.bgColorW=false;
    this.bgColorF=false;
  }

  showWatching() {
    this.list1=false;
    this.list2=true;
    this.list3=false;
    this.bgColorP=false;
    this.bgColorW=true;
    this.bgColorF=false;
  }

  showFinished() {
    this.list1=false;
    this.list2=false;
    this.list3=true;
    this.bgColorP=false;
    this.bgColorW=false;
    this.bgColorF=true;
  }

  public GetWatchlistTVShows() {
    this.tracksterService.getWatchlistTVShows(this.user.registeredUserId)
      .subscribe(response => {
        // @ts-ignore
        this.shows = response;
        this.isDataLoaded = true;
        console.log(response);
      });
  }

  Edit(show:any) {
    console.log(show);
    this.dialogService.openFormDialog(2, show);
  }

  Delete(show: any) {
    console.log(show);
    this.httpClient.delete(environment.apiBaseUrl + "WatchlistTVSeries/Delete/"+ show.tvShowID).subscribe(x=>{
      console.log('ok');
      location.reload();
    });
  }
}
