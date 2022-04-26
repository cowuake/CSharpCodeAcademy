using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Restaurant.MVC.Models.Menu
{
    public class AddEditDishViewModel
    {
        [DisplayName("Dish ID")]
        public int ID { get; set; }

        [DisplayName("Dish name"), Required(ErrorMessage = "Name is mandatory")]
        public string Name { get; set; }

        [DisplayName("Dish type"), Required(ErrorMessage = "Type is mandatory")]
        public string Type { get; set; }

        [DisplayName("Dish price"), Required(ErrorMessage = "Price is mandatory")]
        public decimal Price { get; set; }

        public IEnumerable<SelectListItem> AllowedDishTypes;
    }
}