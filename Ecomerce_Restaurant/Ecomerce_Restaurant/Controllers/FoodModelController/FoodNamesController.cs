﻿using System;
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
        public RestaurantContext db = new RestaurantContext();

        // GET: FoodNames
        public ActionResult Index()
        {
            return View(db.Foods.ToList());
        }

        // GET: FoodNames/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Food foodName = db.Foods.Find(id);
            if (foodName == null)
            {
                return HttpNotFound();
            }
            return View(foodName);
        }

		//// GET: FoodNames/Create
		//public ActionResult Create()
		//{
		//	var v = db.FoodCategoriesesTable.Select(r => r.CategoryName).Distinct();
		//	ViewBag.CategoryNames = v;
		//   return PartialView();
		//}

		//// POST: FoodNames/Create
		//// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
		//// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
		//[HttpPost]
		//[ValidateAntiForgeryToken]
		//public ActionResult Create(FoodName foodName, HttpPostedFileBase foodpic)
		//{
		//	foodName.FoodItemPicName = foodName.Name + ".png";
		//	if (ModelState.IsValid)
		//	{
		//		if (foodpic != null && foodpic.ContentLength > 0)
		//		{
		//			var filename = Path.GetFileName(foodpic.FileName);
		//			string x1 = foodName.Name;
		//			string newfilename = x1 + ".png";
		//			var filePath1 = Path.Combine(Server.MapPath("~/Images"), newfilename);
		//			foodpic.SaveAs(filePath1);
		//			db.FoodNamesTable.Add(foodName);
		//			db.SaveChanges();
		//		}
		//		else {
		//			foodName.FoodItemPicName = "default.png";
		//			db.FoodNamesTable.Add(foodName);
		//			db.SaveChanges();
		//		}
		//		return RedirectToAction("Index");
		//	}
		//	else
		//	{
		//		var v = db.FoodCategoriesesTable.Select(r => r.CategoryName).Distinct();
		//		ViewBag.CategoryNames = v;
		//		return View(foodName);
		//	}

		//}

        // GET: FoodNames/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Food foodName = db.Foods.Find(id);

            var c= db.FoodCategories.Select(r => r.CategoryName).ToList();
            
            return PartialView(foodName);
        }



        // POST: FoodNames/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Food foodName, string oldpicname, HttpPostedFileBase foodpic)
        {
            foodName.FoodImageName = foodName.Name + ".png";


            if (ModelState.IsValid)
            {

                if (foodpic != null && foodpic.ContentLength > 0)
                {
                    string x1 = foodName.Name;
                    string newfilename = x1 + ".png";
                    var filePath1 = Path.Combine(Server.MapPath("~/Images"), newfilename);
                    foodpic.SaveAs(filePath1);
                    db.Entry(foodName).State = EntityState.Modified;
                    db.SaveChanges(); var filename = Path.GetFileName(foodpic.FileName);
                }
                else
                {
                    if (!string.IsNullOrEmpty(oldpicname))
                    {
                        foodName.FoodImageName = oldpicname;
                    }
                    else
                    {
                        foodName.FoodImageName = "default.png";
                    }
                    db.Entry(foodName).State = EntityState.Modified;
                    db.SaveChanges();
                }
                return RedirectToAction("Index", "FoodCategories");

            }
            var c = db.FoodCategories.Select(r => r.CategoryName).ToList();
            return View(foodName);
        }

        // GET: FoodNames/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Food foodName = db.Foods.Find(id);
            if (foodName == null)
            {
                return HttpNotFound();
            }
            return PartialView(foodName);
        }

        // POST: FoodNames/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            Food foodName = db.Foods.Find(id);
            db.Foods.Remove(foodName);
            db.SaveChanges();
            return RedirectToAction("Index", "FoodCategories");
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
