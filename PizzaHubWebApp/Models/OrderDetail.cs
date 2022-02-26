using System;
using System.Collections.Generic;

#nullable disable

namespace PizzaHubWebApp.Models
{
    public partial class OrderDetail
    {
        public int OrderId { get; set; }
        public int? PizzaId { get; set; }
        public int? DrinkId { get; set; }
        public int? ExtraId { get; set; }
        public string Size { get; set; }
        public decimal? Price { get; set; }
        public int Quantity { get; set; }
        public double? Discount { get; set; }

        public virtual Drink Drink { get; set; }
        public virtual Extra Extra { get; set; }
        public virtual Order Order { get; set; }
        public virtual Pizza Pizza { get; set; }
    }
}
