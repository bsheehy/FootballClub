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
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace FrRocks.Controllers
{
  public class ClubCalendarController : ControllerBase
  {
    public ActionResult Scheduler()
    {
      TypedModel<ClubCalendarQry> model = new TypedModel<ClubCalendarQry>();
      model.Init();
      ClubCalendarService service = (ClubCalendarService)this.Injector.Activate(typeof(ClubCalendarService));
      ViewBag.EditClubCalendar = service.CanUserEditClubCalendar();
      SetViewBagSelectLists_ForSearch();
      return View(model);
    }

    public JsonResult Scheduler_Read([DataSourceRequest] DataSourceRequest request, TypedModel<ClubCalendarQry> filter)
    {
      IEnumerable<ClubCalendar> calendarItems = GetClubCalendarItems(filter);
      IMapper Mapper = AutoMapperConfig.MapperConfiguration.CreateMapper();
      IEnumerable<ModelCalendarSchedulerEvent> result = Mapper.Map<IEnumerable<ModelCalendarSchedulerEvent>>(calendarItems);
      return Json(result.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
    }

    [AcceptVerbs(HttpVerbs.Post)]
    public ActionResult Scheduler_Create([DataSourceRequest] DataSourceRequest request, ModelCalendarSchedulerEvent entity)
    {
      ClubCalendarService service = (ClubCalendarService)this.Injector.Activate(typeof(ClubCalendarService));
      if (service.CanUserEditClubCalendar() == false)
      {
        throw new ModelStateException("You do not have permission to 'Create' Club Calendar items.");
      }

      if (ModelState.IsValid)
      {
        if (service.SaveClubCalendar(new ModelStateWrapper(this.ModelState), ref entity))
        {
          return Json(new[] { entity }.ToDataSourceResult(request, ModelState));
        }
      }
      throw new ModelStateException(this.ModelState);
    }

    public ActionResult Scheduler_Update([DataSourceRequest] DataSourceRequest request, ModelCalendarSchedulerEvent entity)
    {
      ClubCalendarService service = (ClubCalendarService)this.Injector.Activate(typeof(ClubCalendarService));
      if (service.CanUserEditClubCalendar() == false)
      {
        throw new ModelStateException("You do not have permission to 'Update' Club Calendar items.");
      }

      if (ModelState.IsValid)
      {
        if (service.SaveClubCalendar(new ModelStateWrapper(this.ModelState), ref entity))
        {
          return Json(new[] { entity }.ToDataSourceResult(request, ModelState));
        }
      }
      throw new ModelStateException(this.ModelState);
    }

    private IEnumerable<ClubCalendar> GetClubCalendarItems(TypedModel<ClubCalendarQry> filter)
    {
      IEnumerable<ClubCalendar> result = new List<ClubCalendar>();
      if (UserController.EffectiveUser.GetClassAccess(typeof(ClubCalendar)).Allows(Access.Read))
      {
        result = filter.Entity.List(this.DbSession).Cast<ClubCalendar>();
      }
      return result;
    }

    [AcceptVerbs(HttpVerbs.Post)]
    public ActionResult Scheduler_Delete([DataSourceRequest] DataSourceRequest request, ModelCalendarSchedulerEvent entity)
    {
      ClubCalendarService service = (ClubCalendarService)this.Injector.Activate(typeof(ClubCalendarService));
      if (service.CanUserEditClubCalendar() == false)
      {
        throw new ModelStateException("You do not have permission to 'Delete' Club Calendar items.");
      }

      if (service.DeleteClubCalendar(new ModelStateWrapper(this.ModelState), entity.Oid.Value))
      {
        return Json(new[] { entity }.ToDataSourceResult(request, ModelState));
      }
      else
      {
        return Json(this.ModelState.ToDataSourceResult());
      }
    }

    public JsonResult Scheduler_GetClubCalendarEventTypes([DataSourceRequest] DataSourceRequest request)
    {
      //IEnumerable<ClubCalendar> calendarItems = GetClubCalendarItems(filter);
      //IMapper Mapper = AutoMapperConfig.MapperConfiguration.CreateMapper();
      //IEnumerable<ModelCalendarSchedulerEvent> result = Mapper.Map<IEnumerable<ModelCalendarSchedulerEvent>>(calendarItems);
      IEnumerable<ClubCalendarEventType> entities = this.DbSession.QueryOver<ClubCalendarEventType>().List<ClubCalendarEventType>().Where(x => x.Active == true).OrderBy(x => x.Name).ToList<ClubCalendarEventType>();
      return Json(entities.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
    }

    private void SetViewOptions(bool checkCookie = false)
    {
      ViewBag.CheckCookie = checkCookie;
    }

    private void SetViewBagSelectLists_ForSearch()
    {
      ViewBag.Teams = UtilsSelectLists.Teams(this.DbSession);
      ViewBag.ClubCalendarEventTypes = UtilsSelectLists.ClubCalendarEventTypes(this.DbSession);
      ViewBag.MaxRecords = UtilsSelectLists.QueryMaxRecords();
    }
  }
}
