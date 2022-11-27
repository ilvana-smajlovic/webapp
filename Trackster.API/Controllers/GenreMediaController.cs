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
    public class GenreMediaController : ControllerBase
    {
        private readonly TracksterContext dbContext;

        public GenreMediaController(TracksterContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpPost]
        public GenreMedia Add([FromBody] GenreMediaAddVM x)
        {
            if (Provjera(x))
            {
                var newGM = new GenreMedia
                {
                    GenreID = x.GenreID,
                    MediaID = x.MediaID,
                };
                dbContext.GenreMedia.Add(newGM);
                dbContext.SaveChanges();
                return newGM;
            }
            return null;
        }
        private bool Provjera(GenreMediaAddVM x)
        {
            //Onemoguci da se dodaju keys koji nisu u tabeli
            if (dbContext.Genres.Find(x.GenreID) == null || dbContext.Medias.Find(x.MediaID) == null)
                return false;
            foreach (var gm in dbContext.GenreMedia)
            {
                //Onemoguci da vec postojeci se dodaju
                if (x.GenreID == gm.GenreID && x.MediaID == gm.MediaID)
                    return false;
            }
            return true;
        }

        [HttpPost("{Id}")]
        public ActionResult Update(int Id, [FromBody] GenreMediaAddVM x)
        {
            GenreMedia? GenreMedia = dbContext.GenreMedia.FirstOrDefault(r => r.Id == Id);

            if (GenreMedia == null)
                return BadRequest("Pogresan ID");
            if (!Provjera(x))
                return BadRequest("Loš unos");

            GenreMedia.GenreID = x.GenreID;
            GenreMedia.MediaID = x.MediaID;

            dbContext.SaveChanges();
            return GetById(Id);
        }
        [HttpPost("{Id}")]
        public ActionResult Delete(int Id)
        {
            GenreMedia? GenreMedia = dbContext.GenreMedia.Find(Id);

            if (GenreMedia == null)
                return BadRequest("Pogresan ID");

            dbContext.GenreMedia.Remove(GenreMedia);
            dbContext.SaveChanges();
            return Ok(GenreMedia);
        }

        [HttpGet("{Id}")]
        public ActionResult GetById(int Id)
        {
            GenreMedia? GenreMedia = dbContext.GenreMedia.Find(Id);

            if (GenreMedia == null)
                return BadRequest("Nepostojeci ID");

            return Ok(
                dbContext.GenreMedia.Where(r => (r.Id == Id))
                .Select(gm => new GenreMediaShowVM()
                {
                    Id = gm.Id,
                    GenreID = gm.GenreID,
                    MediaID = gm.MediaID,
                }).AsQueryable());
        }

        [HttpGet]
        public List<GenreMediaShowVM> GetAll(int? GenreID, int? MediaID)
        {
            var gm = dbContext.GenreMedia
                .Where(gm => (GenreID == null || gm.GenreID == GenreID)
                && (MediaID == null || gm.MediaID == MediaID))
                .OrderBy(gm => gm.Id)
                .Select(s => new GenreMediaShowVM()
                {
                    Id = s.Id,
                    GenreID = s.GenreID,
                    MediaID = s.MediaID
                }).AsQueryable();
            return gm.Take(20).ToList();
        }
    }
}