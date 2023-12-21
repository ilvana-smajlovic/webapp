using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Trackster.Core;
using Trackster.Repository;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;

namespace Trackster.API.Helper.AuthenticationAuthorization
{
    public static class MyAuthTokenExtension
    {
        public class LoginInformation
        {
            public LoginInformation(AuthenticationToken? authenticationToken)
            {
                this.authenticationToken = authenticationToken;
            }

            [JsonIgnore]
            public RegisteredUser? _user => authenticationToken?.registeredUser;
            public AuthenticationToken? authenticationToken { get; set; }

            public bool isLogged => _user != null;

        }

        public class ForgotPasswordRequest
        {
            public string Email { get; set; }
        }


        public static LoginInformation GetLoginInfo(this HttpContext httpContext)
        {
            var token = httpContext.GetAuthToken();

            return new LoginInformation(token);
        }

        public static AuthenticationToken? GetAuthToken(this HttpContext httpContext)
        {
            string token = httpContext.GetMyAuthToken();
            TracksterContext dbContext = httpContext.RequestServices.GetService<TracksterContext>();

            AuthenticationToken? _registeredUserToken = dbContext?.authenticationTokens
                .Include(s => s.registeredUser)
                .FirstOrDefault(x => x.tokenValue == token);

            return _registeredUserToken;

        }


        public static string GetMyAuthToken(this HttpContext httpContext)
        {
            string token = httpContext.Request.Headers["authentication-token"];
            return token;
        }

        private static Random random = new Random();
        public static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }
       
    }
}
