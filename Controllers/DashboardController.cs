using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LibrarySystem.Models;
using LibrarySystem.DAL;
using LibrarySystem.Filters;
using bc = BCrypt.Net;

namespace LibrarySystem.Controllers
{
    [IsAuthenticated]
    [IsAuthorized]
    public class DashboardController : Controller
    {
        // GET: Dashboard
        public ActionResult Index()
        {
            return View();
        }

        // GET /dashboard/alloperations
        public ActionResult AllOperations(string q = "")
        {
            using(var db = new LibSysDb())
            {
                if(q == "")
                {
                    var list = db.Operations.ToList();
                    return View(list);
                }

                var ops = db.Operations
                    .Where(o => o.UserName == q)
                    .ToList();
                return View(ops);
            }
        }

        public ActionResult AddAdmin()
        {
            return View();
        }

        // POST /dashboard/addadmin
        [HttpPost]
        public ActionResult AddAdmin([Bind(Include = "username,password,email,mobile_phone")] User user)
        {
            user.Password = bc.BCrypt.HashPassword(user.Password);
            user.Role = "admin";
            user.Joined_On = DateTime.Now.ToUniversalTime();
            try
            {
                using (var db = new LibSysDb())
                {
                    var TestUsername = db.Users.Find(user.UserName);
                    if (TestUsername != null)
                        return Content("this username already exists");

                    var Testemail = db.Users
                        .Where(s => s.Email == user.Email)
                        .ToList();
                    if (Testemail.Count > 0)
                        return Content("this email address already exists");

                    db.Users.Add(user);
                    db.SaveChanges();
                }
                return new RedirectResult("/user/signin");
            }
            catch (Exception ex)
            {
                throw ex;
                //return Content(ex.Message);
            }
        }

        [HttpPost]
        public ActionResult ConfirmReturn(string username)
        {
            using(var db = new LibSysDb())
            {
                var op = db.Operations
                    .Where(o =>
                    o.UserName == username
                    && o.Status == "ongoing"
                    ).FirstOrDefault();

                if (op == null)
                    return Content("there is no borrow operations");

                op.Status = "closed";
                op.Date = DateTime.Now;
                db.SaveChanges();
                return new RedirectResult("/dashboard/alloperations?q=" + username);
            }
        }
    }
}