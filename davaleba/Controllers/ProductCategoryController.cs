using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using davaleba.Models;

namespace davaleba.Controllers
{
    [RoutePrefix("ProductCategory")]
    public class ProductCategoryController : Controller
    {

        ProjectDbEntities _db = new ProjectDbEntities();
        ProductCategoryDataProvider ProductCategoryData = new ProductCategoryDataProvider();
        ProjectDataProvider data = new ProjectDataProvider();

        // GET: Users
        [Route]
        public ActionResult Index()
        {
            var result = ProductCategoryData.AllProductCategory();
            return View(result);
        }

        // GET: Users/Details/5
        [Route("Details/{id?}")]
        public ActionResult Details(int id)
        {
            var result = ProductCategoryData.GetProductCategoryById(id);

            if (result == null)
            {
                return HttpNotFound();
            }


            return View(result);
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
