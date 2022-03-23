using System.Collections.Generic;
using System.Linq;
using PizzaHubWebApp.Models;

namespace PizzaHubWebApp.DAO
{
    public class RankDao
    {
        private readonly PizzaHubContext _context;

        public RankDao(PizzaHubContext context)
        {
            _context = context;
        }

        public IEnumerable<Rank> GetAllRanks()
        {
            var rankList = _context.Ranks.ToList();
            return rankList;
        }

        public Rank GetRankByRankId(int? id)
        {
            var rank = _context.Ranks.Single(r => r.RankId == id);
            return rank;
        }
    }
}