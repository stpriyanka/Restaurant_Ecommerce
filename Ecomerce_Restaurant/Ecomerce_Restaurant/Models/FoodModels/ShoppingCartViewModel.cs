using System.Collections.Generic;

namespace Ecomerce_Restaurant.Models.FoodModels
{
	public class ShoppingCartViewModel
	{
		public List<Cart> CartItems { get; set; }

		public double CartTotal { get; set; }

	}
}