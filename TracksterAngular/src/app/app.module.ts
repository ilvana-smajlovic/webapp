import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppComponent } from './app.component';
import { HomeComponent } from './home/home.component';
import {RouterModule, RouterOutlet} from "@angular/router";
import { ProfileComponent } from './profile/profile.component';
import { MovieWatchlistComponent } from './movie-watchlist/movie-watchlist.component';
import { TvseriesWatchlistComponent } from './tvseries-watchlist/tvseries-watchlist.component';
import { AboutUsComponent } from './about-us/about-us.component';

@NgModule({
  declarations: [
    AppComponent,
    HomeComponent,
    ProfileComponent,
    MovieWatchlistComponent,
    TvseriesWatchlistComponent,
    AboutUsComponent
  ],
  imports: [
    BrowserModule,
    RouterModule.forRoot([
      {path: 'putanja-home', component: HomeComponent},
      {path: 'putanja-profile', component: ProfileComponent},
      {path: 'putanja-movie-watchlist', component: MovieWatchlistComponent},
      {path: 'putanja-tvseries-watchlist', component: TvseriesWatchlistComponent},
      {path: 'putanja-about-us', component: AboutUsComponent},
    ])
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
