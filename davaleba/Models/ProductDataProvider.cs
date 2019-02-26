using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Hosting;
using System.Web.Mvc;

namespace davaleba.Models
{
    public class ProductDataProvider
    {
        ProjectDbEntities _db = new ProjectDbEntities();


        public bool Exist(Product product)
        {
            return _db.Products.FirstOrDefault(e => e.Name == product.Name) == null ? false : true;
        }

        public bool ExistCustomName(ProductCustomClass product)
        {
            return _db.Products.FirstOrDefault(e => e.Name == product.Name) == null ? false : true;
        }
        //public bool alreadyExists(string name, int userCategoryId = 0)
        //{
        //    return _db.User_Categories.FirstOrDefault(e => e.Name == name && e.Id != userCategoryId) != null ? true : false;
        //}

        public Product GetProductById(int id)
        {
            return _db.Products.FirstOrDefault(e => e.Id == id);
        }

        //Create
        public void CreateProduct(ProductCustomClass product, HttpPostedFileBase[] images)
        {

            var prod = new Product();
            prod.Id = product.Id;
            prod.Name = product.Name;
            prod.Price = product.Price;
            prod.InStock = product.InStock;
            prod.Description = product.Description;
            prod.UserId = product.UserId;
            prod.Percent = product.Percent;
            prod.Last_Price = product.Last_Price;
            prod.BrandId = product.BrandId;

            if (!Exist(prod))
            { _db.Products.Add(prod); }

            _db.SaveChanges();

            _db.Products_Categories.Add(
               new Products_Categories()
               { CategoriesId = product.CategoryId, ProductId = prod.Id });

            _db.SaveChanges();

            var mapPath = HostingEnvironment.MapPath("~/MyPhotos/");
            foreach (var file in images)
            {
                if (file != null) { 
                    file.SaveAs(mapPath + file.FileName);
                _db.Images.Add(new Image() { Name = file.FileName, ProductId = prod.Id, Url = "~/MyPhotos/" + file.FileName });
                _db.SaveChanges();
                }
                else
                {
                    continue;
                }
           }
        }

        //Edit 
        public void EditProduct(ProductCustomClass product, HttpPostedFileBase[] images)
        {
            var result = _db.Products.FirstOrDefault(e => e.Id == product.Id);
            var categoryResult = _db.Products_Categories.FirstOrDefault(e => e.ProductId == product.Id);
            if (!ExistCustomName(product) || result.Name == product.Name)
            {

                result.Name = product.Name;
                result.Price = product.Price;
                result.InStock = product.InStock;
                result.Description = product.Description;
                result.UserId = product.UserId;
                result.Percent = product.Percent;
                result.Last_Price = product.Last_Price;
                result.BrandId = product.BrandId;

            }
            _db.SaveChanges();
  
            categoryResult.CategoriesId = product.CategoryId;
            _db.SaveChanges();

            var mapPath = HostingEnvironment.MapPath("~/MyPhotos/");
            foreach (var file in images)
            {
                if (file != null)
                {
                    file.SaveAs(mapPath + file.FileName);
                    _db.Images.Add(new Image() { Name = file.FileName, ProductId = result.Id, Url = "~/MyPhotos/" + file.FileName });
                    _db.SaveChanges();
                }
                else
                {
                    continue;
                }
            }
          
        }


        ////Delete
        ////public void deleteUser(Users user)
        ////{
        ////    var result = _db.Users.FirstOrDefault(e => e.Id == user.Id);
        ////    result.IsActive = false;
        ////    _db.SaveChanges();
        ////}


        public void FullDeleteProduct(Product product)
        {
            foreach (var item in product.Images)
            {
                string fullPath = HostingEnvironment.MapPath(item.Url);
                if (System.IO.File.Exists(fullPath))
                {
                    System.IO.File.Delete(fullPath);
                }

            }
            var deleteImage = _db.Images.Where(e => e.ProductId == product.Id);
            var deleteProductCategory = _db.Products_Categories.Where(e => e.ProductId == product.Id);
            var deleteOrders = _db.Orders.Where(e => e.ProductId == product.Id);
            var deleteProduct = _db.Products.Where(e => e.Id == product.Id);
            
            


            _db.Images.RemoveRange(deleteImage);
            _db.Products_Categories.RemoveRange(deleteProductCategory);
            _db.Orders.RemoveRange(deleteOrders);
            _db.Products.RemoveRange(deleteProduct);
            _db.SaveChanges();
        }

        //search 
        public IEnumerable<Product> GetProduct(string name)
        {

            return _db.Products.OrderByDescending(e=>e.Percent).Where(e => e.Name.Contains(name));
        }


        public IEnumerable<Product> AllProduct()
        {
            return _db.Products;
        }

        public void DeletePhoto(Product product)
        {
           var findImage=_db.Images.FirstOrDefault(e => e.ProductId == product.Id);
            string fullPath = HostingEnvironment.MapPath(findImage.Url);
                if (System.IO.File.Exists(fullPath))
                {
                    System.IO.File.Delete(fullPath);
                }
            _db.SaveChanges();
        }

        public IEnumerable<Product> GetProductByBrandId(int brandId)
        {
            var result = _db.Products.Where(e => e.BrandId == brandId); 
            return result;
        }
        public IEnumerable<Product> GetProductByCategoryId(int categoryId)
        {

            var result = _db.Products_Categories.Where(e => e.CategoriesId == categoryId);
            var products = _db.Products.Where(e => result.Any(a => a.CategoriesId == categoryId && a.ProductId == e.Id));
            return products;
        }
    }
}