using Accounting.DATA.Entity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Text;

namespace Accounting.DATA.Helper
{
    public class AdminSessionControl : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            //eğer session null veya oturumun sonlanmışsa account login sayfasına gider.
            if (filterContext == null || filterContext.HttpContext == null || filterContext.HttpContext.Session == null)
            {
                filterContext.HttpContext.Response.Redirect("/Admin/Account/Login");
            }
            else
            {
                var user = filterContext.HttpContext.Session.User();
                //change roleGuid
                if (user == null || user.RoleId != new Guid("roleGuid"))
                {
                    filterContext.Result = new RedirectToRouteResult(
                                            new RouteValueDictionary{
                                           { "area", "Admin" },
                                           { "controller", "Account" },
                                          { "action", "Login" }
                                         });

                    //filterContext.HttpContext.Response.Redirect("/Account/Login");
                }
            }

            base.OnActionExecuting(filterContext);
        }
    }
}
