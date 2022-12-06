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
    public class UserFavouritesController : ControllerBase
    {
        private readonly TracksterContext dbContext;

        public UserFavouritesController(TracksterContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpPost]
        public ActionResult Add([FromBody] UserFavouritesAddVM x)
        {
            //Trazi da li postoji ta media
            RegisteredUser? RegisteredUser = dbContext.RegisteredUsers.FirstOrDefault(m => m.RegisteredUserId == x.UserID);
            if (RegisteredUser == null)
                return BadRequest("User doesn't exist");

            //Trazi da li postoji ta media
            Media? Media = dbContext.Medias.FirstOrDefault(m => m.MediaId == x.MediaID);
            if (Media == null)
                return BadRequest("Media doesn't exist");

            if (!Provjera(x))
                return BadRequest("Media already in favourites");

            var newGM = new UserFavourites
            {
                UserID = x.UserID,
                MediaID = x.MediaID,
            };
            dbContext.UserFavourites.Add(newGM);
            dbContext.SaveChanges();
            return Ok("Succesfully added");
        }
        private bool Provjera(UserFavouritesAddVM x)
        {
            foreach (var gm in dbContext.UserFavourites)
            {
                //Onemoguci da vec postojeci se dodaju
                if (x.UserID == gm.UserID && x.MediaID == gm.MediaID)
                    return false;
            }
            return true;
        }

        [HttpPost("{UserId}/{MediaId}")]
        public ActionResult Delete(int UserId, int MediaId)
        {
            var UserMedia = dbContext.UserFavourites
                .Where(gm => gm.UserID == UserId && gm.MediaID == MediaId).Single();

            dbContext.UserFavourites.Remove(UserMedia);
            dbContext.SaveChanges();
            return Ok("Media removed from favourites");
        }

        [HttpGet]
        public List<UserFavourites> GetAll(int? UserID, int? MediaID)
        {
            var gm = dbContext.UserFavourites
                .Include(uf=> uf.User.ProfilePicture)
                .Include(gm => gm.Media)
                .Include(gm => gm.Media.Poster)
                .Include(gm => gm.Media.Status)
                .Where(gm => (UserID == null || gm.UserID == UserID)
                && (MediaID == null || gm.MediaID == MediaID))
                .OrderBy(gm => gm.Id)
                .Select(s => new UserFavourites()
                {
                    Id = s.Id,
                    UserID=s.UserID,
                    MediaID = s.MediaID,
                }).AsQueryable();
            return gm.Take(20).ToList();
        }
    }
}