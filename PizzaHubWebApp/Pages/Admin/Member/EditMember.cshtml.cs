using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PizzaHubWebApp.DAO;
using PizzaHubWebApp.Models;
using System;
using System.IO;

namespace PizzaHubWebApp.Pages.Admin.Member
{
    public class EditMember : PageModel
    {
        public Models.Member Member { get; set; }
        private readonly MemberDao _memberDao;
        public EditMember(PizzaHubContext pizzaHubContext)
        {
            _memberDao = new MemberDao(pizzaHubContext);
        }
        public void OnGet(string id)
        {
            try
            {
                int memberId = int.Parse(id);
                Member = _memberDao.GetMemberById(memberId);
            }
            catch (Exception)
            {
                Response.Redirect("/Admin/Member/MemberManagement");
            }
        }
        public IActionResult OnPostEdit(int id, string email, string pass, DateTime dob, string phone, string address, string city, string country, IFormFile ava)
        {
            try
            {
                Models.Member member = _memberDao.GetMemberById(id);
                if (member != null)
                {
                    if(email.Trim() != member.Email.Trim())
                    {
                        if(_memberDao.GetMemberByEmail(email.Trim()) != null)
                        {
                            throw new Exception("trung email");
                        }
                        member.Email = email;
                    }
                    member.Password = pass;
                    member.Dob = dob;
                    member.MobileNumber = phone;
                    member.Address = address;
                    member.City = city;
                    member.Country = country;
                    if (ava != null)
                    {
                        try
                        {
                            System.IO.File.Delete(Path.Combine(System.IO.Path.GetPathRoot(@"..\..\..\") + "wwwroot\\Assets\\Avatar\\Bust\\", member.Avatar));
                            ava.CopyTo(new FileStream(System.IO.Path.GetPathRoot(@"..\..\..\") + "wwwroot\\Assets\\Avatar\\Bust\\" + ava.FileName, FileMode.Create));
                            member.Avatar = ava.FileName;
                        }
                        catch (Exception) { }
                    }
                    _memberDao.EditMember(member);
                    ViewData["AddMessage"] = "Add success";
                    return Redirect("/Admin/Member/MemberManagement");
                }
                else
                {
                    ViewData["AddMessage"] = "Add failed: email has been used";
                    return Redirect("/Admin/Member/MemberManagement");
                }
            }
            catch (Exception) {
                    return Redirect("/Admin/Member/MemberManagement");
            }
        }
    }
}