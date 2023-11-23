using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Trackster.Core.ViewModels;
using Trackster.Core;
using Trackster.Repository;
using Microsoft.EntityFrameworkCore;

namespace Trackster.API.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class WatchlistMovieController : ControllerBase
    {
        private readonly TracksterContext dbContext;

        public WatchlistMovieController(TracksterContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpPost]
        public WatchlistMovie Add([FromBody] WatchlistMediaAddVM x)
        {
            if (Provjera(x))
            {
                var newGM = new WatchlistMovie
                {
                    UserRegisteredUserId = x.UserRegisteredUserId,
                    MovieID = x.MediaID,
                    StateID = x.StateID,
                    RatingID = x.RatingID
                };
                dbContext.WatchlistMovies.Add(newGM);
                dbContext.SaveChanges();
                return newGM;
            }
            return null;
        }
        private bool Provjera(WatchlistMediaAddVM x)
        {
            //Onemoguci da se dodaju keys koji nisu u tabeli
            if (dbContext.RegisteredUsers.Find(x.UserRegisteredUserId) == null ||
                dbContext.Movies.Find(x.MediaID) == null ||
                dbContext.States.Find(x.StateID) == null ||
                dbContext.Ratings.Find(x.RatingID) == null)
                return false;
            foreach (var gm in dbContext.WatchlistMovies)
            {
                //Onemoguci da vec postojeci se dodaju
                if (x.UserRegisteredUserId == gm.UserRegisteredUserId && x.MediaID == gm.MovieID &&
                    x.StateID == gm.StateID && x.RatingID == gm.RatingID)
                    return false;
            }
            return true;
        }

        [HttpPost("{Id}")]
        public ActionResult Update(int Id, [FromBody] WatchlistMediaAddVM x)
        {
            WatchlistMovie? WatchlistMovie = dbContext.WatchlistMovies.FirstOrDefault(r => r.Id == Id);

            if (WatchlistMovie == null)
                return BadRequest("Pogresan ID");
            if (!Provjera(x))
                return BadRequest("Loš unos");

            WatchlistMovie.UserRegisteredUserId = x.UserRegisteredUserId;
            WatchlistMovie.MovieID = x.MediaID;
            WatchlistMovie.StateID = x.StateID;
            WatchlistMovie.RatingID = x.RatingID;

            dbContext.SaveChanges();
            return GetById(Id);
        }

        [HttpDelete("{Id}")]
        public ActionResult Delete(int Id)
        {
            WatchlistMovie? WatchlistMovie = dbContext.WatchlistMovies.Find(Id);

            if (WatchlistMovie == null)
                return BadRequest("Pogresan ID");

            dbContext.WatchlistMovies.Remove(WatchlistMovie);
            dbContext.SaveChanges();
            return GetById(Id);
        }

        [HttpGet("{Id}")]
        public ActionResult GetById(int Id)
        {
            WatchlistMovie? WatchlistMovie = dbContext.WatchlistMovies.Find(Id);

            if (WatchlistMovie == null)
                return BadRequest("Nepostojeci ID");

            return Ok(
                dbContext.WatchlistMovies
                .Include(s => s.User)
                .Include(s => s.Movie)
                .Include(s => s.Rating)
                .Include(s => s.State).Where(r => (r.Id == Id))
                .Select(gm => new WatchlistMediaShowVM()
                {
                    Id = gm.Id,
                    UserRegisteredUserId = gm.UserRegisteredUserId,
                    MediaID = gm.MovieID,
                    StateID = gm.StateID,
                    RatingID = gm.RatingID
                }).AsQueryable());
        }

        [HttpGet]
        public List<WatchlistMovie> GetAll(int? UserID, int? MediaID, int? StateID, int? RatingID)
        {
            var gm = dbContext.WatchlistMovies
                .Include(gm => gm.User)
                .Include(gm => gm.Movie.Media)
                .Include(gm => gm.State)
                .Include(gm => gm.Rating)
                .Where(gm => (UserID == null || gm.UserRegisteredUserId == UserID)
                && (MediaID == null || gm.Movie.MediaId == MediaID)
                && (StateID == null || gm.StateID == StateID)
                && (RatingID == null || gm.RatingID == RatingID))
                .OrderBy(gm => gm.Id);
            return gm.Take(20).ToList();
        }
    }
}
