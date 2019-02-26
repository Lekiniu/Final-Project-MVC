using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace davaleba.Models
{
    public class BrandDataProvider
    {
        ProjectDbEntities _db = new ProjectDbEntities();


        public bool Exist(BrandCustomClass brand)
        {
            return _db.Brands.FirstOrDefault(e => e.Name == brand.Name) == null ? false : true;
        }
        //public bool alreadyExists(string name, int userCategoryId = 0)
        //{
        //    return _db.User_Categories.FirstOrDefault(e => e.Name == name && e.Id != userCategoryId) != null ? true : false;
        //}

        public Brand GetBrandById(int id)
        {
            return _db.Brands.FirstOrDefault(e => e.Id == id);
        }

        //Create
        public void CreateBrand(BrandCustomClass brand)
        {
            if (!Exist(brand))
            {
                _db.Brands.Add(new Brand()
                {
                    Name = brand.Name,
                    Id = brand.Id

                });

            }
            _db.SaveChanges();
        }

        //Edit 
        public void EditBrand(BrandCustomClass brand)
        {
            var result = _db.Brands.FirstOrDefault(e => e.Id == brand.Id);
            if (!Exist(brand))
            {
                result.Name = brand.Name;
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


        public void FullDeleteBrand(Brand brand)
        {
            var deleteImage = _db.Images.Where(e => e.Product.BrandId == brand.Id);
            var deleteOrders = _db.Orders.Where(e => e.Product.BrandId == brand.Id);
            var deleteProductCategory = _db.Products_Categories.Where(e => e.Product.BrandId == brand.Id);
            var deleteProduct = _db.Products.Where(e => e.BrandId == brand.Id);
            var deletebrand = _db.Brands.Where(e => e.Id == brand.Id);


            _db.Images.RemoveRange(deleteImage);
            _db.Orders.RemoveRange(deleteOrders);
            _db.Products_Categories.RemoveRange(deleteProductCategory);
            _db.Products.RemoveRange(deleteProduct);
            _db.Brands.RemoveRange(deletebrand);
            _db.SaveChanges();
        }

        //search 
        public IEnumerable<Brand> GetBrand(string name)
        {

            return _db.Brands.Where(e => e.Name.Contains(name));
        }


        public IEnumerable<Brand> AllBrand()
        {
            return _db.Brands;
        }
    }
}