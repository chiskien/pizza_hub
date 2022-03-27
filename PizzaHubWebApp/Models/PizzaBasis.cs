using System;
using System.Collections.Generic;

#nullable disable

namespace PizzaHubWebApp.Models
{
    public partial class PizzaBasis
    {
        public PizzaBasis()
        {
            Carts = new HashSet<Cart>();
            OrdersDetails = new HashSet<OrdersDetail>();
        }

        public int BaseId { get; set; }
        public string Base { get; set; }

        public virtual ICollection<Cart> Carts { get; set; }
        public virtual ICollection<OrdersDetail> OrdersDetails { get; set; }
    }
}
