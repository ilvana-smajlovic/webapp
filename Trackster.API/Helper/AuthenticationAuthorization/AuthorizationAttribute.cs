using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;

namespace Trackster.API.Helper.AuthenticationAuthorization
{
    public class AuthorizationAttribute : TypeFilterAttribute
    {
        public AuthorizationAttribute(bool admin, bool regular)
           : base(typeof(MyAuthorizeImpl))
        {
            Arguments = new object[] { };
        }
    }


    public class MyAuthorizeImpl : IActionFilter
    {
        private readonly bool _admin;
        public readonly bool _regular;

        public MyAuthorizeImpl(bool admin, bool regular)
        {
            _admin = admin;
            _regular = regular;
        }
        public void OnActionExecuted(ActionExecutedContext context)
        {


        }

        public void OnActionExecuting(ActionExecutingContext filterContext)
        {

            if (filterContext.HttpContext.GetLoginInfo().isLogged)
            {
                filterContext.Result = new UnauthorizedResult();
                return;
            }

            UserNavigation.Save(filterContext.HttpContext);

            if (filterContext.HttpContext.GetLoginInfo().isLogged)
            {
                return;//ok - ima pravo pristupa
            }


            //else nema pravo pristupa
            filterContext.Result = new UnauthorizedResult();
        }
    }
}
