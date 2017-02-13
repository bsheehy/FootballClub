using AuditApexSql;
using AuditApexSql.Artifacts;
using AuditApexSql.Interfaces;
using AuditApexSql.Report;
using Club.Domain;
using FrRocks.Utils;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Mallon.Core.PersistentSupport;
using Mallon.Core.Web.Models;
using Mallon.NHibernateSupport.Extensions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace FrRocks.Controllers
{
  public class AuditController : ControllerBase
  {
    #region Properties

    /// <summary>
    /// This is required to allow castle to select and inject the relevant AudtController
    /// </summary>
    public IAuditController auditController { get; set; }

    #endregion

    public ActionResult StandardReport(Guid? oid = null, string type = null)
    {
      if (this.UserController.EffectiveUser.HasRole(this.UserController.EffectiveUser, Constants.AdministrationRoleOids) == false)
      {
        throw new HttpException((int)HttpStatusCode.Forbidden, string.Format("'{0}' does not have permission to Access the Administration Audit section.", this.UserController.LoggedOnUser.Login));
      }

      TypedModel<AuditQuery> model = new TypedModel<AuditQuery>();
      model.Init();

      if (oid != null && oid.HasValue && oid.Value != Guid.Empty && string.IsNullOrEmpty(type) == false)
      {
        Type t = PersistentHelper.GetType(type);
        model.Entity.TableName = this.DbSession.GetSqlTableName(t);
        model.Entity.Where = string.Format("[Key 1] = '{0}'", oid.ToString());
        ViewBag.AuditType = "SingleEntity";
        ViewBag.HideLayout = true;
      }
      SetViewBagSelectLists(model.Entity);
      return View(model);
    }

    public ActionResult StandardReport_ReadData([DataSourceRequest]DataSourceRequest request, [BoundModel(DontLoad = true, DontMerge = false)]TypedModel<AuditQuery> filter)
    {
      if (request.PageSize == 0)
      {
        request.PageSize = 20;
      }

      StandardApexReport report = new StandardApexReport();
      IEnumerable<AuditData> results = this.auditController.GetReportData(report, filter.Entity);
      ViewBag.ListCount = results.Count();
      return Json(results.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
    }

    /// <summary>
    /// Gets Audit Result set for a sepcific entity.
    /// </summary>
    /// <param name="oid"></param>
    /// <param name="t"></param>
    /// <returns></returns>
    public ActionResult StandardReportEntity(Guid oid, Type t)
    {
      if (this.UserController.EffectiveUser.HasRole(this.UserController.EffectiveUser, Constants.AdministrationRoleOids) == false)
      {
        throw new HttpException((int)HttpStatusCode.Forbidden, string.Format("'{0}' does not have permission to Access the Administration Audit section.", this.UserController.LoggedOnUser.Login));
      }

      AuditQuery a = new AuditQuery();
      a.TableName = this.DbSession.GetSqlTableName(t);
      a.Where = string.Format("[Key 1] = '{0}'", oid.ToString());

      StandardApexReport report = new StandardApexReport();
      IEnumerable<AuditData> results = this.auditController.GetReportData(report, a);
      ViewBag.ListCount = results.Count();
      return View(results);
    }

    /// <summary>
    /// Returns data for a specific Entity Oid
    /// </summary>
    /// <param name="request"></param>
    /// <param name="oid"></param>
    /// <param name="t"></param>
    /// <returns></returns>
    public ActionResult StandardReportEntity_ReadData([DataSourceRequest]DataSourceRequest request, Guid oid, Type t)
    {
      if (request.PageSize == 0)
      {
        request.PageSize = 20;
      }

      StandardApexReport report = new StandardApexReport();
      AuditQuery a = new AuditQuery();
      a.TableName = this.DbSession.GetSqlTableName(t);
      a.Where = string.Format("[Key 1] = '{0}'", oid.ToString());

      IEnumerable<AuditData> results = this.auditController.GetReportData(report, a);
      ViewBag.ListCount = results.Count();
      return Json(results.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
    }

    /// <summary>
    /// This was attempt to get Javascript Kendo grid working (i.e. not using Kendo MVC wrappers)
    /// </summary>
    /// <param name="request"></param>
    /// <param name="filter"></param>
    /// <returns></returns>
    [HttpGet]
    public JsonResult StandardReportResultsJavascripExample_ReadData([DataSourceRequest]DataSourceRequest request, [BoundModel(DontLoad = true, DontMerge = false)]TypedModel<AuditQuery> filter)
    {
      if (request.PageSize == 0)
      {
        request.PageSize = 20;
      }

      StandardApexReport report = new StandardApexReport();
      IEnumerable<AuditData> results = this.auditController.GetReportData(report, filter.Entity);
      ViewBag.ListCount = results.Count();

      JsonResult r = Json(results, JsonRequestBehavior.AllowGet);
      return r;
      //return Json(results.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
    }

    public ActionResult StandardReportInstance(Guid oid, Type t)
    {
      AuditQuery m = new AuditQuery();
      m.RowCount = 1000;
      m.FieldName = "oid";
      m.NewValue = oid.ToString();
      m.TableName = this.DbSession.GetSqlTableName(t);
      SetViewBagSelectLists(m);
      return View(m);
    }

    #region Private Methods

    private void SetViewBagSelectLists(AuditQuery m)
    {
      ViewBag.ListCount = m.RowCount;
      ViewBag.AuditActions = UtilsSelectListEnum.SelectListFor<AuditAction>();
      ViewBag.AuditTableNames = new SelectList((IEnumerable)this.auditController.GetLookupData(LookupType.TableName), "Key", "Value", m.TableName);
      ViewBag.AuditModifiedBys = new SelectList((IEnumerable)this.auditController.GetLookupData(LookupType.ModifiedBy), "Key", "Value", m.ModifiedBy);
      ViewBag.AuditColumnNames = new SelectList((IEnumerable)this.auditController.GetLookupData(LookupType.FieldName), "Key", "Value", m.FieldName);
      ViewBag.AuditComputers = new SelectList((IEnumerable)this.auditController.GetLookupData(LookupType.Host), "Key", "Value", m.Host);
      ViewBag.AuditApplications = new SelectList((IEnumerable)this.auditController.GetLookupData(LookupType.Application), "Key", "Value", m.Application);
    }

    #endregion
  }
}