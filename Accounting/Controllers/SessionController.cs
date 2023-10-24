using Accounting.DATA.DataAccess;
using Accounting.DATA.Entity;
using Accounting.DATA.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Accounting.Controllers
{
    public class SessionController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(string email, string password)
        {
            var data = UserDataAccess.Login(email, password);
            if (data != null)
            {
                var session = new SessionDto()
                {
                    Id = data.Id,
                    Name = data.Name,
                    Surname = data.Surname,
                    Email = data.Email,
                    Phone = data.Phone,
                    Username = data.Username,
                    Password = data.Password,
                    Status = data.Status,
                    CreatedOn = data.CreatedOn,
                    ActivationDate = data.ActivationDate,
                    RoleId = data.RoleId,
                };

                HttpContext.Session.SetString("_User", JsonConvert.SerializeObject(session));
                return RedirectToAction("Index", "Home");

            }
            ViewBag.Hata = "Bir Hata Oluştu Tekrar Deneyiniz";
            return View();
        }

        [HttpPost]
        public IActionResult LogOut()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }
    }
}
