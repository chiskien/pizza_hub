using System;
using System.Collections.Generic;

#nullable disable

namespace PizzaHubWebApp.Models
{
    public partial class MemberVoucher
    {
        public int VoucherId { get; set; }
        public int MemberId { get; set; }
        public int Quantity { get; set; }

        public virtual Member Member { get; set; }
        public virtual Voucher Voucher { get; set; }
    }
}
