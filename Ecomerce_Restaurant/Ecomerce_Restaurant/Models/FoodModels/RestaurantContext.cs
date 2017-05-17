using System.Data.Entity;


namespace Ecomerce_Restaurant.Models.FoodModels
{
	public class RestaurantContext : DbContext
	{
		//FoodModelsDB
		public DbSet<FoodCategory> FoodCategories { get; set; }
		public DbSet<Food> Foods { get; set; }
		public DbSet<Cart> Carts { get; set; }

	}
}