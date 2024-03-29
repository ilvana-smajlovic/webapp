﻿using Microsoft.AspNetCore.Http;
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
        public Media Add([FromForm] MediaAddVM x, string Picture)
        {
            bool mediaClear = Provjera(x, Picture);
            if (mediaClear)
            {
                var newMedia = new Media
                {
                    Name = x.Name,
                    AirDate = x.AirDate,
                    Synopsis = x.Synopsis,
                    Rating = x.Rating,
                    Picture = Picture,
                    StatusID = x.Status
                };
                dbContext.Medias.Add(newMedia);
                dbContext.SaveChanges();
                return newMedia;
            }
            return null;
        }

        private bool Provjera(MediaAddVM x, string picture)
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
        public ActionResult Update(int id, [FromForm] MediaAddVM x, string? Picture)
        {
            Media media = dbContext.Medias.FirstOrDefault(r => r.MediaId == id);
            if (media == null)
                return BadRequest("Pogresan ID");
            if (x.Name.IsNullOrEmpty() || x.Name == "string" || x.AirDate == null || x.Synopsis.IsNullOrEmpty() || x.Synopsis == "string"
                || x.Rating == null || x.Status == null)
                return BadRequest("Loš unos");

            byte[] fileBytes = null;
            if (Picture != null)
            {
                media.Picture = Picture;
            }
            else
                media.Picture = media.Picture;

            media.Name = x.Name;
            media.AirDate = x.AirDate;
            media.Synopsis = x.Synopsis;
            media.Rating = x.Rating;
            media.StatusID = x.Status;

            dbContext.SaveChanges();
            return Ok();
        }


        [HttpDelete("{Id}")]
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
            return Ok(dbContext.Medias.Include(t=>t.Status).Where(r => (r.MediaId == Id)).FirstOrDefault());
        }

        [HttpGet]
        public List<Media> GetAll(int? id, string? name)
        {
            var media = dbContext.Medias.Include(t => t.Status)
                .Where(r => (id == null || id == r.MediaId) && (name == null || r.Name.ToLower().StartsWith(name.ToLower())))
                .OrderBy(r => r.MediaId);
            return media.ToList();
        }
    }
}
