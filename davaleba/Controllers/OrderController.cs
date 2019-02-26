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
    [RoutePrefix("Order")]
    public class OrderController : Controller
    {
        ProjectDbEntities _db = new ProjectDbEntities();
        OrderDataProvider orderData = new OrderDataProvider();
        ProjectDataProvider data = new ProjectDataProvider();

        // GET: Users
        [Route]
        public ActionResult Index()
        {
            var result = orderData.AllOrders();
            return View(result);
        }

        // GET: Users/Details/5
        [Route("Details/{id?}")]
        public ActionResult Details(int id)
        {
            var result = orderData.GetOrderById(id);

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
            ViewBag.ProductId = new SelectList(_db.Products.ToList(), "Id", "Name");
            ViewBag.UserId = new SelectList(_db.Users.ToList(), "Id", "First_Name");
            return View();
        }

        // POST: Users/Create
        [Route("Create/{model?}")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ProductId, UserId, soldItem")] OrderCustomClass model)
        {
            ViewBag.ProducId = new SelectList(_db.Products.ToList(), "Id", "Name");
            ViewBag.UserId = new SelectList(_db.Users.ToList(), "Id", "First_Name");

            if (ModelState.IsValid)
            {
                orderData.CreateOrder(model);
                return RedirectToAction("Index");
            }

                return View(model);
           
        }

        //// GET: Users/Edit/5
        [Route("Edit/{id?}")]
        public ActionResult Edit(int id)
        {
            var result = orderData.GetOrderById(id);
            //var categories = data.GetUserCategories();
            //ViewBag.CategoryId = new SelectList(categories, "Id", "Name", result.CategoryId);
            ViewBag.ProductId = new SelectList(_db.Products.ToList(), "Id", "Name", result.ProductId);
            ViewBag.UserId = new SelectList(_db.Users.ToList(), "Id", "First_Name", result.UserId);
            var customOrder = new OrderCustomClass()
            {
               ProductId=result.ProductId,
               UserId=result.UserId,
               soldItem=result.soldItem

            };
            return View(customOrder);
        }


        //// POST: Users/Edit/5
        [Route("Edit/{id?}")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,ProductId, UserId, soldItem")] OrderCustomClass model)
        {
            //ViewBag.ProducId = new SelectList(_db.Product.ToList(), "Id", "Name", model.ProductId);
            //ViewBag.UserId = new SelectList(_db.Users.ToList(), "Id", "First_Name", model.UserId);
            if (ModelState.IsValid)
            {
                orderData.EditOrder(model);
                return RedirectToAction("Index");
            }
            return View(model);
        }

        // GET: Users/Delete/5
        [Route("Delete/{id?}")]
        public ActionResult Delete(int id)
        {
            var result = orderData.GetOrderById(id);
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
        public ActionResult Delete(Order model)
        {
            try
            {
                orderData.FullDeleteOrder(model);
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
