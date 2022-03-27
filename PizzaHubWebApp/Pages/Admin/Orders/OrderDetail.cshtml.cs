using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PizzaHubWebApp.DAO;
using PizzaHubWebApp.Models;

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

        public OrdersDetail OrdersDetail { get; set; }

        public void OnGet(int id)
        {
            OrdersDetail = _orderDetailDao.GetOrderDetailByOrderId(id);
            OrdersDetail.Pizza = _pizzaDao.GetPizzaById(OrdersDetail.PizzaId);
        }
    }
}