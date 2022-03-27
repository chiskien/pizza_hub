using System;
using System.Collections.Generic;

#nullable disable

namespace PizzaHubWebApp.Models
{
    public partial class Size
    {
        public Size()
        {
            OrderDetails = new HashSet<OrderDetail>();
        }

        public int SizeId { get; set; }
        public string Size1 { get; set; }

        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
    }
}
