import { Component, OnInit } from '@angular/core';

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

  constructor() { }

  ngOnInit(): void {
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

}
