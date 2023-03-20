using Trackster.Core;

namespace Trackster.API.Helper
{
    public class UserService
    {

        public RegisteredUser? GetLoggedInUser(HttpContext httpContext)
        {
            try
            {
                return UserHelper.ReadUserFromContext(httpContext) ?? null;
            }
            catch (ArgumentException)
            {
                return null;
            }
        }
    }
}
