using System.Collections.Generic;
using System.Linq;
using PizzaHubWebApp.Models;

namespace PizzaHubWebApp.DAO
{
    public class PizzaSizeDao
    {
        private readonly PizzaHubContext _context;

        public PizzaSizeDao(PizzaHubContext context)
        {
            _context = context;
        }

        public decimal GetPriceByPizzaIdandSize(int pizzaId, int sizeId)
        {
            return 0;
        }

        public IEnumerable<PizzaSize> GetAllSizeAndPriceByPizzaId(int pizzaId)
        {
            var pizzaSizes = _context.PizzaSizes
                .Where(p => p.PizzaId == pizzaId)
                .ToList();
            return pizzaSizes;
        }

        public IEnumerable<Size> GetAllSize()
        {
            return _context.Sizes.ToList();
        }

        public void AddPizzaSizes(List<PizzaSize> pizzaSizes)
        {
            _context.PizzaSizes.AddRange(pizzaSizes);
        }
    }
}