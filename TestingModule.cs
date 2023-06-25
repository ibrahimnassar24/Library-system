using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Web;
using Dapper;

namespace LibrarySystem
{
    public class TestingModule : IHttpModule
    {
        public void Dispose()
        {
        }

        public void Init(HttpApplication context)
        {

            context.PreRequestHandlerExecute += (o, e) =>
            {
                /*
                
                var ctx = (HttpApplication) o ;
                var req = ctx.Context.Request;
                var cookie = req.Cookies["session_id"];
                if (cookie != null && cookie.Value != "")
                {
                    string sql = string.Format
                (
                "SELECT * FROM sessions " +
                "WHERE session_id = {0} AND active = true",
                cookie.Value
                );
                    var res = DB.Connection.Query(sql).ToList()[0];
                    if (res != null)
                    {
                        ctx.Context.Session["username"] = res.username;
                    }
                
                }
                
              */  
            };
        
        }
    }
}