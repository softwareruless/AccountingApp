using Accounting.DATA.DataAccess;
using Accounting.DATA.Helper;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Accounting.Controllers
{
    [SessionControl]
    public class InstallmentController : Controller
    {
        public IActionResult Index()
        {
            var user = HttpContext.Session.User();
            var data = InstallmentDataAccess.GetList(user.Id);
            return View(data);
        }

        public IActionResult InstallmentPayment(Guid InstallmentId)
        {
            var user = HttpContext.Session.User();
            var data = InstallmentDataAccess.GetById(InstallmentId,user.Id);
            return View(data);
        }
    }
}
