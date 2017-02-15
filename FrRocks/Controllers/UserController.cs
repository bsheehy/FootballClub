using AutoMapper;
using Club.Domain.Artifacts;
using Club.Domain.Models;
using Club.Domain.Queries;
using Club.Services.Controllers;
using FrRocks.Models;
using FrRocks.Utils;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Mallon.Core.Artifacts;
using Mallon.Core.Web.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace FrRocks.Controllers
{
  public class UserController : ControllerBase
  {
    public PartialViewResult SelectTeamAdminUser(Guid teamOid, string controlId = null)
    {
      //Only User who is an Admin or a Team Admin can edit the Team
      TypedModel<UserQry> model = new TypedModel<UserQry>();
      model.Init();
      model.Entity.aq_TeamOid = teamOid;

      TeamServiceController service = (TeamServiceController)this.Injector.Activate(typeof(TeamServiceController));
      ViewBag.EditTeam = service.CanUserEditTeam(teamOid);
      ViewBag.UserControlId = controlId;
      ViewBag.TeamOid = teamOid.ToString();
      //SetViewOptions();
      SetViewBagSelectLists_ForSearch(controlId);
      return PartialView(model);
    }

    [HttpPost]
    [ActionName("SelectTeamAdminUser")]
    public ActionResult SelectTeamAdminUserPost(Guid userOid, Guid teamOid)
    {
      if (this.UserController.EffectiveUser.GetClassAccess(typeof(Team)).Allows(Access.Update) == true)
      {
        TeamServiceController service = (TeamServiceController)this.Injector.Activate(typeof(TeamServiceController));
        if (service.SelectTeamAdminUser(new ModelStateWrapper(this.ModelState), userOid, teamOid))
        {
          ModelJsonResult j = new ModelJsonResult();
          j.success = true;
          return Json(j, JsonRequestBehavior.AllowGet);
        }
        throw new ModelStateException(this.ModelState);
      }
      else
      {
        this.ModelState.AddModelError("Person", "You do not have permissions to edit this Team.");
        throw new ModelStateException(this.ModelState);
      }
    }

    /// <summary>
    /// Called for Person searches - options for the result list are also set in here.
    /// </summary>
    /// <param name="personSelect">If True then 'Select Person' enabled and 'Edit' is disabled in List Results.</param>
    /// <returns></returns>
    public PartialViewResult Search(string controlId = null, bool selectMany = false)
    {
      TypedModel<PersonQry> model = new TypedModel<PersonQry>();
      model.Init();
      ViewBag.SelectMany = selectMany;
      SetViewBagSelectLists_ForSearch(controlId);
      return PartialView(model);
    }

    public ActionResult SearchResults_ReadData([DataSourceRequest]DataSourceRequest request, TypedModel<UserQry> filter)
    {
      if (request.PageSize == 0)
      {
        request.PageSize = 30;
      }

      IEnumerable<User> people = GetUser(filter);
      IMapper Mapper = AutoMapperConfig.MapperConfiguration.CreateMapper();
      IEnumerable<ModelUser> result = Mapper.Map<IEnumerable<ModelUser>>(people);
      return Json(result.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
    }

    private void SetViewBagSelectLists_ForSearch(string controlId)
    {
      ViewBag.MaxRecords = UtilsSelectLists.QueryMaxRecords();
    }

    private IEnumerable<User> GetUser(TypedModel<UserQry> filter)
    {
      IEnumerable<User> result = filter.Entity.List(this.DbSession).Cast<User>();
      return result;
    }

    private void SetViewBagSelectLists(TypedModel<User> m)
    {

    }

    public PartialViewResult SelectCommitteeAdminUser(Guid committeeOid, string controlId = null)
    {
      //Only User who is an Admin or a Committee Admin can edit the Committee
      TypedModel<UserQry> model = new TypedModel<UserQry>();
      model.Init();
      model.Entity.aq_CommitteeOid = committeeOid;

      CommitteeServiceController service = (CommitteeServiceController)this.Injector.Activate(typeof(CommitteeServiceController));
      ViewBag.EditCommittee = service.CanUserEditCommittee(committeeOid);
      ViewBag.UserControlId = controlId;
      ViewBag.CommitteeOid = committeeOid.ToString();
      //SetViewOptions();
      SetViewBagSelectLists_ForSearch(controlId);
      return PartialView(model);
    }

    [HttpPost]
    [ActionName("SelectCommitteeAdminUser")]
    public ActionResult SelectCommitteeAdminUserPost(Guid userOid, Guid committeeOid)
    {
      if (this.UserController.EffectiveUser.GetClassAccess(typeof(Committee)).Allows(Access.Update) == true)
      {
        CommitteeServiceController service = (CommitteeServiceController)this.Injector.Activate(typeof(CommitteeServiceController));
        if (service.SelectCommitteeAdminUser(new ModelStateWrapper(this.ModelState), userOid, committeeOid))
        {
          ModelJsonResult j = new ModelJsonResult();
          j.success = true;
          return Json(j, JsonRequestBehavior.AllowGet);
        }
        throw new ModelStateException(this.ModelState);
      }
      else
      {
        this.ModelState.AddModelError("Person", "You do not have permissions to edit this Committee.");
        throw new ModelStateException(this.ModelState);
      }
    }
  }
}