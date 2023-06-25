using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Net;
using System.Security.RightsManagement;
using System.Web;
using System.Data.Entity;



namespace LibrarySystem.Models
{
    public class Borrow
    {

        [Key]
        [Column(Order = 1)]
        public int OperationId { get; set; }
        [Key]
        [Column(Order = 2)]
        public int BookId { get; set; }
        [Column(TypeName = "date")]
        public DateTime Borrow_Date { get; set; }
        [Column(TypeName = "date")]
        public DateTime Expected_Return_Date { get; set; }

        public virtual Operation Operation { get; set; }
        public virtual Book Book { get; set; }
    }
}