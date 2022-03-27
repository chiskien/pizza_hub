using System;
using System.Collections.Generic;

#nullable disable

namespace PizzaHubWebApp.Models
{
    public partial class Order
    {
        public Order()
        {
            OrderDetails = new HashSet<OrderDetail>();
        }

        public int OrderId { get; set; }
        public int? MemberId { get; set; }
        public DateTime OrderDate { get; set; }
        public string Address { get; set; }
        public int StatusId { get; set; }
        public decimal? Freight { get; set; }
        public DateTime? RequiredDate { get; set; }
        public DateTime? ShippedDate { get; set; }
        public string Note { get; set; }

        public virtual Member Member { get; set; }
        public virtual Status Status { get; set; }
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
    }
}
