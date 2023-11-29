import { Component, OnInit } from '@angular/core';
import {Router} from "@angular/router";
import {HttpClient} from "@angular/common/http";
import {environment} from "../../environments/environment";
import {RegisteredUser} from "../models/registered-user";
import {AuthHelper} from "../helper/auth-helper";
import {LoginInfo} from "../helper/login-info";
import {catchError, throwError} from "rxjs";

declare function messageSuccess(a: string):any;
declare function messageError(a: string):any;


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
        .pipe(
          catchError((error) =>{
            messageError('Error during login');
            console.log('Error during login', error);
            return throwError(error);
          })
        )
        .subscribe((x:LoginInfo)=> {
          if (x.isLogged) {
            AuthHelper.setLoginInfo(x);
            this.router.navigateByUrl("/home");
            messageSuccess("Login successful");
          }
          else {
            AuthHelper.setLoginInfo(null);
          }
        });
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
