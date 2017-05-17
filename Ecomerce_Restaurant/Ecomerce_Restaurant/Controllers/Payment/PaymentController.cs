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
			var orderedItems = new Dictionary<Food, int>();

			double amountToPay = 0;

			foreach (var id in distinctList)
			{
				int count = foodIds.Count(i => i.Equals(id));
				var food = _db.Foods.FirstOrDefault(x => x.ID == id);

				if (food == null)
					continue;
				
				food.TotalPrice = food.Price * count;
				//food.TotalAmountToPay += food.TotalPrice;
				amountToPay += food.TotalPrice;
				ViewBag.amount= amountToPay;
				//food.TotalAmountToPay = amountToPay;
				orderedItems.Add(food, count);
			}

			return View(orderedItems);
		}

		public ActionResult PaymentConfirmation(string totalAmoutToPay)
		{
			ViewBag.amount = totalAmoutToPay;
			return View();
		}

		[HttpPost]
		public void PaymentConfirmation(string name,string amount)
		{

			                   string redirecturl = "";

			//Mention URL to redirect content to paypal site
			redirecturl += "https://www.paypal.com/cgi-bin/webscr?cmd=_xclick&business=" +
						   "priyanka_tasnia@yahoo.com";

			//First name i assign static based on login details assign this value
			redirecturl += "&first_name=" + name;

			////City i assign static based on login user detail you change this value
			//redirecturl += "&city=bhubaneswar";

			////State i assign static based on login user detail you change this value
			//redirecturl += "&state=Odisha";

			////Product Name
			//redirecturl += "&item_name=" + itemInfo;

			//Product Name
			redirecturl += "&amount=" + amount;

			////Phone No
			//redirecturl += "&night_phone_a=" + phone;

			////Product Name
			//redirecturl += "&item_name=" + itemInfo;

			////Address 
			//redirecturl += "&address1=" + email;

			//Business contact id
			// redirecturl += "&business=k.tapankumar@gmail.com";

			//Shipping charges if any
			redirecturl += "&shipping=10";

			//Handling charges if any
			redirecturl += "&handling=10";

			//Tax amount if any
			redirecturl += "&tax=10";

			//Add quatity i added one only statically 
			redirecturl += "&quantity=1";

			////Currency code 
			redirecturl += "&currency=" + "SEK";

			Response.Redirect(redirecturl);
		}
	}
}