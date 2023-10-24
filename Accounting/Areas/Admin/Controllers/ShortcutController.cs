using Accounting.DATA.DataAccess;
using Accounting.DATA.Entity;
using Accounting.DATA.Helper;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Accounting.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ShortcutController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult GetList()
        {
            var data = ShortcutDataAccess.GetList();
            return PartialView(data);
        }

        [HttpPost]
        public IActionResult Add(Shortcut shortcut)
        {
            shortcut.CreatedOn = DateTime.Now;
            shortcut.IsDeleted = false;

            var data = ShortcutDataAccess.Add(shortcut);
            return Json(data);
        }

        public IActionResult Update(Guid ShortcutId)
        {
            var data = ShortcutDataAccess.GetById(ShortcutId);
            return PartialView(data);
        }

        [HttpPost]
        public IActionResult Update(Shortcut shortcut)
        {
            var data = ShortcutDataAccess.Update(shortcut);
            return Json(data);
        }

        public IActionResult Delete(Guid ShortcutId)
        {
            var data = ShortcutDataAccess.Delete(ShortcutId);
            return Json(data);
        }

        public IActionResult Detail(Guid ShortcutId)
        {
            var data = ShortcutDataAccess.GetById(ShortcutId);
            return PartialView(data);
        }


    }
}
