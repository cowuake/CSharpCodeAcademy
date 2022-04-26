using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Restaurant.MVC.Models.Menu
{
    public class MenuViewModel
    {
        [DisplayName("Dish ID")]
        public int ID { get; set; }

        [DisplayName("Dish name")]
        public string Name { get; set; }

        [DisplayName("Dish type")]
        public string Type { get; set; }

        [DisplayName("Dish price")]
        public decimal Price { get; set; }

        //[DisplayName("Return Url")]
        //public string ReturnUrl { get; set; }
    }
}