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

        [HttpPost]
        public State Add([FromBody] BasicUpsertVM x)
        {
            bool StateClear = Provjera(x);
            if (StateClear)
            {
                var newState = new State
                {
                    StateName = x.Name,
                };
                dbContext.States.Add(newState);
                dbContext.SaveChanges();
                return newState;
            }
            return null;
        }
        private bool Provjera(BasicUpsertVM x)
        {
            if (x.Name.IsNullOrEmpty() || x.Name == "string")
                return false;
            foreach (State State in dbContext.States)
            {
                if (x.Name.ToLower() == State.StateName.ToLower())
                    return false;
            }
            return !string.IsNullOrWhiteSpace(x.Name);
        }
        [HttpPost("{id}")]
        public ActionResult Update(int id, [FromBody] BasicUpsertVM x)
        {
            State? State = dbContext.States.FirstOrDefault(r => r.StateID == id);

            if (State == null)
                return BadRequest("Pogresan ID");
            if (!Provjera(x))
                return BadRequest("Loš unos");
            State.StateName = x.Name;

            dbContext.SaveChanges();
            return GetById(id);
        }
        [HttpPost("{id}")]
        public ActionResult Delete(int id)
        {
            State? State = dbContext.States.Find(id);

            if (State == null)
                return BadRequest("Pogresan ID");

            dbContext.States.Remove(State);
            dbContext.SaveChanges();
            return Ok(State);
        }

        [HttpGet("{Id}")]
        public ActionResult GetById(int Id)
        {
            return Ok(dbContext.States.Where(r => (r.StateID == Id)));
        }
        [HttpGet]
        public List<State> GetAll(int? Id, string? Name)
        {
            var State = dbContext.States
                .Where(r => (Id == null || r.StateID == Id) && (Name == null || r.StateName.ToLower() == Name.ToLower()))
                .OrderBy(r => r.StateID);
            return State.Take(20).ToList();
        }
    }
}
