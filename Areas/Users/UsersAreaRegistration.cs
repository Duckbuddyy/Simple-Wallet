using System.Web.Mvc;

namespace WebTabanliProje.Areas.Users
{
    public class UsersAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "Users";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute("UserHome", "User/", new { controller = "Home", action = "index" });
            context.MapRoute("UserChangePwd", "User/ChangePassword", new { controller = "Home", action = "ChangeMyPassword" });
            context.MapRoute("UserChangeEmail", "User/ChangeEmail", new { controller = "Home", action = "ChangeMyEmail" });
            context.MapRoute("CreateRecord", "User/CreateRecord", new { controller = "Home", action = "CreateRecord" });
            context.MapRoute("EditRecord", "User/EditRecord", new { controller = "Home", action = "EditRecord" });
            context.MapRoute("DeleteRecord", "User/DeleteRecord", new { controller = "Home", action = "DeleteRecord" });
        }
    }
}