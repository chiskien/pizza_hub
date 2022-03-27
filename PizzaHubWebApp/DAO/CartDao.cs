using System.Collections.Generic;
using System.Linq;
using PizzaHubWebApp.Models;

namespace PizzaHubWebApp.DAO
{
    public class CartDao
    {
        private readonly PizzaHubContext _context;

        public CartDao(PizzaHubContext context)
        {
            _context = context;
        }

        //return cart by user
        public IEnumerable<Cart> GetCartsByMemberId(int memberId)
        {
            var carts = _context.Carts
                .Where(u => u.MemberId == memberId)
                .ToList();
            return carts;
        }
    }
}