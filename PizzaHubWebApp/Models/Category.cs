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
        }

        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public string Image { get; set; }

        public virtual ICollection<Pizza> Pizzas { get; set; }
    }
}
