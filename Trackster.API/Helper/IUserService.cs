using Trackster.Core;

namespace Trackster.API.Helper
{
    public interface IUserService
    {
        RegisteredUser GetLoggedInUser(HttpContext httpContext);
        RegisteredUser Get(int id);
    }
}
