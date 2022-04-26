using System;
using System.Collections.Generic;
using System.Text;

namespace Restaurant.Core.Entities
{
    public class Dish
    {
        public int ID { get; set; }

        public string Name { get; set; }

        public DishType Type { get; set; }

        public decimal Price { get; set; }
    }
}

public enum DishType : byte
{
    Starter,
    FirstCourse,
    MainCourse,
    SideDish,
    Dessert,
    Unassigned
}