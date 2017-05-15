using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using Ecomerce_Restaurant.Models.FoodModels;

namespace Ecomerce_Restaurant.Controllers
{
    public class OrderController : Controller
    {
        private RestaurantContext db = new RestaurantContext();

        // GET: Order
        public ActionResult Index()
        {
            return View(db.FoodNamesTable.ToList());
        }

        // GET: Order/Details/5
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

        // GET: Order/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Order/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Name,CategoryName,FoodDescription,FoodPrice,FoodRating,TotalRatedPeople,FoodItemPicName")] FoodName foodName)
        {
            if (ModelState.IsValid)
            {
                db.FoodNamesTable.Add(foodName);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(foodName);
        }

        // GET: Order/Edit/5
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

        // POST: Order/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Name,CategoryName,FoodDescription,FoodPrice,FoodRating,TotalRatedPeople,FoodItemPicName")] FoodName foodName)
        {
            if (ModelState.IsValid)
            {
                db.Entry(foodName).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(foodName);
        }

        // GET: Order/Delete/5
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

        // POST: Order/Delete/5
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
