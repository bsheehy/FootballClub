using Club.Services.Utils;
using FrRocks.Filters;
using FrRocks.Models;
using Mallon.Core.Artifacts;
using Mallon.Core.Web.Models;
using System;
using System.Web.Mvc;

namespace FrRocks.Controllers
{
  //[Authorize]
  public class UtilController : ControllerBase
  {
    public ActionResult CurrentUserView(TypedModel<User> m)
    {
      if (this.UserController.EffectiveUser.GetClassAccess(m.GetType()).Allows(Access.Update) == false)
      {
        ViewBag.PnlView = Enum.GetName(typeof(PageViewType), PageViewType.ReadOnly);
      }
      else
      {
        ViewBag.PnlView = Enum.GetName(typeof(PageViewType), PageViewType.Edit);

      }
      return PartialView(m);
      //return View(m);
    }

    [HttpPost]
    [ActionName("CurrentUserView")]
    [HandleModelStateException]
    public ActionResult CurrentUserViewPost(TypedModel<User> m)
    {
      try
      {
        DbSession.SaveOrUpdate(m.Entity);
        DbSession.Flush();
        return Json(m.Entity, JsonRequestBehavior.AllowGet);
      }
      catch
      {
        throw new ModelStateException(this.ModelState);
      }
    }
  }
}
