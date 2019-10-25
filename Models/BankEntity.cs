using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace e_CarSharing.Models
{
    public class BankEntity
    {
        public int BankId { get; set; }

        [Required]
        [DataType(DataType.Text)]
        public string BankName { get; set; }
    }
}