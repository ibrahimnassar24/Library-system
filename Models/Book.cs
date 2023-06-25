using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using Dapper;

namespace LibrarySystem.Models
{
    public class Book
    {
        public int Id { get; set; }
        [MaxLength(128)]
        public string Title { get; set; }
        [MaxLength(64)]
        public string Author { get; set; }
        [MaxLength (32)]
        public string Type { get; set; }
        [MaxLength(16)]
        public string Publisher { get; set; }
        [MaxLength(16)]
        public string Edition { get; set; }
        [Column(TypeName = "date")]
        public DateTime Published_On { get; set; }

        public int Pages { get; set; }
        public int Copies { get; set; }
        public bool Availibility { get; set; }
        [MaxLength(128)]
        public string Category { get; set; }
        public float Price_Of_Selling { get; set; }
        public float Price_Of_Reservation { get; set; }
        public float Cost { get; set; }
        public int Reservation_Period { get; set; }

        public virtual ICollection<Borrow> BorrowOperations { get; set; }
        public virtual ICollection<Purchase> PurchaseOperations { get; set; }

    }
}