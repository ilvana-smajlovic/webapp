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
    public class StatusController : ControllerBase
    {
        private readonly TracksterContext dbContext;

        public StatusController(TracksterContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet("{Id}")]
        public ActionResult GetById(int Id)
        {
            return Ok(dbContext.Status.FirstOrDefault(r => (r.StatusID == Id)));
        }
        [HttpGet]
        public List<Status> GetAll()
        {
            var status = dbContext.Status.OrderBy(r => r.StatusID);
            return status.Take(50).ToList();
        }
    }
}