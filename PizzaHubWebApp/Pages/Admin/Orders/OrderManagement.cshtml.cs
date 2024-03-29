﻿using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
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
        [BindProperty] public int StatusId { get; set; }

        public void OnGet()
        {
            Orders = _orderDao.GetAllOrders();
            Status = _statusDao.GetAllStatus();
        }

        public void OnPost()
        {
            if (StatusId != 0) Orders = _orderDao.GetAllOrderByStatus(StatusId);
            else
                Orders = _orderDao.GetAllOrders();

            Status = _statusDao.GetAllStatus();
        }
        public IActionResult OnPostDelete(int id)
        {
            var d = _orderDao.GetOrderById(id);
            if (d != null)
            {
                _orderDao.DeleteOrder(d);
            }
            return Redirect("/Admin/Orders/OrderManagement");
        }
    }
}