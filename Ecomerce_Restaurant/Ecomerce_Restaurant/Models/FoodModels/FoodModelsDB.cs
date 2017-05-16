using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;


namespace Ecomerce_Restaurant.Models.FoodModels
{
	public class RestaurantContext : DbContext
	{
		//FoodModelsDB
		public DbSet<FoodCategories> FoodCategoriesesTable { get; set; }
		public DbSet<FoodName> FoodNamesTable { get; set; }
		public DbSet<Cart> Carts { get; set; }

	}
}