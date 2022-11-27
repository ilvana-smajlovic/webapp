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

        [HttpPost]
        public Status Add([FromBody] BasicUpsertVM x)
        {
            bool StatusClear = Provjera(x);
            if (StatusClear)
            {
                var newStatus = new Status
                {
                    StatusName = x.Name,
                };
                dbContext.Status.Add(newStatus);
                dbContext.SaveChanges();
                return newStatus;
            }
            return null;
        }
        private bool Provjera(BasicUpsertVM x)
        {
            if (x.Name.IsNullOrEmpty() || x.Name == "string")
                return false;
            foreach (Status Status in dbContext.Status)
            {
                if (x.Name.ToLower() == Status.StatusName.ToLower())
                    return false;
            }
            return !string.IsNullOrWhiteSpace(x.Name);
        }
        [HttpPost("{id}")]
        public ActionResult Update(int id, [FromBody] BasicUpsertVM x)
        {
            Status? Status = dbContext.Status.FirstOrDefault(r => r.StatusID == id);

            if (Status == null)
                return BadRequest("Pogresan ID");
            if (!Provjera(x))
                return BadRequest("Loš unos");
            Status.StatusName = x.Name;

            dbContext.SaveChanges();
            return GetById(id);
        }
        [HttpPost("{id}")]
        public ActionResult Delete(int id)
        {
            Status? Status = dbContext.Status.Find(id);

            if (Status == null)
                return BadRequest("Pogresan ID");

            dbContext.Status.Remove(Status);
            dbContext.SaveChanges();
            return Ok(Status);
        }

        [HttpGet("{Id}")]
        public ActionResult GetById(int Id)
        {
            return Ok(dbContext.Status.Where(r => (r.StatusID == Id)));
        }
        [HttpGet]
        public List<Status> GetAll(int? Id, string? Name)
        {
            var status = dbContext.Status
                .Where(r => (Id == null || r.StatusID == Id) && (Name == null || r.StatusName.ToLower() == Name.ToLower()))
                .OrderBy(r => r.StatusID);
            return status.Take(20).ToList();
        }
    }
}
