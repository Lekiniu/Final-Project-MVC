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
    [RoutePrefix("Categories")]
    public class CategoriesController : Controller
    {
        ProjectDbEntities _db = new ProjectDbEntities();
        CategoriesDataProvider CategoriesData = new CategoriesDataProvider();
        ProjectDataProvider data = new ProjectDataProvider();

        // GET: Users
        [Route("Index/{name?}")]
        public ActionResult Index(string name = "")
        {
            ViewBag.Name = name;
            var result = CategoriesData.GetCategories(name);
            return View(result);
        }


        public JsonResult GetCategoryByName(string name = "K")
        {

            var r = CategoriesData.AllCategories().Where(e => e.Name.Contains(name)).Select(n => n.Name);
            return Json(r, JsonRequestBehavior.AllowGet);
        }

        // GET: Users/Details/5
        [Route("Details/{id?}")]
        public ActionResult Details(int id)
        {
            var result = CategoriesData.GetCategoriesById(id);

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
        public ActionResult Create([Bind(Include = "Name")] CategoriesCustomClass model)
        {

            if (ModelState.IsValid)
            {
                CategoriesData.CreateCategories(model);
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
            var result = CategoriesData.GetCategoriesById(id);

            var customCategory = new CategoriesCustomClass()
            {
                Name = result.Name,
                Id = result.Id
            };
            return View(customCategory);
        }


        //// POST: Users/Edit/5
        [Route("Edit/{id?}")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id, Name")] CategoriesCustomClass model)
        {
            if (ModelState.IsValid)
            {
                CategoriesData.EditCategories(model);
                return RedirectToAction("Index");
            }
            return View(model);
        }

        // GET: Users/Delete/5
        [Route("Delete/{id?}")]
        public ActionResult Delete(int id)
        {
            var result = CategoriesData.GetCategoriesById(id);
            if (result == null)
            {
                return HttpNotFound();
            }
            return View(result);
        }

        //POST: Users/Delete/5
        [Route("Delete/{id?}")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(Category model)
        {
            try
            {
                CategoriesData.FullDeleteCategories(model);
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
