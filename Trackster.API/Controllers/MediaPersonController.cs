using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Trackster.Core.ViewModels;
using Trackster.Core;
using Trackster.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

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

        [HttpPost("{MediaName}/{PersonName}/{RoleName}")]
        public ActionResult Add(string MediaName, string PersonName, string RoleName, string? Character)
        {
            //Trazi da li postoji ta media
            Media? Media = dbContext.Medias.FirstOrDefault(m => m.Name == MediaName);
            if (Media == null)
                return BadRequest("Media doesn't exist");

            //Trazi da li postoji ta osoba
            Person? Person = dbContext.People.FirstOrDefault(p => p.Name == PersonName);
            if (Person == null)
                return BadRequest("Person doesn't exist");

            //Trazi da li postoji taj role 
            Role? Role = dbContext.Roles.FirstOrDefault(r=> r.RoleName == RoleName);
            if (Role == null)
                return BadRequest("Role doesn't exist");

            //Samo VA/Actors mogu imati lika vezanog za njih
            if(Role.RoleName != "Voice actor" || Role.RoleName != "Actor")
            {
                if(!Character.IsNullOrEmpty())
                    return BadRequest("Only actors/voice actors can be characters");
            }

            //Trazi da li postoji ta media u MediaPersonRole table
            MediaPersonRole? MPR = dbContext.MediaPersonRoles.FirstOrDefault(r => r.Media.Name == MediaName);
            //Ako ne postoji moramo je dodati
            if (MPR == null)
            {
                var MPR1 = new MediaPersonRole
                {
                    MediaID = Media.MediaId,
                    PersonID = Person.PersonId,
                    RoleID = Role.RoleID,
                    Character = Character
                };
                dbContext.Add(MPR1);
                dbContext.SaveChanges();
                return Ok(dbContext.GenreMedia.FirstOrDefault(r => r.Media.Name == MediaName));
            }

            //Ako media postoji u GenreMedia table, provjeravamo da li ima vec taj zapis
            if (!Provjera(MPR.MediaID, Person.PersonId, Role.RoleID, Character))
                return BadRequest("This already in database");//Ako ima vraca ovaj string

            //Ako nema dodajemo novi zapis u tabelu
            var MPR2 = new MediaPersonRole
            {
                MediaID= Media.MediaId,
                PersonID= Person.PersonId,
                RoleID= Role.RoleID,
                Character= Character
            };
            dbContext.Add(MPR2);
            dbContext.SaveChanges();
            return Ok(
                dbContext.MediaPersonRoles
                .Include(gm => gm.Person.Picture)
                .Include(gm => gm.Media.Status)
                .Include(gm => gm.Person.Gender)
                .Include(gm=> gm.Role)
                .FirstOrDefault(r => r.Media.Name == MediaName)
                );

        }
        private bool Provjera(int MediaId, int PersonId, int RoleId, string Character)
        {
            foreach (var mpr in dbContext.MediaPersonRoles)
            {
                //Ako ima, vraca false
                if (mpr.MediaID == MediaId && mpr.PersonID==PersonId && mpr.RoleID==RoleId && mpr.Character==Character)
                    return false;
            }
            //Ako nema, vraca true
            return true;
        }

        [HttpGet]
        public ActionResult GetByMediaId(int? MediaId)
        {
            return Ok(dbContext.MediaPersonRoles
                .Include(gm => gm.Media)
                .Include(gm => gm.Media.Status)
                .Include(gm => gm.Person.Gender)
                .Where(gm => (MediaId == null || MediaId == gm.MediaID))
                .Select(s => new MediaPersonRole()
                {
                    Id = s.Id,
                    Media = s.Media,
                    Person = s.Person,
                    Role = s.Role,
                    Character = s.Character
                }).AsQueryable());
        }

        [HttpGet]
        public List<MediaPersonRole> GetAll(int? MediaId, string? MediaName, string? PersonName, string? RoleName, string? Character)
        {
            var gm = dbContext.MediaPersonRoles
                .Include(gm => gm.Media)
                .Include(gm => gm.Media.Status)
                .Include(gm => gm.Person.Gender)
                .Where(gm => (MediaName == null || gm.Media.Name.StartsWith(MediaName))
                && (MediaId==null || MediaId==gm.MediaID)
                && (PersonName == null || gm.Person.Name.StartsWith(PersonName))
                && (RoleName == null || gm.Role.RoleName.StartsWith(RoleName))
                && (Character == null || gm.Character.StartsWith(Character)))
                .OrderBy(gm => gm.Id)
                .Select(s => new MediaPersonRole()
                {
                    Id = s.Id,
                    Media = s.Media,
                    Person = s.Person,
                    Role = s.Role,
                    Character = s.Character
                }).AsQueryable();
            return gm.Take(200).ToList();
        }
    }
}
