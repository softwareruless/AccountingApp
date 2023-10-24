using Accounting.DATA.DataAccess;
using Accounting.DATA.Entity;
using Accounting.DATA.Helper;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Accounting.Controllers
{
    [SessionControl]
    public class ExpenseTitleController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult GetList()
        {
            var user = HttpContext.Session.User();
            var data = ExpenseTitleDataAccess.GetList(user.Id);
            return PartialView(data);
        }

        [HttpPost]
        public IActionResult Add(ExpenseTitle expenseTitle)
        {
            var user = HttpContext.Session.User();

            expenseTitle.UserId = user.Id;
            expenseTitle.CreatedOn = DateTime.Now;
            expenseTitle.IsDeleted = false;

            var data = ExpenseTitleDataAccess.Add(expenseTitle);
            return Json(data);
        }

        public IActionResult Update(Guid ExpenseTitleId)
        {
            var user = HttpContext.Session.User();
            var data = ExpenseTitleDataAccess.GetById(ExpenseTitleId, user.Id);
            return PartialView(data);
        }

        [HttpPost]
        public IActionResult Update(ExpenseTitle expenseTitle)
        {
            var user = HttpContext.Session.User();

            expenseTitle.UserId = user.Id;

            var data = ExpenseTitleDataAccess.Update(expenseTitle);
            return Json(data);
        }

        public IActionResult Delete(Guid ExpenseTitleId)
        {
            var user = HttpContext.Session.User();
            var data = ExpenseTitleDataAccess.Delete(ExpenseTitleId, user.Id);
            return Json(data);
        }

        public IActionResult Detail(Guid ExpenseTitleId)
        {
            var user = HttpContext.Session.User();
            var data = ExpenseTitleDataAccess.GetById(ExpenseTitleId, user.Id);
            ViewBag.Safes = SafeDataAccess.GetList(user.Id);
            ViewBag.Banks = BankDataAccess.GetList(user.Id);
            return View(data);
        }
    }
}
