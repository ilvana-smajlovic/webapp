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

        [HttpGet("{Id}")]
        public ActionResult GetById(int Id)
        {
            return Ok(dbContext.Ratings.FirstOrDefault(r => (r.RatingID == Id)));
        }
        [HttpGet]
        public List<Rating> GetAll()
        {
            var rating = dbContext.Ratings.OrderBy(r => r.RatingID);
            return rating.Take(50).ToList();
        }
    }
}