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

        [Required(ErrorMessage = "You must say where are you from!")]
        [DataType(DataType.Text)]
        public string Country { get; set; }

        [Required(ErrorMessage = "You must provide a city!")]
        [DataType(DataType.Text)]
        public string City { get; set; }

        [ForeignKey("BankId")]
        [Required]
        public int BankId { get; set; }
        public BankEntity BankEntity { get; set; }

        public IList<Request> Requests { get; set; } //dont know if has to have a foreign key for Request table
    }
}