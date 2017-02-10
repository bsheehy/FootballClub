using Club.Domain;
using Club.Services.Utils;
using FrRocks.Utils;
using System;
using System.Web.Mvc;

namespace FrRocks.Controllers
{
  public class HomeController : ControllerBase
  {
    public ActionResult Index()
    {
      ViewBag.Message = "Welcome to ASP.NET MVC!";
      ViewBag.dfwIsAdmin = Utility.UserIsAdmin(this.UserController);
      ViewBag.UserMessage = Utility.WelcomeUserMessage(this.UserController);
      ViewBag.Committees = UtilsSelectLists.CommitteesWhereUserIsAdminOrAdministrator(this.DbSession, this.UserController.EffectiveUser, true, DateTime.Now.Year);
      ViewBag.Teams = UtilsSelectLists.TeamsWhereUserIsAdminOrAdministrator(this.DbSession, this.UserController.EffectiveUser, true, DateTime.Now.Year);
      ViewBag.MembershipTypes = UtilsSelectLists.MembershipTypesForAdmin(this.DbSession, this.UserController.EffectiveUser, true, DateTime.Now.Year);

      bool bUserCanEditLottoResults = this.UserController.EffectiveUser.HasRole(this.UserController.EffectiveUser, Constants.AdministrationRoleOids);
      ViewBag.UserCanAddLottoResults = bUserCanEditLottoResults;
      if (bUserCanEditLottoResults)
      {
        //Get Looto Draws without results
        ViewBag.LottoDrawsWithoutResults = UtilsSelectLists.LottoDrawsWithoutResults(this.DbSession, DateTime.Now.Year);

      }

      bool bUserCanEditQualifications = this.UserController.EffectiveUser.HasRole(this.UserController.EffectiveUser, Constants.AdministrationRoleOids);
      ViewBag.UserCanEditQualifications = bUserCanEditQualifications;

      return View();
    }

    public ActionResult About()
    {
      ViewBag.Message = "Your app description page.";

      return View();
    }

    public ActionResult Contact()
    {
      ViewBag.Message = "Your contact page.";

      return View();
    }
  }
}
