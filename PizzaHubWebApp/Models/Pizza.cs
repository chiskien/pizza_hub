using System;
using System.Collections.Generic;

#nullable disable

namespace PizzaHubWebApp.Models
{
    public partial class Pizza
    {
        public Pizza()
        {
<<<<<<< HEAD
            OrdersDetails = new HashSet<OrdersDetail>();
=======
            OrderDetails = new HashSet<OrderDetail>();
>>>>>>> e34a30b89664f92c851f73bcf276ca6347ec97dd
            PizzaToppingDetails = new HashSet<PizzaToppingDetail>();
        }

        public int PizzaId { get; set; }
        public int? CategoryId { get; set; }
        public int? SauceId { get; set; }
        public string Image { get; set; }
        public string Description { get; set; }
        public int? StatusId { get; set; }
        public decimal Price { get; set; }
        public string PizzaName { get; set; }

        public virtual Category Category { get; set; }
        public virtual Sauce Sauce { get; set; }
        public virtual Status Status { get; set; }
        public virtual ICollection<OrdersDetail> OrdersDetails { get; set; }
        public virtual ICollection<PizzaToppingDetail> PizzaToppingDetails { get; set; }
    }
}
