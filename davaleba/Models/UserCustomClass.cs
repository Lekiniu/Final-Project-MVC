using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace davaleba.Models
{
    public class UserCustomClass
    {
        [ScaffoldColumn(false)]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Name")]
        public string First_Name { get; set; }

        [Required]
        [Display(Name = "Surname")]
        public string Last_Name { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Mail { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        //[DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        ////[DataType(DataType.Password)]
        //[Display(Name = "Confirm password")]
        //[Compare("Password",ErrorMessage = "The password and confirmation password do not match.")]
        //public string ConfirmPassword { get; set; }



        [Required]
        [Display(Name = "Category")]
        public int CategoryId { get; set; }

        [Required]
        [Display(Name = "Is User Active?")]
        public bool IsActive { get; set; }
    }
}