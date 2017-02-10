﻿using AutoMapper;
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
  public class PersonController : ControllerBase
  {
    public ActionResult Create([BoundModel(DontLoad = true, DontMerge = true)]TypedModel<Person> m, bool hideLayout = false)
    {
      if (this.UserController.EffectiveUser.GetClassAccess(m.EntityType).Allows(Access.Create) == false)
      {
        return RedirectToAction("View", "Error", new
        {
          errorMsg = "You do not have permission to 'Create' People.",
          redirectUrl = Request.UrlReferrer
        });
      }

      //ViewBag.HideLayout = hideLayout;
      SetViewBagSelectLists(m);
      return View(m);
    }

    [HttpPost]
    [ActionName("Create")]
    public ActionResult CreatePost(TypedModel<Person> m)
    {
      if (this.UserController.EffectiveUser.GetClassAccess(m.EntityType).Allows(Access.Create) == false)
      {
        return RedirectToAction("View", "Error", new
        {
          errorMsg = "You do not have permission to 'Create' People.",
          redirectUrl = Request.UrlReferrer
        });
      }

      if (ModelState.IsValid)
      {
        PersonServiceController service = (PersonServiceController)this.Injector.Activate(typeof(PersonServiceController));
        if (service.SavePerson(new ModelStateWrapper(this.ModelState), m.Entity))
        {
          //Sucessfully saved Person
          return RedirectToAction("Edit", new { Oid = m.Entity.Oid });
        }
      }

      //If not sucessful saved Person then return to View
      SetViewBagSelectLists(m);
      return View(m);
    }

    public ActionResult Edit(TypedModel<Person> m)
    {
      if (this.UserController.EffectiveUser.GetClassAccess(m.EntityType).Allows(Access.Update) == false)
      {
        if (this.UserController.EffectiveUser.GetClassAccess(m.EntityType).Allows(Access.Read) == false)
        {
          return RedirectToAction("View", "Error", new
          {
            errorMsg = "You do not have permission to 'Edit' People.",
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

      ViewBag.EditLottoDirectDebits = UserController.EffectiveUser.GetClassAccess(typeof(AttachedDocument)).Allows(Access.Update);
      ViewBag.EditAttachedDocs = UserController.EffectiveUser.GetClassAccess(typeof(AttachedDocument)).Allows(Access.Update);
      UtilsAttachedDocuments.SetViewDataAttachedDocuments(this.DbSession, ViewData, typeof(Person), m.Entity.Oid);
      SetViewBagSelectLists(m);
      return View(m);
    }

    [HttpPost]
    [ActionName("Edit")]
    public ActionResult EditPost(TypedModel<Person> m)
    {
      if (this.UserController.EffectiveUser.GetClassAccess(m.EntityType).Allows(Access.Update) == false)
      {
        return RedirectToAction("View", "Error", new
        {
          errorMsg = "You do not have permission to 'Edit' People.",
          redirectUrl = Request.UrlReferrer
        });
      }
      ViewBag.PnlView = Enum.GetName(typeof(PageViewType), PageViewType.Edit);

      if (ModelState.IsValid)
      {
        PersonServiceController service = (PersonServiceController)this.Injector.Activate(typeof(PersonServiceController));
        service.SavePerson(new ModelStateWrapper(this.ModelState), m.Entity);
      }

      //If not sucessful saved Person then return to View
      SetViewBagSelectLists(m);
      return View(m);
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
      SetViewOptions();
      SetViewBagSelectLists_ForSearch(controlId);
      return PartialView(model);
    }

    public ActionResult SearchResults_ReadData([DataSourceRequest]DataSourceRequest request, TypedModel<PersonQry> filter)
    {
      if (request.PageSize == 0)
      {
        request.PageSize = 10;
      }

      IEnumerable<Person> people = GetPeople(filter);
      IMapper Mapper = AutoMapperConfig.MapperConfiguration.CreateMapper();
      IEnumerable<ModelPerson> result = Mapper.Map<IEnumerable<ModelPerson>>(people);
      return Json(result.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
    }

    #region Person Guardians

    public PartialViewResult SelectGuardian(Guid personOid, string controlId = null)
    {
      TypedModel<PersonQry> model = new TypedModel<PersonQry>();
      model.Init();
      model.Entity.aq_PersonOid = personOid;
      ViewBag.SelectGuardian = true;
      ViewBag.PersonControlId = controlId;
      ViewBag.PersonOid = personOid.ToString();
      SetViewOptions();
      SetViewBagSelectLists_ForSearch(controlId);
      return PartialView(model);
    }

    [HttpPost]
    [ActionName("SelectGuardian")]
    public ActionResult SelectGuardianPost(Guid personOid, Guid guardianOid)
    {
      if (this.UserController.EffectiveUser.GetClassAccess(typeof(Person)).Allows(Access.Update) == true)
      {
        PersonServiceController service = (PersonServiceController)this.Injector.Activate(typeof(PersonServiceController));
        if (service.SavePersonGuardian(new ModelStateWrapper(this.ModelState), personOid, guardianOid))
        {
          ModelJsonResult j = new ModelJsonResult();
          j.success = true;
          return Json(j, JsonRequestBehavior.AllowGet);
        }
        throw new ModelStateException(this.ModelState);
      }
      else
      {
        this.ModelState.AddModelError("Person", "You do not have permissions to edit Person guardians.");
        throw new ModelStateException(this.ModelState);
      }
    }

    public JsonResult PersonGuardian_GridRead([DataSourceRequest]DataSourceRequest request, Guid personOid)
    {
      if (request.PageSize == 0)
      {
        request.PageSize = 30;
      }

      PersonServiceController service = (PersonServiceController)this.Injector.Activate(typeof(PersonServiceController));
      IEnumerable<PersonGuardian> m = service.GetPersonGuardians(new ModelStateWrapper(this.ModelState), personOid);

      IMapper Mapper = AutoMapperConfig.MapperConfiguration.CreateMapper();
      IEnumerable<ModelPersonGuardian> result = Mapper.Map<IEnumerable<ModelPersonGuardian>>(m);
      return Json(result.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
    }

    [AcceptVerbs(HttpVerbs.Post)]
    public ActionResult PersonGuardian_GridDelete([DataSourceRequest] DataSourceRequest request, ModelPersonGuardian product)
    {
      if (this.UserController.EffectiveUser.GetClassAccess(typeof(Person)).Allows(Access.Delete) == false)
      {
        return RedirectToAction("View", "Error", new
        {
          errorMsg = "You do not have permission to 'Delete' a Persons guardian.",
          redirectUrl = Request.UrlReferrer
        });
      }

      PersonGuardian domainEntity = this.DbSession.Get<PersonGuardian>(product.Oid);
      PersonServiceController service = (PersonServiceController)this.Injector.Activate(typeof(PersonServiceController));
      if (service.DeletePersonGuardian(new ModelStateWrapper(this.ModelState), ref domainEntity))
      {
        IMapper Mapper = AutoMapperConfig.MapperConfiguration.CreateMapper();
        Mapper.Map<PersonGuardian, ModelPersonGuardian>(domainEntity, product);
        return Json(new[] { product }.ToDataSourceResult(request, ModelState));
      }
      return Json(new[] { product }.ToDataSourceResult(request, ModelState));
    }

    #endregion

    #region Team Members

    public PartialViewResult SelectTeamMember(Guid teamOid, string controlId = null)
    {
      TypedModel<PersonQry> model = new TypedModel<PersonQry>();
      model.Init();
      model.Entity.aq_TeamOid = teamOid;
      ViewBag.SelectTeamMember = true;
      ViewBag.PersonControlId = controlId;
      ViewBag.TeamOid = teamOid.ToString();
      SetViewOptions();
      SetViewBagSelectLists_ForSearch(controlId);
      return PartialView(model);
    }

    [HttpPost]
    [ActionName("SelectTeamMember")]
    public ActionResult SelectTeamMemberPost(Guid personOid, Guid teamOid)
    {
      if (this.UserController.EffectiveUser.GetClassAccess(typeof(Team)).Allows(Access.Update) == true)
      {
        TeamServiceController service = (TeamServiceController)this.Injector.Activate(typeof(TeamServiceController));
        if (service.SelectTeamMember(new ModelStateWrapper(this.ModelState), personOid, teamOid))
        {
          ModelJsonResult j = new ModelJsonResult();
          j.success = true;
          return Json(j, JsonRequestBehavior.AllowGet);
        }
        throw new ModelStateException(this.ModelState);
      }
      else
      {
        this.ModelState.AddModelError("Person", "You do not have permissions to edit Person guardians.");
        throw new ModelStateException(this.ModelState);
      }
    }


    #endregion

    #region Committee Members

    public PartialViewResult SelectCommitteeMember(Guid committeeOid, string controlId = null)
    {
      CommitteeServiceController service = (CommitteeServiceController)this.Injector.Activate(typeof(CommitteeServiceController));
      if (service.CanUserEditCommittee(committeeOid) == false)
      {
        this.ModelState.AddModelError("Committee", "You do not have permissions to edit this Committee.");
        throw new ModelStateException(this.ModelState);
      }

      TypedModel<PersonQry> model = new TypedModel<PersonQry>();
      model.Init();
      model.Entity.aq_CommitteeOid = committeeOid;
      ViewBag.SelectCommitteeMember = true;
      ViewBag.PersonControlId = controlId;
      ViewBag.CommitteeOid = committeeOid.ToString();
      SetViewOptions();
      SetViewBagSelectLists_ForSearch(controlId);
      return PartialView(model);
    }

    [HttpPost]
    [ActionName("SelectCommitteeMember")]
    public ActionResult SelectCommitteeMemberPost(Guid personOid, Guid committeeOid)
    {
      if (this.UserController.EffectiveUser.GetClassAccess(typeof(Committee)).Allows(Access.Update) == true)
      {
        CommitteeServiceController service = (CommitteeServiceController)this.Injector.Activate(typeof(CommitteeServiceController));
        if (service.SelectCommitteeMember(new ModelStateWrapper(this.ModelState), personOid, committeeOid))
        {
          ModelJsonResult j = new ModelJsonResult();
          j.success = true;
          return Json(j, JsonRequestBehavior.AllowGet);
        }
        throw new ModelStateException(this.ModelState);
      }
      else
      {
        this.ModelState.AddModelError("Person", "You do not have permissions to edit Person guardians.");
        throw new ModelStateException(this.ModelState);
      }
    }


    #endregion

    #region Qualificaions

    public JsonResult PersonQualification_GridRead([DataSourceRequest]DataSourceRequest request, Guid personOid)
    {
      if (request.PageSize == 0)
      {
        request.PageSize = 30;
      }

      PersonServiceController service = (PersonServiceController)this.Injector.Activate(typeof(PersonServiceController));
      IEnumerable<PersonQualification> m = service.GetQualifications(new ModelStateWrapper(this.ModelState), personOid, true);

      IMapper Mapper = AutoMapperConfig.MapperConfiguration.CreateMapper();
      IEnumerable<ModelPersonQualification> result = Mapper.Map<IEnumerable<ModelPersonQualification>>(m);
      return Json(result.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
    }

    [AcceptVerbs(HttpVerbs.Post)]
    public ActionResult PersonQualification_Update([DataSourceRequest] DataSourceRequest request, ModelPersonQualification product)
    {
      if (this.UserController.EffectiveUser.GetClassAccess(typeof(Person)).Allows(Access.Update) == false)
      {
        return RedirectToAction("View", "Error", new
        {
          errorMsg = "You do not have permission to 'Update' PersonQualification's.",
          redirectUrl = Request.UrlReferrer
        });
      }

      if (product != null && ModelState.IsValid)
      {
        PersonQualification domainEntity = this.DbSession.Get<PersonQualification>(product.Oid);
        domainEntity.Qualification = this.DbSession.Get<Qualification>(product.QualificationOid);
        IMapper Mapper = AutoMapperConfig.MapperConfiguration.CreateMapper();
        Mapper.Map<ModelPersonQualification, PersonQualification>(product, domainEntity);

        PersonServiceController service = (PersonServiceController)this.Injector.Activate(typeof(PersonServiceController));
        if (service.SavePersonQualification(new ModelStateWrapper(this.ModelState), ref domainEntity))
        {
          Mapper.Map<PersonQualification, ModelPersonQualification>(domainEntity, product);
          return Json(new[] { product }.ToDataSourceResult(request, ModelState));
        }
      }
      return Json(new[] { product }.ToDataSourceResult(request, ModelState));
      //throw new ModelStateException(this.ModelState);
    }

    [AcceptVerbs(HttpVerbs.Post)]
    public ActionResult PersonQualification_Delete([DataSourceRequest] DataSourceRequest request, ModelPersonQualification product)
    {
      if (this.UserController.EffectiveUser.GetClassAccess(typeof(PersonQualification)).Allows(Access.Delete) == false)
      {
        return RedirectToAction("View", "Error", new
        {
          errorMsg = "You do not have permission to 'Delete' PersonQualification's.",
          redirectUrl = Request.UrlReferrer
        });
      }

      PersonQualification domainEntity = this.DbSession.Get<PersonQualification>(product.Oid);
      PersonServiceController service = (PersonServiceController)this.Injector.Activate(typeof(PersonServiceController));
      if (service.DeletePersonQualification(new ModelStateWrapper(this.ModelState), ref domainEntity))
      {
        Mapper.Map<PersonQualification, ModelPersonQualification>(domainEntity, product);
        return Json(new[] { product }.ToDataSourceResult(request, ModelState));
      }
      return Json(new[] { product }.ToDataSourceResult(request, ModelState));
      //throw new ModelStateException(this.ModelState);
    }

    [AcceptVerbs(HttpVerbs.Post)]
    public ActionResult PersonQualification_Create([DataSourceRequest] DataSourceRequest request, ModelPersonQualification product, Guid personOid)
    {
      if (this.UserController.EffectiveUser.GetClassAccess(typeof(PersonQualification)).Allows(Access.Create) == false)
      {
        return RedirectToAction("View", "Error", new
        {
          errorMsg = "You do not have permission to 'Create' PersonQualification's.",
          redirectUrl = Request.UrlReferrer
        });
      }
      //product.PersonOid = personOid;

      PersonQualification domainEntity = new PersonQualification();
      IMapper Mapper = AutoMapperConfig.MapperConfiguration.CreateMapper();
      Mapper.Map<ModelPersonQualification, PersonQualification>(product, domainEntity);
      domainEntity.Person = this.DbSession.Get<Person>(personOid);
      domainEntity.Qualification = this.DbSession.Get<Qualification>(product.QualificationOid);

      PersonServiceController service = (PersonServiceController)this.Injector.Activate(typeof(PersonServiceController));
      if (service.SavePersonQualification(new ModelStateWrapper(this.ModelState), ref domainEntity))
      {
        Mapper.Map<PersonQualification, ModelPersonQualification>(domainEntity, product);
        return Json(new[] { product }.ToDataSourceResult(request, ModelState));
      }
      return Json(new[] { product }.ToDataSourceResult(request, ModelState));
    }


    #endregion

    #region Membership Type Members

    public PartialViewResult SelectMembershipMember(Guid membershipTypeOid, string controlId = null)
    {
      TypedModel<PersonQry> model = new TypedModel<PersonQry>();
      model.Init();
      model.Entity.aq_MembershipTypeOid = membershipTypeOid;
      ViewBag.SelectPersonMembership = true;
      ViewBag.PersonControlId = controlId;
      ViewBag.MembershipTypeOid = membershipTypeOid.ToString();
      SetViewOptions();
      SetViewBagSelectLists_ForSearch(controlId);
      return PartialView(model);
    }

    [HttpPost]
    [ActionName("SelectMembershipMember")]
    public ActionResult SelectMembershipMemberPost(Guid personOid, Guid membershipTypeOid)
    {
      MembershipServiceController service = (MembershipServiceController)this.Injector.Activate(typeof(MembershipServiceController));
      if (service.CanUserEditMembership())
      {
        if (service.SelectMembershipMember(new ModelStateWrapper(this.ModelState), personOid, membershipTypeOid))
        {
          ModelJsonResult j = new ModelJsonResult();
          j.success = true;
          return Json(j, JsonRequestBehavior.AllowGet);
        }
        throw new ModelStateException(this.ModelState);
      }
      else
      {
        this.ModelState.AddModelError("Person", "You do not have permissions to add People's Membership.");
        throw new ModelStateException(this.ModelState);
      }
    }

    #endregion

    #region Lotto Winners

    public PartialViewResult SelectLottoWinner(Guid lottoWinnerOid, string controlId = null)
    {
      LottoServiceController service = (LottoServiceController)this.Injector.Activate(typeof(LottoServiceController));
      if (service.CanUserEditLotto() == false)
      {
        this.ModelState.AddModelError("Lotto", "You do not have permissions to edit Lotto details.");
        throw new ModelStateException(this.ModelState);
      }

      TypedModel<PersonQry> model = new TypedModel<PersonQry>();
      model.Init();
      model.Entity.aq_CommitteeOid = lottoWinnerOid;
      ViewBag.SelectLottoWinner = true;
      ViewBag.PersonControlId = controlId;
      ViewBag.CommitteeOid = lottoWinnerOid.ToString();
      SetViewOptions();
      SetViewBagSelectLists_ForSearch(controlId);
      return PartialView(model);
    }

    //[HttpPost]
    //[ActionName("SelectLottoWinner")]
    //public ActionResult SelectLottoWinnerPost(Guid personOid, Guid lottoWinnerOid)
    //{
    //  if (this.UserController.EffectiveUser.GetClassAccess(typeof(Committee)).Allows(Access.Update) == true)
    //  {
    //    LottoServiceController service = (LottoServiceController)this.Injector.Activate(typeof(LottoServiceController));
    //    if (service.SelectLottoResultWinner(new ModelStateWrapper(this.ModelState), personOid, lottoWinnerOid))
    //    {
    //      ModelJsonResult j = new ModelJsonResult();
    //      j.success = true;
    //      return Json(j, JsonRequestBehavior.AllowGet);
    //    }
    //    throw new ModelStateException(this.ModelState);
    //  }
    //  else
    //  {
    //    this.ModelState.AddModelError("Person", "You do not have permissions to edit Person guardians.");
    //    throw new ModelStateException(this.ModelState);
    //  }
    //}

    #endregion

    public ActionResult SelectPerson(TypedModel<Person> m, string controlId = null, string pnlView = null, bool personView = false, bool personEdit = false, bool personSelect = false, bool checkCookie = false)
    {
      ViewBag.PersonControlId = controlId;
      ViewBag.PersonSelect = true;
      ViewBag.PersonEdit = true;
      ViewBag.PnlView = Enum.GetName(typeof(PageViewType), PageViewType.Edit);
      SetViewBagSelectLists(m);
      return View(m);
    }

    private void SetViewBagSelectLists_ForSearch(string controlId)
    {
      ViewBag.PersonControlId = controlId;
      ViewBag.MembershipTypes = UtilsSelectLists.MembershipTypes(this.DbSession);
      ViewBag.MaxRecords = UtilsSelectLists.QueryMaxRecords();
    }

    private void SetViewOptions()
    {
      bool editPerson = UserController.EffectiveUser.GetClassAccess(typeof(Person)).Allows(Access.Update);
      ViewBag.PersonEdit = editPerson;
      if (editPerson)
      {
        ViewBag.PnlView = Utility.setPnlView("Edit", UserController, typeof(Person));
      }
      else
      {
        ViewBag.PnlView = Utility.setPnlView("ReadOnly", UserController, typeof(Person));
      }
    }

    private IEnumerable<Person> GetPeople(TypedModel<PersonQry> filter)
    {
      IEnumerable<Person> result = new List<Person>();
      if (UserController.EffectiveUser.GetClassAccess(typeof(Person)).Allows(Access.Read))
      {
        result = filter.Entity.List(this.DbSession).Cast<Person>();
      }
      return result;
    }

    private void SetViewBagSelectLists(TypedModel<Person> m)
    {
      ViewBag.PersonTitles = UtilsSelectLists.PersonTitles(this.DbSession, (m == null ? null : m.Entity.Title));
      ViewBag.MembershipTypes = UtilsSelectLists.MembershipTypes(this.DbSession);
      ViewBag.Qualifications = UtilsSelectLists.Qualifications(this.DbSession);
    }
  }
}