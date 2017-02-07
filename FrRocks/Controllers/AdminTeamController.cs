using Club.Domain;
using Club.Domain.Artifacts;
using Club.Services.Controllers;
using FrRocks.Models;
using FrRocks.Utils;
using Mallon.Core.Resources;
using Mallon.Core.Web.Models;
using System;
using System.Web.Mvc;

namespace FrRocks.Controllers
{
  [DisplayName("Administer Teams", "Teams")]
  public class AdminTeamController : AdminControllerBase<Team>
  {
    //public override PartialViewResult Index(TypedModel<Team> model)
    //{
    //  if (Utility.UserIsAdmin(this.UserController) == false)
    //  {
    //    throw new HttpException((int)HttpStatusCode.Forbidden, string.Format("'{0}' does not have permission to Access the Administration section.", this.UserController.LoggedOnUser.Login));
    //  }

    //  switch (model.ExtraFields["op"])
    //  {
    //    case "n":
    //      model.Init(new Team());
    //      break;
    //    case "s":
    //      if (ModelState.IsValid)
    //      {
    //        SaveEntity(model);
    //      }
    //      break;
    //    case "d":
    //      try
    //      {
    //        DeleteEntity(model);
    //      }
    //      catch (NHibernate.ADOException err)
    //      {
    //        if (err.InnerException != null && err.InnerException.Message.Contains("DELETE statement conflicted with the REFERENCE constraint"))
    //        {
    //          DbSession.Clear();
    //          SetInactive(model);
    //        }
    //      }
    //      break;
    //  }
    //  return PartialView(model);

    //}

    public override PartialViewResult Index(TypedModel<Team> model)
    {
      ViewBag.TimeTypes = UtilsSelectListEnum.SelectListFor<enumTimeType>(model.Entity.TimeType);
      ViewBag.Sexs = UtilsSelectListEnum.SelectListFor<enumSex>(model.Entity.Sex);
      ViewBag.AdminUsers = UtilsSelectLists.Users(this.DbSession, true);

      TeamServiceController service = (TeamServiceController)this.Injector.Activate(typeof(TeamServiceController));
      ViewBag.EditTeam = service.CanUserEditTeam(model.Entity);

      if (model.ExtraFields["op"] == "d")
      {
        SetInactive(model);
        return PartialView(model);
      }
      else
      {
        return base.Index(model);
      }
    }

    public override void SaveEntity(TypedModel<Team> model)
    {
      TeamServiceController service = (TeamServiceController)this.Injector.Activate(typeof(TeamServiceController));
      if (service.SaveTeam(new ModelStateWrapper(this.ModelState), model.Entity) == false)
      {
        this.Response.Status = "error";
        //throw new ModelStateException(this.ModelState);
      }

    }

    public override void DeleteEntity(TypedModel<Team> model)
    {
      DbSession.Delete(model.Entity);
      DbSession.Flush();
    }

    public override void SetInactive(TypedModel<Team> model)
    {
      model.Entity.Active = false;
      try
      {
        DbSession.SaveOrUpdate(model.Entity);
        DbSession.Flush();
      }
      catch (Exception ex)
      {
        throw new Exception(ex.Message);
      }
    }

  }
}
