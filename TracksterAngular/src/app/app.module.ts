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
import {HTTP_INTERCEPTORS, HttpClient, HttpClientModule} from "@angular/common/http";
import { MedialistComponent } from './medialist/medialist.component';
import { SignUpComponent } from './sign-up/sign-up.component';
import { LogInComponent } from './log-in/log-in.component';
import { Updatepassword1Component } from './updatepassword1/updatepassword1.component';
import { ProfilesettingsComponent } from './profilesettings/profilesettings.component';
import { TosComponent } from './tos/tos.component';
import {TranslateModule, TranslateLoader} from "@ngx-translate/core";
import {TranslateHttpLoader} from "@ngx-translate/http-loader";
import {SearchPipe} from "./search.pipe";
import {FormsModule} from "@angular/forms";
import {MatAutocompleteModule} from "@angular/material/autocomplete";
import { WatchlistFormComponent } from './watchlist-form/watchlist-form.component';
import { NoopAnimationsModule } from '@angular/platform-browser/animations';
import {MatDialogModule} from "@angular/material/dialog";
import {MatDialog} from "@angular/material/dialog";
import {HighlightPipe} from "./highlight.pipe";
import {MatPaginatorModule} from "@angular/material/paginator";
import { TwoFAuthComponent } from './two-f-auth/two-f-auth.component';
import { ForgotpasswordComponent } from './forgotpassword/forgotpassword.component';

export function HttpLoaderFactory(http: HttpClient) {
  return new TranslateHttpLoader(http);
}

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
    MedialistComponent,
    SearchPipe,
    HighlightPipe,
    SignUpComponent,
    LogInComponent,
    Updatepassword1Component,
    ProfilesettingsComponent,
    TosComponent,
    WatchlistFormComponent,
    TwoFAuthComponent,
    ForgotpasswordComponent
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
      {path: 'media/:id', component: MediaComponent},
      {path: 'medialist', component: MedialistComponent},
      {path: 'medialist', component: MedialistComponent},
      {path: 'sign-up', component: SignUpComponent},
      {path: 'log-in', component: LogInComponent},
      {path: 'media', component: MediaComponent},
      {path: 'updatepassword', component: Updatepassword1Component},
      {path: 'forgot-password', component: ForgotpasswordComponent},
      {path: 'ProfileSetings', component: ProfilesettingsComponent},
      {path: 'TermsOfService', component: TosComponent},
      {path: 'media/:id', component: MediaComponent},
      {path: 'two-f-auth', component: TwoFAuthComponent}
    ], {scrollPositionRestoration: 'enabled'}),
    HttpClientModule,
    MatPaginatorModule,
    TranslateModule.forRoot({
      loader: {
        provide: TranslateLoader,
        useFactory: HttpLoaderFactory,
        deps: [HttpClient]
      }
    }),
    FormsModule,
    MatAutocompleteModule,
    MatDialogModule,
    NoopAnimationsModule
  ],
  providers: [HttpClient],
  bootstrap: [AppComponent]
})
export class AppModule { }
