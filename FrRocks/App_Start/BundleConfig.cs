using Club.Services.Utils;
using System.Web.Optimization;

namespace FrRocks
{
  public class BundleConfig
  {
    // For more information on Bundling, visit http://go.microsoft.com/fwlink/?LinkId=254725
    public static void RegisterBundles(BundleCollection bundles)
    {
      //bundles.IgnoreList.Clear();

#if !DEBUG
        BundleTable.EnableOptimizations = true;
#endif
      //get Theme from Web.config - default theme : bootstrap
      //new themes: highcontrast and metro
      string theme = Utility.GetTheme();

      //C:\development\FootballClub\FrRocks\Content\club\club.css


      //Includes Booststrap integration
      //Why the big styles name? Its required or certain styles will not be applied e.g. Close\Maximise buttons on Kendo Dialogs. Cjeck out link below for reason:
      //http://www.mvccentral.net/Story/Details/articles/kahanu/stylebundle-403-error-solved
      bundles.Add(new StyleBundle("~/Content/kendo/2016.3.914/kendocssbootstrap").Include(
        "~/Content/bootstrap.css", //Was affecting the styling of Kendo controls - will integrate at a later date. There is instuctions on Kendo site on ow to do this. MINIFY THIS???
        "~/Content/kendo/2016.3.914/kendo.common-bootstrap.min.css",
        string.Format("~/Content/kendo/2016.3.914/kendo.{0}.min.css", theme),
        "~/Content/kendo/2016.3.914/kendo.dataviz.min.css",
        string.Format("~/Content/kendo/2016.3.914/kendo.dataviz.{0}.min.css", theme)
        ));


      bundles.Add(new StyleBundle("~/Content/cssLoginPage")
      .Include("~/Content/club/club_loginpage.css", new CssRewriteUrlTransform()));


      bundles.Add(new StyleBundle("~/Content/css")
      .Include(string.Format("~/Content/club/club_{0}.css", theme))
        .Include("~/Content/club/club.css", new CssRewriteUrlTransform()));


      bundles.Add(new ScriptBundle("~/bundles/kendo/2016.3.914/kendoscripts").Include(
        //"~/Scripts/kendo/2016.3.914/jquery.min.js",
        "~/Scripts/kendo/2016.3.914/jszip.min.js",
        "~/Scripts/kendo/2016.3.914/kendo.all.min.js",
        "~/Scripts/kendo/2016.3.914/kendo.aspnetmvc.min.js",
        "~/Scripts/kendo/2016.3.914/cultures/kendo.culture.en-GB.min.js"
        ));

      bundles.Add(new ScriptBundle("~/bundles/club").Include(
        "~/Scripts/club/UtilsErrHandler.js",
        "~/Scripts/club/UtilsClub.js",
        "~/Scripts/club/UtilsKendo.js",
        "~/Scripts/club/UtilsDynamicValidation.js"
        ));

      bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
        "~/Scripts/jquery-{version}.js",
        "~/Scripts/jquery-ui-{version}.js",
        "~/Scripts/jquery.unobtrusive*",
        "~/Scripts/jquery.validate*"
        ));
    }

    private const string appSettingsTheme = "sheehy.club.Theme";
  }
}
