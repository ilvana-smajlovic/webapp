import {Component, ElementRef, OnInit} from '@angular/core';
import {Router} from "@angular/router";

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {

  /*
  @viewchild('BtnToTop') private ButtonToTop! : ElementRef<HTMLButtonElement>//Da je div isao bih HTMLDivElement

  window.onscroll = function() {scrollFunction()};

  scrollFunction() {
    if (document.body.scrollTop > 20 || document.documentElement.scrollTop > 20) {
      this.ButtonToTop.nativeElement.style.display = "block"
    } else {
      this.ButtonToTop.nativeElement.style.display = "none";
    }
  }

// When the user clicks on the button, scroll to the top of the document
  function topFunction() {
    document.body.scrollTop = 0; // For Safari
    document.documentElement.scrollTop = 0; // For Chrome, Firefox, IE and Opera
  }
  */

  constructor(private router: Router) { }

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

  Open1() {
    this.router.navigateByUrl("medialist");
  }
  openSignUp() {
    this.router.navigateByUrl("sign-up");
  }
  openLogIn() {
    this.router.navigateByUrl("log-in");
  }
}
