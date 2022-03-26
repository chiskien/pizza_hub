using System.Collections.Generic;
using System.Linq;
using PizzaHubWebApp.Models;

namespace PizzaHubWebApp.DAO
{
    public class StatusDao
    {
        private readonly PizzaHubContext _pizzaHubContext;

        public StatusDao(PizzaHubContext pizzaHubContext)
        {
            _pizzaHubContext = pizzaHubContext;
        }

        public Status GetStatusById(int? statusId)
        {
            var status = _pizzaHubContext.Statuses
                .SingleOrDefault(c => c.StatusId == statusId);
            return status;
        }

        public IEnumerable<Status> GetAllStatus()
        {
            return _pizzaHubContext.Statuses.ToList();
        }
    }
}