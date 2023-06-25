using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using LibrarySystem.Models;
using System.Security.Authentication.ExtendedProtection;
using System.Web.UI.WebControls;
using bc = BCrypt.Net;

namespace LibrarySystem.DAL
{
    public class LibSysDbInitializer : DropCreateDatabaseIfModelChanges<LibSysDb>
    {

        protected override void Seed(LibSysDb context)
        {

            var ListOfBooks = new List<Book>()
            {
                new Book()
                {
                    Title = "Demon Copperhead: A Pulitzer Prize Winner",
                    Author = "Barbara Kingsolver",
                    Type = "hardcover",
                    Edition = "first",
                    Publisher = "Amazon.com",
                    Published_On = new DateTime(2021, 10, 18),
                    Pages = 264,
                    Copies = 20,
                    Availibility = true,
                    Category = "novel",
                    Price_Of_Selling = 48.79F,
                    Price_Of_Reservation = 20.25F,
                    Cost = 30.25F,
                    Reservation_Period = 3
                },
                new Book()
                {
                    Title = "Lessons in Chemistry",
                    Author = "Bonnie Garmus",
                    Type = "hardcover",
                    Edition="first",
                    Publisher = "new",
                    Pages = 279,
                    Copies=20,
                    Availibility = true,
                    Price_Of_Reservation = 17.75F,
                    Price_Of_Selling = 46.27F,
                    Cost = 31.75F,
                    Category = "novel litterature",
                    Published_On= new DateTime(2022, 4, 5)
                },
                new Book()
                {
                    Title = "A War Too Far",
                    Author = "David Lee Corley",
                    Edition = "first",
                    Type = "hardcover",
                    Publisher = "Amazon.com",
                    Published_On = new DateTime(2019, 9, 22),
                    Copies = 20,
                    Availibility = true,
                    Price_Of_Selling = 45.5F,
                    Price_Of_Reservation = 17.25F,
                    Reservation_Period = 3,
                    Cost = 29.30F,
                    Category = "novel fantsy"
                },
                new Book()
                {
                    Title = "The Forgotten 500",
                    Author = "Gregory A. Freeman",
                    Type = "hardcover",
                    Edition = "first",
                    Publisher = "Apress",
                    Published_On = new DateTime(2006, 9, 2),
                    Pages = 228,
                    Copies = 20,
                    Availibility = true,
                    Price_Of_Reservation = 15.25F,
                    Price_Of_Selling = 43.5F,
                    Cost = 29.75F,
                    Category = "novel fantasy adventure",
                    Reservation_Period = 3,
                },
                new Book()
                {
                    Title = "Attack On Titans",
                    Author = "Eren yager",
                    Type = "comic",
                    Edition = "tenth",
                    Publisher = "shonen jump",
                    Published_On = new DateTime(2013, 8, 12),
                    Availibility = false,
                    Category = "manga action",
                    Pages = 88,
                    Copies = 20,
                    Price_Of_Reservation = 12.9F,
                    Cost = 19.8F,
                    Price_Of_Selling = 30.26F,
                    Reservation_Period = 3
                },
                new Book()
                {
                    Title = "Naruto Shippoden",
                    Author = "Naruto ozomakie",
                    Type = "comic",
                    Edition = "second",
                    Published_On= new DateTime(2007, 6, 10),
                    Publisher = "shonen jump",
                    Copies = 20,
                    Pages = 720,
                    Availibility = true,
                    Cost = 40.53F,
                    Price_Of_Reservation = 26.35F,
                    Price_Of_Selling = 50.76F,
                    Category = "manga adventure action"
                }
    };

            ListOfBooks.ForEach(b => context.Books.Add(b));

            context.Users.Add(
                new User()
                {
                    UserName = "admin",
                    Password = bc.BCrypt.HashPassword("admin"),
                    Email = "admin@outlook.com",
                    Role = "admin",
                    Mobile_Phone = "123567879",
                    Joined_On = DateTime.Now
                }
                );
            
            context.SaveChanges();
        }
    }
}