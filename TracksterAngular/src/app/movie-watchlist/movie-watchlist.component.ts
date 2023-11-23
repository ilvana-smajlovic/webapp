import { Component, OnInit } from '@angular/core';
import {RegisteredUser} from "../models/registered-user";
import {resolve} from "@angular/compiler-cli";
import {HttpClient} from "@angular/common/http";
import {environment} from "../../environments/environment";

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


  constructor(private httpKlijent:HttpClient) { }

  ngOnInit(): void {

  }

  private async fetchUserInformation(){
    const fetchInfo=async ():Promise<RegisteredUser> =>{
      return new Promise(resolve =>{
        const userInfo=new RegisteredUser();
          this.httpKlijent
            .get(environment.apiBaseUrl + 'RegisteredUser/GetById/', {
              headers: {
                Authorization: `Bearer ${sessionStorage.getItem(
                  'token'
                )}`,
              },
                observe: 'response',
              })
            .subscribe({
              next: response =>{
                if (response.status === 200){
                  const user=JSON.parse(
                    JSON.stringify(response.body)
                  );
                }
              }
            })
      })
    }
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
