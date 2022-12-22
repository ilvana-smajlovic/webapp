import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppComponent } from './app.component';
import { HomeComponent } from './home/home.component';
import {RouterModule, RouterOutlet} from "@angular/router";
import { ProfileComponent } from './profile/profile.component';
import { MovieWatchlistComponent } from './movie-watchlist/movie-watchlist.component';
import { TvseriesWatchlistComponent } from './tvseries-watchlist/tvseries-watchlist.component';
import { AboutUsComponent } from './about-us/about-us.component';
import { NavbarComponent } from './navbar/navbar.component';
import { MediaComponent } from './media/media.component';
import {HttpClientModule} from "@angular/common/http";
import { MedialistComponent } from './medialist/medialist.component';

@NgModule({
  declarations: [
    AppComponent,
    HomeComponent,
    ProfileComponent,
    MovieWatchlistComponent,
    TvseriesWatchlistComponent,
    AboutUsComponent,
    NavbarComponent,
    MediaComponent,
    MedialistComponent
  ],
  imports: [
    BrowserModule,
    RouterModule.forRoot([
      {path: '', redirectTo: 'home', pathMatch: 'full'},
      {path: 'home', component: HomeComponent},
      {path: 'profile', component: ProfileComponent},
      {path: 'movie-watchlist', component: MovieWatchlistComponent},
      {path: 'tvseries-watchlist', component: TvseriesWatchlistComponent},
      {path: 'about-us', component: AboutUsComponent},
    ]),
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
