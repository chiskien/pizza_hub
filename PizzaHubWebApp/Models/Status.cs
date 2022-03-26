using System;
using System.Collections.Generic;

#nullable disable

namespace PizzaHubWebApp.Models
{
    public partial class Status
    {
        public Status()
        {
            Orders = new HashSet<Order>();
            Pizzas = new HashSet<Pizza>();
        }

        public int StatusId { get; set; }
        public string Status1 { get; set; }

        public virtual ICollection<Order> Orders { get; set; }
        public virtual ICollection<Pizza> Pizzas { get; set; }
    }
}
