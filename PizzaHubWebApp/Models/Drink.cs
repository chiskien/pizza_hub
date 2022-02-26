using System;
using System.Collections.Generic;

#nullable disable

namespace PizzaHubWebApp.Models
{
    public partial class Drink
    {
        public int DrinkId { get; set; }
        public string Name { get; set; }
        public string Brand { get; set; }
        public string Image { get; set; }
        public int SizeId { get; set; }
        public decimal Price { get; set; }

        public virtual Size Size { get; set; }
    }
}
