using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace davaleba.Models
{
    public class ProductCategoryDataProvider
    {
        ProjectDbEntities _db = new ProjectDbEntities();


        public Products_Categories  GetProductCategoryById(int id)
        {
            return _db.Products_Categories.FirstOrDefault(e => e.Id == id);
        }

       

        public IEnumerable<Products_Categories> AllProductCategory()
        {
            return _db.Products_Categories;
        }
    }
}
