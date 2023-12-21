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
using OtpNet;
using static Trackster.API.Helper.AuthenticationAuthorization.MyAuthTokenExtension;
using Newtonsoft.Json.Linq;
using static QRCoder.PayloadGenerator;
using Microsoft.AspNetCore.Cors;

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

            Random random= new Random();

            var code = random.Next(1000, 9999);

            var noviToken = new AuthenticationToken()
            {
                isAddress = Request.HttpContext.Connection.RemoteIpAddress?.ToString(),
                tokenValue = jwtToken,
                registeredUser = loggedInUser,
                timeOfGeneration = DateTime.Now,
                twoFCode=code.ToString()
            };

            dbContext.Add(noviToken);
            dbContext.SaveChanges();

            #endregion

            EmailLog.SuccessfulLogin(noviToken, Request.HttpContext);

            return new LoginInformation(noviToken);
        }

        [HttpGet("{code}")]
        public ActionResult Unlock(string code)
        {
            var userInfo = HttpContext.GetLoginInfo().authenticationToken;

            if (userInfo == null)
            {
                return BadRequest("Korisnik nije logiran");
            }

            var token = dbContext.authenticationTokens.FirstOrDefault(s => s.twoFCode == code && s.id == userInfo.id);
            if (token != null)
            {
                token.twoFIsUnlocked = true;
                dbContext.SaveChanges();
                return Ok();
            }

            return BadRequest("Pogresan URL");
        }

        [EnableCors]
        [HttpPost]
        public ActionResult ForgotPassword([FromBody]string userEmail)
        {
            AuthenticationToken? authenticationToken = HttpContext.GetAuthToken();


            if (authenticationToken == null)
            {
                string password = RandomString(10);

                RegisteredUser? user = dbContext.RegisteredUsers.FirstOrDefault(s => s.Email == userEmail);
                if (user == null)
                {
                    return BadRequest("Korisnik ne postoji");
                }

                EmailLog.ForgotPassword(user, password);

                user.Password = PasswordChecker(password);


                dbContext.SaveChanges();
                return Ok();
            }

            return Ok();
        }

        [HttpPost]
        public ActionResult<LoginInformation> UpdatePassword(UserLogInVM x)
        {
            var User = ProvjeraEmail(x.Email);
            if (User == null)
                return BadRequest("Email");

            string? UserPassword = PasswordChecker(x.Password);

            RegisteredUser? user = dbContext.RegisteredUsers
                .FirstOrDefault(k =>
                k.Email == x.Email);
            if (user == null)
            {
                return new LoginInformation(null);
            }
            else
            {
                user.Password = UserPassword;
            }

            dbContext.SaveChanges();
            return Ok();
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
        public ActionResult<AuthenticationToken> Get()
        {
            AuthenticationToken? authenticationToken = HttpContext.GetAuthToken();

            return authenticationToken;
        }
 
    }
}
