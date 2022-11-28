﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Trackster.Core;
using Trackster.Core.ViewModels;
using Trackster.Repository;

namespace Trackster.API.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class RegisteredUserController : ControllerBase
    {
        private readonly TracksterContext dbContext;

        public RegisteredUserController(TracksterContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpPost]
        public RegisteredUser Add([FromForm] RegisteredUserAddVM x, IFormFile picture)
        {
            bool userClear = Provjera(x, picture);
            if (userClear)
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
                var newUser = new RegisteredUser
                {
                    Username = x.Username,
                    Email = x.Email,
                    Password = x.Password,
                    ProfilePicture = new Picture(x.Username, fileBytes),
                    Bio = x.Bio,
                };
                dbContext.RegisteredUsers.Add(newUser);
                dbContext.SaveChanges();
                return newUser;
            }
            return null;
        }

        private bool Provjera(RegisteredUserAddVM x, IFormFile picture)
        {
            if (x.Username.IsNullOrEmpty() || x.Username == "string" || x.Email.IsNullOrEmpty() || x.Email == "string" ||
                x.Password.IsNullOrEmpty() || x.Password == "string" || picture == null || x.Bio.IsNullOrEmpty() || x.Bio == "string")
                return false;
            foreach (RegisteredUser user in dbContext.RegisteredUsers)
            {
                if (x.Username.ToLower() == user.Username.ToLower())
                    return false;
            }
            return true;
        }

        [HttpPost("{id}")]
        public ActionResult Update(int id, [FromForm] RegisteredUserAddVM x, IFormFile? picture)
        {
            RegisteredUser user = dbContext.RegisteredUsers.FirstOrDefault(r => r.RegisteredUserId == id);
            if (user == null)
                return BadRequest("Pogresan ID");
            if (x.Username.IsNullOrEmpty() || x.Username == "string" || x.Email.IsNullOrEmpty() || x.Email == "string" ||
                x.Password.IsNullOrEmpty() || x.Password == "string" || x.Bio.IsNullOrEmpty() || x.Bio == "string")
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
                user.ProfilePicture = new Picture(x.Username, fileBytes);
            }
            else
                user.ProfilePicturePictureId = user.ProfilePicturePictureId;

            user.Username = x.Username;
            user.Email = x.Email;
            user.Password = x.Password;
            user.Bio = x.Bio;

            dbContext.SaveChanges();
            return Ok();
        }


        [HttpPost("{Id}")]
        public ActionResult Delete(int Id)
        {
            RegisteredUser user = dbContext.RegisteredUsers.Find(Id);

            if (user == null)
                return BadRequest("Pogresan ID");

            dbContext.RegisteredUsers.Remove(user);
            dbContext.SaveChanges();
            return Ok(user);
        }

        [HttpGet("{Id}")]
        public ActionResult GetById(int Id)
        {
            return Ok(dbContext.RegisteredUsers.Where(r => (r.RegisteredUserId == Id)));
        }

        [HttpGet]
        public List<RegisteredUser> GetAll(int? id, string? username)
        {
            var user = dbContext.RegisteredUsers
                .Where(r => (id == null || id == r.RegisteredUserId) && (username == null || r.Username.ToLower() == username.ToLower()))
                .OrderBy(r => r.RegisteredUserId);
            return user.ToList();
        }
    }
}
