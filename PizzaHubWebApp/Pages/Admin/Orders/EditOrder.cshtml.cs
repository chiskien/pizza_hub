using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PizzaHubWebApp.DAO;
using PizzaHubWebApp.Models;

namespace PizzaHubWebApp.Pages.Admin.Orders
{
    public class EditOrderModel : PageModel
    {
        public Order Order { get; set; }
        private readonly OrderDao _orderDao;
        private readonly StatusDao _statusDao;
        public IEnumerable<Status> Statuses { get; set; }

        public EditOrderModel(PizzaHubContext pizzaHubContext)
        {
            _orderDao = new OrderDao(pizzaHubContext);
            _statusDao = new StatusDao(pizzaHubContext);
        }

        public void OnGet(string id)
        {
            try
            {
                var orderid = int.Parse(id);
                Order = _orderDao.GetOrderById(orderid);
                Statuses = _statusDao.GetAllStatus();
            }
            catch (Exception)
            {
                Response.Redirect("/Admin/Orders/OrderManagement");
            }
        }

        public IActionResult OnPostEdit(int id, string address, DateTime orderdate, DateTime requireddate, DateTime shippeddate, int status)
        {
            try
            {
                var order = _orderDao.GetOrderById(id);
                if (order != null)
                {
                    order.Address = address;

                    if (orderdate > System.Data.SqlTypes.SqlDateTime.MinValue.Value && orderdate < System.Data.SqlTypes.SqlDateTime.MaxValue.Value)
                    {
                        order.OrderDate = orderdate;
                    }
                    if (requireddate > System.Data.SqlTypes.SqlDateTime.MinValue.Value && requireddate < System.Data.SqlTypes.SqlDateTime.MaxValue.Value)
                    {
                        order.RequiredDate = requireddate;
                    }
                    if (shippeddate > System.Data.SqlTypes.SqlDateTime.MinValue.Value && shippeddate < System.Data.SqlTypes.SqlDateTime.MaxValue.Value)
                    {
                        order.ShippedDate = shippeddate;
                    }
                    order.StatusId = status;
                    _orderDao.UpdateOrder(order);
                    return Redirect("/Admin/Orders/OrderManagement");
                }
                else
                {
                    return Redirect("/Admin/Orders/OrderManagement");
                }
            }
            catch (Exception)
            {
                return Redirect("/Admin/Orders/OrderManagement");
            }
        }
    }
}
