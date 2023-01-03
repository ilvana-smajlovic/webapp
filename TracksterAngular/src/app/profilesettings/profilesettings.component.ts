import { Component, OnInit } from '@angular/core';
import {Router} from "@angular/router";

@Component({
  selector: 'app-profilesettings',
  templateUrl: './profilesettings.component.html',
  styleUrls: ['./profilesettings.component.css']
})
export class ProfilesettingsComponent implements OnInit {

  constructor(private router: Router) { }

  ngOnInit(): void {
  }

  url:string = "https://cdn.pixabay.com/photo/2015/10/05/22/37/blank-profile-picture-973460_1280.png";
  show = false;


  onselectFile(e:any){
    if(e.target.files){
      var reader = new FileReader();
      reader.readAsDataURL(e.target.files[0]);
      reader.onload=(event:any)=>{
        this.url=event.target.result;
      }

      /*const image = e.target.files[0];
      const formdata = new FormData();
      formdata.append('picture', image)*/
    }
  }

  Change1() {
    this.router.navigateByUrl("Updatepassword/email");
  }
  Change2() {
    this.router.navigateByUrl("Updatepassword/username");
  }
}
