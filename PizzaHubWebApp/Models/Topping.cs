using System;
using System.Collections.Generic;

#nullable disable

namespace PizzaHubWebApp.Models
{
    public partial class Topping
    {
        public int ToppingId { get; set; }
        public string ToppingName { get; set; }
        public int? CategoryId { get; set; }
        public string Image { get; set; }
        public decimal? UnitPrice { get; set; }
    }
}
