using AutoMapper;
using Club.Domain.Artifacts;
using Club.Domain.Models;
using Club.Domain.Queries;
using Club.Services.Controllers;
using Club.Services.Utils;
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
  public class LottoController : ControllerBase
  {
    #region Lotto Results

    public PartialViewResult SearchLottoResults(string controlId = null, bool selectMany = false)
    {
      LottoServiceController service = (LottoServiceController)this.Injector.Activate(typeof(LottoServiceController));
      TypedModel<LottoResultQry> model = new TypedModel<LottoResultQry>();
      model.Init();
      ViewBag.LottoResultEdit = service.CanUserEditLottoResult();
      SetViewBagSelectLists_ForSearch(controlId);
      return PartialView(model);
    }

    public PartialViewResult ViewLottoResult(TypedModel<LottoResult> m)
    {
      return PartialView(m);
    }

    public ActionResult CreateLottoResult([BoundModel(DontLoad = true, DontMerge = true)]TypedModel<LottoResult> m)
    {
      LottoServiceController service = (LottoServiceController)this.Injector.Activate(typeof(LottoServiceController));
      if (this.UserController.EffectiveUser.GetClassAccess(m.EntityType).Allows(Access.Create) == false || service.CanUserEditLottoResult(new ModelStateWrapper(this.ModelState)) == false)
      {
        return RedirectToAction("View", "Error", new
        {
          errorMsg = "You do not have permission to 'Create' Lotto Results.",
          redirectUrl = Request.UrlReferrer
        });
      }
      return View(m);
    }

    [HttpPost]
    [ActionName("CreateLottoResult")]
    public ActionResult CreateLottoResultPost(TypedModel<LottoResult> m)
    {
      LottoServiceController service = (LottoServiceController)this.Injector.Activate(typeof(LottoServiceController));
      if (this.UserController.EffectiveUser.GetClassAccess(m.EntityType).Allows(Access.Create) == false || service.CanUserEditLottoResult(new ModelStateWrapper(this.ModelState)) == false)
      {
        return RedirectToAction("View", "Error", new
        {
          errorMsg = "You do not have permission to 'Create' Lotto Result.",
          redirectUrl = Request.UrlReferrer
        });
      }

      if (ModelState.IsValid)
      {
        if (service.CreateLottoResult(new ModelStateWrapper(this.ModelState), m.Entity))
        {
          //Sucessfully saved LottoResult
          return RedirectToAction("SearchLottoResults");
        }
      }

      return View(m);
    }

    public JsonResult LottoResult_GridRead([DataSourceRequest]DataSourceRequest request, TypedModel<LottoResultQry> filter)
    {
      if (request.PageSize == 0)
      {
        request.PageSize = 10;
      }

      IEnumerable<LottoResult> lottoResults = GetLottoResults(filter);
      IMapper Mapper = AutoMapperConfig.MapperConfiguration.CreateMapper();
      IEnumerable<ModelLottoResult> result = Mapper.Map<IEnumerable<ModelLottoResult>>(lottoResults);
      return Json(result.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
    }

    [AcceptVerbs(HttpVerbs.Post)]
    public ActionResult LottoResult_Delete([DataSourceRequest] DataSourceRequest request, ModelLottoResult product)
    {
      LottoServiceController service = (LottoServiceController)this.Injector.Activate(typeof(LottoServiceController));
      if (this.UserController.EffectiveUser.GetClassAccess(typeof(LottoResult)).Allows(Access.Delete) == false)
      {
        return RedirectToAction("View", "Error", new
        {
          errorMsg = "You do not have permission to 'Delete' Team Members's.",
          redirectUrl = Request.UrlReferrer
        });
      }
      if (service.DeleteLottoResult(new ModelStateWrapper(this.ModelState), product.Oid))
      {
        return Json(new[] { product }.ToDataSourceResult(request, ModelState));
      }
      return Json(new[] { product }.ToDataSourceResult(request, ModelState));
      //throw new ModelStateException(this.ModelState);
    }

    private IEnumerable<LottoResult> GetLottoResults(TypedModel<LottoResultQry> filter)
    {
      IEnumerable<LottoResult> result = new List<LottoResult>();
      if (UserController.EffectiveUser.GetClassAccess(typeof(LottoResult)).Allows(Access.Read))
      {
        result = filter.Entity.List(this.DbSession).Cast<LottoResult>();
      }
      return result;
    }

    #endregion

    #region Lotto Direct Debits
    public JsonResult LottoTicketDirectDebit_GridRead([DataSourceRequest]DataSourceRequest request, Guid personOid)
    {
      if (request.PageSize == 0)
      {
        request.PageSize = 30;
      }

      LottoServiceController service = (LottoServiceController)this.Injector.Activate(typeof(LottoServiceController));
      IEnumerable<LottoTicketDirectDebit> m = service.LottoTicketDirectDebitsGet(new ModelStateWrapper(this.ModelState), personOid, true);

      IMapper Mapper = AutoMapperConfig.MapperConfiguration.CreateMapper();
      IEnumerable<ModelLottoTicketDirectDebit> result = Mapper.Map<IEnumerable<ModelLottoTicketDirectDebit>>(m);
      return Json(result.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
    }

    [AcceptVerbs(HttpVerbs.Post)]
    public ActionResult LottoTicketDirectDebit_Update([DataSourceRequest] DataSourceRequest request, ModelLottoTicketDirectDebit product)
    {
      if (this.UserController.EffectiveUser.GetClassAccess(typeof(LottoTicketDirectDebit)).Allows(Access.Update) == false)
      {
        return RedirectToAction("View", "Error", new
        {
          errorMsg = "You do not have permission to 'Update' LottoTicketDirectDebit's.",
          redirectUrl = Request.UrlReferrer
        });
      }

      if (product != null && ModelState.IsValid)
      {
        LottoTicketDirectDebit domainEntity = this.DbSession.Get<LottoTicketDirectDebit>(product.Oid);
        IMapper Mapper = AutoMapperConfig.MapperConfiguration.CreateMapper();
        Mapper.Map<ModelLottoTicketDirectDebit, LottoTicketDirectDebit>(product, domainEntity);

        LottoServiceController service = (LottoServiceController)this.Injector.Activate(typeof(LottoServiceController));
        if (service.LottoTicketDirectDebitSave(new ModelStateWrapper(this.ModelState), ref domainEntity))
        {
          Mapper.Map<LottoTicketDirectDebit, ModelLottoTicketDirectDebit>(domainEntity, product);
          return Json(new[] { product }.ToDataSourceResult(request, ModelState));
        }
      }
      return Json(new[] { product }.ToDataSourceResult(request, ModelState));
      //throw new ModelStateException(this.ModelState);
    }

    [AcceptVerbs(HttpVerbs.Post)]
    public ActionResult LottoTicketDirectDebit_Delete([DataSourceRequest] DataSourceRequest request, ModelLottoTicketDirectDebit product)
    {
      if (this.UserController.EffectiveUser.GetClassAccess(typeof(LottoTicketDirectDebit)).Allows(Access.Delete) == false)
      {
        return RedirectToAction("View", "Error", new
        {
          errorMsg = "You do not have permission to 'Delete' LottoTicketDirectDebit's.",
          redirectUrl = Request.UrlReferrer
        });
      }

      LottoTicketDirectDebit domainEntity = this.DbSession.Get<LottoTicketDirectDebit>(product.Oid);
      LottoServiceController service = (LottoServiceController)this.Injector.Activate(typeof(LottoServiceController));
      if (service.LottoTicketDirectDebitDelete(new ModelStateWrapper(this.ModelState), ref domainEntity))
      {
        Mapper.Map<LottoTicketDirectDebit, ModelLottoTicketDirectDebit>(domainEntity, product);
        return Json(new[] { product }.ToDataSourceResult(request, ModelState));
      }
      return Json(new[] { product }.ToDataSourceResult(request, ModelState));
      //throw new ModelStateException(this.ModelState);
    }

    [AcceptVerbs(HttpVerbs.Post)]
    public ActionResult LottoTicketDirectDebit_Create([DataSourceRequest] DataSourceRequest request, ModelLottoTicketDirectDebit product, Guid personOid)
    {
      if (this.UserController.EffectiveUser.GetClassAccess(typeof(LottoTicketDirectDebit)).Allows(Access.Create) == false)
      {
        return RedirectToAction("View", "Error", new
        {
          errorMsg = "You do not have permission to 'Create' LottoTicketDirectDebit's.",
          redirectUrl = Request.UrlReferrer
        });
      }
      //product.PersonOid = personOid;

      LottoTicketDirectDebit domainEntity = new LottoTicketDirectDebit();
      IMapper Mapper = AutoMapperConfig.MapperConfiguration.CreateMapper();
      Mapper.Map<ModelLottoTicketDirectDebit, LottoTicketDirectDebit>(product, domainEntity);
      domainEntity.Person = this.DbSession.Get<Person>(personOid);

      LottoServiceController service = (LottoServiceController)this.Injector.Activate(typeof(LottoServiceController));
      if (service.LottoTicketDirectDebitSave(new ModelStateWrapper(this.ModelState), ref domainEntity))
      {
        Mapper.Map<LottoTicketDirectDebit, ModelLottoTicketDirectDebit>(domainEntity, product);
        return Json(new[] { product }.ToDataSourceResult(request, ModelState));
      }
      return Json(new[] { product }.ToDataSourceResult(request, ModelState));
    }

    public PartialViewResult SearchDirectDebits(string controlId = null)
    {
      TypedModel<LottoTicketDirectDebitQry> model = new TypedModel<LottoTicketDirectDebitQry>();
      model.Init();
      SetViewBagSelectLists_ForSearch(controlId);
      SetViewOptions_LottoTicketDirectDebit();
      return PartialView(model);
    }

    public ActionResult SearchDirectDebits_ReadData([DataSourceRequest]DataSourceRequest request, TypedModel<LottoTicketDirectDebitQry> filter)
    {
      if (request.PageSize == 0)
      {
        request.PageSize = 10;
      }

      IEnumerable<LottoTicketDirectDebit> entities = GetLottoTicketDirectDebits(filter);
      IMapper Mapper = AutoMapperConfig.MapperConfiguration.CreateMapper();
      IEnumerable<ModelLottoTicketDirectDebit> result = Mapper.Map<IEnumerable<ModelLottoTicketDirectDebit>>(entities);
      return Json(result.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
    }

    public ActionResult LottoTicketDirectDebitWinners()
    {
      TypedModel<ModelLottoTicketResult> model = new TypedModel<ModelLottoTicketResult>();
      model.Init();
      SetViewBagSelectLists_ForSearch("");
      SetViewOptions_LottoTicketDirectDebit();
      return View(model);
    }

    [AcceptVerbs(HttpVerbs.Post)]
    public ActionResult LottoTicketDirectDebitWinners_Read([DataSourceRequest] DataSourceRequest request, ModelLottoTicketResult m)
    {
      IList<ModelLottoTicketDirectDebit> winners;
      LottoServiceController service = (LottoServiceController)this.Injector.Activate(typeof(LottoServiceController));
      if (service.LottoTicketDirectDebitsGetWinners(new ModelStateWrapper(this.ModelState), m, out winners))
      {
        return Json(winners.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
      }
      return Json(new[] { m }.ToDataSourceResult(request, ModelState));
      //throw new ModelStateException(this.ModelState);
    }

    private IEnumerable<LottoTicketDirectDebit> GetLottoTicketDirectDebits(TypedModel<LottoTicketDirectDebitQry> filter)
    {
      IEnumerable<LottoTicketDirectDebit> result = new List<LottoTicketDirectDebit>();
      if (UserController.EffectiveUser.GetClassAccess(typeof(LottoTicketDirectDebit)).Allows(Access.Read))
      {
        result = filter.Entity.List(this.DbSession).Cast<LottoTicketDirectDebit>();
      }
      return result;
    }

    private void SetViewBagSelectLists_ForSearch(string controlId)
    {
      ViewBag.PersonControlId = controlId;
      ViewBag.MaxRecords = UtilsSelectLists.QueryMaxRecords();
    }

    private void SetViewOptions_LottoTicketDirectDebit()
    {
      bool editPerson = UserController.EffectiveUser.GetClassAccess(typeof(LottoTicketDirectDebit)).Allows(Access.Update);
      ViewBag.PersonEdit = editPerson;
      if (editPerson)
      {
        ViewBag.PnlView = Utility.setPnlView("Edit", UserController, typeof(LottoTicketDirectDebit));
      }
      else
      {
        ViewBag.PnlView = Utility.setPnlView("ReadOnly", UserController, typeof(LottoTicketDirectDebit));
      }
    }

    #endregion
  }
}