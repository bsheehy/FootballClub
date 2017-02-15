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
using Mallon.Documents.Artifacts;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace FrRocks.Controllers
{
  public class QualificationController : ControllerBase
  {
    public ActionResult Create([BoundModel(DontLoad = true, DontMerge = true)]TypedModel<Qualification> m, bool hideLayout = false)
    {
      if (this.UserController.EffectiveUser.GetClassAccess(m.EntityType).Allows(Access.Create) == false)
      {
        return RedirectToAction("View", "Error", new
        {
          errorMsg = "You do not have permission to 'Create' Qualification.",
          redirectUrl = Request.UrlReferrer
        });
      }

      return View(m);
    }

    [HttpPost]
    [ActionName("Create")]
    public ActionResult CreatePost(TypedModel<Qualification> m)
    {
      if (this.UserController.EffectiveUser.GetClassAccess(m.EntityType).Allows(Access.Create) == false)
      {
        return RedirectToAction("View", "Error", new
        {
          errorMsg = "You do not have permission to 'Create' Qualification.",
          redirectUrl = Request.UrlReferrer
        });
      }

      if (ModelState.IsValid)
      {
        QualificationServiceController service = (QualificationServiceController)this.Injector.Activate(typeof(QualificationServiceController));
        if (service.SaveQualification(new ModelStateWrapper(this.ModelState), m.Entity))
        {
          //Sucessfully saved Qualification
          return RedirectToAction("Edit", new { Oid = m.Entity.Oid });
        }
      }

      //If not sucessful saved Qualification then return to View
      return View(m);
    }

    public ActionResult Edit(TypedModel<Qualification> m)
    {
      if (this.UserController.EffectiveUser.GetClassAccess(m.EntityType).Allows(Access.Update) == false)
      {
        if (this.UserController.EffectiveUser.GetClassAccess(m.EntityType).Allows(Access.Read) == false)
        {
          return RedirectToAction("View", "Error", new
          {
            errorMsg = "You do not have permission to 'Edit' Qualification.",
            redirectUrl = Request.UrlReferrer
          });
        }
        else
        {
          //Go to view only access
          ViewBag.PnlView = Enum.GetName(typeof(PageViewType), PageViewType.ReadOnly);
        }
      }
      else
      {
        ViewBag.PnlView = Enum.GetName(typeof(PageViewType), PageViewType.Edit);
      }

      QualificationServiceController service = (QualificationServiceController)this.Injector.Activate(typeof(QualificationServiceController));
      ViewBag.EditQualification = service.CanUserEditQualification();
      ViewBag.EditAttachedDocs = UserController.EffectiveUser.GetClassAccess(typeof(AttachedDocument)).Allows(Access.Update);
      UtilsAttachedDocuments.SetViewDataAttachedDocuments(this.DbSession, ViewData, typeof(Qualification), m.Entity.Oid);
      return View(m);
    }

    [HttpPost]
    [ActionName("Edit")]
    public ActionResult EditPost(TypedModel<Qualification> m)
    {
      if (this.UserController.EffectiveUser.GetClassAccess(m.EntityType).Allows(Access.Update) == false)
      {
        return RedirectToAction("View", "Error", new
        {
          errorMsg = "You do not have permission to 'Edit' Qualification.",
          redirectUrl = Request.UrlReferrer
        });
      }
      ViewBag.PnlView = Enum.GetName(typeof(PageViewType), PageViewType.Edit);

      QualificationServiceController service = (QualificationServiceController)this.Injector.Activate(typeof(QualificationServiceController));
      if (ModelState.IsValid)
      {
        service.SaveQualification(new ModelStateWrapper(this.ModelState), m.Entity);
      }

      //If not sucessful saved Qualification then return to View
      ViewBag.EditQualification = service.CanUserEditQualification();
      ViewBag.EditAttachedDocs = UserController.EffectiveUser.GetClassAccess(typeof(AttachedDocument)).Allows(Access.Update);
      UtilsAttachedDocuments.SetViewDataAttachedDocuments(this.DbSession, ViewData, typeof(Qualification), m.Entity.Oid);
      return View(m);
    }

    public PartialViewResult Search(string controlId = null, bool selectMany = false)
    {
      TypedModel<QualificationQry> model = new TypedModel<QualificationQry>();
      model.Init();
      ViewBag.SelectMany = selectMany;
      SetViewOptions();
      SetViewBagSelectLists_ForSearch(controlId);
      return PartialView(model);
    }

    public ActionResult SearchResults_ReadData([DataSourceRequest]DataSourceRequest request, TypedModel<QualificationQry> filter)
    {
      if (request.PageSize == 0)
      {
        request.PageSize = 30;
      }

      IEnumerable<Qualification> qualifications = GetQualifications(filter);
      IMapper Mapper = AutoMapperConfig.MapperConfiguration.CreateMapper();
      IEnumerable<ModelQualification> result = Mapper.Map<IEnumerable<ModelQualification>>(qualifications);
      return Json(result.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
    }

    private IEnumerable<Qualification> GetQualifications(TypedModel<QualificationQry> filter)
    {
      IEnumerable<Qualification> result = new List<Qualification>();
      if (UserController.EffectiveUser.GetClassAccess(typeof(Qualification)).Allows(Access.Read))
      {
        result = filter.Entity.List(this.DbSession).Cast<Qualification>();
      }
      return result;
    }

    private void SetViewOptions()
    {
      bool editQualification = UserController.EffectiveUser.GetClassAccess(typeof(Qualification)).Allows(Access.Update);
      ViewBag.QualificationEdit = editQualification;
      if (editQualification)
      {
        ViewBag.PnlView = Utility.setPnlView("Edit", UserController, typeof(Qualification));
      }
      else
      {
        ViewBag.PnlView = Utility.setPnlView("ReadOnly", UserController, typeof(Qualification));
      }

      bool editPerson = UserController.EffectiveUser.GetClassAccess(typeof(Person)).Allows(Access.Update);
      ViewBag.PersonEdit = editPerson;
    }

    #region Qualification Members

    public JsonResult QualificationMember_GridRead([DataSourceRequest]DataSourceRequest request, Guid qualificationOid)
    {
      if (request.PageSize == 0)
      {
        request.PageSize = 30;
      }

      IEnumerable<PersonQualification> m = GetQualificationMembers(qualificationOid);
      IMapper Mapper = AutoMapperConfig.MapperConfiguration.CreateMapper();
      IEnumerable<ModelPersonQualification> result = Mapper.Map<IEnumerable<ModelPersonQualification>>(m);
      return Json(result.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
    }

    [AcceptVerbs(HttpVerbs.Post)]
    public ActionResult QualificationMember_Delete([DataSourceRequest] DataSourceRequest request, ModelPersonQualification product)
    {
      if (this.UserController.EffectiveUser.GetClassAccess(typeof(PersonQualification)).Allows(Access.Delete) == false)
      {
        return RedirectToAction("View", "Error", new
        {
          errorMsg = "You do not have permission to 'Delete' Qualification Members's.",
          redirectUrl = Request.UrlReferrer
        });
      }

      PersonQualification domainEntity = this.DbSession.Get<PersonQualification>(product.Oid);
      QualificationServiceController service = (QualificationServiceController)this.Injector.Activate(typeof(QualificationServiceController));
      if (service.DeletePersonQualification(new ModelStateWrapper(this.ModelState), ref domainEntity))
      {
        IMapper Mapper = AutoMapperConfig.MapperConfiguration.CreateMapper();
        Mapper.Map<PersonQualification, ModelPersonQualification>(domainEntity, product);
        return Json(new[] { product }.ToDataSourceResult(request, ModelState));
      }
      return Json(new[] { product }.ToDataSourceResult(request, ModelState));
      //throw new ModelStateException(this.ModelState);
    }

    private IEnumerable<PersonQualification> GetQualificationMembers(TypedModel<QualificationQry> filter)
    {
      IEnumerable<PersonQualification> result = new List<PersonQualification>();
      if (UserController.EffectiveUser.GetClassAccess(typeof(PersonQualification)).Allows(Access.Read))
      {
        result = filter.Entity.List(this.DbSession).Cast<PersonQualification>();
      }
      return result;
    }

    private IEnumerable<PersonQualification> GetQualificationMembers(Guid qualificationOid)
    {
      IEnumerable<PersonQualification> result = this.DbSession.QueryOver<PersonQualification>().List<PersonQualification>().Where(x => x.Qualification.Oid == qualificationOid).OrderBy(x => x.Person.Surname);
      return result;
    }

    #endregion

    #region Search Person Qualifications

    public PartialViewResult SearchPersonQualification(string controlId = null, bool selectMany = false)
    {
      TypedModel<PersonQualificationQry> model = new TypedModel<PersonQualificationQry>();
      model.Init();
      ViewBag.SelectMany = selectMany;
      SetViewOptions();
      SetViewBagSelectLists_ForSearch(controlId);
      return PartialView(model);
    }

    public ActionResult SearchPersonQualificationResults_ReadData([DataSourceRequest]DataSourceRequest request, TypedModel<PersonQualificationQry> filter)
    {
      if (request.PageSize == 0)
      {
        request.PageSize = 30;
      }

      IEnumerable<PersonQualification> personQualifications = GetPersonQualifications(filter);
      IMapper Mapper = AutoMapperConfig.MapperConfiguration.CreateMapper();
      IEnumerable<ModelPersonQualification> result = Mapper.Map<IEnumerable<ModelPersonQualification>>(personQualifications);
      return Json(result.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
    }


    private IEnumerable<PersonQualification> GetPersonQualifications(TypedModel<PersonQualificationQry> filter)
    {
      IEnumerable<PersonQualification> result = new List<PersonQualification>();
      if (UserController.EffectiveUser.GetClassAccess(typeof(Qualification)).Allows(Access.Read))
      {
        result = filter.Entity.List(this.DbSession).Cast<PersonQualification>();
      }
      return result;
    }

    #endregion

    private void SetViewBagSelectLists_ForSearch(string controlId)
    {
      ViewBag.TeamControlId = controlId;
      ViewBag.Qualifications = UtilsSelectLists.Qualifications(this.DbSession);
      ViewBag.MaxRecords = UtilsSelectLists.QueryMaxRecords();
    }
  }
}