using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Trackster.API.Helper;
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
        public UserService _userService { get; set; }

        public RegisteredUserController(TracksterContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpPost]
        public RegisteredUser Add([FromForm] RegisteredUserAddVM x, string Picture)
        {
            bool userClear = Provjera(x, Picture);
            if (userClear)
            {
                var newUser = new RegisteredUser
                {
                    Username = x.Username,
                    Email = x.Email,
                    Password = x.Password,
                    Picture = Picture,
                };
                dbContext.RegisteredUsers.Add(newUser);
                dbContext.SaveChanges();
                return newUser;
            }
            return null;
        }

        private bool Provjera(RegisteredUserAddVM x, string picture)
        {
            if (x.Username.IsNullOrEmpty() || x.Username == "string" || x.Email.IsNullOrEmpty() || x.Email == "string" ||
                x.Password.IsNullOrEmpty() || x.Password == "string" || picture == null)
                return false;
            foreach (RegisteredUser user in dbContext.RegisteredUsers)
            {
                if (x.Username.ToLower() == user.Username.ToLower())
                    return false;
            }
            return true;
        }

        [HttpPost("{id}")]
        public ActionResult Update(int id, [FromForm] RegisteredUserAddVM x, string? Picture)
        {
            RegisteredUser user = dbContext.RegisteredUsers.FirstOrDefault(r => r.RegisteredUserId == id);
            if (user == null)
                return BadRequest("Pogresan ID");
            if (x.Username.IsNullOrEmpty() || x.Username == "string" || x.Email.IsNullOrEmpty() || x.Email == "string" ||
                x.Password.IsNullOrEmpty() || x.Password == "string")
                return BadRequest("Loš unos");

            byte[] fileBytes = null;
            if (Picture != null)
            {
               user.Picture = Picture;
            }
            else
                user.Picture = user.Picture;

            user.Username = x.Username;
            user.Email = x.Email;
            user.Password = x.Password;

            dbContext.SaveChanges();
            return Ok();
        }


        [HttpDelete("{Id}")]
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
            return Ok(dbContext.RegisteredUsers.
                Where(r => (r.RegisteredUserId == Id)).FirstOrDefault());
        }
        [HttpGet]
        public ActionResult<object> GetLoggedInUserName()
        {
            var user = _userService.GetLoggedInUser(HttpContext);

            return user == null ? Accepted() :
                Ok(new
                {
                    username = user.ToString(),
                    userId = user.RegisteredUserId
                });
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
