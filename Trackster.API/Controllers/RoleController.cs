using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Trackster.Core.ViewModels;
using Trackster.Core;
using Trackster.Repository;

namespace Trackster.API.Controllers
{
    
    //[Route("api/[controller]")]
    [ApiController]
    [Route("[controller]/[action]")]
    public class RoleController : ControllerBase
    {
        private readonly TracksterContext dbContext;

        public RoleController(TracksterContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpPost]
        public Role Add([FromBody] BasicUpsertVM x)
        {
            bool UlogaClear = Provjera(x);
            if (UlogaClear)
            {
                var newRole = new Role
                {
                    RoleName = x.Name,
                };
                dbContext.Roles.Add(newRole);
                dbContext.SaveChanges();
                return newRole;
            }
            return null;
        }
        private bool Provjera(BasicUpsertVM x)
        {
            if (x.Name.IsNullOrEmpty() || x.Name == "string")
                return false;
            foreach(Role role in dbContext.Roles)
            {
                if (x.Name.ToLower() == role.RoleName.ToLower())
                    return false;
            }
            return !string.IsNullOrWhiteSpace(x.Name);
        }

        [HttpPost("{id}")]
        public ActionResult Update(int id, [FromBody] BasicUpsertVM x)
        {
            Role? Role = dbContext.Roles.FirstOrDefault(r => r.RoleID == id);

            if (Role == null)
                return BadRequest("Pogresan ID");
            if(!Provjera(x))
                return BadRequest("Loš unos");
            Role.RoleName=x.Name;

            dbContext.SaveChanges();
            return GetById(id);
        }
        [HttpPost("{id}")]
        public ActionResult Delete(int id)
        {
            Role? Role = dbContext.Roles.Find(id);

            if (Role == null)
                return BadRequest("Pogresan ID");

            dbContext.Roles.Remove(Role);
            dbContext.SaveChanges();
            return Ok(Role);
        }

        [HttpGet("{Id}")]
        public ActionResult GetById(int Id)
        {
            return Ok(dbContext.Roles.Where(r => (r.RoleID == Id)));
        }
        [HttpGet]
        public List<Role> GetAll(int? Id, string? Name)
        {
            var uloga = dbContext.Roles
                .Where(r => (Id == null || r.RoleID == Id) && (Name == null || r.RoleName.ToLower() == Name.ToLower()))
                .OrderBy(r => r.RoleID);
            return uloga.Take(20).ToList();
        }
    }
}