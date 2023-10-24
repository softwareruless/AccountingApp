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
    public class CustomerController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult GetList()
        {
            var user = HttpContext.Session.User();
            var data = CustomerDataAccess.GetListModel(user.Id);
            return PartialView(data);
        }

        [HttpPost]
        public IActionResult Add(Customer model)
        {
            var user = HttpContext.Session.User();

            model.UserId = user.Id;
            model.CreatedOn = DateTime.Now;
            model.IsDeleted = false;

            var data = CustomerDataAccess.Add(model);
            return Json(data);
        }

        public IActionResult Update(Guid id)
        {
            var user = HttpContext.Session.User();
            var data = CustomerDataAccess.GetById(id,user.Id);
            return PartialView(data);
        }
        
        [HttpPost]
        public IActionResult Update(Customer model)
        {
            var data = CustomerDataAccess.Update(model);
            return Json(data);
        }

        public IActionResult Delete(Guid id)
        {
            var data = CustomerDataAccess.Delete(id);
            return Json(data);
        }

        public IActionResult Detail(Guid id)
        {
            var user = HttpContext.Session.User();
            var data = CustomerDataAccess.GetById(id, user.Id);
            return PartialView(data);
        }

        public IActionResult Current(Guid CustomerId)
        {
            var user = HttpContext.Session.User();
            ViewBag.Customer = CustomerDataAccess.GetById(CustomerId,user.Id);
            ViewBag.Payments = PaymentDataAccess.GetByCustomerId(CustomerId, user.Id);
            ViewBag.Currents = PaymentDataAccess.CalculateCurrentByDateForCustomer(CustomerId, user.Id);
            return View();
        }
    }
}
