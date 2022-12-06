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

        [HttpGet("{Id}")]
        public ActionResult GetById(int Id)
        {
            return Ok(dbContext.Genres.FirstOrDefault(r => (r.GenreID == Id)));
        }
        [HttpGet]
        public List<Genre> GetAll()
        {
            var zanr = dbContext.Genres.OrderBy(r => r.GenreID);
            return zanr.Take(50).ToList();
        }
    }
}