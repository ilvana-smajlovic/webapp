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
    public class WatchlistTVSeriesController : ControllerBase
    {
        private readonly TracksterContext dbContext;

        public WatchlistTVSeriesController(TracksterContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpPost]
        public WatchlistTVShow Add([FromBody] WatchlistMediaAddVM x)
        {
            if (Provjera(x))
            {
                var newGM = new WatchlistTVShow
                {
                    UserRegisteredUserId = x.UserRegisteredUserId,
                    TVShowID = x.MediaID,
                    StateID = x.StateID,
                    RatingID = x.RatingID
                };
                dbContext.WatchlistTVShows.Add(newGM);
                dbContext.SaveChanges();
                return newGM;
            }
            return null;
        }
        private bool Provjera(WatchlistMediaAddVM x)
        {

            //Onemoguci da se dodaju keys koji nisu u tabeli
            if (dbContext.RegisteredUsers.Find(x.UserRegisteredUserId) == null ||
                dbContext.TVShows.Find(x.MediaID) == null ||
                dbContext.States.Find(x.StateID) == null ||
                dbContext.Ratings.Find(x.RatingID) == null)
                return false;
            foreach (var gm in dbContext.WatchlistTVShows)
            {
                //Onemoguci da vec postojeci se dodaju
                if (x.UserRegisteredUserId == gm.UserRegisteredUserId && x.MediaID == gm.TVShowID &&
                    x.StateID == gm.StateID && x.RatingID == gm.RatingID)
                    return false;
            }
            return true;
        }

        [HttpPost("{Id}")]
        public ActionResult Update(int Id, [FromBody] WatchlistMediaAddVM x)
        {
            WatchlistTVShow? WatchlistTVShow = dbContext.WatchlistTVShows.FirstOrDefault(r => r.TVShowID == Id);

            if (WatchlistTVShow == null)
                return BadRequest("Pogresan ID");
            if (!Provjera(x))
                return BadRequest("Loš unos");

            WatchlistTVShow.UserRegisteredUserId = WatchlistTVShow.UserRegisteredUserId;
            WatchlistTVShow.TVShow.MediaId = WatchlistTVShow.TVShow.MediaId;
            WatchlistTVShow.StateID = x.StateID;
            WatchlistTVShow.RatingID = x.RatingID;

            dbContext.SaveChanges();
            return Ok();
        }

        [HttpDelete("{Id}")]
        public IActionResult Delete(int Id)
        {
            WatchlistTVShow? WatchlistTVShow = dbContext.WatchlistTVShows.FirstOrDefault(r=>r.TVShowID == Id);

            if (WatchlistTVShow == null)
                return BadRequest("Pogresan ID");

            dbContext.WatchlistTVShows.Remove(WatchlistTVShow);
            dbContext.SaveChanges();
            return Ok();
        }

        [HttpGet("{Id}")]
        public ActionResult GetById(int Id)
        {
            WatchlistTVShow? WatchlistTVShow = dbContext.WatchlistTVShows.Find(Id);

            if (WatchlistTVShow == null)
                return BadRequest("Nepostojeci ID");

            return Ok(
                dbContext.WatchlistTVShows
                .Include(s=>s.User)
                .Include(s=>s.TVShow)
                .Include(s=>s.Rating)
                .Include(s=>s.State).Where(r => (r.Id == Id))
                .Select(gm => new WatchlistMediaShowVM()
                {
                    Id = gm.Id,
                    UserRegisteredUserId = gm.UserRegisteredUserId,
                    MediaID = gm.TVShow.MediaId,
                    StateID = gm.StateID,
                    RatingID = gm.RatingID
                }).AsQueryable());
        }

        [HttpGet]
        public List<WatchlistTVShow> GetAll(int? UserID, int? MediaID, int? StateID, int? RatingID)
        {
            var gm = dbContext.WatchlistTVShows
                 .Include(gm => gm.User)
                 .Include(gm => gm.TVShow.Media)
                 .Include(gm => gm.State)
                 .Include(gm => gm.Rating)
                 .Where(gm => (UserID == null || gm.UserRegisteredUserId == UserID)
                 && (MediaID == null || gm.TVShow.MediaId == MediaID)
                 && (StateID == null || gm.StateID == StateID)
                 && (RatingID == null || gm.RatingID == RatingID))
                 .OrderBy(gm => gm.Id);
            return gm.Take(20).ToList();
        }
    }
}
