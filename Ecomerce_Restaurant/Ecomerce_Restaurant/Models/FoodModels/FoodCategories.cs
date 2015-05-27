using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;


namespace Ecomerce_Restaurant.Models.FoodModels
{
    public class FoodCategories
    {

        [Key]
        public int ID { get; set; }

        [Required]
        [Display(Name = "Category Name")]
        public string CategoryName { get; set; }

        [Display(Name = "Category Description")]
        public string CategoryDescription { get; set; }
    
    
    }
    public class FoodName 
    
    {

        [Key]
        public int ID { get; set; }
        
        
        [Required]
        [Display (Name = "Food Name")]
        public string Name { get; set; }
       
        
        [Required]
        public string CategoryName { get; set; }
        
        
        [DataType(DataType.Text)]
        [Display( Name = "Food Content")]
        public string FoodDescription { get; set; }
        
        
        [Required]
        public double FoodPrice { get; set; }
        
        
        [Display( Name= "Current Rating")]
        public double FoodRating { get; set; }

        
        [Display(Name = "Total Ratings")]
        public int TotalRatedPeople { get; set; }
        
        
        public string FoodItemPicName { get; set; }



    }


}