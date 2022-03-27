using System;
using System.Collections.Generic;

#nullable disable

namespace PizzaHubWebApp.Models
{
    public partial class Size
    {
        public Size()
        {
            OrdersDetails = new HashSet<OrdersDetail>();
        }

        public int SizeId { get; set; }
        public string Size1 { get; set; }

        public virtual ICollection<OrdersDetail> OrdersDetails { get; set; }
    }
}
