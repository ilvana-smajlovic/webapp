import { Component, OnInit } from '@angular/core';
import {HttpClient, HttpParams} from "@angular/common/http";
import {Router} from "@angular/router";

@Component({
  selector: 'app-sign-up',
  templateUrl: './sign-up.component.html',
  styleUrls: ['./sign-up.component.css']
})
export class SignUpComponent implements OnInit {

  constructor(private httpKlijent: HttpClient, private router: Router) { }

  ngOnInit(): void {
  }

  show = false;
  passwordType1: string = 'password'; passwordType2: string = 'password';
  passwordShown: boolean = false;
  Email: string = ''; Username: string = ''; Password1: string = ''; Password2: string = '';
  Uslov: number = 0; EmailUslov: number = 0; UsernameUslov: number = 0;
  isChecked: boolean = false;
  EmailRegex = new RegExp('^([A-Z]|[a-z]|[0-9])*([._])?([A-Z]|[a-z]|[0-9])+(@gmail.com|@edu.fit.ba)$')
  UsernameRegex = new RegExp('^([A-Z]|[a-z]|[0-9]|[_])*$');
  PasswordRegex = new RegExp('^([A-Z]|[a-z]|[0-9]|/)*$');

  public togglePassword1(){
    if(this.passwordShown){
      this.passwordShown=false;
      this.passwordType1='password';
    }else{
      this.passwordShown=true;
      this.passwordType1='text';
    }
  }
  public togglePassword2(){
    if(this.passwordShown){
      this.passwordShown=false;
      this.passwordType2='password';
    }else{
      this.passwordShown=true;
      this.passwordType2='text';
    }
  }

  isCheckedFunc() {
    this.isChecked = !this.isChecked;
  }
  ToEnable() {
    return this.EmailTest() && this.UsernameTest() && this.PasswordTest() && this.isChecked;
  }
  EmailTest(){
    return this.EmailRegex.test(this.Email) && this.Email.length>11 && this.EmailUslov==0;
  }
  UsernameTest(){
    return this.UsernameRegex.test(this.Username) && this.Username.length>6 && this.UsernameUslov==0;
  }
  PasswordTest(){
    let test = this.PasswordRegex.test(this.Password1);
    return test && this.Password1.length>7 && this.Password1 == this.Password2;
  }

  CheckEmail(Email: string) {
    let param = new HttpParams().set('Email', this.Email);
    this.httpKlijent.get<number>("https://localhost:7242/SignUp/EmailCheck", {params:param}).subscribe(x=>{
      this.EmailUslov=x;
    });
  }
  CheckUsername(Username: string) {
    let param = new HttpParams().set('Username', this.Username);
    this.httpKlijent.get<number>("https://localhost:7242/SignUp/UsernameCheck", {params:param}).subscribe(x=>{
        this.UsernameUslov=x;
      });
  }
  SignUp() {
    let SignUpInfo = {
      Email:this.Email,
      Username: this.Username,
      Password:this.Password2
    };
    this.httpKlijent.post("https://localhost:7242/SignUp/Add", SignUpInfo, {observe:"response"}).subscribe(
      {
        next:(obj)=>{
          if(obj.status==200){
            this.Uslov=0;
            this.router.navigateByUrl('/two-f-auth');
          }
        }, error :(error)=>{
            if(error.status==400){
              this.Uslov=1;
            }
        }
      });
  }

  LogIn() {
    this.router.navigateByUrl("log-in");
  }

  OpenTOS() {
    this.router.navigateByUrl('TermsOfService')
  }
}
