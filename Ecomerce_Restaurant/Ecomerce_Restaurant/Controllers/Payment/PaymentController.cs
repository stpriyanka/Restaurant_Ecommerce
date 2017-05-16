using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Ecomerce_Restaurant.Models.FoodModels;

namespace Ecomerce_Restaurant.Controllers.Payment
{
	public class PaymentController : Controller
	{
		readonly RestaurantContext _db = new RestaurantContext();

		// GET: Payment
		[HttpPost]
		public ActionResult Index(string[] itemIds)
		{
			var foodIds = new List<int>();
			if (itemIds.ToList().Count > 0)
			{
				foodIds = itemIds[0].Split(',').ToList().Select(int.Parse).ToList();
			}

			var distinctList = foodIds.Distinct().ToList();
			Dictionary<Food, int> orderedItems = new Dictionary<Food, int>();
			
			foreach (var id in distinctList)
			{
				int count = foodIds.Count(i => i.Equals(id));
				var food = _db.Foods.FirstOrDefault(x => x.ID == id);
				if (food != null) orderedItems.Add(food,count);
			}

			return View(orderedItems);
		}
	}
}