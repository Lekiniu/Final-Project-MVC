using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace davaleba.Models
{
    public class ProductCustomClass
    {
        [ScaffoldColumn(false)]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Name")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Price")]
        public int Price { get; set; }

        [Required]
        [Display(Name = "InStock")]
        public bool InStock { get; set; }

        [Required]
        [Display(Name = "Description")]
        public string Description { get; set; }

        [Required]
        [Display(Name = "User")]
        public Nullable<int> UserId { get; set; }

        [Required]
        [Display(Name = "Percent")]
        public Nullable<int> Percent { get; set; }
        [Required]
        [Display(Name = "Last Price")]
        public Nullable<int> Last_Price { get; set; }

        [Required]
        [Display(Name = "Brand")]
        public Nullable<int> BrandId { get; set; }

        [Required]
        [Display(Name = "კატეგორია")]
        public int CategoryId { get; set; }
    }
}