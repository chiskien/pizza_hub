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
        public Member GetMemberByEmail(string email)
        {
            Member member = null;
            member = _pizzaHubContext.Members.FirstOrDefault(m => m.Email == email);
            return member;
        }
        public void AddMember(Member member)
        {
            _pizzaHubContext.Members.Add(member);
            _pizzaHubContext.SaveChanges();
        }
    }
}