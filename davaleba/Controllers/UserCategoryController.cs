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
    [RoutePrefix("UserCategory")]
    public class UserCategoryController : Controller
    {
        ProjectDbEntities _db = new ProjectDbEntities();
        UserCategoryDataProvider UserCategoryData = new UserCategoryDataProvider();
        ProjectDataProvider data = new ProjectDataProvider();


        // GET: Users
        [Route("{name?}")]
        public ActionResult Index(string name = "")
        {
            var result = UserCategoryData.GetUsersCategory(name).OrderBy(e=>e.Id);
            return View(result);
        }

        // GET: Users/Details/5
        [Route("Details/{id?}")]
        public ActionResult Details(int id)
        {
            var result = UserCategoryData.GetUserCategoryById(id);

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
        public ActionResult Create([Bind(Include = "Name")] UserCategoryCustomClass model)
        {
            if (ModelState.IsValid)
            {
                UserCategoryData.CreateUserCategory(model);
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
            var result = UserCategoryData.GetUserCategoryById(id);

            var customUserCategory = new UserCategoryCustomClass()
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
        
        public ActionResult Edit([Bind(Include = "Id, Name")] UserCategoryCustomClass model)
        {
            if (ModelState.IsValid)
            {
                UserCategoryData.EditUserCategory(model);
                return RedirectToAction("Index");
            }
            return View(model);
        }

        // GET: Users/Delete/5
        [Route("Delete/{id?}")]
        public ActionResult Delete(int id)
        {
            var result = UserCategoryData.GetUserCategoryById(id);
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
        public ActionResult Delete(User_Categories model)
        {
            try
            {
                UserCategoryData.FullDeleteCategoryUser(model);
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
