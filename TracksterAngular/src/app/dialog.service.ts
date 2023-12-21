import { Injectable } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import {WatchlistFormComponent} from "./watchlist-form/watchlist-form.component";
import { MatDialogModule } from '@angular/material/dialog';

@Injectable({
  providedIn: 'root',
})
export class DialogService {
  constructor(private dialog: MatDialog) {}

  openFormDialog(mediaType:number, media:any): void {
    const dialogRef = this.dialog.open(WatchlistFormComponent, {
      width: '400px',
      data: {mediaType, media},
    });

    dialogRef.afterClosed().subscribe((result) => {
      console.log('The dialog was closed', result);
    });
  }
}
