using System;
using System.Collections.Generic;

#nullable disable

namespace PizzaHubWebApp.Models
{
    public partial class PizzaToppingDetail
    {
        public int PizzaId { get; set; }
        public int ToppingId { get; set; }

        public virtual Pizza Pizza { get; set; }
        public virtual Topping Topping { get; set; }
    }
}
