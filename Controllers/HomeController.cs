using System.Web.Mvc;
using System.Web.Security;
using WebTabanliProje.Models;
using WebTabanliProje.ViewModels;
using NHibernate.Linq;
using System.Linq;
using System.Net.Mail;

namespace WebTabanliProje.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet] public ActionResult Index()
        {
            if (System.Web.HttpContext.Current.User.Identity.IsAuthenticated)
            {
                if (System.Web.HttpContext.Current.User.IsInRole("Admin"))
                    return Redirect("/Admin");
                else if (System.Web.HttpContext.Current.User.IsInRole("User"))
                    return Redirect("/User");
            }
            return View();
        }
        [HttpPost] public ActionResult SignUp(UserRegister data)
        {
            if (Database.Session.Query<User>().Any(u => u.Email == data.Email))
                ModelState.AddModelError("SignUp", "Email already in use");
            if (Database.Session.Query<User>().Any(u => u.UserName == data.UserName))
                ModelState.AddModelError("SignUp", "Username already in use");
            if (!ModelState.IsValid)
            {
                ViewBag.Action = "SignUp";
                return View("~/Views/Home/index.cshtml");
            }
            var user = new User { Email = data.Email, UserName = data.UserName };
            user.Roles.Add(Database.Session.Query<Role>().Single(r => r.Id == 0));
            user.SetPassword(data.UserPassword);
            Database.Session.Save(user);
            Database.Session.Flush();
            FormsAuthentication.SetAuthCookie(user.UserName, true);
            return RedirectToAction("index");
        }
        [HttpPost] public ActionResult SignIn(UserLogin data)
        {
            var user = Database.Session.Query<User>().FirstOrDefault(p => p.UserName == data.UserName);
            if (user == null || !user.CheckPassword(data.UserPassword))
                ModelState.AddModelError("SignIn", "Username or password is incorrect");
            if (!ModelState.IsValid)
            {
                ViewBag.Action = "SignIn";
                return View("~/Views/Home/index.cshtml");
            }
            FormsAuthentication.SetAuthCookie(data.UserName, true);
            return RedirectToAction("Index");
        }
        [HttpPost] public ActionResult SendForm(ContactForm data)
        {
            MailMessage ePosta = new MailMessage();
            ePosta.From = new MailAddress(data.Email);
            ePosta.To.Add("emirhansoylu98@gmail.com");
            ePosta.Subject = "Web Tabanlı Proje";
            ePosta.Body = data.Name + " sended you an email with this message: \n" + data.Message + "\n" + data.Email ;

            SmtpClient smtp = new SmtpClient("smtp.gmail.com");
            smtp.Credentials = new System.Net.NetworkCredential("webtabanliproje2019@gmail.com", "trakyaedutr");
            smtp.Port = 587;
            smtp.Host = "smtp.gmail.com";
            smtp.EnableSsl = true;
            smtp.Send(ePosta);
            ViewBag.Action = "Contact";
            return View("~/Views/Home/index.cshtml");
        }
    }
}