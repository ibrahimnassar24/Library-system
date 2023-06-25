using System;
using System.Web.Mvc;
using System.Net;
using LibrarySystem.Models;
using LibrarySystem.DAL;
using LibrarySystem.Filters;
using bc = BCrypt.Net;
using System.Data.SqlTypes;
using System.Linq;
using System.Net.Mime;

namespace LibrarySystem.Controllers
{
    public class UserController : Controller
    {
        // GET: User
        [IsAuthenticated]
        [IsAuthorized]
        public ActionResult Index()
        {
            return View();
        }

        // GET /user/show
        [IsAuthenticated]
        public ActionResult Show()
        {
            try
            {
                using (var db = new LibSysDb())
                {
                    var username = Session["username"].ToString();
                    var user = db.Users.Find(username);
                    return View(user);
                }
            }
            catch
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
        }

        // GET user/signup
        public ActionResult SignUp()
        {
            return View();
        }

        // POST /user/signup
        [HttpPost]
        public ActionResult SignUp([Bind(Include = "username,password,email,mobile_phone")] User user)
        {
            user.Password = bc.BCrypt.HashPassword(user.Password);
            user.Role = "customer";
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

        // GET /user/signin
        public ActionResult SignIn()
        {
            return View();
        }

        // Post /user/signin
        [HttpPost]
        public ActionResult SignIn([Bind(Include = "username,password")] User user)
        {
            try
            {
                using (var db = new LibSysDb())
                {
                    var obj = db.Users.Find(user.UserName);
                    if (obj == null) return new HttpStatusCodeResult(400, "username or password is incorrect");

                    var res = bc.BCrypt.Verify(user.Password, obj.Password);
                    if (!res) return new HttpStatusCodeResult(400, "username or password is incorrect");

                    Session["username"] = user.UserName;
                    Session["role"] = obj.Role;
                    Session.Timeout = 60 * 24;

                    return new RedirectResult("/");
                }
            }
            catch (Exception ex)
            {
                return Content(ex.Message);
            }
        }

        // GET /user/signout
        [IsAuthenticated]
        public ActionResult signOut()
        {
            Session.Clear();
            return new RedirectResult("/user/signin");
        }

        // GET /user/edit
        [IsAuthenticated]
        public ActionResult Edit()
        {
            try
            {
                using (var db = new LibSysDb())
                {
                    var username = Session["username"].ToString();
                    var user = db.Users.Find(username);
                    return View(user);
                }
            }
            catch (Exception ex)
            {
                return Content(ex.Message);
            }
        }

        // POST /user/edit
        [HttpPost]
        [IsAuthenticated]
        public ActionResult Edit(User user)
        {
            try
            {
                using (var db = new LibSysDb())
                {
                    var username = Session["username"].ToString();
                    var CurrentUser = db.Users.Find(username);

                    CurrentUser.First_Name = user.First_Name;
                    CurrentUser.Last_Name = user.Last_Name;
                    CurrentUser.Date_Of_Birth = user.Date_Of_Birth;
                    CurrentUser.Gender = user.Gender;
                    CurrentUser.Address = user.Address;
                    CurrentUser.Land_Phone = user.Land_Phone;

                    db.SaveChanges();
                    return new RedirectResult("/user/show");

                }
            }
            catch (Exception ex)
            {
                throw ex;
                //return Content(ex.Message);
            }
        }


    }
}