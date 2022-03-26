using System.Collections.Generic;
using PizzaHubWebApp.Models;

namespace PizzaHubWebApp.DAO
{
    public class OrderDao
    {
        private readonly PizzaHubContext _context;

        public OrderDao(PizzaHubContext context)
        {
            _context = context;
        }

        public IEnumerable<Order> GetAllOrders()
        {
            throw new System.NotImplementedException();
        }
    }
}