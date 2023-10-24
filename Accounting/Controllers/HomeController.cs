using Accounting.DATA.DataAccess;
using Accounting.DATA.Helper;
using Accounting.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Accounting.Controllers
{
    [SessionControl]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            var user = HttpContext.Session.User();
            ViewBag.Shortcuts = UserShortcutDataAccess.GetList(user.Id);
            return View();
        }

        public IActionResult DailyReports()
        {
            var user = HttpContext.Session.User();
            var response = DashBoardDataAccess.DailyReports(user.Id);
            return Json(response);
        }

        public IActionResult SevenDaysReports()
        {
            var user = HttpContext.Session.User();
            var response = DashBoardDataAccess.SevenDaysReports(user.Id);
            return Json(response);
        }

        //2. satir

        public IActionResult SevenDailyBankProcess(Guid? BankId)
        {
            var user = HttpContext.Session.User();
            var response = DashBoardDataAccess.SevenDailyBankProcess(BankId, user.Id);
            return Json(response);
        }

        public IActionResult SevenDailySafeProcess(Guid? SafeId)
        {
            var user = HttpContext.Session.User();
            var response = DashBoardDataAccess.SevenDailySafeProcess(SafeId, user.Id);
            return Json(response);
        }

        public IActionResult Balances()
        {
            var user = HttpContext.Session.User();
            var response = DashBoardDataAccess.Balances(user.Id);
            return Json(response);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
