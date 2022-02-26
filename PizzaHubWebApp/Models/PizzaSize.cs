using System;
using System.Collections.Generic;

#nullable disable

namespace PizzaHubWebApp.Models
{
    public partial class PizzaSize
    {
        public int PizzaId { get; set; }
        public int SizeId { get; set; }
        public decimal Price { get; set; }

        public virtual Pizza Pizza { get; set; }
        public virtual Size Size { get; set; }
    }
}
