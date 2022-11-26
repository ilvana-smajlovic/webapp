using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Trackster.Core.ViewModels;
using Trackster.Core;
using Trackster.Repository;

namespace Trackster.API.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class MediaPersonController : ControllerBase
    {
        private readonly TracksterContext dbContext;

        public MediaPersonController(TracksterContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpPost]
        public MediaPersonRole Add([FromBody] MediaPersonRoleAddVM x)
        {
            if (Provjera(x))
            {
                var newGM = new MediaPersonRole
                {
                    MediaID = x.MediaID,
                    PersonID = x.PersonID,
                    RoleID = x.RoleID,
                    Character = x.Character
                };
                dbContext.MediaPersonRoles.Add(newGM);
                dbContext.SaveChanges();
                return newGM;
            }
            return null;
        }
        private bool Provjera(MediaPersonRoleAddVM x)
        {
            //Onemoguci da se dodaju keys koji nisu u tabeli
            if (dbContext.Medias.Find(x.MediaID) == null ||
                dbContext.People.Find(x.PersonID) == null ||
                dbContext.Roles.Find(x.RoleID) == null)
                return false;
            foreach (var gm in dbContext.MediaPersonRoles)
            {
                //Onemoguci da vec postojeci se dodaju
                if (x.PersonID == gm.PersonID && x.MediaID == gm.MediaID &&
                    x.RoleID == gm.RoleID && x.Character == gm.Character)
                    return false;
            }
            return true;
        }

        [HttpPost("{Id}")]
        public ActionResult Update(int Id, [FromBody] MediaPersonRoleAddVM x)
        {
            MediaPersonRole? MediaPersonRole = dbContext.MediaPersonRoles.FirstOrDefault(r => r.Id == Id);

            if (MediaPersonRole == null)
                return BadRequest("Pogresan ID");
            if (!Provjera(x))
                return BadRequest("Loš unos");

            MediaPersonRole.MediaID = x.MediaID;
            MediaPersonRole.PersonID = x.PersonID;
            MediaPersonRole.RoleID = x.RoleID;
            MediaPersonRole.Character = x.Character;

            dbContext.SaveChanges();
            return GetById(Id);
        }

        [HttpPost("{Id}")]
        public ActionResult Delete(int Id)
        {
            MediaPersonRole? MediaPersonRole = dbContext.MediaPersonRoles.Find(Id);

            if (MediaPersonRole == null)
                return BadRequest("Pogresan ID");

            dbContext.MediaPersonRoles.Remove(MediaPersonRole);
            dbContext.SaveChanges();
            return GetById(Id);
        }

        [HttpGet("{Id}")]
        public ActionResult GetById(int Id)
        {
            MediaPersonRole? MediaPersonRole = dbContext.MediaPersonRoles.Find(Id);

            if (MediaPersonRole == null)
                return BadRequest("Nepostojeci ID");

            return Ok(
                dbContext.MediaPersonRoles.Where(r => (r.Id == Id))
                .Select(gm => new MediaPersonRoleShowVM()
                {
                    Id = gm.Id,
                    MediaID = gm.MediaID,
                    PersonID = gm.PersonID,
                    RoleID = gm.RoleID,
                    Character = gm.Character
                }).AsQueryable());
        }

        [HttpGet]
        public List<MediaPersonRoleShowVM> GetAll(int? PersonID, int? MediaID, int? RoleID, string? Character)
        {
            var gm = dbContext.MediaPersonRoles
                .Where(gm => (PersonID == null || gm.PersonID == PersonID)
                && (MediaID == null || gm.MediaID == MediaID)
                && (RoleID == null || gm.RoleID == RoleID)
                && (Character == null || gm.Character == Character))
                .OrderBy(gm => gm.Id)
                .Select(gm => new MediaPersonRoleShowVM()
                {
                    Id = gm.Id,
                    MediaID = gm.MediaID,
                    PersonID = gm.PersonID,
                    RoleID = gm.RoleID,
                    Character = gm.Character
                }).AsQueryable();
            return gm.Take(20).ToList();
        }
    }
}
