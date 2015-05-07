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
    public class FoodCategoriesController : Controller
    {
        FoodModelsDB db = new FoodModelsDB();

        // GET: FoodCategories
        public ActionResult Index()
        {
        //    var p = db.FoodNamesTable.OrderBy(r => r.CategoryName).ToList() ;
            var q = db.FoodCategoriesesTable.OrderBy(r => r.CategoryName).ToList();
            
            return View(q);
        }
        //[HttpPost]
        //public ActionResult addviewtoindex() 
        //{

        //    var p = db.FoodNamesTable.OrderBy(r => r.CategoryName).ToList();

        //    return (p);
        //}

        // GET: FoodCategories/Details/5
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

        // GET: FoodCategories/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: FoodCategories/Create
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

        // GET: FoodCategories/Edit/5
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

        // POST: FoodCategories/Edit/5
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

        // GET: FoodCategories/Delete/5
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

        // POST: FoodCategories/Delete/5
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
