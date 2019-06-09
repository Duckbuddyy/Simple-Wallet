using System.Web.Mvc;

namespace WebTabanliProje.Areas.Admin
{
    public class AdminAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Admin";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute("AdminHome", "Admin/", new { controller = "Home", action = "index" });
            context.MapRoute("AdminUsers", "Admin/Users", new { controller = "Users", action = "index" });
            context.MapRoute("AdminUsersCreate", "Admin/Users/Create", new { controller = "Users", action = "create" });
            context.MapRoute("AdminUsersEdit", "Admin/Users/Edit", new { controller = "Users", action = "edit" });
            context.MapRoute("AdminUsersResetPwd", "Admin/Users/ResetPassword", new { controller = "Users", action = "resetpassword" });
            context.MapRoute("AdminUsersDeleteUser", "Admin/Users/Delete", new { controller = "Users", action = "delete" });
        }
    }
}