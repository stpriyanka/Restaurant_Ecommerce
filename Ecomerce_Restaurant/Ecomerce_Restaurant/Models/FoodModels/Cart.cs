using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Ecomerce_Restaurant.Models.FoodModels
{
	public class Cart
	{
		[Index]
		[Key]
		public int ID { get; set; }


		[Index]
		public int CartID { get; set; }

		[Index]
		public int FoodID { get; set; }


		public int Count { get; set; }


		public FoodName Food { get; set; }

	}


	public class CartExtention
	{

		RestaurantContext db = new RestaurantContext();

		public int ShoppingCartId { get; set; }


		public void AddToCart(FoodName Food)
		{
			var cartItem = db.Carts.FirstOrDefault(c => c.CartID == ShoppingCartId && c.FoodID == Food.ID);

			if (cartItem == null)
			{
				cartItem = new Cart
				{
					CartID = ShoppingCartId,
					Food = Food,
					FoodID = Food.ID,
					Count = 1
				};
				db.Carts.Add(cartItem);
			}
			else
			{
				cartItem.Count++;
			}

			db.SaveChanges();
		}



		//public static CartExtention GetCart(HttpContextBase context)
		//{
		//	var cart = new CartExtention();

		//	cart.ShoppingCartId = cart.GetCartId(context);

		//	return cart;
		//}

		//public static CartExtention GetCart(Controller controller)
		//{
		//	return GetCart(controller.HttpContext);
		//}


	}
}