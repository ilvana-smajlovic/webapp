import { Component, OnInit } from '@angular/core';
import {Router} from "@angular/router";
import {HttpClient, HttpHeaders} from "@angular/common/http";
import {environment} from "../../environments/environment";

declare function messageSuccess(a: string):any;
declare function messageError(a: string):any;

@Component({
  selector: 'app-forgotpassword',
  templateUrl: './forgotpassword.component.html',
  styleUrls: ['./forgotpassword.component.css']
})
export class ForgotpasswordComponent implements OnInit {

  token:any;
  email:string;
  constructor(private router: Router, private httpClient: HttpClient) { }

  ngOnInit() {

  }

  SendMail() {
    var emailInput = document.getElementById("emailInput") as HTMLInputElement;
    this.email = JSON.stringify(emailInput.value);
    console.log(this.email);
    if(this.email != null) {
     this.httpClient.post(environment.apiBaseUrl + "Authentication/ForgotPassword", {userEmail: this.email})
       .subscribe(x=>{
         this.router.navigateByUrl("/log-in");
       });
    }
    else {
      messageError("Unable to send, check email again");
    }
  }
}
