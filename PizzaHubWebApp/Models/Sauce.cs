using System;
using System.Collections.Generic;

#nullable disable

namespace PizzaHubWebApp.Models
{
    public partial class Sauce
    {
        public Sauce()
        {
            Pizzas = new HashSet<Pizza>();
        }

        public int SauceId { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Pizza> Pizzas { get; set; }
    }
}
