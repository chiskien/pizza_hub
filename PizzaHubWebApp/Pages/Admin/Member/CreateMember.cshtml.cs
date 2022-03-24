using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PizzaHubWebApp.DAO;
using PizzaHubWebApp.Models;

namespace PizzaHubWebApp.Pages.Admin.Member
{
    public class CreateMember : PageModel
    {
        private readonly MemberDao _memberDao;
        public CreateMember(PizzaHubContext pizzaHubContext)
        {
            _memberDao = new MemberDao(pizzaHubContext);
        }
        public void OnGet()
        {
        }
        public void OnPost(string email, string pass, DateTime dob, string phone, string address, string city, string country, IFormFile ava)
        {
            Models.Member member = _memberDao.GetMemberByEmail(email);
            if (member == null)
            {
                Models.Member m = new Models.Member();
                m.Email = email;
                m.Password = pass;
                m.Dob = dob;
                m.MobileNumber = phone;
                m.Address = address;
                m.City = city;
                m.Country = country;
                if(ava != null)
                {
                    ava.CopyTo(new FileStream(System.IO.Path.GetPathRoot(@"..\..\..\") + "wwwroot\\Assets\\Avatar\\Bust\\"+ ava.FileName, FileMode.Create));
                    m.Avatar = ava.FileName;
                }
                else
                {
                    DirectoryInfo d = new DirectoryInfo(System.IO.Path.GetPathRoot(@"..\..\..\") + "wwwroot\\Assets\\Avatar\\Bust");
                    FileInfo[] Files = d.GetFiles("*.svg");
                    Random r = new Random();
                    m.Avatar = Files[r.Next(0, Files.Length)].Name;
                }
                _memberDao.AddMember(m);
                ViewData["AddMessage"] = "Add success";
                Response.Redirect("/Admin/Member/MemberManagement");
            }
            else
            {
                ViewData["AddMessage"] = "Add failed: email has been used";
                Response.Redirect("/Admin/Member/MemberManagement");
            }
        }
    }
}