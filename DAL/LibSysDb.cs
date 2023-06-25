using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using LibrarySystem.Models;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace LibrarySystem.DAL
{
    public class LibSysDb : DbContext
    {
        public LibSysDb() : base()
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<Operation> Operations { get; set; }
        public DbSet<Borrow> Borrow_Operations { get; set; }
        public DbSet<Purchase> Purchase_Operations { get; set; }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            
        }
    }
}