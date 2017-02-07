using Club.Domain.Artifacts;
using Club.Services.Controllers;
using FrRocks.Models;
using Mallon.Core.Resources;
using Mallon.Core.Web.Models;
using System;
using System.Web.Mvc;

namespace FrRocks.Controllers
{
  [DisplayName("Committee Member Type", "Committee Member Types")]
  public class AdminCommitteeMemberTypeController : AdminControllerBase<CommitteeMemberType>
  {
    //public override PartialViewResult Index(TypedModel<CommitteeMemberType> model)
    //{
    //  if (Utility.UserIsAdmin(this.UserController) == false)
    //  {
    //    throw new HttpException((int)HttpStatusCode.Forbidden, string.Format("'{0}' does not have permission to Access the Administration section.", this.UserController.LoggedOnUser.Login));
    //  }

    //  switch (model.ExtraFields["op"])
    //  {
    //    case "n":
    //      model.Init(new CommitteeMemberType());
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

    public override PartialViewResult Index(TypedModel<CommitteeMemberType> model)
    {
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

    public override void SaveEntity(TypedModel<CommitteeMemberType> model)
    {
      CommitteeServiceController service = (CommitteeServiceController)this.Injector.Activate(typeof(CommitteeServiceController));
      if (service.SaveCommitteeMemberType(new ModelStateWrapper(this.ModelState), model.Entity) == false)
      {
        this.Response.Status = "error";
        //throw new ModelStateException(this.ModelState);
      }

    }

    public override void DeleteEntity(TypedModel<CommitteeMemberType> model)
    {
      CommitteeServiceController service = (CommitteeServiceController)this.Injector.Activate(typeof(CommitteeServiceController));
      if (service.DeleteCommitteeMemberType(new ModelStateWrapper(this.ModelState), model.Entity) == false)
      {
        this.Response.Status = "error";
        //throw new ModelStateException(this.ModelState);
      }

      DbSession.Delete(model.Entity);
      DbSession.Flush();
    }

    public override void SetInactive(TypedModel<CommitteeMemberType> model)
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
