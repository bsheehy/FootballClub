using AutoMapper;
using Club.Domain.Artifacts;
using Club.Domain.Models;
using Club.Domain.Queries;
using Club.Services.Controllers;
using Club.Services.Utils;
using FrRocks.Utils;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Mallon.Core.Artifacts;
using Mallon.Core.Web.Models;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace FrRocks.Controllers
{
  public class CommitteeMinutesController : ControllerBase
  {
    public PartialViewResult Search(string controlId = null, bool selectMany = false)
    {
      TypedModel<CommitteeMinutesQry> model = new TypedModel<CommitteeMinutesQry>();
      model.Init();
      ViewBag.SelectMany = selectMany;
      SetViewOptions();
      SetViewBagSelectLists_ForSearch(controlId);
      return PartialView(model);
    }

    public ActionResult SearchResults_ReadData([DataSourceRequest]DataSourceRequest request, TypedModel<CommitteeMinutesQry> filter)
    {
      if (request.PageSize == 0)
      {
        request.PageSize = 30;
      }

      IEnumerable<CommitteeMinute> teams = GetCommitteeMinutes(filter);
      IMapper Mapper = AutoMapperConfig.MapperConfiguration.CreateMapper();
      IEnumerable<ModelCommitteeMinute> result = Mapper.Map<IEnumerable<ModelCommitteeMinute>>(teams);
      return Json(result.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
    }

    private void SetViewBagSelectLists_ForSearch(string controlId)
    {
      ViewBag.CommitteeControlId = controlId;
      ViewBag.Committees = UtilsSelectLists.Committees(this.DbSession);
      ViewBag.MaxRecords = UtilsSelectLists.QueryMaxRecords();
    }

    private void SetViewOptions()
    {
      bool editCommittee = UserController.EffectiveUser.GetClassAccess(typeof(Committee)).Allows(Access.Update);
      ViewBag.CommitteeEdit = editCommittee;
      if (editCommittee)
      {
        ViewBag.PnlView = Utility.setPnlView("Edit", UserController, typeof(Committee));
      }
      else
      {
        ViewBag.PnlView = Utility.setPnlView("ReadOnly", UserController, typeof(Committee));
      }

      bool editPerson = UserController.EffectiveUser.GetClassAccess(typeof(Person)).Allows(Access.Update);
      ViewBag.PersonEdit = editPerson;
    }

    private IEnumerable<CommitteeMinute> GetCommitteeMinutes(TypedModel<CommitteeMinutesQry> filter)
    {
      IEnumerable<CommitteeMinute> result = new List<CommitteeMinute>();
      if (UserController.EffectiveUser.GetClassAccess(typeof(CommitteeMinute)).Allows(Access.Read))
      {
        result = filter.Entity.List(this.DbSession).Cast<CommitteeMinute>();
      }
      return result;
    }

    private void SetViewBagSelectLists(TypedModel<Committee> m)
    {
      ViewBag.Committees = UtilsSelectLists.CommitteesWhereUserIsAdminOrAdministrator(this.DbSession, this.UserController.EffectiveUser);
    }
  }
}