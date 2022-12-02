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
    public class UserFavouritesController : ControllerBase
    {
        private readonly TracksterContext dbContext;

        public UserFavouritesController(TracksterContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpPost]
        public UserFavourites Add([FromBody] UserFavouritesAddVM x)
        {
            if (Provjera(x))
            {
                var newGM = new UserFavourites
                {
                    UserID = x.UserID,
                    MediaID = x.MediaID,
                };
                dbContext.UserFavourites.Add(newGM);
                dbContext.SaveChanges();
                return newGM;
            }
            return null;
        }
        private bool Provjera(UserFavouritesAddVM x)
        {
            //Onemoguci da se dodaju keys koji nisu u tabeli
            if (dbContext.Genres.Find(x.UserID) == null ||
                dbContext.Medias.Find(x.MediaID) == null)
                return false;
            foreach (var gm in dbContext.UserFavourites)
            {
                //Onemoguci da vec postojeci se dodaju
                if (x.UserID == gm.UserID && x.MediaID == gm.MediaID)
                    return false;
            }
            return true;
        }

        [HttpPost("{Id}")]
        public ActionResult Update(int Id, [FromBody] UserFavouritesAddVM x)
        {
            UserFavourites? UserFavourites = dbContext.UserFavourites.FirstOrDefault(r => r.Id == Id);

            if (UserFavourites == null)
                return BadRequest("Pogresan ID");
            if (!Provjera(x))
                return BadRequest("Loš unos");

            UserFavourites.UserID = x.UserID;
            UserFavourites.MediaID = x.MediaID;

            dbContext.SaveChanges();
            return GetById(Id);
        }
        [HttpPost("{Id}")]
        public ActionResult Delete(int Id)
        {
            UserFavourites? UserFavourites = dbContext.UserFavourites.Find(Id);

            if (UserFavourites == null)
                return BadRequest("Pogresan ID");

            dbContext.UserFavourites.Remove(UserFavourites);
            dbContext.SaveChanges();
            return GetById(Id);
        }

        [HttpGet("{Id}")]
        public ActionResult GetById(int Id)
        {
            UserFavourites? UserFavourites = dbContext.UserFavourites.Find(Id);

            if (UserFavourites == null)
                return BadRequest("Nepostojeci ID");

            return Ok(
                dbContext.UserFavourites.Where(r => (r.Id == Id))
                .Select(gm => new UserFavouritesShowVM()
                {
                    Id = gm.Id,
                    UserID = gm.UserID,
                    MediaID = gm.MediaID,
                }).AsQueryable());
        }

        [HttpGet]
        public List<UserFavouritesShowVM> GetAll(int? UserID, int? MediaID)
        {
            var gm = dbContext.UserFavourites
                .Where(gm => (UserID == null || gm.UserID == UserID)
                && (MediaID == null || gm.MediaID == MediaID))
                .OrderBy(gm => gm.Id)
                .Select(s => new UserFavouritesShowVM()
                {
                    Id = s.Id,
                    UserID = s.UserID,
                    MediaID = s.MediaID
                }).AsQueryable();
            return gm.Take(20).ToList();
        }
    }
}