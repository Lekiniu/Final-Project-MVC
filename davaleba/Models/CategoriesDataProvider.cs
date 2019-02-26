using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace davaleba.Models
{
    public class CategoriesDataProvider
    {
        ProjectDbEntities _db = new ProjectDbEntities();


        public bool Exist(CategoriesCustomClass category)
        {
            return _db.Categories.FirstOrDefault(e => e.Name == category.Name) == null ? false : true;
        }
        //public bool alreadyExists(string name, int userCategoryId = 0)
        //{
        //    return _db.User_Categories.FirstOrDefault(e => e.Name == name && e.Id != userCategoryId) != null ? true : false;
        //}

        public Category GetCategoriesById(int id)
        {
            return _db.Categories.FirstOrDefault(e => e.Id == id);
        }

        //Create
        public void CreateCategories(CategoriesCustomClass category)
        {
            if (!Exist(category))
            {
                _db.Categories.Add(new Category()
                {
                    Name = category.Name,
                    Id = category.Id
                });
            }
            _db.SaveChanges();
        }

        //Edit 
        public void EditCategories(CategoriesCustomClass category)
        {
            var result = _db.Categories.FirstOrDefault(e => e.Id == category.Id);
            if (!Exist(category))
            {
                result.Name = category.Name;
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


        public void FullDeleteCategories(Category category)
        {
            
            var deleteProductCategory = _db.Products_Categories.Where(e => e.CategoriesId == category.Id);
            var deletecategory = _db.Categories.Where(e => e.Id == category.Id);
           
            _db.Products_Categories.RemoveRange(deleteProductCategory);
            _db.Categories.RemoveRange(deletecategory);
            _db.SaveChanges();
        }

        //search 
        public IEnumerable<Category> GetCategories(string name)
        {
            return _db.Categories.Where(e => e.Name.Contains(name));
        }


        public IEnumerable<Category> AllCategories()
        {
            return _db.Categories;
        }
    }
}