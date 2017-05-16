using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Ecomerce_Restaurant.Models.FoodModels
{
	public class ShoppingCartViewModel
	{
		public List<Cart> CartItems { get; set; }

		public double CartTotal { get; set; }

	}
}