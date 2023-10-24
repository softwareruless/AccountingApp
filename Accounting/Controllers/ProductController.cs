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
    public class ProductController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult GetList()
        {
            var user = HttpContext.Session.User();
            var data = ProductDataAccess.GetList(user.Id);
            return PartialView(data);
        }

        [HttpPost]
        public IActionResult Add(Product model)
        {
            var user = HttpContext.Session.User();

            model.UserId = user.Id;
            model.CreatedOn = DateTime.Now;
            model.IsDeleted = false;

            var data = ProductDataAccess.Add(model);
            return Json(data);
        }
        
        public PartialViewResult Update(Guid id)
        {
            var data = ProductDataAccess.GetById(id);
            return PartialView(data);
        }

        [HttpPost]
        public IActionResult Update(Product model)
        {
            var data = ProductDataAccess.Update(model);
            return Json(data);
        }

        public IActionResult Delete(Guid id)
        {
            var data = ProductDataAccess.Delete(id);
            return Json(data);
        }

        public IActionResult Detail(Guid id)
        {
            var data = ProductDataAccess.GetById(id);
            return PartialView(data);
        }
    }
}
