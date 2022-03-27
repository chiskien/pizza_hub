using System.Collections.Generic;
using System.Linq;
using PizzaHubWebApp.Models;

namespace PizzaHubWebApp.DAO
{
    public class OrderDetailDao
    {
        private readonly PizzaHubContext _pizzaHubContext;

        public OrderDetailDao(PizzaHubContext pizzaHubContext)
        {
            _pizzaHubContext = pizzaHubContext;
        }

        public OrdersDetail GetOrderDetailByOrderId(int orderId)
        {
            return null;
        }
    }
}