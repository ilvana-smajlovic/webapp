using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Trackster.Core.ViewModels;
using Trackster.Core;
using Trackster.Repository;
using Microsoft.IdentityModel.Tokens;

namespace Trackster.API.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class RatingController : ControllerBase
    {
        private readonly TracksterContext dbContext;

        public RatingController(TracksterContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpPost]
        public Rating Add([FromBody] BasicUpsertVM x)
        {
            bool RatingClear = Provjera(x);
            if (RatingClear)
            {
                var newRating = new Rating
                {
                    RatingValue = int.Parse(x.Name),
                };
                dbContext.Ratings.Add(newRating);
                dbContext.SaveChanges();
                return newRating;
            }
            return null;
        }
        private bool Provjera(BasicUpsertVM x)
        {
            if (x.Name.IsNullOrEmpty() || x.Name == "string")
                return false;
            if (string.IsNullOrWhiteSpace(x.Name))
                return false;
            foreach (Rating Rating in dbContext.Ratings)
            {
                if (int.Parse(x.Name.ToLower()) == Rating.RatingValue)
                    return false;
            }
            return true;
        }
        [HttpPost("{id}")]
        public ActionResult Update(int id, [FromBody] BasicUpsertVM x)
        {
            Rating? Rating = dbContext.Ratings.FirstOrDefault(r => r.RatingID == id);

            if (Rating == null)
                return BadRequest("Pogresan ID");
            if (!Provjera(x))
                return BadRequest("Loš unos");
            Rating.RatingValue = int.Parse(x.Name);

            dbContext.SaveChanges();
            return GetById(id);
        }
        [HttpPost("{id}")]
        public ActionResult Delete(int id)
        {
            Rating? Rating = dbContext.Ratings.Find(id);

            if (Rating == null)
                return BadRequest("Pogresan ID");

            dbContext.Ratings.Remove(Rating);
            dbContext.SaveChanges();
            return Ok(Rating);
        }

        [HttpGet("{Id}")]
        public ActionResult GetById(int Id)
        {
            return Ok(dbContext.Ratings.Where(r => (r.RatingID == Id)));
        }
        [HttpGet]
        public List<Rating> GetAll(int? Id, string? Name)
        {
            var rating = dbContext.Ratings
                .Where(r => (Id == null || r.RatingID == Id) && (Name == null || r.RatingValue == int.Parse(Name.ToLower())))
                .OrderBy(r => r.RatingID);
            return rating.Take(20).ToList();
        }
    }
}
