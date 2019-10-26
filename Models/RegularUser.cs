using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace e_CarSharing.Models
{
    public class RegularUser
    {
        public int RegularUserId { get; set; }

        [Required(ErrorMessage = "You must provide a name!")]
        [DataType(DataType.Text), MaxLength(80)] 
        public string Name { get; set; }

        [Required(ErrorMessage = "You must provide a city!")]
        [DataType(DataType.Text), MaxLength(60)]
        public string City { get; set; }

        [ForeignKey("BankAccount")]
        [Required]
        public int BankAccountId { get; set; }
        public BankAccount BankAccount { get; set; }

        public IList<Request> Requests { get; set; } //dont know if has to have a foreign key for Request table

        public RegularUser()
        {
            Requests = new List<Request>();
        }
    }
}