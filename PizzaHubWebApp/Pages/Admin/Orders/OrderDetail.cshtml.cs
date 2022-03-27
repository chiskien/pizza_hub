using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PizzaHubWebApp.DAO;
using PizzaHubWebApp.Models;
using System.Collections.Generic;

namespace PizzaHubWebApp.Pages.Admin.Orders
{
    public class OrderDetail : PageModel
    {
        private readonly OrderDao _orderDao;
        private readonly OrderDetailDao _orderDetailDao;
        private readonly PizzaDao _pizzaDao;

        public OrderDetail(PizzaHubContext context)
        {
            _orderDetailDao = new OrderDetailDao(context);
            _pizzaDao = new PizzaDao(context);
            _orderDao = new OrderDao(context);
        }

        public IEnumerable<OrdersDetail> OrdersDetail { get; set; }
        public Order Order { get; set; }

        public void OnGet(int id)
        {
            OrdersDetail = _orderDetailDao.GetOrdersDetailsByPizzaId(id);
            Order = _orderDao.GetOrderById(id);
        }
    }
}