using Accounting.DATA.DataAccess;
using Accounting.DATA.Entity;
using Accounting.DATA.Helper;
using Microsoft.AspNetCore.Mvc;

namespace Accounting.Controllers
{
    [SessionControl]
    public class CompanyInfoController : Controller
    {

        public IActionResult Index()
        {
            var user = HttpContext.Session.User();
            var data = UserDataAccess.GetById(user.Id);
            return View(data);
        }
        
        public IActionResult Update(User user)
        {
            var userId = HttpContext.Session.User().Id;
            user.Id = userId;
            var data = UserDataAccess.Update(user);
            return Json(data);
        }
    }
}
