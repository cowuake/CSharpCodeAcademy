using System.ComponentModel;

namespace Restaurant.MVC.Models.Menu
{
    public class DishDetailsViewModel
    {
        [DisplayName("Dish ID")]
        public int ID { get; set; }

        [DisplayName("Dish name")]
        public string Name { get; set; }

        [DisplayName("Dish type")]
        public string Type { get; set; }

        [DisplayName("Dish price")]
        public decimal Price { get; set; }
    }
}