using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations.Schema;

namespace LibrarySystem.Models
{
    public class Purchase
    {
        [Key]
        [Column(Order = 1)]
        public int OperationId { get; set; }
        [Key]
        [Column(Order = 2)]
        public int Bookid { get; set; }
        public int Copies { get; set; }

        public virtual Operation Operation { get; set; }
        public virtual Book Book { get; set; }
    }
}