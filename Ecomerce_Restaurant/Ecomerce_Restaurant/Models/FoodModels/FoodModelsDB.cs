using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;


namespace Ecomerce_Restaurant.Models.FoodModels
{
    public class FoodModelsDB:DbContext
    {
        public DbSet<FoodCategories> FoodCategoriesesTable { get; set; }
        public DbSet<FoodName> FoodNamesTable { get; set; }
    
    }
}