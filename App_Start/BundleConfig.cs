using System.Web;
using System.Web.Optimization;

namespace EduAx
{
    public class BundleConfig
    {
        // Para obtener más información sobre las uniones, visite https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js", "~/Scripts/Site.js"));

            bundles.Add(new StyleBundle("~/styles/layout").Include(
                      "~/Content/Site.css"));
        }
    }
}
