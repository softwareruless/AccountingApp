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
    public class PaymentController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Create(Guid CustomerId, PaymentType Type, string? amount, Guid? InvoiceId, Guid? InstallmentId, Guid? CheckId)
        {
            var user = HttpContext.Session.User();
            var Customer = CustomerDataAccess.GetById(CustomerId, user.Id);
            ViewBag.Safes = SafeDataAccess.GetList(user.Id);
            ViewBag.Banks = BankDataAccess.GetList(user.Id);
            ViewBag.Type = Type;
            if (amount != null)
            {
                ViewBag.amount = Math.Round(Double.Parse(amount), 2).ToString().Replace(",", ".");
            }
            if (InvoiceId != null)
            {
                ViewBag.InvoiceId = InvoiceId;
            }
            if (InstallmentId != null)
            {
                ViewBag.InstallmentId = InstallmentId;
            }
            if (CheckId != null)
            {
                ViewBag.CheckId = CheckId;
            }

            return View(Customer);
        }

        [HttpPost]
        public IActionResult Create(Payment payment)
        {
            var user = HttpContext.Session.User();
            payment.UserId = user.Id;
            var data = PaymentDataAccess.Add(payment);
            return Json(data);
        }

        public IActionResult CalculateCurrentByCustomer(Guid CustomerId)
        {
            var user = HttpContext.Session.User();
            var Balance = PaymentDataAccess.CalculateCurrentByCustomer(CustomerId, user.Id);
            return Json(Balance);
        }

        public IActionResult Update(Guid PaymentId)
        {
            var user = HttpContext.Session.User();
            var data = PaymentDataAccess.GetById(PaymentId, user.Id);
            return PartialView(data);
        }

        [HttpPost]
        public IActionResult Update(Payment payment)
        {
            var user = HttpContext.Session.User();
            payment.UserId = user.Id;
            var data = PaymentDataAccess.Update(payment);
            return Json(data);
        }

        public IActionResult Delete(Guid PaymentId)
        {
            var user = HttpContext.Session.User();
            var data = PaymentDataAccess.Delete(PaymentId, user.Id);
            return Json(data);
        }
    }
}
