using System;
using System.Collections.Generic;

#nullable disable

namespace PizzaHubWebApp.Models
{
    public partial class PizzaBase
    {
        public int PizzaId { get; set; }
        public int BaseId { get; set; }

        public virtual Base Base { get; set; }
        public virtual Pizza Pizza { get; set; }
    }
}
