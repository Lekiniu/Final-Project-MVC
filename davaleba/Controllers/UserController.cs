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
    [RoutePrefix("Users")]
    [TrackExecutionTime]
    public class UsersController : Controller
    {
        ProjectDbEntities _db = new ProjectDbEntities();
        UsersDataProvider UserData = new UsersDataProvider();
        ProjectDataProvider data = new ProjectDataProvider();

        // GET: Users
        [Route("Index/{name?}/{surname?}/{mail?}")]
        public ActionResult Index(int page = 1, string name = "", string surname = "", string mail = "")
        {
            ViewBag.Name = name;
            ViewBag.Surname = surname;
            ViewBag.Mail = mail;

            var result = UserData.GetUsers(name, surname, mail);
            return View(result.ToList().OrderBy(e => e.Id).ToPagedList(page, 4));
        }
        public ActionResult SortBy(string sortBy, int page = 1, string name = "", string surname = "", string mail = "", bool fromPage = false)
        {
            ViewBag.CurrentSort = sortBy;
            ViewBag.Name = name;
            ViewBag.Surname = surname;
            ViewBag.Mail = mail;
            var Users = UserData.AllUsers();

            if (!fromPage)
            {
                ViewBag.SortBySurname = sortBy == null || sortBy == "Surname" ? "Surname Desc" : "Surname";
                ViewBag.SortByName = sortBy == null || sortBy == "Name" ? "Name Desc" : "Name";
            }
            else
            {
                ViewBag.SortBySurname = sortBy == null || sortBy == "Surname" ? "Surname" : "Surname Desc";
                ViewBag.SortByName = sortBy == null || sortBy == "Name" ? "Name" : "Name Desc";
            }
                switch (sortBy)
            {
                case "Surname Desc":
                    Users = UserData.AllUsers().Where(e => e.First_Name.Contains(name) && 
                                                            e.Last_Name.Contains(surname) 
                                                                && e.Mail.Contains(mail))
                                               .OrderByDescending(e => e.Last_Name).ToList(); break;
                case "Name":
                    Users = UserData.AllUsers().Where(e => e.First_Name.Contains(name) 
                                                      && e.Last_Name.Contains(surname)
                                                             && e.Mail.Contains(mail))
                                                      .OrderBy(e => e.First_Name).ToList(); break;
                case "Name Desc":
                    Users = UserData.AllUsers().Where(e => e.First_Name.Contains(name) && 
                                                            e.Last_Name.Contains(surname) 
                                                                && e.Mail.Contains(mail))
                                             .OrderByDescending(e => e.First_Name).ToList(); break;

                case "Surname":
                    Users = UserData.AllUsers().Where(e => e.First_Name.Contains(name) &&
                                                            e.Last_Name.Contains(surname)
                                                                && e.Mail.Contains(mail))
                                               .OrderBy(e => e.Last_Name).ToList(); break;


                default:
                    Users = UserData.AllUsers().Where(e => e.First_Name.Contains(name) 
                                                       && e.Last_Name.Contains(surname) 
                                                             && e.Mail.Contains(mail))
                                                      .OrderBy(e => e.Id).ToList(); break;
            }

            return View("_UserTableView", Users.ToPagedList(page, 4));
        }




        public JsonResult GetUserMail(string mail = "")
        {

            var r = UserData.AllUsers().Where(e => e.Mail.Contains(mail)).Select(n => n.Mail);
            return Json(r, JsonRequestBehavior.AllowGet);
        }

        // GET: Users/Details/5
        [Route("Details/{id?}")]
        public ActionResult Details(int id)
        {
             var result = UserData.GetUserById(id);
            
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
            //var categories = data.GetUserCategories();
            ViewBag.CategoryId = new SelectList(_db.User_Categories.ToList(), "Id", "Name");
            return View();
        }

        // POST: Users/Create
        [Route("Create/{model?}")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "First_Name,Last_Name,Mail,Password,CategoryId, IsActive")] UserCustomClass model)
        {
           ViewBag.CategoryId = new SelectList(_db.User_Categories.ToList(), "Id", "Name");
            if (ModelState.IsValid)
            {
                UserData.CreateUser(model);
                return RedirectToAction("Index");
            }

                return View(model);
           
        }

        //// GET: Users/Edit/5
        [Route("Edit/{id?}")]
        public ActionResult Edit(int id)
        {
            var result = UserData.GetUserById(id);
            //var categories = data.GetUserCategories();
            ViewBag.CategoryId = new SelectList(_db.User_Categories.ToList(), "Id", "Name", result.CategoryId);
            var customUser = new UserCustomClass()
            {
                First_Name = result.First_Name,
                Last_Name = result.Last_Name,
                Mail = result.Mail,
                Password = result.Password,        
                CategoryId = result.CategoryId,
                IsActive = result.IsActive
            };
            return View(customUser);
        }


        //// POST: Users/Edit/5
        [Route("Edit/{id?}")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,First_Name,Last_Name,Mail,Password,IsActive,CategoryId")] UserCustomClass model)
        {
            ViewBag.CategoryId = new SelectList(_db.User_Categories.ToList(), "Id", "Name");
            if (ModelState.IsValid)
            {
                UserData.EditUser(model);
                return RedirectToAction("Index");
            }
            return View(model);
        }

        // GET: Users/Delete/5
        [Route("Delete/{id?}")]
        public ActionResult Delete(int id)
        {
            var result = UserData.GetUserById(id);
            
           
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
        public ActionResult Delete(User model)
        {
            try
            {
                UserData.FullDeleteUser(model);
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
        [HttpPost]
        [ValidateAntiForgeryToken]
      
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
