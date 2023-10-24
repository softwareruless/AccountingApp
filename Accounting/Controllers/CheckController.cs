using Accounting.DATA.DataAccess;
using Accounting.DATA.Entity;
using Accounting.DATA.Enums;
using Accounting.DATA.Helper;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Accounting.Controllers
{
    [SessionControl]
    public class CheckController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult GetList()
        {
            var user = HttpContext.Session.User();
            var data = CheckDataAccess.GetList(user.Id);
            return PartialView(data);
        }

        public IActionResult Add(CheckKind Kind)
        {
            var user = HttpContext.Session.User();
            ViewBag.Customer = CustomerDataAccess.GetList(user.Id);
            ViewBag.Kind = Kind;
            return View();
        }

        [HttpPost]
        public IActionResult Add(Check check)
        {
            var user = HttpContext.Session.User();

            check.UserId = user.Id;
            check.CreatedOn = DateTime.Now;
            check.IsDeleted = false;

            var data = CheckDataAccess.Add(check);
            return Json(data);
        }

        public IActionResult Update(Guid CheckId)
        {
            var user = HttpContext.Session.User();
            var data = CheckDataAccess.GetById(CheckId, user.Id);
            ViewBag.Customer = CustomerDataAccess.GetList(user.Id);
            return View(data);
        }

        [HttpPost]
        public IActionResult Update(Check check)
        {
            var user = HttpContext.Session.User();
            check.UserId = user.Id;
            var data = CheckDataAccess.Update(check);
            return Json(data);
        }

        public IActionResult Detail(Guid CheckId)
        {
            var user = HttpContext.Session.User();
            var check = CheckDataAccess.GetById(CheckId, user.Id);
            ViewBag.Customer = CustomerDataAccess.GetList(user.Id);
            return View(check);
        }

        public IActionResult Delete(Guid CheckId)
        {
            var user = HttpContext.Session.User();
            var result = CheckDataAccess.Delete(CheckId, user.Id);
            return Json(result);
        }

        public IActionResult MakeEndorsed(Guid CheckId)
        {
            var user = HttpContext.Session.User();
            var check = CheckDataAccess.GetById(CheckId, user.Id);
            ViewBag.Customer = CustomerDataAccess.GetList(user.Id);
            return View(check);
        }

        [HttpPost]
        public IActionResult MakeEndorsed(Check check)
        {
            var user = HttpContext.Session.User();
            check.UserId = user.Id;
            var data = CheckDataAccess.MakeEndorsed(check);
            return Json(data);
        }

        public IActionResult SendBank(Guid CheckId)
        {
            var user = HttpContext.Session.User();
            var check = CheckDataAccess.GetById(CheckId, user.Id);
            return View(check);
        }

        [HttpPost]
        public IActionResult SendBank(Check check)
        {
            var user = HttpContext.Session.User();
            check.UserId = user.Id;
            var data = CheckDataAccess.SendBank(check);
            return Json(data);
        }
    }
}
