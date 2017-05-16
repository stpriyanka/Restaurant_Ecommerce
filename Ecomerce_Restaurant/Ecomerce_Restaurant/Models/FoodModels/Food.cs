using System.ComponentModel.DataAnnotations;

namespace Ecomerce_Restaurant.Models.FoodModels
{
	public class Food
	{

		[Key]
		public int ID { get; set; }


		[Required]
		[Display(Name = "Food Name")]
		public string Name { get; set; }


		[Required]
		public string CategoryName { get; set; }


		[DataType(DataType.Text)]
		[Display(Name = "Food Content")]
		public string FoodDescription { get; set; }


		[Required]
		public double Price { get; set; }


		[Display(Name = "Current Rating")]
		public double FoodRating { get; set; }


		[Display(Name = "Total Ratings")]
		public int RatingCount { get; set; }

		public string FoodImageName { get; set; }

	}
}