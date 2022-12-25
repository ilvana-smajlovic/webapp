import {RegisteredUser} from "./registered-user";
import {TvShow} from "./tv-show";
import {State} from "./state";
import {Rating} from "./rating";

export class WatchlistTvShow {
  watchlistTvShow:number;
  user:RegisteredUser;
  tvShow:TvShow;
  state:State;
  rating:Rating;
}
