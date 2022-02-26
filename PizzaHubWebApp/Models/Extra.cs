using System;
using System.Collections.Generic;

#nullable disable

namespace PizzaHubWebApp.Models
{
    public partial class Extra
    {
        public int ExtraId { get; set; }
        public string ExtraName { get; set; }
        public string Image { get; set; }
        public decimal Price { get; set; }
    }
}
