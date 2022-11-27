using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Trackster.Core.ViewModels;
using Trackster.Core;
using Trackster.Repository;

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
                    UserID = x.UserID,
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
            if (dbContext.RegisteredUsers.Find(x.UserID) == null ||
                dbContext.Movies.Find(x.MediaID) == null ||
                dbContext.States.Find(x.MediaID) == null ||
                dbContext.Ratings.Find(x.RatingID) == null)
                return false;
            foreach (var gm in dbContext.WatchlistMovies)
            {
                //Onemoguci da vec postojeci se dodaju
                if (x.UserID == gm.UserID && x.MediaID == gm.MovieID &&
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

            WatchlistMovie.UserID = x.UserID;
            WatchlistMovie.MovieID = x.MediaID;
            WatchlistMovie.StateID = x.StateID;
            WatchlistMovie.RatingID = x.RatingID;

            dbContext.SaveChanges();
            return GetById(Id);
        }

        [HttpPost("{Id}")]
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
                dbContext.WatchlistMovies.Where(r => (r.Id == Id))
                .Select(gm => new WatchlistMediaShowVM()
                {
                    Id = gm.Id,
                    UserID = gm.UserID,
                    MediaID = gm.MovieID,
                    StateID = gm.StateID,
                    RatingID = gm.RatingID
                }).AsQueryable());
        }

        [HttpGet]
        public List<WatchlistMediaShowVM> GetAll(int? UserID, int? MediaID, int? StateID, int? RatingID)
        {
            var gm = dbContext.WatchlistMovies
                .Where(gm => (UserID == null || gm.UserID == UserID)
                && (MediaID == null || gm.MovieID == MediaID)
                && (StateID == null || gm.StateID == StateID)
                && (RatingID == null || gm.RatingID == RatingID))
                .OrderBy(gm => gm.Id)
                .Select(s => new WatchlistMediaShowVM()
                {
                    Id = s.Id,
                    UserID = s.UserID,
                    MediaID = s.MovieID,
                    StateID = s.StateID,
                    RatingID = s.RatingID
                }).AsQueryable();
            return gm.Take(20).ToList();
        }
    }
}
