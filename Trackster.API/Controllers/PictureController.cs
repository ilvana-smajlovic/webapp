using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Trackster.Core;
using Trackster.Core.ViewModels;
using Trackster.Repository;

namespace Trackster.API.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class PictureController : ControllerBase
    {
        private readonly TracksterContext dbContext;
        private readonly IWebHostEnvironment _environment;
        private readonly object _logger;

        public PictureController(TracksterContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpPost]
        public ActionResult Add([FromForm]string name, IFormFile picture)
        {
            bool pictureClear = Provjera(name, picture);
            if (pictureClear)
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
                var newPicture = new Picture()
                {
                    Name = name,
                    File = fileBytes
                };
                dbContext.Pictures.Add(newPicture);
                dbContext.SaveChanges();
            }
            return Ok();
        }

        private bool Provjera(string name, IFormFile picture)
        {
            if (name.IsNullOrEmpty() || name == "string" || picture.Equals(string.Empty))
                return false;
            foreach (Picture picture1 in dbContext.Pictures)
            {
                if (name.ToLower() == picture1.Name.ToLower())
                    return false;
            }
            return true;
        }

        [HttpPost("{id}")]
        public ActionResult Update(int id, string name, IFormFile picture)
        {
            Picture? picture1=dbContext.Pictures.FirstOrDefault(r=>(r.PictureId== id));

            if (picture1 == null)
                return BadRequest("Pogresan ID");
            if (name.IsNullOrEmpty() || name=="string" || picture1.Name!=name)
                return BadRequest("Loš unos");
            picture1.Name = name;
            byte[] fileBytes = null;
            if (picture.Length > 0)
            {
                using (var ms = new MemoryStream())
                {
                    picture.CopyTo(ms);
                    fileBytes = ms.ToArray();
                }
            }
            picture1.File = fileBytes;
            dbContext.SaveChanges();
            return GetById(id);
        }

        [HttpPost("{Id}")]
        public ActionResult Delete(int Id)
        {
            Picture? picture = dbContext.Pictures.Find(Id);

            if (picture == null)
                return BadRequest("Pogresan ID");

            dbContext.Pictures.Remove(picture);
            dbContext.SaveChanges();
            return Ok(picture);
        }

        [HttpGet("{Id}")]
        public ActionResult GetById(int Id)
        {
            return Ok(dbContext.Pictures.Where(r => (r.PictureId == Id)).FirstOrDefault());
        }

        [HttpGet]
        public List<Picture> GetAll(int? pictureId, string? name)
        {
            var picture = dbContext.Pictures
                .Where(r => (pictureId == null || r.PictureId == pictureId) && (name == null || name.ToLower() == r.Name.ToLower()))
                .OrderBy(r => r.PictureId);
            return picture.Take(20).ToList();
        }

    }
}
