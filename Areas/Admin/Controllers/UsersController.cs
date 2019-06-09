using NHibernate.Linq;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using WebTabanliProje.Areas.Admin.ViewModels;
using WebTabanliProje.Infrastructure;
using WebTabanliProje.Models;

namespace WebTabanliProje.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    public class UsersController : Controller
    {
        [HttpGet] [SelectedTabAttribute("Users")] public ActionResult Index()
        {
            return View(new UsersIndex
            {
                Users = Database.Session.Query<User>().ToList()
            });
        }
        [HttpGet] [SelectedTabAttribute("Create")] public ActionResult Create() {
            return View(new UsersCreate {
                Roles = Database.Session.Query<Role>().Select(role => new RoleCheckBox()
                {
                    Id = role.Id,
                    IsChecked = false,
                    RoleName = role.RoleName
                }).ToList()
            });
        }
        [HttpPost] public ActionResult Create(UsersCreate data) {
            if (Database.Session.Query<User>().Any(u => u.Email == data.Email))
                ModelState.AddModelError("Create", "Email already in use");
            if (Database.Session.Query<User>().Any(u => u.UserName == data.UserName))
                ModelState.AddModelError("Create", "Username must be unique");
            if (!ModelState.IsValid)
                return View(new UsersCreate { Roles = Database.Session.Query<Role>().Select(role => new RoleCheckBox() {
                        Id = role.Id,
                        IsChecked = false,
                        RoleName = role.RoleName
                    }).ToList()
                });
            var user = new User { Email = data.Email, UserName = data.UserName };
            SyncRoles(data.Roles, user.Roles);
            user.SetPassword(data.UserPassword);
            Database.Session.Save(user);
            Database.Session.Flush();
            return RedirectToAction("index");
        }
        [HttpGet] [SelectedTabAttribute("Edit")] public ActionResult Edit(int Id)
        {
            var user = Database.Session.Load<User>(Id);
            if (user == null)
                return HttpNotFound();
            return View(new UsersEdit {
                UserName = user.UserName,
                Email = user.Email,
                Roles = Database.Session.Query<Role>().Select(role => new RoleCheckBox {
                    Id = role.Id,
                    IsChecked = user.Roles.Contains(role),
                    RoleName = role.RoleName}).ToList()
            });
        }
        [HttpPost] public ActionResult Edit(int Id, UsersEdit data)
        {
            var user = Database.Session.Load<User>(Id);
            if (user == null)
                return HttpNotFound();
            if (Database.Session.Query<User>().Any(u => u.UserName == data.UserName && u.Id != Id))
                ModelState.AddModelError("Edit", "Username already in use");
            if (Database.Session.Query<User>().Any(u => u.Email == data.Email && u.Id != Id))
                ModelState.AddModelError("Edit", "Email already in use");
            if (!ModelState.IsValid)
                return View(new UsersEdit {
                    UserName = user.UserName,
                    Email = user.Email,
                    Roles = Database.Session.Query<Role>().Select(role => new RoleCheckBox {
                        Id = role.Id,
                        IsChecked = user.Roles.Contains(role),
                        RoleName = role.RoleName
                    }).ToList()});
            user.UserName = data.UserName;
            user.Email = data.Email;
            SyncRoles(data.Roles, user.Roles);
            Database.Session.Update(user);
            Database.Session.Flush();
            return RedirectToAction("index");
        }
        [HttpGet] [SelectedTabAttribute("ResetPassword")] public ActionResult ResetPassword(int Id)
        {
            var user = Database.Session.Load<User>(Id);
            if (user == null)
                return HttpNotFound();
            return View(new UsersResetPassword
            {
                UserName = user.UserName
            });
        }
        [HttpPost] public ActionResult ResetPassword(int Id, UsersResetPassword data)
        {
            var user = Database.Session.Load<User>(Id);
            if (user == null) HttpNotFound();
            data.UserName = user.UserName;
            user.SetPassword(data.UserPassword);
            Database.Session.Update(user);
            Database.Session.Flush();
            return RedirectToAction("index");
        }
        [HttpPost] public ActionResult Delete(int Id)
        {
            var user = Database.Session.Load<User>(Id);
            if (user.UserName == User.Identity.Name)
                return RedirectToAction("index");
            if (user == null)
                return HttpNotFound();
            List<UserRecord> user_Records = Database.Session.Query<UserRecord>().Where(ur => ur.User_Id == Id).ToList();
            foreach(UserRecord ur in user_Records)
            {
                Database.Session.Delete(Database.Session.Load<Record>(ur.Record_Id));
                Database.Session.Delete(ur);
            }
            Database.Session.Delete(user);
            Database.Session.Flush();
            return RedirectToAction("index");
        }
        private void SyncRoles(IList<RoleCheckBox> checkBoxes, IList<Role> roles)
        {
            var selectedRoles = new List<Role>();
            foreach (var role in Database.Session.Query<Role>())
            {
                var checkbox = checkBoxes.Single(c => c.Id == role.Id);
                checkbox.RoleName = role.RoleName;
                if (checkbox.IsChecked)
                    selectedRoles.Add(role);
            }
            foreach (var toAdd in selectedRoles.Where(t => !roles.Contains(t)))
                roles.Add(toAdd);
            foreach (var toRemove in roles.Where(t => !selectedRoles.Contains(t)).ToList())
                roles.Remove(toRemove);
        }
    }
}