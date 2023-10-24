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
    public class UserShortcutController : Controller
    {
        public IActionResult Index()
        {
            var Shortcuts = ShortcutDataAccess.GetList();
            return View(Shortcuts);
        }

        public IActionResult GetList()
        {
            var user = HttpContext.Session.User();
            var data = UserShortcutDataAccess.GetList(user.Id);
            return PartialView(data);
        }

        public IActionResult Add()
        {
            var user = HttpContext.Session.User();
            var data = UserShortcutDataAccess.GetListForAdd(user.Id);
            return PartialView(data);
        }

        [HttpPost]
        public IActionResult Add(List<Guid> shortcuts)
        {
            var user = HttpContext.Session.User();
            var data = UserShortcutDataAccess.Add(shortcuts, user.Id);
            return Json(data);
        }

        public IActionResult Delete(Guid UserShortcutId)
        {
            var user = HttpContext.Session.User();
            var data = UserShortcutDataAccess.Delete(UserShortcutId, user.Id);
            return Json(data);
        }

    }
}
