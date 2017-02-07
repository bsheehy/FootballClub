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
      ViewBag.UserCanAddLottoResults = this.UserController.EffectiveUser.HasRole(this.UserController.EffectiveUser, Constants.AdministrationRoleOids);

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
