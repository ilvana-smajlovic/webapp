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
            public LoginInformation(AuthenticationToken? autenticationToken)
            {
                this.autenticationToken = autenticationToken;
            }

            public RegisteredUser? _user => autenticationToken?.registeredUser;
            public AuthenticationToken? autenticationToken { get; set; }

            public bool isLogged => _user != null;

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

            AuthenticationToken? _registeredUser = dbContext.authenticationTokens
                .Include(s => s.registeredUser)
                .SingleOrDefault(x => x.tokenValue == token);

            return _registeredUser;
        }


        public static string GetMyAuthToken(this HttpContext httpContext)
        {
            string token = httpContext.Request.Headers["authentication-token"];
            return token;
        }
    }
}
