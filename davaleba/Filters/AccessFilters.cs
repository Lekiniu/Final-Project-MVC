using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.Web.Mvc;
using System.Web.Routing;
using davaleba.Helpers;
using davaleba.Models;

namespace WebApplication37.Filters
{
    public class LoginFilter : ActionFilterAttribute, IActionFilter
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            ProjectDbEntities _db = new ProjectDbEntities();
            if (!LoginHelper.IsLoggedIn())
            {
                filterContext.Result = new RedirectToRouteResult(
                    new RouteValueDictionary { { "contoller", "AccountTest" }, { "Action", "Login" } });
            }
            else
            {
                User user = LoginHelper.CurrentUser();
                var userFromDb = _db.Users.FirstOrDefault(e => e.Password == user.Password && e.Mail == user.Mail);
                if (userFromDb == null)
                {
                    filterContext.Result = new HttpStatusCodeResult(HttpStatusCode.Forbidden);
                }
            }
            base.OnActionExecuting(filterContext);
        }
    }


    public class AccessFilter : ActionFilterAttribute, IActionFilter
    {
        public enum categories
        {
            admin = 1,
            user = 2
        }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            ProjectDbEntities _db = new ProjectDbEntities();
            User user = LoginHelper.CurrentUser();

            if (!LoginHelper.IsLoggedIn() || user.CategoryId != (int)categories.admin)
            {
                filterContext.Result = new HttpStatusCodeResult(HttpStatusCode.Forbidden);
            }
            base.OnActionExecuting(filterContext);
        }
    }

}