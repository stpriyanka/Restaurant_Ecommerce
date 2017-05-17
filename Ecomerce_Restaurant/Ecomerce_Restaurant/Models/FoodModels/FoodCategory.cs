using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Ecomerce_Restaurant.Models.FoodModels
{
	public class FoodCategory
	{
		[Key]
		public int ID { get; set; }

		[Required]
		[Display(Name = "Category Name")]
		public string CategoryName { get; set; }

		[Display(Name = "Category Description")]
		public string CategoryDescription { get; set; }

		
	}
}