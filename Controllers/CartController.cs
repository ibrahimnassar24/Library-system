using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LibrarySystem.Models;
using LibrarySystem.DAL;
using LibrarySystem.Filters;

namespace LibrarySystem.Controllers
{
    [IsAuthenticated]
    public class CartController : Controller
    {
        // GET: Cart
        public ActionResult Index()
        {
            var db = new LibSysDb();

            var username = Session["username"].ToString();
            var ops = db.Operations
                .Where(o =>
                o.UserName == username
                && o.Status == "active").ToList();

            return View(ops);

        }

        [HttpPost]
        public ActionResult Finish(string pay_method)
        {
            try
            {
                using (var db = new LibSysDb())
                {
                    var username = Session["username"].ToString();
                    var ops = db.Operations
                        .Where(o =>
                        o.UserName == username
                        && o.Status == "active").ToList();

                    foreach (var op in ops)
                    {
                        op.Pay_Method = pay_method;
                        if (op.Type == "borrow")
                        {
                            foreach (var borrow in op.BorrowOperations)
                            {
                                op.Total_Cost += (float)Math.Round(borrow.Book.Price_Of_Reservation, 2);
                            }
                            op.Status = "ongoing";
                        }
                        else
                        {
                            foreach (var purchase in op.PurchaseOperations)
                            {
                                op.Total_Cost += (float)Math.Round(purchase.Copies * purchase.Book.Price_Of_Selling, 2);

                            }
                            op.Status = "closed";
                            op.Date = DateTime.Now;
                        }
                    }
                    db.SaveChanges();
                }
                return View("Finish");
            }
            catch (Exception ex)
            {
                return Content(ex.Message);
            }
        }

       
    }
}