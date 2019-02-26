using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace davaleba.Models
{
    public class LoginModel
    {
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "პაროლი")]
        public string Password { get; set; }

        [Required]
        [EmailAddress(ErrorMessage = "მეილი არასწორია")]
        [Display(Name = "სახელი")]
        public string EMail { get; set; }
    }
}