using Trackster.Core;

namespace Trackster.API.Helper
{
    public static class UserHelper
    {
        public static RegisteredUser ReadUserFromContext(HttpContext httpContext)
        {
            if(httpContext==null)
            {
                throw new ArgumentNullException(nameof(httpContext));
            }
            if (httpContext.Items["User"] == null)
            {
                throw new ArgumentException(nameof(httpContext));
            }
            if (httpContext.Items["User"] is not RegisteredUser)
            {
                throw new ArgumentException("User is not of type User");
            }
            return httpContext.Items["User"] as RegisteredUser;
        }
    }
}
