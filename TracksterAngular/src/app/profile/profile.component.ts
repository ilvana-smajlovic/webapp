import { Component, OnInit } from '@angular/core';
import {Router} from "@angular/router";
import {AuthHelper} from "../helper/auth-helper";
import {HttpClient} from "@angular/common/http";
import {environment} from "../../environments/environment";
import {RegisteredUser} from "../models/registered-user";
import {LoginInfo} from "../helper/login-info";
import {UserFavourites} from "../models/user-favourites";
import "../../assets/popupmessage.js";

declare function messageSuccess(a: string):any;
declare function messageError(a:string):any;


@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.css'],
})

export class ProfileComponent implements OnInit {

  token : any;
  user:any;
  userFavourites:any;

  constructor(private httpClient: HttpClient, private router: Router) { }

  ngOnInit() {
    this.token = AuthHelper.getLoginInfo();
    this.user=this.token.authenticationToken.registeredUser;
    this.getFavourites();
  }

  getFavourites(){
    this.httpClient.get<UserFavourites>(environment.apiBaseUrl + "UserFavourites/GetAll?UserID=" + this.user.registeredUserId)
      .subscribe(response=>{
      this.userFavourites=response;
    });
  }

  OpenMedia(f:any) {
    console.log(f);
    this.router.navigate(['/media', f.mediaId]);
  }

  OpenSettings() {
    this.router.navigateByUrl("ProfileSetings");
  }

  LogOut() {
    AuthHelper.setLoginInfo(null);

    this.httpClient.post(environment.apiBaseUrl + "Authentication/Logout", null).subscribe((x:any)=>{
      this.router.navigateByUrl('/log-in');
      messageSuccess("Logout successful");
    });
  }
}
