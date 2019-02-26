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
    [RoutePrefix("Brand")]
    public class BrandController : Controller
    {
        ProjectDbEntities _db = new ProjectDbEntities();
        BrandDataProvider brandData = new BrandDataProvider();
        ProjectDataProvider data = new ProjectDataProvider();

        // GET: Users
        [Route("Index/{name?}")]
        public ActionResult Index(string name = "")
        {
            ViewBag.Name = name;
            var result = brandData.GetBrand(name);
            return View(result);
        }

        public JsonResult GetBrandByName(string name = "")
        {
            
            var r = brandData.AllBrand().Where(e => e.Name.Contains(name)).Select(n => n.Name);
            return Json(r, JsonRequestBehavior.AllowGet);
        }

        // GET: Users/Details/5
        [Route("Details/{id?}")]
        public ActionResult Details(int id)
        {
            var result = brandData.GetBrandById(id);

            if (result == null)
            {
                return HttpNotFound();
            }
            return View(result);
        }

        // GET: Users/Create
        [Route("Create/{model?}")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Users/Create
        [Route("Create/{model?}")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Name")] BrandCustomClass model)
        {

            if (ModelState.IsValid)
            {
                brandData.CreateBrand(model);
                return RedirectToAction("Index");
            }
            else
            {

                return View(model);
            }
        }

        //// GET: Users/Edit/5
        [Route("Edit/{id?}")]
        public ActionResult Edit(int id)
        {
            var result = brandData.GetBrandById(id);

            var customUserCategory = new BrandCustomClass()
            {
                Name = result.Name,
                Id = result.Id
            };
            return View(customUserCategory);
        }


        //// POST: Users/Edit/5
        [Route("Edit/{id?}")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id, Name")] BrandCustomClass model)
        {
            if (ModelState.IsValid)
            {
                brandData.EditBrand(model);
                return RedirectToAction("Index");
            }
            return View(model);
        }

        // GET: Users/Delete/5
        [Route("Delete/{searchId?}")]
        public ActionResult Delete(int id)
        {
            var result = brandData.GetBrandById(id);
            if (result == null)
            {
                return HttpNotFound();
            }
            return View(result);
        }

        //POST: Users/Delete/5
        [Route("Delete/{searchId?}")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(Brand model)
        {
            try
            {
                brandData.FullDeleteBrand(model);
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
