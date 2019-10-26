using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace e_CarSharing.Models
{
    public class BankAccount
    {
        public int BankAccountId { get; set; }

        [Required]
        [DataType(DataType.Text), MaxLength(100)]
        public string BankName { get; set; }

        [Required(ErrorMessage = "You need to provide a Bank Account Number")]
        [DataType(DataType.Text)]
        public string BankAccountNumber { get; set; }
    }
}