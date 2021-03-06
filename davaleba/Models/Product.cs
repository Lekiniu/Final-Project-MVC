//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace davaleba.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Product
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Product()
        {
            this.Images = new HashSet<Image>();
            this.Orders = new HashSet<Order>();
            this.Products_Categories = new HashSet<Products_Categories>();
        }
    
        public int Id { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }
        public bool InStock { get; set; }
        public string Description { get; set; }
        public Nullable<int> UserId { get; set; }
        public Nullable<int> Percent { get; set; }
        public Nullable<int> Last_Price { get; set; }
        public Nullable<int> BrandId { get; set; }
    
        public virtual Brand Brand { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Image> Images { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Order> Orders { get; set; }
        public virtual User User { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Products_Categories> Products_Categories { get; set; }
    }
}
