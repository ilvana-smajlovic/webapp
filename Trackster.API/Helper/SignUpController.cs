using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Trackster.Core.ViewModels;
using Trackster.Core;
using Trackster.Repository;
using Microsoft.IdentityModel.Tokens;
using Microsoft.EntityFrameworkCore;

namespace Trackster.API.Helper
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class SignUpController : ControllerBase
    {
        private readonly TracksterContext dbContext;

        public SignUpController(TracksterContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet]
        public int EmailCheck(string Email)
        {
            if (Provjera1(Email))
                return 1;
            return 0;
        }
        [HttpGet]
        public int UsernameCheck(string Username)
        {
            if (Provjera2(Username))
                return 1;
            return 0;
        }

        private bool Provjera1(string uslov)
        {
            var useremail = dbContext.RegisteredUsers.FirstOrDefault(x => x.Email == uslov);
            if (useremail == null)
                return false;
            return true;

            //foreach (RegisteredUser user in dbContext.RegisteredUsers)
            //{
            //    if (user.Email == uslov)
            //        return true;
            //}
            //return false;
        }
        private bool Provjera2(string uslov)
        {
            var user_username = dbContext.RegisteredUsers.FirstOrDefault(x => x.Username == uslov);
            if (user_username == null)
                return false;
            return true;

            //foreach (RegisteredUser user in dbContext.RegisteredUsers)
            //{
            //    if (user.Username == uslov)
            //        return ;
            //}
            //return false;
        }


        [HttpPost]
        public ActionResult Add(RegisteredUserAddVM x)
        {
            var newUser = new RegisteredUser
            {
                Username = x.Username,
                Email = x.Email,
                Password = PasswordHelper.Hash(x.Password),
                Picture = "https://cdn.pixabay.com/photo/2015/10/05/22/37/blank-profile-picture-973460_1280.png",
                Bio = "Your bio goes here:"
            };
            dbContext.RegisteredUsers.Add(newUser);
            dbContext.SaveChanges();
            return Ok();
        }
    }
}