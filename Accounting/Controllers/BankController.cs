using Accounting.DATA.DataAccess;
using Accounting.DATA.Entity;
using Accounting.DATA.Helper;
using Accounting.DATA.Model;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Accounting.Controllers
{
    [SessionControl]
    public class BankController : Controller
    {
        public IActionResult Index()
        {
            var user = HttpContext.Session.User();
            var data = BankDataAccess.GetList(user.Id);
            return View(data);
        }

        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Add(Bank model)
        {
            var user = HttpContext.Session.User();

            model.UserId = user.Id;
            model.CreatedOn = DateTime.Now;
            model.IsDeleted = false;

            var data = BankDataAccess.Add(model);
            if (data.Status == true)
            {
                return RedirectToAction("Index", "Bank");
            }
            return View();
        }

        public IActionResult Delete(Guid id)
        {
            var data = BankDataAccess.Delete(id);
            return RedirectToAction("Index");
        }

        public IActionResult Update(Guid BankId)
        {
            var user = HttpContext.Session.User();
            var data = BankDataAccess.GetById(BankId, user.Id);
            return View(data);
        }

        [HttpPost]
        public IActionResult Update(Bank model)
        {
            var data = BankDataAccess.Update(model);
            if (data.Status == true)
            {
                return RedirectToAction("Index");
            }
            return View(model);
        }

        public IActionResult Detail(Guid BankId)
        {
            var user = HttpContext.Session.User();
            var data = BankDataAccess.GetById(BankId, user.Id);
            return View(data);
        }

        public IActionResult Calculate(Guid BankId)
        {
            var user = HttpContext.Session.User();
            var response = BankDataAccess.Calculate(BankId, user.Id, new DateFilter() { StartDate = DateTime.Now.AddDays(-10), EndDate = DateTime.Now });
            return Json(response);
        }

        public IActionResult GetListDetail(Guid BankId)
        {
            var user = HttpContext.Session.User();
            var response = BankDataAccess.GetListDetail(BankId, user.Id, new DateFilter() { StartDate = DateTime.Now.AddDays(-10), EndDate = DateTime.Now });
            return PartialView(response);
        }
    }
}
