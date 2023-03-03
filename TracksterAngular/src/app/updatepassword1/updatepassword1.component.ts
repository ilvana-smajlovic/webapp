import { Component, OnInit } from '@angular/core';
import {Router} from "@angular/router";

@Component({
  selector: 'app-updatepassword1',
  templateUrl: './updatepassword1.component.html',
  styleUrls: ['./updatepassword1.component.css']
})
export class Updatepassword1Component {

  constructor(private router: Router) { }

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

  EditPassword() {
    this.router.navigateByUrl("Updatepassword/username");
  }

}
