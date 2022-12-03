using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Trackster.Core;
using Trackster.Core.ViewModels;
using Trackster.Repository;

namespace Trackster.API.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class TVShowController : ControllerBase
    {
        private readonly TracksterContext dbContext;

        public TVShowController(TracksterContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpPost]
        public TVShow Add([FromBody] TVShowAddVM x)
        {
            bool showClear = Provjera(x);
            if (showClear)
            {
                var newShow = new TVShow
                {
                    MediaId = x.MediaID,
                    SeasonCount = x.SeasonCount,
                    EpisodeCount = x.EpisodeCount,
                    EpisodeRuntime = x.EpisodeRuntime,
                };
                dbContext.TVShows.Add(newShow);
                dbContext.SaveChanges();
                return newShow;
            }
            return null;
        }

        private bool Provjera(TVShowAddVM x)
        {
            if (x.MediaID == null || x.SeasonCount == null || x.EpisodeCount == null || x.EpisodeRuntime == null)
                return false;
            foreach (TVShow show in dbContext.TVShows)
            {
                if (x.MediaID == show.MediaId)
                    return false;
            }
            return true;
        }
        [HttpPost("{id}")]
        public ActionResult Update(int id, [FromBody] TVShowAddVM x)
        {
            TVShow? show = dbContext.TVShows.FirstOrDefault(r => r.TVShowID == id);

            if (show == null)
                return BadRequest("Pogresan ID");
            if (x.MediaID == null || x.SeasonCount == null || x.EpisodeCount == null || x.EpisodeRuntime == null)
                return BadRequest("Loš unos");
            show.MediaId = x.MediaID;
            show.SeasonCount = x.SeasonCount;
            show.EpisodeCount = x.EpisodeCount;
            show.EpisodeRuntime = x.EpisodeRuntime;

            dbContext.SaveChanges();
            return GetById(id);
        }
        [HttpPost("{id}")]
        public ActionResult Delete(int id)
        {
            TVShow? show = dbContext.TVShows.Find(id);

            if (show == null)
                return BadRequest("Pogresan ID");

            dbContext.TVShows.Remove(show);
            dbContext.SaveChanges();
            return Ok(show);
        }
        [HttpGet("{Id}")]
        public ActionResult GetById(int Id)
        {
            return Ok(dbContext.TVShows
                .Include(t => t.Media.Status)
                .Include(t => t.Media.Poster)
                .Where(r => (r.TVShowID == Id)));
        }
        [HttpGet]
        public List<TVShow> GetAll(int? Id)
        {
            var show = dbContext.TVShows
                .Include(t=> t.Media.Status)
                .Include(t=>t.Media.Poster)
                .Where(r => (Id == null || r.TVShowID == Id))
                .OrderBy(r => r.TVShowID);
            return show.Take(20).ToList();
        }
    }
}
