﻿using System;
using System.Collections.Generic;

#nullable disable

namespace PizzaHubWebApp.Models
{
    public partial class Member
    {
        public Member()
        {
            Orders = new HashSet<Order>();
        }

        public int MemberId { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Avatar { get; set; }
        public DateTime Dob { get; set; }
        public string MobileNumber { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public int RankId { get; set; }
        public double? Point { get; set; }
        public int? Voucher { get; set; }
        public bool? Role { get; set; }

        public virtual Rank Rank { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
    }
}
