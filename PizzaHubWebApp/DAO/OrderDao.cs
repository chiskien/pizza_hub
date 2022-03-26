using System.Collections.Generic;
using System.Linq;
using PizzaHubWebApp.Models;

namespace PizzaHubWebApp.DAO
{
    public class OrderDao
    {
        private readonly StatusDao _statusDao;
        private readonly PizzaHubContext _context;

        public OrderDao(PizzaHubContext context)
        {
            _context = context;
            _statusDao = new StatusDao(context);
        }

        public IEnumerable<Order> GetAllOrders()
        {
            var orderList = _context.Orders.ToList();
            foreach (var order in orderList)
                order.Status = _statusDao.GetStatusById(order.StatusId);
            return orderList;
        }
    }
}