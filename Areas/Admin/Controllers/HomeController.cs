using System.Web.Mvc;
using WebTabanliProje.Infrastructure;

namespace WebTabanliProje.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    [SelectedTab("Home")]
    public class HomeController : Controller
    {
        // GET: Admin/Home
        public ActionResult Index()
        {
            return View();
        }
    }
}