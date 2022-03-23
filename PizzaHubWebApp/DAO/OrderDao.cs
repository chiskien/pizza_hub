using System.Collections.Generic;
using System.Linq;
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
            var listOrder = _context.Orders.ToList();
            var dao = new MemberDao(_context);
            foreach (var order in listOrder) order.Member = dao.GetMemberById(order.MemberId);
            return listOrder;
        }

        public Order GetOrderById(int id)
        {
            var order = _context.Orders.Single(o => o.OrderId == id);
            return order;
        }
    }
}