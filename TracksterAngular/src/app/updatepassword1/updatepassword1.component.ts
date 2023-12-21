import { Component, OnInit } from '@angular/core';
import {Router} from "@angular/router";
import {HttpClient} from "@angular/common/http";
import {environment} from "../../environments/environment";

declare function messageSuccess(a: string):any;
declare function messageError(a: string):any;


@Component({
  selector: 'app-updatepassword1',
  templateUrl: './updatepassword1.component.html',
  styleUrls: ['./updatepassword1.component.css']
})

export class Updatepassword1Component {

  constructor(private router: Router, private httpClient: HttpClient) { }

  password:string;
  email:string;
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


  public CheckInput(){
    var emailInput = document.getElementById("email") as HTMLInputElement;
    this.email = emailInput.value;

    var pass1Input=document.getElementById("pass1") as HTMLInputElement;
    var pass1=pass1Input.value;

    var pass2Input=document.getElementById("pass2") as HTMLInputElement;
    var pass2=pass2Input.value;

    if(pass1===pass2){
      this.password=pass1;
    }
  }

  public Change() {

    this.CheckInput();
    let user = {
      Email:this.email,
      Password:this.password
    };

    console.log(this.email);
    console.log(this.password);

    if(user!=null){
      this.httpClient.post(environment.apiBaseUrl + "Authentication/UpdatePassword", user).subscribe(x=>{
        this.router.navigateByUrl("log-in");
        messageSuccess("Password successfully changed");
      })
    }
    else{
      messageError("Unable to change password");
    }
  }
}
