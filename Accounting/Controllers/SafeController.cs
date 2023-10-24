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
    public class SafeController : Controller
    {
        public IActionResult Index()
        {
            var user = HttpContext.Session.User();
            var data = SafeDataAccess.GetList(user.Id);
            return View(data);
        }

        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Add(Safe model)
        {
            var user = HttpContext.Session.User();

            model.Id = Guid.NewGuid();
            model.UserId = user.Id;
            model.IsActive = true;
            model.CreatedOn = DateTime.Now;
            model.IsDeleted = false;

            var data = SafeDataAccess.Add(model);
            if (data.Status == true)
            {
                return RedirectToAction("Index", "Safe");
            }
            return View();
        }

        public IActionResult Delete(Guid id)
        {
            var data = SafeDataAccess.Delete(id);
            return RedirectToAction("Index");
        }

        public IActionResult Update(Guid SafeId)
        {
            var user = HttpContext.Session.User();
            var data = SafeDataAccess.GetById(SafeId, user.Id);
            return View(data);
        }

        [HttpPost]
        public IActionResult Update(Safe model)
        {
            var data = SafeDataAccess.Update(model);
            if (data.Status == true)
            {
                return RedirectToAction("Index");
            }
            return View(model);
        }

        public IActionResult Detail(Guid SafeId)
        {
            var user = HttpContext.Session.User();
            var data = SafeDataAccess.GetById(SafeId, user.Id);
            return View(data);
        }

        public IActionResult Calculate(Guid SafeId)
        {
            var user = HttpContext.Session.User();
            var response = SafeDataAccess.Calculate(SafeId, user.Id, new DateFilter() { StartDate = DateTime.Now.AddDays(-10), EndDate = DateTime.Now });
            return Json(response);
        }

        public IActionResult GetListDetail(Guid SafeId)
        {
            var user = HttpContext.Session.User();
            var response = SafeDataAccess.GetListDetail(SafeId, user.Id, new DateFilter() { StartDate = DateTime.Now.AddDays(-10), EndDate = DateTime.Now });
            return PartialView(response);
        }
    }
}
