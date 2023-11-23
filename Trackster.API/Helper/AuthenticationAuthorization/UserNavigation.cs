using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http.Extensions;
using Trackster.Core;
using Trackster.Repository;

namespace Trackster.API.Helper.AuthenticationAuthorization
{
    public class UserNavigation
    {
        public static int Save(HttpContext httpContext, IExceptionHandlerPathFeature exceptionMessage = null)
        {
            RegisteredUser korisnik = httpContext.GetLoginInfo()._user;

            var request = httpContext.Request;

            var queryString = request.Query;

            if (queryString.Count == 0 && !request.HasFormContentType)
                return 0;

            //IHttpRequestFeature feature = request.HttpContext.Features.Get<IHttpRequestFeature>();
            string details = "";
            if (request.HasFormContentType)
            {
                foreach (string key in request.Form.Keys)
                {
                    details += " | " + key + "=" + request.Form[key];
                }
            }

            var x = new LogUserNavigation
            {
                user = korisnik,
                _time = DateTime.Now,
                queryPath = request.GetEncodedPathAndQuery(),
                postData = details,
                ipAddress = request.HttpContext.Connection.RemoteIpAddress?.ToString(),
            };

            if (exceptionMessage != null)
            {
                x.isException = true;
                x.exceptionMessage = exceptionMessage.Error.Message + " |" + exceptionMessage.Error.InnerException;
            }

            TracksterContext? db = httpContext.RequestServices.GetService<TracksterContext>();

            db.Add(x);
            db.SaveChanges();

            return x.id;
        }
    }
}
