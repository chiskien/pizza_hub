using System;
using System.Collections.Generic;

#nullable disable

namespace PizzaHubWebApp.Models
{
    public partial class Voucher
    {
        public int VoucherId { get; set; }
        public string VoucherCode { get; set; }
        public string Description { get; set; }
        public int Discount { get; set; }
        public int Quantity { get; set; }
    }
}
