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
    #region Lotto Draw

    //public ActionResult CreateLotto()
    //{
    //  LottoServiceController service = (LottoServiceController)this.Injector.Activate(typeof(LottoServiceController));
    //  if (this.UserController.EffectiveUser.GetClassAccess(typeof(Lotto)).Allows(Access.Create) == false || service.CanUserEditLotto(new ModelStateWrapper(this.ModelState)) == false)
    //  {
    //    return RedirectToAction("View", "Error", new
    //    {
    //      errorMsg = "You do not have permission to 'Create' Lotto Draws.",
    //      redirectUrl = Request.UrlReferrer
    //    });
    //  }
    //  Lotto m = new Lotto();
    //  return View(m);
    //}

    //[HttpPost]
    //[ActionName("CreateLotto")]
    //public ActionResult CreateLottoPost(Lotto m)
    //{
    //  LottoServiceController service = (LottoServiceController)this.Injector.Activate(typeof(LottoServiceController));
    //  if (this.UserController.EffectiveUser.GetClassAccess(typeof(Lotto)).Allows(Access.Create) == false || service.CanUserEditLotto(new ModelStateWrapper(this.ModelState)) == false)
    //  {
    //    return RedirectToAction("View", "Error", new
    //    {
    //      errorMsg = "You do not have permission to 'Create' Lotto Draws.",
    //      redirectUrl = Request.UrlReferrer
    //    });
    //  }

    //  if (ModelState.IsValid)
    //  {
    //    if (service.SaveLotto(new ModelStateWrapper(this.ModelState), m))
    //    {
    //      //Sucessfully saved Lotto
    //      return RedirectToAction("EditLotto", new { Oid = m.Oid });
    //    }
    //  }

    //  return View(m);
    //}

    //public ActionResult EditLotto(Lotto m)
    //{
    //  LottoServiceController service = (LottoServiceController)this.Injector.Activate(typeof(LottoServiceController));
    //  if (this.UserController.EffectiveUser.GetClassAccess(typeof(Lotto)).Allows(Access.Update) == false || service.CanUserEditLotto(new ModelStateWrapper(this.ModelState)) == false)
    //  {
    //    return RedirectToAction("View", "Error", new
    //    {
    //      errorMsg = "You do not have permission to 'Edit' Lotto Draws.",
    //      redirectUrl = Request.UrlReferrer
    //    });
    //  }
    //  return View(m);
    //}

    //[HttpPost]
    //[ActionName("EditLotto")]
    //public ActionResult EditLottoPost(Lotto m)
    //{
    //  LottoServiceController service = (LottoServiceController)this.Injector.Activate(typeof(LottoServiceController));
    //  if (this.UserController.EffectiveUser.GetClassAccess(typeof(Lotto)).Allows(Access.Update) == false || service.CanUserEditLotto(new ModelStateWrapper(this.ModelState)) == false)
    //  {
    //    return RedirectToAction("View", "Error", new
    //    {
    //      errorMsg = "You do not have permission to 'Edit' Lotto Draws.",
    //      redirectUrl = Request.UrlReferrer
    //    });
    //  }

    //  if (ModelState.IsValid)
    //  {
    //    if (service.SaveLotto(new ModelStateWrapper(this.ModelState), m))
    //    {
    //      //Sucessfully saved Lotto
    //      return RedirectToAction("EditLotto", new { Oid = m.Oid });
    //    }
    //  }

    //  return View(m);
    //}

    public ActionResult CreateLotto([BoundModel(DontLoad = true, DontMerge = true)]TypedModel<Lotto> m)
    {
      LottoServiceController service = (LottoServiceController)this.Injector.Activate(typeof(LottoServiceController));
      if (this.UserController.EffectiveUser.GetClassAccess(m.EntityType).Allows(Access.Create) == false || service.CanUserEditLotto(new ModelStateWrapper(this.ModelState)) == false)
      {
        return RedirectToAction("View", "Error", new
        {
          errorMsg = "You do not have permission to 'Create' Lotto Draws.",
          redirectUrl = Request.UrlReferrer
        });
      }

      return View(m);
    }

    [HttpPost]
    [ActionName("CreateLotto")]
    public ActionResult CreateLottoPost(TypedModel<Lotto> m)
    {
      LottoServiceController service = (LottoServiceController)this.Injector.Activate(typeof(LottoServiceController));
      if (this.UserController.EffectiveUser.GetClassAccess(m.EntityType).Allows(Access.Create) == false || service.CanUserEditLotto(new ModelStateWrapper(this.ModelState)) == false)
      {
        return RedirectToAction("View", "Error", new
        {
          errorMsg = "You do not have permission to 'Create' Lotto Draws.",
          redirectUrl = Request.UrlReferrer
        });
      }

      if (ModelState.IsValid)
      {
        if (service.SaveLotto(new ModelStateWrapper(this.ModelState), m.Entity))
        {
          //Sucessfully saved Lotto
          return RedirectToAction("EditLotto", new { Oid = m.Entity.Oid });
        }
      }
      ViewBag.EditLotto = service.CanUserEditLotto();
      return View(m);
    }

    public ActionResult EditLotto(TypedModel<Lotto> m)
    {
      LottoServiceController service = (LottoServiceController)this.Injector.Activate(typeof(LottoServiceController));
      if (this.UserController.EffectiveUser.GetClassAccess(m.EntityType).Allows(Access.Update) == false || service.CanUserEditLotto(new ModelStateWrapper(this.ModelState)) == false)
      {
        return RedirectToAction("View", "Error", new
        {
          errorMsg = "You do not have permission to 'Edit' Lotto Draws.",
          redirectUrl = Request.UrlReferrer
        });
      }

      ViewBag.EditLotto = service.CanUserEditLotto();
      return View(m);
    }

    [HttpPost]
    [ActionName("EditLotto")]
    public ActionResult EditLottoPost(TypedModel<Lotto> m)
    {
      LottoServiceController service = (LottoServiceController)this.Injector.Activate(typeof(LottoServiceController));
      if (this.UserController.EffectiveUser.GetClassAccess(m.EntityType).Allows(Access.Update) == false || service.CanUserEditLotto(new ModelStateWrapper(this.ModelState)) == false)
      {
        return RedirectToAction("View", "Error", new
        {
          errorMsg = "You do not have permission to 'Edit' Lotto Draws.",
          redirectUrl = Request.UrlReferrer
        });
      }

      if (ModelState.IsValid)
      {
        service.SaveLotto(new ModelStateWrapper(this.ModelState), m.Entity);
      }

      ViewBag.EditLotto = service.CanUserEditLotto();
      return View(m);
    }

    #endregion

    #region Lotto Results

    public PartialViewResult SearchLottoResults(string controlId = null, bool selectMany = false)
    {
      LottoServiceController service = (LottoServiceController)this.Injector.Activate(typeof(LottoServiceController));
      TypedModel<LottoResultQry> model = new TypedModel<LottoResultQry>();
      model.Init();
      ViewBag.LottoResultEdit = service.CanUserEditLotto();
      SetViewBagSelectLists_ForSearch(controlId);
      return PartialView(model);
    }

    public PartialViewResult ViewLottoResult(TypedModel<LottoResult> m)
    {
      return PartialView(m);
    }

    public PartialViewResult CreateLottoResultAjax(Guid lottoOid)
    {
      LottoServiceController service = (LottoServiceController)this.Injector.Activate(typeof(LottoServiceController));
      if (service.CanUserEditLotto(new ModelStateWrapper(this.ModelState)) == false)
      {
        this.ModelState.AddModelError("Lotto", "You do not have permission to 'Create' Lotto Result.");
        throw new ModelStateException(this.ModelState);
      }

      LottoResult m = new LottoResult();
      m.Lotto = this.DbSession.Get<Lotto>(lottoOid);
      return PartialView(m);
    }

    [HttpPost]
    [ActionName("CreateLottoResultAjax")]
    public ActionResult CreateLottoResultAjaxPost(TypedModel<LottoResult> m)
    {
      LottoServiceController service = (LottoServiceController)this.Injector.Activate(typeof(LottoServiceController));
      if (service.CanUserEditLotto(new ModelStateWrapper(this.ModelState)) == false)
      {
        this.ModelState.AddModelError("Lotto", "You do not have permission to 'Create' Lotto Result.");
        throw new ModelStateException(this.ModelState);
      }

      if (ModelState.IsValid)
      {
        if (service.CreateLottoResult(new ModelStateWrapper(this.ModelState), m.Entity))
        {
          ModelJsonResult j = new ModelJsonResult();
          j.success = true;
          return Json(j, JsonRequestBehavior.AllowGet);
        }
      }
      throw new ModelStateException(this.ModelState) { DisplayKeyInError = false };
    }

    //public JsonResult CreateLottoResultAjaxPost(LottoResult m)
    //{
    //  LottoServiceController service = (LottoServiceController)this.Injector.Activate(typeof(LottoServiceController));
    //  if (service.CanUserEditLotto(new ModelStateWrapper(this.ModelState)) == false)
    //  {
    //    this.ModelState.AddModelError("Lotto", "You do not have permission to 'Create' Lotto Result.");
    //    throw new ModelStateException(this.ModelState);
    //  }

    //  if (ModelState.IsValid)
    //  {
    //    if (service.CreateLottoResult(new ModelStateWrapper(this.ModelState), m))
    //    {
    //      ModelJsonResult j = new ModelJsonResult();
    //      j.success = true;
    //      return Json(j, JsonRequestBehavior.AllowGet);
    //    }
    //  }
    //  throw new ModelStateException(this.ModelState);
    //}

    public ActionResult CreateLottoResult([BoundModel(DontLoad = true, DontMerge = true)]TypedModel<LottoResult> m)
    {
      LottoServiceController service = (LottoServiceController)this.Injector.Activate(typeof(LottoServiceController));
      if (this.UserController.EffectiveUser.GetClassAccess(m.EntityType).Allows(Access.Create) == false || service.CanUserEditLotto(new ModelStateWrapper(this.ModelState)) == false)
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
      if (this.UserController.EffectiveUser.GetClassAccess(m.EntityType).Allows(Access.Create) == false || service.CanUserEditLotto(new ModelStateWrapper(this.ModelState)) == false)
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

    #region Lotto Result Winners

    [AcceptVerbs(HttpVerbs.Post)]
    public ActionResult LottoResultWinners_GridDelete([DataSourceRequest] DataSourceRequest request, ModelLottoResultWinner product)
    {
      if (this.UserController.EffectiveUser.GetClassAccess(typeof(TeamMember)).Allows(Access.Delete) == false)
      {
        return RedirectToAction("View", "Error", new
        {
          errorMsg = "You do not have permission to 'Delete' Team Members's.",
          redirectUrl = Request.UrlReferrer
        });
      }

      LottoResultWinner domainEntity = this.DbSession.Get<LottoResultWinner>(product.Oid);
      LottoServiceController service = (LottoServiceController)this.Injector.Activate(typeof(LottoServiceController));
      if (service.DeleteLottoResultWinner(new ModelStateWrapper(this.ModelState), ref domainEntity))
      {
        return Json(new[] { product }.ToDataSourceResult(request, ModelState));
      }
      return Json(new[] { product }.ToDataSourceResult(request, ModelState));
    }

    public JsonResult LottoResultWinners_GridRead([DataSourceRequest]DataSourceRequest request, Guid lottoResultOid)
    {
      if (request.PageSize == 0)
      {
        request.PageSize = 10;
      }

      IEnumerable<LottoResultWinner> lottoResultWinners = this.DbSession.QueryOver<LottoResultWinner>().Where(x => x.LottoResult.Oid == lottoResultOid).List<LottoResultWinner>();
      IMapper Mapper = AutoMapperConfig.MapperConfiguration.CreateMapper();
      IEnumerable<ModelLottoResultWinner> result = Mapper.Map<IEnumerable<ModelLottoResultWinner>>(lottoResultWinners);
      return Json(result.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
    }

    public PartialViewResult CreateLottoResultWinnerAjax(Guid lottoResultOid)
    {
      LottoServiceController service = (LottoServiceController)this.Injector.Activate(typeof(LottoServiceController));
      if (service.CanUserEditLotto(new ModelStateWrapper(this.ModelState)) == false)
      {
        this.ModelState.AddModelError("Lotto", "You do not have permission to 'Create' Lotto Result Winners.");
        throw new ModelStateException(this.ModelState);
      }

      TypedModel<LottoResultWinner> m = new TypedModel<LottoResultWinner>();
      m.Init();
      m.Entity.LottoResult = this.DbSession.Get<LottoResult>(lottoResultOid);
      return PartialView(m);
    }

    [HttpPost]
    [ActionName("CreateLottoResultWinnerAjax")]
    public ActionResult CreateLottoResultWinnerAjaxPost(TypedModel<LottoResultWinner> m)
    {
      LottoServiceController service = (LottoServiceController)this.Injector.Activate(typeof(LottoServiceController));
      if (service.CanUserEditLotto(new ModelStateWrapper(this.ModelState)) == false)
      {
        this.ModelState.AddModelError("Lotto", "You do not have permission to 'Create' Lotto Result Winners.");
        throw new ModelStateException(this.ModelState);
      }

      if (ModelState.IsValid)
      {
        if (service.CreateLottoResultWinner(new ModelStateWrapper(this.ModelState), ref m))
        {
          ModelJsonResult j = new ModelJsonResult();
          j.success = true;
          return Json(j, JsonRequestBehavior.AllowGet);
        }
      }

      return View(m);
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
      //throw new ModelStateException(this.ModelState);
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