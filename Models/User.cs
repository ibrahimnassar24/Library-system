using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using Dapper;
using BC = BCrypt.Net.BCrypt;
namespace LibrarySystem.Models
{

    public class User
    {

        public User()
        {
            this.Date_Of_Birth = DateTime.Today;
            this.Joined_On = DateTime.Today;
        }


        [MaxLength(64)]
        public string First_Name { get; set; }
        [MaxLength(64)]
        public string Last_Name { get; set; }
        [Key]
        [MaxLength(64)]
        public string UserName { get; set; }
        [Required]
        [MaxLength(64)]
        public string Password { get; set; }
        [Required]
        public string Role { get; set; }
        [MaxLength(16)]
        public string Gender { get; set; }
        [Column(TypeName = "date")]
        public DateTime? Date_Of_Birth { get; set; }
        
        public DateTime Joined_On { get; set; }
        [Required]
        public string Mobile_Phone { get; set; }
        public string Land_Phone { get; set; }
        [Required]
        public string Email { get; set; }
        public string Address { get; set; }

        public virtual ICollection<Operation> Operations { get; set; }
    }

    
}