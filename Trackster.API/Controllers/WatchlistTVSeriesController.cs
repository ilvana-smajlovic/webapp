using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Trackster.Core.ViewModels;
using Trackster.Core;
using Trackster.Repository;

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
                    UserID = x.UserID,
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
            if (dbContext.RegisteredUsers.Find(x.UserID) == null ||
                dbContext.TVShows.Find(x.MediaID) == null ||
                dbContext.States.Find(x.MediaID) == null ||
                dbContext.Ratings.Find(x.RatingID) == null)
                return false;
            foreach (var gm in dbContext.WatchlistTVShows)
            {
                //Onemoguci da vec postojeci se dodaju
                if (x.UserID == gm.UserID && x.MediaID == gm.TVShowID &&
                    x.StateID == gm.StateID && x.RatingID == gm.RatingID)
                    return false;
            }
            return true;
        }

        [HttpPost("{Id}")]
        public ActionResult Update(int Id, [FromBody] WatchlistMediaAddVM x)
        {
            WatchlistTVShow? WatchlistTVShow = dbContext.WatchlistTVShows.FirstOrDefault(r => r.Id == Id);

            if (WatchlistTVShow == null)
                return BadRequest("Pogresan ID");
            if (!Provjera(x))
                return BadRequest("Loš unos");

            WatchlistTVShow.UserID = x.UserID;
            WatchlistTVShow.TVShowID = x.MediaID;
            WatchlistTVShow.StateID = x.StateID;
            WatchlistTVShow.RatingID = x.RatingID;

            dbContext.SaveChanges();
            return GetById(Id);
        }

        [HttpPost("{Id}")]
        public ActionResult Delete(int Id)
        {
            WatchlistTVShow? WatchlistTVShow = dbContext.WatchlistTVShows.Find(Id);

            if (WatchlistTVShow == null)
                return BadRequest("Pogresan ID");

            dbContext.WatchlistTVShows.Remove(WatchlistTVShow);
            dbContext.SaveChanges();
            return GetById(Id);
        }

        [HttpGet("{Id}")]
        public ActionResult GetById(int Id)
        {
            WatchlistTVShow? WatchlistTVShow = dbContext.WatchlistTVShows.Find(Id);

            if (WatchlistTVShow == null)
                return BadRequest("Nepostojeci ID");

            return Ok(
                dbContext.WatchlistTVShows.Where(r => (r.Id == Id))
                .Select(gm => new WatchlistMediaShowVM()
                {
                    Id = gm.Id,
                    UserID = gm.UserID,
                    MediaID = gm.TVShowID,
                    StateID = gm.StateID,
                    RatingID = gm.RatingID
                }).AsQueryable());
        }

        [HttpGet]
        public List<WatchlistMediaShowVM> GetAll(int? UserID, int? MediaID, int? StateID, int? RatingID)
        {
            var gm = dbContext.WatchlistTVShows
                .Where(gm => (UserID == null || gm.UserID == UserID)
                && (MediaID == null || gm.TVShowID == MediaID)
                && (StateID == null || gm.StateID == StateID)
                && (RatingID == null || gm.RatingID == RatingID))
                .OrderBy(gm => gm.Id)
                .Select(s => new WatchlistMediaShowVM()
                {
                    Id = s.Id,
                    UserID = s.UserID,
                    MediaID = s.TVShowID,
                    StateID = s.StateID,
                    RatingID = s.RatingID
                }).AsQueryable();
            return gm.Take(20).ToList();
        }
    }
}
