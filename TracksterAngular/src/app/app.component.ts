import {Component, OnInit} from '@angular/core';
import {TranslateService} from "@ngx-translate/core";
import {AuthHelper} from "./helper/auth-helper";
import {environment} from "../environments/environment";
import {HttpClient} from "@angular/common/http";
import {Router} from "@angular/router";


declare function messageSuccess(a: string):any;
declare function messageWarning(a: string):any;
declare function messageInfo(a: string):any;

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit{
  title = 'Trackster';

  constructor(private httpClient: HttpClient, private router: Router) {
  }


  logged_in=0;

  ngOnInit(): void {
    this.loggedin();
  }

  loggedin(){
    if(this.logged_in==0)
    {
      this.logged_in=1;
    }
    else {
      this.logged_in=0;
    }
  }

  LogOut() {
    AuthHelper.setLoginInfo(null);

    this.httpClient.post(environment.apiBaseUrl + "Authentication/Logout", null).subscribe((x:any)=>{
      this.router.navigateByUrl('/log-in');
      messageSuccess("Logout successful");
    });
  }


  BackToTop() {
    document.documentElement.scrollTop=0;
  }

}
