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

        public IEnumerable<OrdersDetail> GetOrdersDetailsByPizzaId(int id)
        {
            var ordersDetails = _pizzaHubContext.OrderDetails.Where(o => o.OrderId == id).ToList();
            foreach(var o in ordersDetails)
            {
                o.Order = _pizzaHubContext.Orders.FirstOrDefault(o => o.OrderId == id);
                if (o.PizzaId.HasValue)
                {
                    o.Pizza = _pizzaHubContext.Pizzas.FirstOrDefault(p => p.PizzaId == o.PizzaId);
                }
                else
                {
                    o.Drink = _pizzaHubContext.Drinks.FirstOrDefault(p => p.DrinkId == o.DrinkId);
                }
                o.Size = _pizzaHubContext.Sizes.FirstOrDefault(p => p.SizeId == o.SizeId);
                o.Base = _pizzaHubContext.PizzaBases.FirstOrDefault(p => p.BaseId == o.BaseId);
            }
            return ordersDetails;
        }
    }
}