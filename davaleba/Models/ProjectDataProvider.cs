using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace davaleba.Models
{
    public class ProjectDataProvider
    {
        ProjectDbEntities _db = new ProjectDbEntities();

        public IEnumerable<Products_Categories> GetProductWithCategory()
        {
            return _db.Products_Categories;
        }


        public IEnumerable<Product> GetProducts()
        {
            return _db.Products;
        }
        public IEnumerable<User_Categories> GetUserCategories()
        {
            return _db.User_Categories;
        }

        public IEnumerable<User> GetUsers()
        {
            return _db.Users;
        }

        public IEnumerable<Brand> GetBrands()
        {
            return _db.Brands;
        }

        public IEnumerable<Category> GetProductCategories()
        {
            return _db.Categories;
        }
        public IEnumerable<Order> GetOrders()
        {
            return _db.Orders;
        }
        public User getUserById(int id)
        {
            return _db.Users.FirstOrDefault(e => e.Id == id);
        }

    }
}