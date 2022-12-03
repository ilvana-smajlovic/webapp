import { Component, OnInit } from '@angular/core';
import {TracksterService} from "../services/trackster.service";
import {Media} from "../models/media";

@Component({
  selector: 'app-media',
  templateUrl: './media.component.html',
  styleUrls: ['./media.component.css']
})
export class MediaComponent implements OnInit {

  id: number;
  selectedMedia: Media;
  isDataLoaded: boolean = false;

  constructor(private tracksterService: TracksterService) {
  }

  ngOnInit(): void {
    this.id=1;
    console.log('testiram log');
    this.getMediaById(this.id);
  }

  getMediaById(id: number) {
    this.tracksterService.getMediaById(id)
      .subscribe(response => {
        this.selectedMedia = response;
        this.isDataLoaded = true;
      });
  }
}
