using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Trackster.Core.ViewModels;
using Trackster.Core;
using Trackster.Repository;
using Microsoft.EntityFrameworkCore;

namespace Trackster.API.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class MovieController : ControllerBase
    {
        private readonly TracksterContext dbContext;

        public MovieController(TracksterContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpPost]
        public Movie Add([FromBody] MovieAddVM x)
        {
            bool movieClear = Provjera(x);
            if (movieClear)
            {
                var newMovie = new Movie
                {
                    MediaId= x.MediaId,
                    Runtime= x.Runtime
                };
                dbContext.Movies.Add(newMovie);
                dbContext.SaveChanges();
                return newMovie;
            }
            return null;
        }

        private bool Provjera(MovieAddVM x)
        {
            if (x.MediaId == null || x.Runtime == null)
                return false;
            foreach (Movie movie in dbContext.Movies)
            {
                if (x.MediaId == movie.MediaId)
                    return false;
            }
            return true;
        }
        [HttpPost("{id}")]
        public ActionResult Update(int id, [FromBody] MovieAddVM x)
        {
            Movie? movie = dbContext.Movies.FirstOrDefault(r => r.MovieID == id);

            if (movie == null)
                return BadRequest("Pogresan ID");
            if (x.MediaId == null || x.Runtime==null)
                return BadRequest("Loš unos");
            movie.MediaId= x.MediaId;
            movie.Runtime= x.Runtime;

            dbContext.SaveChanges();
            return GetById(id);
        }
        [HttpPost("{id}")]
        public ActionResult Delete(int id)
        {
            Movie? movie = dbContext.Movies.Find(id);

            if (movie == null)
                return BadRequest("Pogresan ID");

            dbContext.Movies.Remove(movie);
            dbContext.SaveChanges();
            return Ok(movie);
        }
        [HttpGet("{Id}")]
        public ActionResult GetById(int Id)
        {
            return Ok(dbContext.Movies.Include(t=>t.Media.Poster).Include(t=>t.Media.Status)
                .Where(r => (r.MovieID == Id)).FirstOrDefault());
        }
        [HttpGet]
        public List<Movie> GetAll(int? Id)
        {
            var movie = dbContext.Movies.Include(t=>t.Media.Status).Include(t=>t.Media.Poster)
                .Where(r => (Id == null || r.MovieID == Id))
                .OrderBy(r => r.MovieID);
            return movie.Take(20).ToList();
        }
    }
}
