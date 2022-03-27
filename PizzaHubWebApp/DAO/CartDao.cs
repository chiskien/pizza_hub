using System;
using System.Collections.Generic;
using System.Linq;
using PizzaHubWebApp.Models;

namespace PizzaHubWebApp.DAO
{
    public class CartDao
    {
        private readonly PizzaHubContext _context;

        public CartDao(PizzaHubContext context)
        {
            _context = context;
        }

        //return cart by user
        public IEnumerable<Cart> GetCartsByMemberId(int memberId)
        {
            var carts = _context.Carts
                .Where(u => u.MemberId == memberId)
                .ToList();
            return carts;
        }

        public void AddToCart(int pizzaId, int memeberId, int amount)
        {
            //find if pizza has already in cart
            var carts = GetCartsByMemberId(memeberId);
            foreach (var cart in carts)
                if (cart.PizzaId == pizzaId)
                {
                    var existedCart = _context.Carts
                        .Single(c => c.PizzaId == pizzaId && c.MemberId == memeberId);
                    existedCart.Amount += amount;
                    _context.SaveChanges();
                }

            var newCart = new Cart
            {
                MemberId = memeberId,
                PizzaId = pizzaId,
                Amount = amount
            };
            _context.Carts.Add(newCart);
            _context.SaveChanges();
        }

        public void CheckOut(Cart cart, string address, string note)
        {
            var order = new Order
            {
                Address = address,
                MemberId = cart.MemberId,
                StatusId = 3,
                Note = note,
                OrderDate = DateTime.Now
            };
            var orderDao = new OrderDao(_context);
            orderDao.AddOrder(order);
        }
    }
}