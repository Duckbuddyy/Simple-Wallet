using System.Web.Optimization;

namespace WebTabanliProje.App_Start
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new StyleBundle("~/Admin/Styles")
                .Include("~/Content/font-awesome.min.css")
                .Include("~/Content/Styles/bootstrap.min.css")
                .Include("~/Content/Styles/Admin.css"));

            bundles.Add(new StyleBundle("~/User/Styles")
                .Include("~/Content/font-awesome.min.css")
                .Include("~/Content/Styles/bootstrap.min.css")
                .Include("~/Content/Styles/User.css")
                .Include("~/Content/apexcharts.css"));

            bundles.Add(new StyleBundle("~/Styles")
                .Include("~/Content/font-awesome.min.css")
                .Include("~/Content/Styles/bootstrap.min.css")
                .Include("~/Content/Styles/Site.css")
                .Include("~/Content/aos.css")
                .Include("~/Content/lightbox.css"));

            bundles.Add(new ScriptBundle("~/Admin/Scripts")
                .Include("~/Scripts/jquery-3.4.1.min.js")
                .Include("~/Scripts/bootstrap.js")
                .Include("~/Areas/Admin/Scripts/Forms.js"));

            bundles.Add(new ScriptBundle("~/User/Scripts")
                .Include("~/Scripts/jquery-3.4.1.min.js")
                .Include("~/Scripts/bootstrap.js")
                .Include("~/Scripts/apexcharts.js")
                .Include("~/Areas/Users/Scripts/Forms.js"));

            bundles.Add(new ScriptBundle("~/Scripts")
                .Include("~/Scripts/jquery-3.4.1.min.js")
                .Include("~/Scripts/popper.min.js")
                .Include("~/Scripts/popper-utils.min.js")
                .Include("~/Scripts/bootstrap.js")
                .Include("~/Scripts/aos.js"));
        }
    }
}