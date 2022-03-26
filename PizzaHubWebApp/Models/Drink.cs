using System;
using System.Collections.Generic;

#nullable disable

namespace PizzaHubWebApp.Models
{
    public partial class Drink
    {
        public int DrinkId { get; set; }
        public string DrinkName { get; set; }
        public string Brand { get; set; }
        public string Image { get; set; }
    }
}
