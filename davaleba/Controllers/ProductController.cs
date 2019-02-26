using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using davaleba.Models;
using PagedList;
using PagedList.Mvc;


namespace davaleba.Controllers
{
    [RoutePrefix("Product")]
    public class ProductController : Controller
    {
        ProjectDbEntities _db = new ProjectDbEntities();
        ProductDataProvider productData = new ProductDataProvider();
        ProjectDataProvider data = new ProjectDataProvider();

        // GET: Users
        [Route("Index/{name?}/{price?}")]
        public ActionResult Index(int page = 1, string name = "")
        {
            ViewBag.Name = name;
           
            var result = productData.GetProduct(name);
            return View(result.ToList().OrderBy(e=>e.Id).ToPagedList(page, 4));
        }


        public ActionResult SortBy(string sortBy, string sortByName, int page = 1, string name = "", bool fromPage = false)
        {
            ViewBag.CurrentSort = sortBy;
            ViewBag.Name = name;
            var product = productData.AllProduct();
            if (!fromPage)
            {
                ViewBag.SortByPrice = sortBy == null  || sortBy == "Price" ?  "Price Desc" : "Price";
                ViewBag.SortByName = sortBy == null || sortBy == "Name" ? "Name Desc" : "Name";
            }
            else
            {
                ViewBag.SortByPrice = sortBy == null || sortBy == "Price" ? "Price" : "Price Desc";
                ViewBag.SortByName = sortBy == null || sortBy == "Name" ? "Name" : "Name Desc";
            }

                switch (sortBy)
            {
                case "Price Desc":
                    product = productData.AllProduct().Where(e => e.Name.Contains(name))
                                               .OrderByDescending(e => e.Last_Price).ToList(); break;
                case "Name":
                    product = productData.AllProduct().Where(e => e.Name.Contains(name))
                                                      .OrderBy(e => e.Name).ToList(); break;
                case "Name Desc":
                    product = productData.AllProduct().Where(e => e.Name.Contains(name))
                                             .OrderByDescending(e => e.Name).ToList(); break;     
                case "Price":
                    product = productData.AllProduct().Where(e => e.Name.Contains(name))
                                                    .OrderBy(e => e.Last_Price).ToList(); break;
                default:
                    product = productData.AllProduct().Where(e => e.Name.Contains(name))
                                                    .OrderBy(e => e.Id).ToList(); break;
            }

            return View("_ProductTableView", product.ToPagedList(page, 4));
        }
        public JsonResult GetProductByName(string term)
        {

            var r = productData.AllProduct().Where(e => e.Name.Contains(term)).Select(n => n.Name);
            return Json(r, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetProductByBrandId(int brandId)
        {
            var result = productData.GetProductByBrandId(brandId);
            return View(result);
        }
        public ActionResult GetProductByCategoryId(int categoryId)
        {
            var result = productData.GetProductByCategoryId(categoryId);
            return View(result);
        }

        // GET: Users/Details/5
        [Route("Details/{id?}")]
        public ActionResult Details(int id)
        {
             var result = productData.GetProductById(id);
            
            if (result == null)
            {
                return HttpNotFound();
            }

           
            return View(result);
        }

        // GET: Users/Create
        [Route("Create/{model?}/{images?}")]
        public ActionResult Create()
        {
            ViewBag.CategoryId = new SelectList(_db.Categories.ToList(), "Id", "Name");
            ViewBag.UserId = new SelectList(_db.Users.ToList(), "Id", "First_Name");
            ViewBag.BrandId= new SelectList(_db.Brands.ToList(), "Id", "Name");
            return View();
        }

        // POST: Users/Create
        [Route("Create/{model?}/{images?}")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Name, Price, InStock, Description, UserId, Percent, Last_Price, BrandId, CategoryId, images")] ProductCustomClass model, HttpPostedFileBase[] images)
        {
            ViewBag.CategoryId = new SelectList(_db.Categories.ToList(), "Id", "Name");
            ViewBag.UserId = new SelectList(_db.Users.ToList(), "Id", "First_Name");
            ViewBag.BrandId = new SelectList(_db.Brands.ToList(), "Id", "Name");
            if (ModelState.IsValid)
            {
                productData.CreateProduct(model, images);
                return RedirectToAction("Index");
            }
                return View(model);  
        }

        //// GET: Users/Edit/5
        [Route("Edit/{id?}/{images?}")]
        public ActionResult Edit(int id)
        {   var categoryResult = _db.Products_Categories.FirstOrDefault(e => e.ProductId == id);
            var result = productData.GetProductById(id);
            //var categories = data.GetUserCategories();
            ViewBag.CategoryId = new SelectList(_db.Categories.ToList(), "Id", "Name", categoryResult.CategoriesId);
            ViewBag.UserId = new SelectList(_db.Users.ToList(), "Id", "First_Name", result.UserId);
            ViewBag.BrandId = new SelectList(_db.Brands.ToList(), "Id", "Name", result.BrandId);
           
            var customUser = new ProductCustomClass()
            {
                Name =  result.Name,
                Price =  result.Price,
                InStock =   result.InStock, 
                Description = result.Description,
                UserId = result.UserId,
                Percent = result.Percent,
                Last_Price =  result.Last_Price,
                BrandId = result.BrandId,
                CategoryId=categoryResult.CategoriesId,
        };
            return View(customUser);
        }


        //// POST: Users/Edit/5
        [Route("Edit/{id?}/{images?}")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id, Name, Price, InStock, Description, UserId, Percent, Last_Price, BrandId, CategoryId, images")] ProductCustomClass model, HttpPostedFileBase[] images)
        {
            
            if (ModelState.IsValid)
            {
                productData.EditProduct(model, images);
                return RedirectToAction("Index");
            }
            return View(model);
        }

        // GET: Users/Delete/5
        [Route("Delete/{id?}")]
        public ActionResult Delete(int id)
        {
            var result = productData.GetProductById(id);
            
           
            if(result == null)
            {
                return HttpNotFound();
            }
            return View(result);
        }

        //POST: Users/Delete/5
        [Route("Delete/{id?}")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(Product model)
        {
            try
            {
                productData.FullDeleteProduct(model);
            }
            catch
            {
                return View(model);
            }
            return RedirectToAction("index");
        }
        //[ValidateAntiForgeryToken]
        //public ActionResult Delete(Users model)
        //{
        //    try
        //    {
        //        UserData.deleteUser(model);
        //    }
        //    catch
        //    {
        //        return View(model);
        //    }
        //    return RedirectToAction("index");
        //}
        [Route("DeletePhoto/{model?}")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeletePhoto(Product model)
        {
            try
            {
                productData.DeletePhoto(model);
            }
            catch
            {
                return View(model);
            }
            return RedirectToAction("index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
