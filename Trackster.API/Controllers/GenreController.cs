using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Trackster.Core.ViewModels;
using Trackster.Core;
using Trackster.Repository;
using Microsoft.IdentityModel.Tokens;
using Microsoft.EntityFrameworkCore;

namespace Trackster.API.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class GenreController : ControllerBase
    {
        private readonly TracksterContext dbContext;

        public GenreController(TracksterContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpPost]
        public Genre Add([FromBody] BasicUpsertVM x)
        {
            bool ZanrClear = Provjera(x);
            if (ZanrClear)
            {
                var newGenre = new Genre
                {
                    GenreName = x.Name,
                };
                dbContext.Genres.Add(newGenre);
                dbContext.SaveChanges();
                return newGenre;
            }
            return null;
        }
        private bool Provjera(BasicUpsertVM x)
        {
            if (x.Name.IsNullOrEmpty() || x.Name == "string")
                return false;
            foreach (Genre Genre in dbContext.Genres)
            {
                if (x.Name.ToLower() == Genre.GenreName.ToLower())
                    return false;
            }
            return !string.IsNullOrWhiteSpace(x.Name);
        }
        [HttpPost("{id}")]
        public ActionResult Update(int id, [FromBody] BasicUpsertVM x)
        {
            Genre? Genre = dbContext.Genres.FirstOrDefault(r => r.GenreID == id);

            if (Genre == null)
                return BadRequest("Pogresan ID");
            if (!Provjera(x))
                return BadRequest("Loš unos");
            Genre.GenreName = x.Name;

            dbContext.SaveChanges();
            return GetById(id);
        }
        [HttpPost("{id}")]
        public ActionResult Delete(int id)
        {
            Genre? Genre = dbContext.Genres.Find(id);

            if (Genre == null)
                return BadRequest("Pogresan ID");

            dbContext.Genres.Remove(Genre);
            dbContext.SaveChanges();
            return Ok(Genre);
        }

        [HttpGet("{Id}")]
        public ActionResult GetById(int Id)
        {
            return Ok(dbContext.Genres.Where(r => (r.GenreID == Id)));
        }
        [HttpGet]
        public List<Genre> GetAll(int? Id, string? Name)
        {
            var zanr = dbContext.Genres
                .Where(r => (Id == null || r.GenreID == Id) && (Name == null || r.GenreName.ToLower() == Name.ToLower()))
                .OrderBy(r => r.GenreID);
            return zanr.Take(20).ToList();
        }
    }
}
