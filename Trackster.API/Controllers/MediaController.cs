using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Trackster.Core.ViewModels;
using Trackster.Core;
using Trackster.Repository;
using Microsoft.IdentityModel.Tokens;
using Microsoft.EntityFrameworkCore;

namespace Trackster.API.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class MediaController : ControllerBase
    {
        private readonly TracksterContext dbContext;

        public MediaController(TracksterContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpPost]
        public Media Add([FromForm] MediaAddVM x, IFormFile picture)
        {
            bool mediaClear = Provjera(x, picture);
            if (mediaClear)
            {
                byte[] fileBytes = null;
                if (picture.Length > 0)
                {
                    using (var ms = new MemoryStream())
                    {
                        picture.CopyTo(ms);
                        fileBytes = ms.ToArray();
                    }
                }
                var newMedia = new Media
                {
                    Name = x.Name,
                    AirDate = x.AirDate,
                    Synopsis = x.Synopsis,
                    Rating = x.Rating,
                    Poster = new Picture(x.Name, fileBytes),
                    StatusID = x.Status
                };
                dbContext.Medias.Add(newMedia);
                dbContext.SaveChanges();
                return newMedia;
            }
            return null;
        }

        private bool Provjera(MediaAddVM x, IFormFile picture)
        {
            if (x.Name.IsNullOrEmpty() || x.Name == "string" || x.AirDate == null || x.Synopsis.IsNullOrEmpty() || x.Synopsis == "string"
                || x.Rating == null || picture == null || x.Status == null)
                return false;
            foreach (Media media in dbContext.Medias)
            {
                if (x.Name.ToLower() == media.Name.ToLower())
                    return false;
            }
            return true;
        }

        [HttpPost("{id}")]
        public ActionResult Update(int id, [FromForm] MediaAddVM x, IFormFile? picture)
        {
            Media media = dbContext.Medias.FirstOrDefault(r => r.MediaId == id);
            if (media == null)
                return BadRequest("Pogresan ID");
            if (x.Name.IsNullOrEmpty() || x.Name == "string" || x.AirDate == null || x.Synopsis.IsNullOrEmpty() || x.Synopsis == "string"
                || x.Rating == null || x.Status == null)
                return BadRequest("Loš unos");

            byte[] fileBytes = null;
            if (picture != null)
            {
                if (picture.Length > 0)
                {
                    using (var ms = new MemoryStream())
                    {
                        picture.CopyTo(ms);
                        fileBytes = ms.ToArray();
                    }
                }
                media.Poster = new Picture(x.Name, fileBytes);
            }
            else
                media.PosterPictureId = media.PosterPictureId;

            media.Name = x.Name;
            media.AirDate = x.AirDate;
            media.Synopsis = x.Synopsis;
            media.Rating = x.Rating;
            media.StatusID = x.Status;

            dbContext.SaveChanges();
            return Ok();
        }


        [HttpPost("{Id}")]
        public ActionResult Delete(int Id)
        {
            Media media = dbContext.Medias.Find(Id);

            if (media == null)
                return BadRequest("Pogresan ID");

            dbContext.Medias.Remove(media);
            dbContext.SaveChanges();
            return Ok(media);
        }

        [HttpGet("{Id}")]
        public ActionResult GetById(int Id)
        {
            return Ok(dbContext.Medias.Include(t=>t.Poster).Include(t=>t.Status).Where(r => (r.MediaId == Id)).FirstOrDefault());
        }

        [HttpGet]
        public List<Media> GetAll(int? id, string? name)
        {
            var media = dbContext.Medias.Include(t => t.Poster).Include(t => t.Status)
                .Where(r => (id == null || id == r.MediaId) && (name == null || r.Name.ToLower() == name.ToLower()))
                .OrderBy(r => r.MediaId);
            return media.ToList();
        }
    }
}
