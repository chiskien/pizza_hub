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
            return _pizzaHubContext.Members.ToList();
        }
    }
}