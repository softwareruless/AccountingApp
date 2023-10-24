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
    public class ExpenseController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Add(Expense expense)
        {
            var user = HttpContext.Session.User();
            expense.UserId = user.Id;
            var data = ExpenseDataAccess.Add(expense);
            return Json(data);
        }

        public IActionResult GetList(Guid ExpenseTitleId)
        {
            var user = HttpContext.Session.User();
            var data = ExpenseDataAccess.GetList(user.Id, ExpenseTitleId);
            return PartialView(data);
        }

        public IActionResult Update(Guid ExpenseId)
        {
            var user = HttpContext.Session.User();
            var data = ExpenseDataAccess.GetById(ExpenseId, user.Id);
            ViewBag.Safes = SafeDataAccess.GetList(user.Id);
            ViewBag.Banks = BankDataAccess.GetList(user.Id);
            return PartialView(data);
        }

        [HttpPost]
        public IActionResult Update(Expense expense)
        {
            var user = HttpContext.Session.User();

            expense.UserId = user.Id;

            var data = ExpenseDataAccess.Update(expense);
            return Json(data);
        }

        public IActionResult Delete(Guid ExpenseId)
        {
            var user = HttpContext.Session.User();
            var data = ExpenseDataAccess.Delete(ExpenseId, user.Id);
            return Json(data);
        }

        public IActionResult PayView(Guid ExpenseId)
        {
            var user = HttpContext.Session.User();
            var data = ExpenseDataAccess.GetById(ExpenseId, user.Id);
            ViewBag.Safes = SafeDataAccess.GetList(user.Id);
            return PartialView(data);
        }

        [HttpPost]
        public IActionResult Pay(Payment payment)
        {
            var user = HttpContext.Session.User();
            payment.UserId = user.Id;
            payment.Date = DateTime.Now;
            var data = ExpenseDataAccess.Pay(payment);
            return Json(data);
        }

        public IActionResult GetBalance(Guid ExpenseTitleId)
        {
            var user = HttpContext.Session.User();
            var data = ExpenseDataAccess.CalculateBalance(ExpenseTitleId, user.Id);
            return Json(data);
        }
    }
}
