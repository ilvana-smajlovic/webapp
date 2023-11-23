using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;
using Trackster.Core;
using Trackster.Repository;

namespace Trackster.API.Helper
{
    public class UserService : IUserService
    {
        private readonly TracksterContext dbContext;

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

        public RegisteredUser Get(int id)
        {
            try
            {
                return dbContext.RegisteredUsers.
                    Where(r => (r.RegisteredUserId == id)).FirstOrDefault();
            }
            catch (Exception)
            {

                return null;
            }
        }
   
    }
}
