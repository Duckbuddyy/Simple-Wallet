using System.Web.Mvc;
using System.Web.Security;

namespace WebTabanliProje.Controllers
{
    public class AuthController : Controller
    {
        [HttpGet]
        public ActionResult Login()
        {
            ViewBag.Action = "SignIn";
            return View("~/Views/Home/index.cshtml");
        }

        [HttpGet]
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToRoute("Home");
        }
    }
}