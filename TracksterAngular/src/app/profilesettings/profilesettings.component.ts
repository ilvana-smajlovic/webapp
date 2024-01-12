import { Component, OnInit } from '@angular/core';
import {Router} from "@angular/router";
import {deleteOutputDir} from "@angular-devkit/build-angular/src/utils";
import {HttpClient, HttpHeaders} from "@angular/common/http";
import {environment} from "../../environments/environment";
import {AuthHelper} from "../helper/auth-helper";
import {LoginInfo} from "../helper/login-info";

declare function messageSuccess(a:string):any;

@Component({
  selector: 'app-profilesettings',
  templateUrl: './profilesettings.component.html',
  styleUrls: ['./profilesettings.component.css']
})
export class ProfilesettingsComponent implements OnInit {

  constructor(private router: Router, private httpClient : HttpClient) { }

  defaultUrl:string = "https://cdn.pixabay.com/photo/2015/10/05/22/37/blank-profile-picture-973460_1280.png";
  url:string='';
  show = false;
  email:string;
  username:string;
  bio:string;
  token:any;
  user:any;

  ngOnInit(): void {
    this.token = AuthHelper.getLoginInfo();
    this.user=this.token.authenticationToken.registeredUser;
    this.url=this.user.picture;
  }


  onselectFile(e:any){
    if(e.target.files){
      var reader = new FileReader();
      reader.readAsDataURL(e.target.files[0]);
      reader.onload=(event:any)=>{
        this.url=event.target.result;
      }

      /*const image = e.target.files[0];
      const formdata = new FormData();
      formdata.append('picture', image)*/
    }
  }

  Change() {
    this.router.navigateByUrl("updatepassword");
  }

  SaveChanges() {
    this.email=(document.getElementById("email") as HTMLInputElement).value;
    this.username=(document.getElementById("username") as HTMLInputElement).value;
    this.bio = (document.getElementById("bio") as HTMLInputElement).value;

    if(this.url=='')
      this.url=this.user.picture;

    let userInfo={
      Username: this.username,
      Email:this.email,
      Bio:this.bio,
      Picture:this.url
    }

    this.httpClient.post<LoginInfo>(environment.apiBaseUrl + "RegisteredUser/Update/" + this.user.registeredUserId, userInfo, {
      headers: new HttpHeaders({ 'Content-Type': 'application/json;charset=UTF-8'})})
      .subscribe(x=>{
        console.log(x);
        if(x.isLogged){
          AuthHelper.setLoginInfo(x);
          this.router.navigateByUrl("profile");
        }
      });
  }

  Delete() {
    const isConfirmed = window.confirm("Are you sure you want to delete your profile?");

    if (isConfirmed) {
      this.httpClient.delete(environment.apiBaseUrl + "RegisteredUser/Delete/" + this.user.registeredUserId)
        .subscribe(x=>{
          messageSuccess("Profile deleted");
          AuthHelper.setLoginInfo(null);
          this.router.navigateByUrl("home");
        })
    }
  }
}
