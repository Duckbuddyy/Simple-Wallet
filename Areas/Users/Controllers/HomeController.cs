using NHibernate.Linq;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using WebTabanliProje.Areas.Users.ViewModels;
using WebTabanliProje.Infrastructure;
using WebTabanliProje.Models;

namespace WebTabanliProje.Areas.Users.Controllers
{
    [Authorize(Roles = "User")]
    public class HomeController : Controller
    {
        public static List<string> CategoriesList = null;
        public static int user_Id = 0;
        [SelectedTab("Home")][HttpGet] public ActionResult Index()
        {
            CategoriesList = Database.Session.Query<Category>().Select(column => column.Category_Name).ToList();
            user_Id = Database.Session.Query<User>().Single(u => u.UserName == User.Identity.Name).Id;
            List<int> user_Record_Id = Database.Session.Query<UserRecord>().Where(ur => ur.User_Id == user_Id).Select(column => column.Record_Id).ToList();
            List<Record> user_Records = Database.Session.Query<Record>().Where(ur => user_Record_Id.Contains(ur.Record_Id)).ToList();
            List<string> categoryString = new List<string>();
            foreach (Record record in user_Records)
                categoryString.Add(Database.Session.Query<Category>().Single(category => category.Category_Id == record.Category).Category_Name);
            if (TempData["UserAlert"] != null) { 
                ViewBag.UserAlert = TempData["UserAlert"].ToString();
                TempData.Remove("UserAlert");
            }
            return View(new RecordsIndex { Records = user_Records, CategoryString = categoryString });
        }
        [HttpGet] [SelectedTab("Create")] public ActionResult CreateRecord() {
            return View(new RecordsCreate());
        }
        [HttpPost] public ActionResult CreateRecord(RecordsCreate rc)
        {
            if (user_Id == 0)
                user_Id = Database.Session.Query<User>().Single(u => u.UserName == User.Identity.Name).Id;
            if (user_Id == 0) return HttpNotFound();
            bool type;
                if (rc.Type == "Income") type = true;
                else type = false;
            int record_Id = Database.Session.Query<Record>().Select(column => column.Record_Id).Max();

            Record record = new Record()
            {
                Amount = rc.Amount,
                Category = Database.Session.Query<Category>().SingleOrDefault(category => category.Category_Name == rc.Category).Category_Id,
                Type = type,
                Note = rc.Note,
                Record_Id = record_Id + 1
            };

            UserRecord userRecord = new UserRecord()
            {
                Record_Id = record_Id + 1,
                User_Id = user_Id
            };
            Database.Session.Save(record);
            Database.Session.Save(userRecord);
            Database.Session.Flush();
            TempData["UserAlert"] = "NewRecord";
            return RedirectToAction("index");
        }
        [SelectedTab("Edit")][HttpGet] public ActionResult EditRecord(int record_Id)
        {
            if (user_Id == 0)
                user_Id = Database.Session.Query<User>().Single(u => u.UserName == User.Identity.Name).Id;
            if (user_Id == 0) return HttpNotFound();
            List<int> user_Record_Id = Database.Session.Query<UserRecord>().Where(ur => ur.User_Id == user_Id).Select(column => column.Record_Id).ToList();
            if (!user_Record_Id.Contains(record_Id))
            {
                TempData["UserAlert"] = "ErrorEditRecord";
                return RedirectToAction("index");
            }
            Record record = Database.Session.Query<Record>().FirstOrDefault(rec => rec.Record_Id == record_Id);
            string category = Database.Session.Query<Category>().FirstOrDefault(cat => cat.Category_Id == record.Category).Category_Name;
            string type;
            if (record.Type) type = "Income";
            else type = "Expense";

            return View(new RecordsEdit() { Record_Id= record_Id, Amount = record.Amount, Category = category, Note = record.Note, Type = type });
        }
        [HttpPost] public ActionResult EditRecord(RecordsEdit re)
        {
            if (user_Id == 0)
                user_Id = Database.Session.Query<User>().Single(u => u.UserName == User.Identity.Name).Id;
            if (user_Id == 0) return HttpNotFound();
            List<int> user_Record_Id = Database.Session.Query<UserRecord>().Where(ur => ur.User_Id == user_Id).Select(column => column.Record_Id).ToList();
            if (!user_Record_Id.Contains(re.Record_Id))
            {
                TempData["UserAlert"] = "ErrorEditRecord";
                return RedirectToAction("index");
            }
            bool type;
                if (re.Type == "Income") type = true;
                else type = false;
            Record record = new Record()
            {
                Amount = re.Amount,
                Category = Database.Session.Query<Category>().SingleOrDefault(category => category.Category_Name == re.Category).Category_Id,
                Type = type,
                Note = re.Note,
                Record_Id = re.Record_Id
            };
            Database.Session.Update(record);
            Database.Session.Flush();
            TempData["UserAlert"] = "EditRecord";
            return RedirectToAction("index");
        }
        [HttpPost] public ActionResult DeleteRecord(int record_Id)
        {
            if (user_Id == 0)
                user_Id = Database.Session.Query<User>().Single(u => u.UserName == User.Identity.Name).Id;
            if (user_Id == 0) return HttpNotFound();
            Record record = Database.Session.Load<Record>(record_Id);
            UserRecord userRecord = Database.Session.Query<UserRecord>().Where(ur => ur.Record_Id == record_Id && ur.User_Id == user_Id).First();
            Database.Session.Delete(record);
            Database.Session.Delete(userRecord);
            Database.Session.Flush();
            TempData["UserAlert"] = "DeleteRecord";
            return RedirectToAction("Index");
        }
        [SelectedTab("ChangePassword")][HttpGet] public ActionResult ChangeMyPassword()
        {
            User user = Database.Session.Query<User>().Single(u => u.UserName == User.Identity.Name);
            if (user == null)
                return HttpNotFound();
            ChangePassword cw = new ChangePassword() { UserName = user.UserName }; 
            return View("~/Areas/Users/Views/Home/ChangePassword.cshtml", cw);
        }
        [HttpPost] public ActionResult ChangeMyPassword(ChangePassword cw)
        {
            User user = Database.Session.Query<User>().Single(u => u.UserName == User.Identity.Name);
            if (user == null)
                return HttpNotFound();
            else if (!user.CheckPassword(cw.UserPassword)){
                ModelState.AddModelError("ChangePassword", "Your password is incorrect");
                return View("~/Areas/Users/Views/Home/ChangePassword.cshtml", new ChangePassword() { UserName = user.UserName});
            }
            user.SetPassword(cw.NewPassword);
            Database.Session.Save(user);
            Database.Session.Flush();
            TempData["UserAlert"] = "Pwd";
            return RedirectToAction("index");
        }
        [SelectedTab("ChangeEmail")][HttpGet] public ActionResult ChangeMyEmail()
        {
            User user = Database.Session.Query<User>().Single(u => u.UserName == User.Identity.Name);
            if (user == null)
                return HttpNotFound();
            ChangeEmail ce = new ChangeEmail() { Email = user.Email };
            
            return View("~/Areas/Users/Views/Home/ChangeEmail.cshtml",ce);
        }
        [HttpPost]public ActionResult ChangeMyEmail(ChangeEmail ce)
        {
            User user = Database.Session.Query<User>().Single(u => u.UserName == User.Identity.Name);
            if (user == null)
                return HttpNotFound();
            if (!user.CheckPassword(ce.UserPassword))
                ModelState.AddModelError("ChangeEmail", "Your password is incorrect");
            if (Database.Session.Query<User>().Any(u => u.Email == ce.NewEmail))
                ModelState.AddModelError("ChangeEmail", "This e-mail using already");
            else if (user.Email == ce.NewEmail)
                ModelState.AddModelError("ChangeEmail", "You have to enter different email to change");
            if (!ModelState.IsValid)
                return View("~/Areas/Users/Views/Home/ChangeEmail.cshtml", new ChangeEmail() { Email = user.Email });
            user.Email = ce.NewEmail ;
            Database.Session.Save(user);
            Database.Session.Flush();
            TempData["UserAlert"] = "Email";
            return RedirectToAction("index");
        }
    }
}