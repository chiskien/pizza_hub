using System.Collections.Generic;
using System.Linq;
using PizzaHubWebApp.Models;

namespace PizzaHubWebApp.DAO
{
    public class SauceDao
    {
        private readonly PizzaHubContext _context;

        public SauceDao(PizzaHubContext context)
        {
            _context = context;
        }

        public IEnumerable<Sauce> GetAllSauces()
        {
            return _context.Sauces.ToList();
        }
    }
}