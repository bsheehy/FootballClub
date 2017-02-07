
using Club.Services.Utils;
using FrRocks.Utils;
using Mallon.Core.Artifacts;
using Mallon.Core.Interfaces;
using Mallon.Core.Web.Logic;
using Mallon.Core.Web.Models;
using System;
using System.Web.Mvc;
using System.Web.Security;
namespace FrRocks.Controllers
{
  public class AccountController : ControllerBase
  {
    public IEventLog Log { get; set; }

    [AllowAnonymous]
    [OutputCache(NoStore = true, Duration = 0, VaryByParam = "None")]
    public ActionResult Index([BoundModel(DontLoad = true, DontMerge = true)]TypedModel<User> m)
    {
      LogOutUser();
      string url = Request.QueryString["ReturnUrl"];
      ViewBag.ReturnUrl = url;
      m.Init(new User());
      return View(m);
    }

    [HttpPost]
    [ActionName("Index")]
    [AllowAnonymous]
    [ValidateAntiForgeryToken]
    public ActionResult IndexPost(TypedModel<User> m)
    {
      Mallon.Core.Artifacts.User user = null;
      bool notAllowed = false;
      try
      {
        user = UserController.Logon(m.Entity.Login, m.Entity.Password);
        ViewBag.LogonFailed = false;
      }
      catch (Exception e)
      {
        notAllowed = true;
        ViewBag.LogonFailed = true;
        ModelState.AddModelError("", e.Message);

        string password = "";
        try
        {
          //dont record full password in event log - just first 3 chars
          password = (m.Entity.Password.Length >= 3) ? String.Format("{0}***", m.Entity.Password.Substring(0, 3)) : m.Entity.Password.Substring(0, m.Entity.Password.Length);
        }
        catch
        { }
        if (!String.IsNullOrEmpty(m.Entity.Login.Trim()))
        {
          Log.Exception(this, e, String.Format("Invalid login attempt u:{0} p:{1}", m.Entity.Login, password));
        }
      }

      if (user != null && notAllowed == false)
      {
        string url = Request.QueryString["ReturnUrl"];

        if (String.IsNullOrWhiteSpace(url) || url == "/" || url == "~/")
        {
          string urlTemp = Request.Url.AbsoluteUri;
          int i = urlTemp.IndexOf("Account");
          if (i > 0)
          {
            url = urlTemp.Substring(0, i);
          }
        }
        return Redirect(url);
      }
      else
      {
        //VERY IMPORTANT Log user out if not valid or it results in problems.
        LogOutUser();
        return View();
      }
    }

    [AllowAnonymous]
    public PartialViewResult LogInStrip()
    {
      ViewBag.SuperUser = Utility.UserIsAdmin(this.UserController);
      return PartialView(UserController.EffectiveUser);
    }

    [AllowAnonymous]
    public ActionResult LogOut()
    {
      LogOutUser();
      return RedirectToAction("Index");
    }

    [AllowAnonymous]
    public ActionResult Help()
    {
      return PartialView();
    }

    protected MembershipPvd membershipPvd;

    private void LogOutUser()
    {
      FormsAuthentication.SignOut();
      if (UserController != null)
      {
        UserController.Logoff();
      }
    }
  }
}
