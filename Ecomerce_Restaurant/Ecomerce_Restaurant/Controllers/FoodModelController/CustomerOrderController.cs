using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Ecomerce_Restaurant.Models.FoodModels;

namespace Ecomerce_Restaurant.Controllers.FoodModelController
{
    public class CustomerOrderController : Controller
    {
        private FoodModelsDB db = new FoodModelsDB();

        // GET: CustomerOrder
        public ActionResult Index()
        {
            return View(db.FoodCategoriesesTable.ToList());
        }

        // GET: CustomerOrder/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FoodCategories foodCategories = db.FoodCategoriesesTable.Find(id);
            if (foodCategories == null)
            {
                return HttpNotFound();
            }
            return View(foodCategories);
        }

        // GET: CustomerOrder/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CustomerOrder/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,CategoryName,CategoryDescription")] FoodCategories foodCategories)
        {
            if (ModelState.IsValid)
            {
                db.FoodCategoriesesTable.Add(foodCategories);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(foodCategories);
        }

        // GET: CustomerOrder/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FoodCategories foodCategories = db.FoodCategoriesesTable.Find(id);
            if (foodCategories == null)
            {
                return HttpNotFound();
            }
            return View(foodCategories);
        }

        // POST: CustomerOrder/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,CategoryName,CategoryDescription")] FoodCategories foodCategories)
        {
            if (ModelState.IsValid)
            {
                db.Entry(foodCategories).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(foodCategories);
        }

        // GET: CustomerOrder/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FoodCategories foodCategories = db.FoodCategoriesesTable.Find(id);
            if (foodCategories == null)
            {
                return HttpNotFound();
            }
            return View(foodCategories);
        }

        // POST: CustomerOrder/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            FoodCategories foodCategories = db.FoodCategoriesesTable.Find(id);
            db.FoodCategoriesesTable.Remove(foodCategories);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
