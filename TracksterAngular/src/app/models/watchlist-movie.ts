import {RegisteredUser} from "./registered-user";
import {Movie} from "./movie";
import {State} from "./state";
import {Rating} from "./rating";

export class WatchlistMovie {
  watchlistMovieId:number;
  user:RegisteredUser;
  movie:Movie;
  state:State;
  rating:Rating;
}
