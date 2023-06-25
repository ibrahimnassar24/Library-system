using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Dapper;
using Microsoft.Ajax.Utilities;
using Newtonsoft.Json;

namespace LibrarySystem.Models
{
    public class Operation
    {
        public Operation()
        {
            this.Date = null;
        }
        public int Id { get; set; }
        public string UserName { get; set; }
        [JsonRequired]
        public string Type { get; set; }
        public string Status { get; set; }
        public string Pay_Method { get; set; }
        public DateTime? Date { get; set; }
        public float Total_Cost { get; set; }

        public virtual User User { get; set; }
        public virtual ICollection<Borrow> BorrowOperations { get; set; }
        public virtual ICollection<Purchase> PurchaseOperations { get; set;}

        

    }


    

}