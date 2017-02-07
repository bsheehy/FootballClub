using Club.Domain.Artifacts;
using Club.Services.Controllers;
using Club.Services.Utils;
using FrRocks.Models;
using FrRocks.Utils;
using Mallon.Core.Artifacts;
using Mallon.Core.Web.Models;
using Mallon.Documents.Artifacts;
using System;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace FrRocks.Controllers
{
  /// <summary>
  /// There is only ever a SINGLE CLUB DETAILS entity in the darabase
  /// You can only Edit the existing ClubDetails details never CREATE or DELETE it\them
  /// </summary>
  public class ClubDetailsController : ControllerBase
  {
    //public ActionResult Edit(TypedModel<ClubDetails> m)
    public ActionResult Edit()
    {
      bool isAdministrator = Utility.UserIsAdmin(this.UserController);
      ViewBag.SuperUser = isAdministrator;
      if (isAdministrator)
      {
        ViewBag.PnlView = Enum.GetName(typeof(PageViewType), PageViewType.Edit);
      }
      else
      {
        ViewBag.PnlView = Enum.GetName(typeof(PageViewType), PageViewType.ReadOnly);
      }

      ClubDetails clubDetails = this.DbSession.Get<ClubDetails>(Utility.GetClubId());
      TypedModel<ClubDetails> m = new TypedModel<ClubDetails>();
      m.Init(clubDetails);
      ViewBag.EditAttachedDocs = UserController.EffectiveUser.GetClassAccess(typeof(AttachedDocument)).Allows(Access.Update);
      UtilsAttachedDocuments.SetViewDataAttachedDocuments(this.DbSession, ViewData, typeof(ClubDetails), m.Entity.Oid);
      //SetViewBagSelectLists(m);
      return View(m);
    }

    [HttpPost]
    [ActionName("Edit")]
    public ActionResult EditPost(TypedModel<ClubDetails> m)
    {
      if (Utility.UserIsAdmin(this.UserController) == false)
      {
        throw new HttpException((int)HttpStatusCode.Forbidden, "You must have Administrator priveleges\role to edit the ClubDetails details.");
      }

      if (ModelState.IsValid)
      {
        ClubDetailsServiceController service = (ClubDetailsServiceController)this.Injector.Activate(typeof(ClubDetailsServiceController));
        service.SaveClubDetails(new ModelStateWrapper(this.ModelState), m.Entity);
      }

      ViewBag.SuperUser = true;
      ViewBag.PnlView = Enum.GetName(typeof(PageViewType), PageViewType.Edit);
      ViewBag.EditAttachedDocs = UserController.EffectiveUser.GetClassAccess(typeof(AttachedDocument)).Allows(Access.Update);
      UtilsAttachedDocuments.SetViewDataAttachedDocuments(this.DbSession, ViewData, typeof(ClubDetails), m.Entity.Oid);
      return View(m);
    }
  }
}