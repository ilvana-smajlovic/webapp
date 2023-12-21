import { Component, OnInit } from '@angular/core';
import {HttpClient, HttpHeaders} from "@angular/common/http";
import {ActivatedRoute, Router} from "@angular/router";
import {environment} from "../../environments/environment";
import {LoginInfo} from "../helper/login-info";
import {AuthHelper} from "../helper/auth-helper";

@Component({
  selector: 'app-two-f-auth',
  templateUrl: './two-f-auth.component.html',
  styleUrls: ['./two-f-auth.component.css']
})
export class TwoFAuthComponent implements OnInit {

  code: string = "";
  token:any;
  constructor(private httpClient: HttpClient, private router: Router, private route: ActivatedRoute) { }

  ngOnInit(): void {
    this.token=AuthHelper.getLoginInfo();
    console.log('u 2f', this.token);
  }

  Unlock() {
    console.log(this.token);
    if(this.token && this.token.authenticationToken){
      this.httpClient.get(environment.apiBaseUrl + "Authentication/Unlock/" + this.code, {
        headers: new HttpHeaders({
          'authentication-token': this.token.authenticationToken.tokenValue
        })
      }).subscribe(
        x=>{
          this.router.navigateByUrl("/home");
        });
    }else {
      console.error("Authentication token is null or undefined");
    }
  }
}
