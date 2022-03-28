using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PizzaHubWebApp.Models;
using PizzaHubWebApp.DAO;
using Microsoft.AspNetCore.Http;

namespace PizzaHubWebApp.Pages
{
    public class Cart : PageModel
    {
        public Pizza Pizza { get; set; }
        public int Size { get; set; }
        public int Pizzabase { get; set; }
        public int Quantity { get; set; }
        public decimal TotalPrice { get; set; }

        private readonly PizzaDao _pizzaDao;
        private readonly OrderDao _orderDao;

        public Cart(PizzaHubContext pizzaHubContext)
        {
            _pizzaDao = new PizzaDao(pizzaHubContext);
            _orderDao = new OrderDao(pizzaHubContext);
        }
        public void OnGet()
        {
            Pizza = _pizzaDao.GetPizzaById(HttpContext.Session.GetInt32("pizza").Value);
            Size = HttpContext.Session.GetInt32("size").Value;
            Pizzabase = HttpContext.Session.GetInt32("pizzabase").Value;
            Quantity = HttpContext.Session.GetInt32("quantity").Value;
            TotalPrice = Quantity * Pizza.Price;
        }
        public IActionResult OnPost(int id, int size, int pizzabase, int quantity, string address, string note)
        {
            Order o = new Order();
            if (HttpContext.Session.GetInt32("member").HasValue)
            {
                o.MemberId = HttpContext.Session.GetInt32("member").Value;
            }
            o.OrderDate = System.DateTime.Now;
            o.Address = address;
            o.Freight = 10;
            o.Note = note;
            o.StatusId = 3;
            OrdersDetail ordersDetail = new OrdersDetail();
            ordersDetail.PizzaId = id;
            ordersDetail.SizeId = size;
            ordersDetail.BaseId = pizzabase;
            ordersDetail.Quantity = quantity;
            _orderDao.AddOrder(o, ordersDetail);
            return RedirectToPage("Checkout");
        }
    }
}