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
        public decimal? Price { get; set; }
        public int Quantity { get; set; }
        public double? Discount { get; set; }
        public int? PizzaSize { get; set; }
        public int? BaseId { get; set; }
        public int? DrinkSize { get; set; }

        public virtual Base Base { get; set; }
        public virtual Drink Drink { get; set; }
        public virtual DrinkSize DrinkSizeNavigation { get; set; }
        public virtual Extra Extra { get; set; }
        public virtual Order Order { get; set; }
        public virtual Pizza Pizza { get; set; }
        public virtual PizzaSize PizzaSizeNavigation { get; set; }
    }
}
