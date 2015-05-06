using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Ecomerce_Restaurant.Models.FoodModels;
using System.IO;

namespace Ecomerce_Restaurant.Controllers.FoodModelController
{
    public class FoodNamesController : Controller
    {
        private FoodModelsDB db = new FoodModelsDB();

        // GET: FoodNames
        public ActionResult Index()
        {
            return View(db.FoodNamesTable.ToList());
        }

        // GET: FoodNames/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FoodName foodName = db.FoodNamesTable.Find(id);
            if (foodName == null)
            {
                return HttpNotFound();
            }
            return View(foodName);
        }

        // GET: FoodNames/Create
        public ActionResult Create()
        {
            ViewBag.CategoryNames = db.FoodCategoriesesTable.Select(r => r.CategoryName).Distinct();

            return View();
        }

        // POST: FoodNames/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(FoodName foodName, HttpPostedFileBase foodpic)
        {
            foodName.FoodItemPicName = foodName.Name + ".png";
            if (ModelState.IsValid)
            {
                db.FoodNamesTable.Add(foodName);
                db.SaveChanges();

                if (foodpic != null && foodpic.ContentLength > 0)
                {
                    var filename = Path.GetFileName(foodpic.FileName);
                    string x1 = foodName.Name;
                    string newfilename = x1 + ".png";
                    var filePath1 = Path.Combine(Server.MapPath("~/Images"), newfilename);
                    foodpic.SaveAs(filePath1);
                }

                return RedirectToAction("Index");
            }

            return View(foodName);
        }

        // GET: FoodNames/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FoodName foodName = db.FoodNamesTable.Find(id);
            if (foodName == null)
            {
                return HttpNotFound();
            }
            return View(foodName);
        }

        // POST: FoodNames/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(FoodName foodName)
        {
            if (ModelState.IsValid)
            {
                db.Entry(foodName).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(foodName);
        }

        // GET: FoodNames/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FoodName foodName = db.FoodNamesTable.Find(id);
            if (foodName == null)
            {
                return HttpNotFound();
            }
            return View(foodName);
        }

        // POST: FoodNames/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            FoodName foodName = db.FoodNamesTable.Find(id);
            db.FoodNamesTable.Remove(foodName);
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
