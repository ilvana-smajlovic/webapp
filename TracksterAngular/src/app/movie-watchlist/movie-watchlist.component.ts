import { Component, OnInit } from '@angular/core';

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


  constructor() { }

  ngOnInit(): void {

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
}
