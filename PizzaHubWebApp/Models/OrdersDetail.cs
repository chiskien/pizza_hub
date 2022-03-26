using System;
using System.Collections.Generic;

#nullable disable

namespace PizzaHubWebApp.Models
{
    public partial class OrdersDetail
    {
        public int OrderId { get; set; }
        public int PizzaId { get; set; }
        public int? DrinkId { get; set; }
        public int? SizeId { get; set; }
        public int? BaseId { get; set; }
        public int? Quantity { get; set; }
        public double? Discount { get; set; }
        public decimal TotalPrice { get; set; }

        public virtual PizzaBasis Base { get; set; }
        public virtual Drink Drink { get; set; }
        public virtual Order Order { get; set; }
        public virtual Pizza Pizza { get; set; }
        public virtual Size Size { get; set; }
    }
}
