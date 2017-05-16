using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ecomerce_Restaurant.Models.FoodModels
{
	public class Cart
	{
		[Key]
		[Index]
		public int ID { get; set; }

		[Index]
		public IList<int> FoodIDs { get; set; }

		public int Count { get; set; }

		[Display(Name = "Total Price")]
		public decimal TotalPrice { get; set; }
	}


	//public class CartExtention
	//{

	//	RestaurantContext db = new RestaurantContext();

	//	public int ShoppingCartId { get; set; }


	//	public void AddToCart(Food Food)
	//	{
	//		var cartItem = db.Carts.FirstOrDefault(c => c.CartID == ShoppingCartId && c.FoodID == Food.ID);

	//		if (cartItem == null)
	//		{
	//			cartItem = new Cart
	//			{
	//				CartID = ShoppingCartId,
	//				Food = Food,
	//				FoodID = Food.ID,
	//				Count = 1
	//			};
	//			db.Carts.Add(cartItem);
	//		}
	//		else
	//		{
	//			cartItem.Count++;
	//		}

	//		db.SaveChanges();
	//	}



	//	//public static CartExtention GetCart(HttpContextBase context)
	//	//{
	//	//	var cart = new CartExtention();

	//	//	cart.ShoppingCartId = cart.GetCartId(context);

	//	//	return cart;
	//	//}

	//	//public static CartExtention GetCart(Controller controller)
	//	//{
	//	//	return GetCart(controller.HttpContext);
	//	//}


	//}
}