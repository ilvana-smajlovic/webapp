import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {

  constructor() { }

  logged_in=0;

  ngOnInit(): void {
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

  BackToTop() {
    document.documentElement.scrollTop=0;
  }
}
