using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace davaleba.Models
{
    public class UserCategoryDataProvider
    {
         ProjectDbEntities _db = new ProjectDbEntities();


        public bool Exist(UserCategoryCustomClass category)
        {
            return _db.User_Categories.FirstOrDefault(e => e.Name ==category.Name) == null ? false : true;
        }
        //public bool alreadyExists(string name, int userCategoryId = 0)
        //{
        //    return _db.User_Categories.FirstOrDefault(e => e.Name == name && e.Id != userCategoryId) != null ? true : false;
        //}

        public User_Categories GetUserCategoryById(int id)
        {
            return _db.User_Categories.FirstOrDefault(e => e.Id == id);
        }

        //Create
        public void CreateUserCategory(UserCategoryCustomClass userCategory)
        {
            if (!Exist(userCategory))
            {
                _db.User_Categories.Add(new User_Categories()
                {
                    Name = userCategory.Name,
                    Id = userCategory.Id

                });

            }
            _db.SaveChanges();
        }

        //Edit 
        public void EditUserCategory(UserCategoryCustomClass userCategory)
        {
            var result = _db.User_Categories.FirstOrDefault(e => e.Id == userCategory.Id);
            if (!Exist(userCategory))
            {   
                result.Name = userCategory.Name;
            }
            _db.SaveChanges(); 
        }
        
        
    ////Delete
    ////public void deleteUser(Users user)
    ////{
    ////    var result = _db.Users.FirstOrDefault(e => e.Id == user.Id);
    ////    result.IsActive = false;
    ////    _db.SaveChanges();
    ////}


    public void FullDeleteCategoryUser(User_Categories userCategory)
        {
            var deleteImage = _db.Images.Where(e => e.Product.User.CategoryId == userCategory.Id);
            var deleteProductCategory = _db.Products_Categories.Where(e => e.Product.User.CategoryId == userCategory.Id);
          
            var deleteProduct = _db.Products.Where(e => e.User.CategoryId== userCategory.Id);
            var deleteOrders = _db.Orders.Where(e => e.User.CategoryId == userCategory.Id);
            var deleteUsers = _db.Users.Where(e => e.CategoryId == userCategory.Id);
            var deleteCategory = _db.User_Categories.Where(e => e.Id == userCategory.Id);


            _db.Images.RemoveRange(deleteImage);
            _db.Products_Categories.RemoveRange(deleteProductCategory);
            _db.Orders.RemoveRange(deleteOrders);
            _db.Products.RemoveRange(deleteProduct);
            _db.Users.RemoveRange(deleteUsers);
            _db.User_Categories.RemoveRange(deleteCategory);
            _db.SaveChanges();
        }

        //search 
        public IEnumerable<User_Categories> GetUsersCategory(string name)
        {

            return _db.User_Categories.Where(e => e.Name.Contains(name));
        }


        public IEnumerable<User_Categories> AllUsersCategory()
        {
            return _db.User_Categories;
        }
    }
}