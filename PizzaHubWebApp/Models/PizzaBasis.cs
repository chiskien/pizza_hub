using System;
using System.Collections.Generic;

#nullable disable

namespace PizzaHubWebApp.Models
{
    public partial class PizzaBasis
    {
        public PizzaBasis()
        {
            OrderDetails = new HashSet<OrderDetail>();
        }

        public int BaseId { get; set; }
        public string Base { get; set; }

        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
    }
}
