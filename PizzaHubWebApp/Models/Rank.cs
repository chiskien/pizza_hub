using System;
using System.Collections.Generic;

#nullable disable

namespace PizzaHubWebApp.Models
{
    public partial class Rank
    {
        public Rank()
        {
            Members = new HashSet<Member>();
        }

        public int RankId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public virtual ICollection<Member> Members { get; set; }
    }
}
