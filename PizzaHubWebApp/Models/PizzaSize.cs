using System;
using System.Collections.Generic;

#nullable disable

namespace PizzaHubWebApp.Models
{
    public partial class PizzaSize
    {
        public int SizeId { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
    }
}
