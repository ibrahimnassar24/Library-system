using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Dapper;

namespace LibrarySystem.Filters
{
    public class IsAuthorized : ActionFilterAttribute, IAuthorizationFilter
    {

        
        public void OnAuthorization(AuthorizationContext filterContext)
        {
            if (filterContext.HttpContext.Session["role"].ToString() != "admin")
                filterContext.Result = new HttpStatusCodeResult(HttpStatusCode.Unauthorized);
        }

        
    }
}