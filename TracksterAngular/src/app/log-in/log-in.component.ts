import { Component, OnInit } from '@angular/core';
import {Router} from "@angular/router";
import {HttpClient} from "@angular/common/http";
import {environment} from "../../environments/environment";
import {RegisteredUser} from "../models/registered-user";
import {AuthHelper} from "../helper/auth-helper";
import {LoginInfo} from "../helper/login-info";


@Component({
  selector: 'app-log-in',
  templateUrl: './log-in.component.html',
  styleUrls: ['./log-in.component.css']
})
export class LogInComponent implements OnInit {

  constructor(private httpKlijent: HttpClient, private router: Router) { }

  ngOnInit(): void {
  }

  passwordType1: string = 'password';
  passwordShown: boolean = false;

  Email: string = ''; Password: string = '';
  EmailRegex = new RegExp('^([A-Z]|[a-z]|[0-9])*([._])?([A-Z]|[a-z]|[0-9])+(@gmail.com|@edu.fit.ba)$')

  ToEnable() {
    return this.EmailRegex.test(this.Email) && this.Email !='' && this.Password.length>7;
  }

  LogIn() {
    let LogInInfo = {
      Email:this.Email,
      Password:this.Password
    };
    this.httpKlijent.post<LoginInfo>(environment.apiBaseUrl+ "Authentication/LogInAuth", LogInInfo)
      .subscribe((x:LoginInfo)=> {
        if (x.isLogged) {
          AuthHelper.setLoginInfo(x);
          this.router.navigateByUrl("/home");

        }
        else {
          AuthHelper.setLoginInfo(null);
        }
      });
  }

  private async fetchUserInformation(){
    const fetchInfo = async (): Promise<RegisteredUser> =>{
      return new Promise(resolve => {
        const userInfo = new RegisteredUser();
        this.httpKlijent.get("https://localhost:7242/LogIn/GetLoggedInUser", {
          headers:{
            Authorization:  `Bearer ${sessionStorage.getItem(
              'token'
            )}`,
          },
          observe: 'response',
        })
          .subscribe({
            next: response =>{
              console.log(response.status);
              if(response.status === 200){
                const user = JSON.parse(
                  JSON.stringify(response.body)
                );
                userInfo.registeredUserId=user.registeredUserId;
                userInfo.username=user.username;
                userInfo.email=user.email;
                userInfo.picture=user.picture;
                userInfo.bio=user.bio;
              }
            }
          })
        console.log(userInfo);
      })
    }
  }


  public togglePassword1(){
    if(this.passwordShown){
      this.passwordShown=false;
      this.passwordType1='password';
    }else{
      this.passwordShown=true;
      this.passwordType1='text';
    }
  }

  SignUp() {
    this.router.navigateByUrl("sign-up");
  }
  OpenPassword() {
    this.router.navigateByUrl("Updatepassword/email");
  }
}
