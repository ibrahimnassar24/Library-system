using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.DynamicData;
using System.Web.Mvc;
using System.Web.Mvc.Filters;
using Dapper;

namespace LibrarySystem.Filters
{
    public class IsAuthenticated : ActionFilterAttribute, IAuthenticationFilter
    {
        
        public void OnAuthentication(AuthenticationContext filterContext)
        {
            
            if (filterContext.HttpContext.Session["username"] == null)
            {
                filterContext.Result = new RedirectResult("/user/signin");
            }

        }

        public void OnAuthenticationChallenge(AuthenticationChallengeContext filterContext)
        {
        }

    }
}