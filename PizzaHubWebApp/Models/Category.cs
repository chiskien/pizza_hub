using System;
using System.Collections.Generic;

#nullable disable

namespace PizzaHubWebApp.Models
{
    public partial class Category
    {
        public Category()
        {
            Pizzas = new HashSet<Pizza>();
            Toppings = new HashSet<Topping>();
        }

        public int CategoryId { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Pizza> Pizzas { get; set; }
        public virtual ICollection<Topping> Toppings { get; set; }
    }
}
