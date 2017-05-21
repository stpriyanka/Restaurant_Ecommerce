using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ecomerce_Restaurant.Models.FoodModels
{
	public class Food
	{

		[Key]
		//[Index]
		public int ID { get; set; }


		[Required]
		[Display(Name = "Name")]
		public string Name { get; set; }


		[Required]
		//[Index]
		[Display(Name = "Category")]
		public string CategoryName { get; set; }


		[DataType(DataType.Text)]
		[Display(Name = "Food Content")]
		public string FoodDescription { get; set; }


		[Required]
		public double Price { get; set; }


		[Display(Name = "Current Rate")]
		[Index]
		public double FoodRating { get; set; }


		[Display(Name = "Rating #")]
		public int RatingCount { get; set; }

		[Display(Name = "Image")]
		public string FoodImageName { get; set; }

		[NotMapped]
		[Display(Name = " Total Price")]
		public double TotalPrice { get; set; }

		//[NotMapped]
		//[Display(Name = "Total Amount")]
		//public double TotalAmountToPay { get; set; }

	}
}