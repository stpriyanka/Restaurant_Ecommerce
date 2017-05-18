using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
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
				ViewBag.amount = amountToPay;

				//food.TotalAmountToPay = amountToPay;
				orderedItems.Add(food, count);
			}

			//buyer.FoodIDs = distinctList;
			//buyer.

			return View(orderedItems);
		}

		public ActionResult PaymentConfirmation(string totalAmoutToPay)
		{
			ViewBag.amount = totalAmoutToPay;
			return View();
		}

		[HttpPost]
		public void PaymentConfirmation(string name, string amount, string personNumber, string phoneNumber)
		{
			var path = "http://localhost:44305/";
		
			string redirecturl = "";

			//Mention URL to redirect content to paypal site
			redirecturl += "https://www.sandbox.paypal.com/cgi-bin/webscr?cmd=_xclick&business=" + "priyanka_tasnia@yahoo.com";
			//"https://www.paypal.com/cgi-bin/webscr?cmd=_xclick&business=" +
			//		   "priyanka_tasnia@yahoo.com";


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
			redirecturl += "&currency_code=" + "SEK";

			redirecturl += "&item_number=1" + personNumber; //to identify payment from list
			
			redirecturl += "&return="+path;

			//string path = ConfigurationManager.AppSettings["BaseURL"].ToString() ;
			//string businessPaypalId = "bill_1324043702_biz@sdsol.com";
			//string redirect = "";
			//redirect += "https://www.sandbox.paypal.com/cgi-bin/webscr?cmd=_xclick&business=" + businessPaypalId;
			//redirect += "&item_name=" + BuyDetail[0].ToString();
			//redirect += "&amount=" + String.Format("{0:0.00} ", BuyDetail[3].ToString());
			//redirect += "&item_number=1";
			//redirect += "&currency_code=USD";

			//redirect += "&cancel_return=" + path + "/BuyPayPal.aspx";
			//redirect += "&notify_url=" + path + "/BuyPayPal.aspx";
			//redirect += "&custom=" + Did.ToString();
			//Response.Redirect(redirect);

			//Ger buyer info and store somewhere 

			WrappedBuyerInfo buyer = new WrappedBuyerInfo()
			{
				Name = name,
				PaidAmount = double.Parse(amount, System.Globalization.CultureInfo.InvariantCulture),
				UniquePaymentID = Guid.NewGuid(),
				PhoneNumber = phoneNumber,
				PersonNumber = personNumber,

			};


			Response.Redirect(redirecturl);
		}
	}


	public class WrappedBuyerInfo
	{
		[NotMapped]
		public Guid UniquePaymentID { get; set; }


		[NotMapped]
		public string Name { get; set; }

		[NotMapped]
		public string PhoneNumber { get; set; }

		[NotMapped]
		public string PersonNumber { get; set; }


		[NotMapped]
		public List<int> FoodIDs { get; set; }


		[NotMapped]
		public int NumberOfItems { get; set; }


		[NotMapped]
		public double PaidAmount { get; set; }

	}
}