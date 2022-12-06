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
    public class StateController : ControllerBase
    {
        private readonly TracksterContext dbContext;

        public StateController(TracksterContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet("{Id}")]
        public ActionResult GetById(int Id)
        {
            return Ok(dbContext.States.FirstOrDefault(r => (r.StateID == Id)));
        }
        [HttpGet]
        public List<State> GetAll()
        {
            var State = dbContext.States.OrderBy(r => r.StateID);
            return State.Take(50).ToList();
        }
    }
}