using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Ecomerce_Restaurant.Models.FoodModels;
using System.Threading;
using PagedList;

namespace Ecomerce_Restaurant.Controllers.FoodModelController
{
	[Authorize(Roles = "Admin")]
	public class FoodCategoriesController : Controller
	{
		RestaurantContext db = new RestaurantContext();

		// GET: FoodCategories

		//[Authorize(Users = "priyanka_tasnia@yahoo.com")]

		public ActionResult Index(string currentFilter, int? page)
		{
			//    var p = db.FoodNamesTable.OrderBy(r => r.CategoryName).ToList() ;
			var q = db.FoodCategories.OrderBy(r => r.CategoryName).ToList();

			int pageSize = 3;
			int pageNumber = (page ?? 1);
			return View(q.ToPagedList(pageNumber, pageSize));
		}

		// GET: FoodCategories/Details/5
		public ActionResult Details(int? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			FoodCategory foodCategories = db.FoodCategories.Find(id);
			if (foodCategories == null)
			{
				return HttpNotFound();
			}
			return View(foodCategories);
		}

		// GET: FoodCategories/Create
		public ActionResult Create()
		{
			Thread.Sleep(1000);
			return PartialView();
		}

		// POST: FoodCategories/Create
		// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
		// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Create([Bind(Include = "ID,CategoryName,CategoryDescription")] FoodCategory foodCategories)
		{
			if (ModelState.IsValid)
			{
				db.FoodCategories.Add(foodCategories);
				db.SaveChanges();
				return RedirectToAction("Index");
			}

			return View(foodCategories);
		}

		// GET: FoodCategories/Edit/5
		public ActionResult Edit(int? id)
		{
			FoodCategory foodCategories = db.FoodCategories.Find(id);
			Thread.Sleep(3000);
			return PartialView(foodCategories);
		}

		// POST: FoodCategories/Edit/5
		// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
		// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Edit([Bind(Include = "ID,CategoryName,CategoryDescription")] FoodCategory foodCategories)
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
		public ActionResult Delete(int? delid)
		{
			if (delid == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			FoodCategory foodCategories = db.FoodCategories.Find(delid);
			if (foodCategories == null)
			{
				return HttpNotFound();
			}
			return PartialView(foodCategories);
		}

		// POST: FoodCategories/Delete/5
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Delete(int delid)
		{
			FoodCategory foodCategories = db.FoodCategories.Find(delid);
			db.FoodCategories.Remove(foodCategories);
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

		public ActionResult foodview()
		{

			var v = db.Foods.ToList();
			return PartialView(v);
		}

		// GET: FoodNames/Create
		public ActionResult CreateFoodItem()
		{
			var v = db.FoodCategories.Select(r => r.CategoryName).Distinct();
			ViewBag.CategoryNames = v;
			return PartialView();
		}

		// POST: FoodNames/Create
		// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
		// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult CreateFoodItem(Food foodName, HttpPostedFileBase foodpic)
		{
			foodName.FoodImageName = foodName.Name + ".png";
			if (ModelState.IsValid)
			{
				if (foodpic != null && foodpic.ContentLength > 0)
				{
					var filename = Path.GetFileName(foodpic.FileName);
					string x1 = foodName.Name;
					string newfilename = x1 + ".png";
					var s = Server.MapPath("~/Image");
					var filePath1 = Path.Combine(Server.MapPath("~/Images"), newfilename);
					foodpic.SaveAs(filePath1);
					db.Foods.Add(foodName);
					db.SaveChanges();
				}
				else
				{
					foodName.FoodImageName = "default.png";
					db.Foods.Add(foodName);
					db.SaveChanges();
				}
				return RedirectToAction("Index");
			}
			else
			{
				var v = db.FoodCategories.Select(r => r.CategoryName).Distinct();
				ViewBag.CategoryNames = v;
				return View(foodName);
			}


		}


		// GET: FoodNames/Edit/5
		public ActionResult EditFood(int? id)
		{
			var v = db.FoodCategories.Select(r => r.CategoryName).Distinct();
			ViewBag.CategoryNames = v;

			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			Food foodName = db.Foods.Find(id);

			var c = db.FoodCategories.Select(r => r.CategoryName).ToList();

			return PartialView(foodName);
		}



		// POST: FoodNames/Edit/5
		// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
		// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult EditFood(Food foodName, string oldpicname, HttpPostedFileBase foodpic)
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
					db.SaveChanges();
					var filename = Path.GetFileName(foodpic.FileName);
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
		public ActionResult DeleteFood(int? id)
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
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult DeleteFood(int id)
		{
			Food foodName = db.Foods.Find(id);
			db.Foods.Remove(foodName);
			db.SaveChanges();
			return RedirectToAction("Index", "FoodCategories");
		}


	}
}
