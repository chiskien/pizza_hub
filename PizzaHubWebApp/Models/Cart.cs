using System;
using System.Collections.Generic;

#nullable disable

namespace PizzaHubWebApp.Models
{
    public partial class Cart
    {
        public int MemberId { get; set; }
        public int PizzaId { get; set; }
        public int? SizeId { get; set; }
        public int? Base { get; set; }
        public int? Amount { get; set; }

        public virtual PizzaBasis BaseNavigation { get; set; }
        public virtual Member Member { get; set; }
        public virtual Pizza Pizza { get; set; }
        public virtual Size Size { get; set; }
    }
}
