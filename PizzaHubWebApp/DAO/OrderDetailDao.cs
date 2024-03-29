﻿using System.Collections.Generic;
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
            var ordersDetails = _pizzaHubContext.OrdersDetails.Where(o => o.OrderId == id).ToList();
            foreach(var o in ordersDetails)
            {
                double total = 0;
                o.Order = _pizzaHubContext.Orders.FirstOrDefault(o => o.OrderId == id);
                if (o.PizzaId.HasValue)
                {
                    o.Pizza = _pizzaHubContext.Pizzas.FirstOrDefault(p => p.PizzaId == o.PizzaId);
                    total += (double)o.Pizza.Price * o.Quantity.Value;
                    if (o.Discount.HasValue)
                    {
                        total -= (double)o.Pizza.Price * o.Discount.Value;
                    }
                }
                else
                {
                    o.Drink = _pizzaHubContext.Drinks.FirstOrDefault(p => p.DrinkId == o.DrinkId);
                    total += (double)o.Drink.Price * o.Quantity.Value;
                    if (o.Discount.HasValue)
                    {
                        total -= (double)o.Drink.Price * o.Discount.Value;
                    }
                }
                if (o.SizeId.HasValue)
                {
                    total += (o.SizeId.Value - 1) * 50;
                }
                o.Size = _pizzaHubContext.Sizes.FirstOrDefault(p => p.SizeId == o.SizeId);
                o.Base = _pizzaHubContext.PizzaBases.FirstOrDefault(p => p.BaseId == o.BaseId);
                o.TotalPrice = (decimal)total;
            }
            return ordersDetails;
        }
    }
}