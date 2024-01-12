using Microsoft.EntityFrameworkCore;
using Trackster.API.Controllers;
using Trackster.Core;
using Trackster.Repository;

namespace Trackster.API.Helper
{
    public class EmailLog
    {
        private readonly TracksterContext dbContext;

        public EmailLog(TracksterContext dbContext)
        {
           this.dbContext=dbContext;
        }
        public static void SuccessfulLogin(AuthenticationToken token, HttpContext httpContext)
        {
            var user = token.registeredUser;
            var message = $"{user.Username}, <br> " +
                             $"Your 2 factor authentication code is <br>" +
                             $"{token.twoFCode}<br>" +
                             $"Login info {DateTime.Now}";


            EmailSender.Send(user.Email, "2f auth code", message, true);
        }

        public static void ForgotPassword(RegisteredUser user, string password)
        {
            var message = $"{user.Username}, <br> " +
                              $"Your new password is <br>" +
                              $"{password}<br>" +
                              $"Login info {DateTime.Now}";


            EmailSender.Send(user.Email, "New password", message, true);
        }
    }
}
