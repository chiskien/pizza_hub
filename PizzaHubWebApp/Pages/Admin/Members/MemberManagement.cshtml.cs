using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PizzaHubWebApp.DAO;
using PizzaHubWebApp.Models;

namespace PizzaHubWebApp.Pages.Admin.Members
{
    public class MemberManagement : PageModel
    {
        private readonly MemberDao _memberDao;


        public MemberManagement(PizzaHubContext context)
        {
            _memberDao = new MemberDao(context);
        }

        public void OnGet()
        {
            Members = _memberDao.GetAllMembers();
        }

        public IEnumerable<Member> Members { get; set; }
        public IActionResult OnPostDelete(int id)
        {
            var d = _memberDao.GetMemberById(id);
            if (d != null)
            {
                _memberDao.DeleteMember(d);
            }
            return Redirect("/Admin/Members/MemberManagement");
        }
    }
}