using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LibrarySystem.DAL;
using LibrarySystem.Models;
using LibrarySystem.Filters;
using Microsoft.Identity.Client;

namespace LibrarySystem.Controllers
{
    public class BookController : Controller
    {

        private LibSysDb db = new LibSysDb();

        // GET: Book
        public ActionResult Index()
        {
            
            return View(db.Books.ToList());
        }

        // GET /book/show/id
        public ActionResult Details (int id)
        {
            try
            {
                using (var db = new LibSysDb())
                {
                    Book book = db.Books.Find(id);
                    if (book == null) throw new Exception();
                    return View(book);
                }
            }
            catch
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.NotFound);
            }

        }

        // GET /book/create
        [IsAuthenticated]
        [IsAuthorized]
        public ActionResult Create ()
        {
            return View();
        }

        //POST /book/create
        [IsAuthenticated]
        [IsAuthorized]
        [HttpPost]
        public ActionResult Create (Book book)
        {
            try
            {
                using(var db = new LibSysDb())
                {
                    db.Books.Add(book);
                    db.SaveChanges();
                    return new RedirectResult("/");
                }
            }
            catch (Exception ex)
            {
                return Content(ex.Message);
            }
        }
    }
}