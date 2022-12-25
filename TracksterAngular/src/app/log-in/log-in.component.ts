import { Component, OnInit } from '@angular/core';
import {Router} from "@angular/router";

@Component({
  selector: 'app-log-in',
  templateUrl: './log-in.component.html',
  styleUrls: ['./log-in.component.css']
})
export class LogInComponent implements OnInit {

  constructor(private router: Router) { }

  ngOnInit(): void {
  }

  passwordType1: string = 'password';
  passwordShown: boolean = false;


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
}
