using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PizzaHubWebApp.DAO;
using PizzaHubWebApp.Models;

namespace PizzaHubWebApp.Pages.Admin.Orders
{
    public class OrderManagement : PageModel
    {
        private readonly OrderDao _orderDao;
        private readonly StatusDao _statusDao;

        public OrderManagement(PizzaHubContext context)
        {
            _statusDao = new StatusDao(context);
            _orderDao = new OrderDao(context);
        }

        public IEnumerable<Order> Orders { get; set; }
        public IEnumerable<Status> Status { get; set; }
        public int StatusId { get; set; }

        public void OnGet()
        {
            Orders = _orderDao.GetAllOrders();
            Status = _statusDao.GetAllStatus();
        }

        public void OnPost()
        {
        }
    }
}