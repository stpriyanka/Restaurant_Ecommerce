using Ecomerce_Restaurant.Models.FoodModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Ecomerce_Restaurant.Controllers
{
    public class HomeController : Controller
    {

        FoodModelsDB db = new FoodModelsDB();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }


        [HttpPost]
        public ActionResult SearchView(string searchvalue)
        {

            var v = db.FoodNamesTable.Where(r => r.CategoryName.Contains(searchvalue)).ToList();
            return View(v);
        }

        public ActionResult AdminView() 
        {

            var v = db.FoodNamesTable.OrderBy(r => r.CategoryName).ToList(); 
            return View(v);
        }
    

    }
}