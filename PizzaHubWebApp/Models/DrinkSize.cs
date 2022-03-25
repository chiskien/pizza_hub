﻿using System;
using System.Collections.Generic;

#nullable disable

namespace PizzaHubWebApp.Models
{
    public partial class DrinkSize
    {
        public DrinkSize()
        {
            Drinks = new HashSet<Drink>();
        }

        public int SizeId { get; set; }
        public string Name { get; set; }
        public decimal? Price { get; set; }

        public virtual ICollection<Drink> Drinks { get; set; }
    }
}
