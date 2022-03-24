using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PizzaHubWebApp.DAO;
using PizzaHubWebApp.Models;

namespace PizzaHubWebApp.Pages.Admin.Order
{
    public class OrderManagement : PageModel
    {
        private readonly OrderDao _orderDao;

        public OrderManagement(PizzaHubContext context)
        {
            _orderDao = new OrderDao(context);
        }

        public IEnumerable<Models.Order> Orders { get; set; }

        public void OnGet()
        {
            Orders = _orderDao.GetAllOrders();
        }
    }
}