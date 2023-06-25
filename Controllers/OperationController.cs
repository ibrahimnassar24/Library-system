using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LibrarySystem.DAL;
using LibrarySystem.Models;
using LibrarySystem.Filters;
using Microsoft.Identity.Client;
using Antlr.Runtime.Misc;
using Microsoft.Ajax.Utilities;
using System.Net.Mime;

namespace LibrarySystem.Controllers
{
    [IsAuthenticated]
    public class OperationController : Controller
    {
        // GET: Operation
        [IsAuthorized]
        public ActionResult Index()
        {
            return View();
        }

        // POST /operation/borrow
        public ActionResult Borrow(Borrow borrow)
        {
            borrow.Borrow_Date = DateTime.Now.Date;
            borrow.Expected_Return_Date = borrow.Borrow_Date.AddDays(3).Date;
            try
            {

                using (var db = new LibSysDb())
                {
                    var username = Session["username"].ToString();

                    var OngoingOperation = db.Operations
                        .Where(o =>
                        o.UserName == username
                        && o.Type == "borrow"
                        && o.Status == "ongoing")
                        .ToList();
                    if (OngoingOperation.Count > 0)
                        return Content("you have to return the books you borrowed first, then borrow another");

                    var book = db.Books.Find(borrow.BookId);
                    if (book.Copies < 1)
                        return Content("there is not enough copies");
                    book.Copies -= 1;
                    var ActiveOperation = db.Operations
                        .Where(o =>
                        o.UserName == username
                        && o.Type == "borrow"
                        && o.Status == "active")
                        .ToList();
                    if (ActiveOperation.Count > 0)
                    {
                        ActiveOperation[0].BorrowOperations.Add(borrow);
                    }
                    else
                    {
                        var op = new Operation()
                        {
                            Type = "borrow",
                            Status = "active",
                            UserName = username
                        };
                        db.Operations.Add(op);
                        borrow.Operation = op;
                        db.Borrow_Operations.Add(borrow);
                    }
                    db.SaveChanges();
                    return new RedirectResult("/");

                }

            }
            catch
            {
                return Content("already in cart");
            }
        }

        //POST /operation/purchase
        public ActionResult Purchase(Purchase purchase)
        {
            try
            {

                using (var db = new LibSysDb())
                {
                    var username = Session["username"].ToString();

                    var book = db.Books.Find(purchase.Bookid);
                    if (book.Copies < purchase.Copies)
                        return Content("there is not enough copies");

                    book.Copies -= purchase.Copies;

                    var ActiveOperation = db.Operations
                        .Where(o =>
                        o.UserName == username
                        && o.Type == "purchase"
                        && o.Status == "active")
                        .ToList();
                    if (ActiveOperation.Count > 0)
                    {
                        ActiveOperation[0].PurchaseOperations.Add(purchase);
                    }
                    else
                    {
                        var op = new Operation()
                        {
                            Type = "purchase",
                            Status = "active",
                            UserName = username
                        };
                        db.Operations.Add(op);
                        purchase.Operation = op;
                        db.Purchase_Operations.Add(purchase);
                    }

                    db.SaveChanges();
                    return new RedirectResult("/");

                }

            }
            catch
            {
                return Content("already in cart");
            }
        }

        public ActionResult Remove(int id)
        {
            var username = Session["username"].ToString ();
            using(var db = new LibSysDb())
            {
                var ops = db.Operations
                    .Where(o =>
                    o.UserName == username
                    && o.Status == "active").ToList();

                foreach(var op in ops)
                {
                    if(op.Type == "borrow")
                    {
                        var borrow = op.BorrowOperations.Where(b => b.BookId == id).FirstOrDefault();
                        borrow.Book.Copies += 1;
                        db.Borrow_Operations.Remove(borrow);
                    }
                    else
                    {
                        var purchase = op.PurchaseOperations.Where(p => p.Bookid == id).FirstOrDefault();
                        purchase.Book.Copies += purchase.Copies;
                        db.Purchase_Operations .Remove(purchase);
                    }
                }

                db.SaveChanges ();
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.NoContent);
            }
        }

    }
}