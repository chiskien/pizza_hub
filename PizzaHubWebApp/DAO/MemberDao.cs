using System.Collections.Generic;
using PizzaHubWebApp.Models;
using System.Linq;

namespace PizzaHubWebApp.DAO
{
    public class MemberDao
    {
        private readonly PizzaHubContext _pizzaHubContext;

        public MemberDao(PizzaHubContext pizzaHubContext)
        {
            _pizzaHubContext = pizzaHubContext;
        }

        public Member GetMemberLogin(string email, string pass)
        {
            Member member = null;
            member = _pizzaHubContext.Members.FirstOrDefault(m => m.Email == email && m.Password == pass);
            return member;
        }

        public IEnumerable<Member> GetAllMembers()
        {
            var rankDao = new RankDao(_pizzaHubContext);
            var members = _pizzaHubContext.Members.ToList();
            foreach (var member in members) member.Rank = rankDao.GetRankByRankId(member.RankId);
            return members;
        }

        public Member GetMemberById(int id)
        {
            return _pizzaHubContext.Members.Single(m => m.MemberId == id);
        }
    }
}