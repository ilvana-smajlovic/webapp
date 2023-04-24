import { Component, OnInit } from '@angular/core';
import {Router} from "@angular/router";
import {HttpClient} from "@angular/common/http";

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
    this.httpKlijent.post("https://localhost:7242/LogIn/LogInAuth", LogInInfo, {observe:'response'}).subscribe(
      {
        next:response =>{
          if(response.status===200){
            sessionStorage.setItem('token', JSON.parse(JSON.stringify(response.body))['value']);
          }
        }, error :(error)=>{
          if(error.status==400){
          }
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
