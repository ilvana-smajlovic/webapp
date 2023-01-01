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
    public class GenreMediaController : ControllerBase
    {
        private readonly TracksterContext dbContext;

        public GenreMediaController(TracksterContext dbContext)
        {
            this.dbContext = dbContext;
        }


        [HttpPost("{MediaName}")]
        public ActionResult Add(string MediaName, [FromBody] GenreMediaAddVM x)
        {
            //Trazi da li postoji ta media uopste
            Media? Media = dbContext.Medias.FirstOrDefault(m => m.Name == MediaName);
            if (Media == null)
                return BadRequest("Media doesn't exist");//Ako ne postoji, vraca ovaj string

            //Trazi da li postoji taj genre uopste
            Genre? Genre = dbContext.Genres.FirstOrDefault(g => g.GenreName == x.GenreName);
            if (Genre==null)
                return BadRequest("Genre doesn't exist");//Ako ne postoji, vraca ovaj string

            //Trazi da li postoji ta media u GenreMedia table
            GenreMedia? GenreMedia = dbContext.GenreMedia.FirstOrDefault(r => r.Media.Name == MediaName);
            //Ako ne postoji moramo je dodati zajedno sa zanrom
            if (GenreMedia==null)
            {
                var GM1 = new GenreMedia
                {
                    MediaID = Media.MediaId,
                    GenreID = Genre.GenreID
                };
                dbContext.Add(GM1);
                dbContext.SaveChanges();
                return Ok(dbContext.GenreMedia.FirstOrDefault(r => r.Media.Name == MediaName));
            }

            //Ako media postoji u GenreMedia table, provjeravamo da li ima vec taj Genre
            if (!Provjera(GenreMedia.MediaID, Genre.GenreID))
                return BadRequest("Genre is already in media");//Ako ima vraca ovaj string

            //Ako nema dodajemo novi Genre u mediju
            var GM2 = new GenreMedia
            {
                MediaID = GenreMedia.MediaID,
                GenreID = Genre.GenreID
            };
            dbContext.Add(GM2);
            dbContext.SaveChanges();
            return Ok(
                dbContext.GenreMedia
                .Include(gm=>gm.Media.Status)
                .Include(gm=>gm.Genre)
                .FirstOrDefault(r => r.Media.Name == MediaName)
                );
        }
        private bool Provjera(int MediaId, int GenreId)
        {
            foreach (var gm in dbContext.GenreMedia)
            {
                //Ako ima, vraca false
                if (gm.MediaID == MediaId && gm.GenreID == GenreId)
                    return false;
            }
            //Ako nema, vraca true
            return true;
        }
       
    [HttpGet]
        public List<GenreMedia> GetAll(int? mediaId, string? MediaName, string? GenreName)
        {
            var gm = dbContext.GenreMedia
                .Include(gm => gm.Media)
                .Include(gm => gm.Media.Status)
                .Include(gm => gm.Genre)
                .Where(gm => (mediaId==null || mediaId==gm.MediaID) 
                && (GenreName == null || gm.Genre.GenreName.ToLower().StartsWith(GenreName))
                && (MediaName == null || gm.Media.Name.ToLower().StartsWith(MediaName)))
                .OrderBy(gm => gm.Id);
            return gm.Take(200).ToList();
        }
    }
}