using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using PizzaHubWebApp.Models;

namespace PizzaHubWebApp.DAO
{
    public class OrderDao
    {
        private readonly StatusDao _statusDao;
        private readonly MemberDao _memberDao;
        private readonly PizzaHubContext _context;

        public OrderDao(PizzaHubContext context)
        {
            _context = context;
            _statusDao = new StatusDao(context);
            _memberDao = new MemberDao(context);
        }

        public IEnumerable<Order> GetAllOrders()
        {
            var orderList = _context.Orders.ToList();
            foreach (var order in orderList)
            {
                order.Status = _statusDao.GetStatusById(order.StatusId);
                if (order.MemberId.HasValue)
                {
                    order.Member = _memberDao.GetMemberById(order.MemberId.Value);
                }
            }
            return orderList;
        }

        public IEnumerable<Order> GetOrdersByMemberId(int memberId)
        {
            var orderList = _context.Orders
                .Where(o => o.MemberId == memberId)
                .ToList();
            foreach (var order in orderList)
                order.Status = _statusDao.GetStatusById(order.StatusId);
            return orderList;
        }

        public Order GetOrderById(int orderId)
        {
            Order order = null;
            order = _context.Orders.FirstOrDefault(o => o.OrderId == orderId);
            if (order.MemberId.HasValue)
            {
                order.Member = _memberDao.GetMemberById(order.MemberId.Value);
            }
            order.Status = _statusDao.GetStatusById(order.StatusId);
            return order;
        }

        public Order GetOrderByIdNoTracking(int orderId)
        {
            return _context.Orders.AsNoTracking().Single(o => o.OrderId == orderId);
        }

        public void AddOrder(Order order, OrdersDetail ordersDetail)
        {
            _context.Orders.Add(order);
            order.OrderId = order.OrderId;
            _context.OrdersDetails.Add(ordersDetail);
            _context.SaveChanges();
        }

        public void UpdateOrder(Order order)
        {
            var existedOrder = GetOrderByIdNoTracking(order.OrderId);
            if (existedOrder == null) return;
            _context.Entry(order).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public void DeleteOrder(Order order)
        {
            var existedOrder = GetOrderByIdNoTracking(order.OrderId);
            if (existedOrder == null) return;
            _context.Remove(order);
            _context.SaveChanges();
        }

        public IEnumerable<Order> GetAllOrderByStatus(int statusId)
        {
            return _context.Orders.Where(s => s.StatusId == statusId)
                .ToList();
        }
    }
}