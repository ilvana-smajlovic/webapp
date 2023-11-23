using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Trackster.API.Helper;
using Trackster.API.Helper.AuthenticationAuthorization;
using Trackster.Core;
using Trackster.Core.ViewModels;
using Trackster.Repository;
using static Trackster.API.Helper.AuthenticationAuthorization.MyAuthTokenExtension;

namespace Trackster.API.Controllers
{
    //[Authorize]
    [ApiController]
    [Route("[controller]/[action]")]
    public class AuthenticationController : Controller
    {
        private readonly TracksterContext dbContext;
        private readonly UserService userService;

        public AuthenticationController(TracksterContext dbContext)
        {
            this.dbContext = dbContext;
        }

        private RegisteredUser ProvjeraEmail(string email)
        {
            var useremail = dbContext.RegisteredUsers.FirstOrDefault(x => x.Email == email);
            return useremail;
        }
        private string PasswordChecker(string password)
        {
            var HashPassword = PasswordHelper.Hash(password);
            return HashPassword;
        }

        [HttpPost]
        public ActionResult<LoginInformation> LogInAuth(UserLogInVM x)
        {

            #region UserAuth

            var User = ProvjeraEmail(x.Email);
            if (User == null)
                return BadRequest("Email");

            string? UserPassword = PasswordChecker(x.Password);
            if (User.Password != UserPassword)
                return BadRequest("Password");

            RegisteredUser? loggedInUser = dbContext.RegisteredUsers
                .FirstOrDefault(k =>
                k.Email == x.Email && k.Password == UserPassword);
            if (loggedInUser == null)
            {
                return new LoginInformation(null);
            }


            #endregion

            #region JWT Token generation

            var builder = WebApplication.CreateBuilder();

            var issuer = builder.Configuration["Jwt:Issuer"];
            var audience = builder.Configuration["Jwt:Audience"];
            var key = Encoding.ASCII.GetBytes(builder.Configuration["Jwt:Key"]);

            var TokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(JwtRegisteredClaimNames.Sub, User.RegisteredUserId.ToString()),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                }),
                Expires = DateTime.UtcNow.AddMonths(1),
                Issuer = issuer,
                Audience = audience,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha512Signature),
            };

            var tokenhandler = new JwtSecurityTokenHandler();
            var token = tokenhandler.CreateToken(TokenDescriptor);
            var jwtToken = tokenhandler.WriteToken(token);

            var noviToken = new AuthenticationToken()
            {
                isAddress = Request.HttpContext.Connection.RemoteIpAddress?.ToString(),
                tokenValue = jwtToken,
                registeredUser = loggedInUser,
                timeOfGeneration = DateTime.Now
            };

            dbContext.Add(noviToken);
            dbContext.SaveChanges();

            return new LoginInformation(noviToken);

            #endregion

        }

        [HttpPost]
        public ActionResult Logout()
        {
            AuthenticationToken? authenticationToken = HttpContext.GetAuthToken();

            if (authenticationToken == null)
                return Ok();

            dbContext.Remove(authenticationToken);
            dbContext.SaveChanges();
            return Ok();
        }

        [HttpGet]
        public ActionResult<object> GetLoggedInUser()
        {
            var user = userService.GetLoggedInUser(HttpContext);
            return user == null ? Accepted() :
                Ok(new
                {
                    username = user.ToString(),
                    userId = user.RegisteredUserId,
                    picture = user.Picture
                }
                );
        }
 
    }
}
