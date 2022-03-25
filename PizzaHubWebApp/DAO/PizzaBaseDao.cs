using System.Collections.Generic;
using System.Linq;
using PizzaHubWebApp.Models;

namespace PizzaHubWebApp.DAO
{
    public class PizzaBaseDao
    {
        private readonly PizzaHubContext _context;

        public PizzaBaseDao(PizzaHubContext context)
        {
            _context = context;
        }

        public IEnumerable<Base> GetAllBase()
        {
            return _context.Bases.ToList();
        }
    }
}