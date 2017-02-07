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
  public class CommitteeController : ControllerBase
  {
    public ActionResult Create([BoundModel(DontLoad = true, DontMerge = true)]TypedModel<Committee> m, bool hideLayout = false)
    {
      if (this.UserController.EffectiveUser.GetClassAccess(m.EntityType).Allows(Access.Create) == false)
      {
        return RedirectToAction("View", "Error", new
        {
          errorMsg = "You do not have permission to 'Create' Committee.",
          redirectUrl = Request.UrlReferrer
        });
      }

      //ViewBag.HideLayout = hideLayout;
      SetViewBagSelectLists(m);
      return View(m);
    }

    [HttpPost]
    [ActionName("Create")]
    public ActionResult CreatePost(TypedModel<Committee> m)
    {
      if (this.UserController.EffectiveUser.GetClassAccess(m.EntityType).Allows(Access.Create) == false)
      {
        return RedirectToAction("View", "Error", new
        {
          errorMsg = "You do not have permission to 'Create' Committee.",
          redirectUrl = Request.UrlReferrer
        });
      }

      if (ModelState.IsValid)
      {
        CommitteeServiceController service = (CommitteeServiceController)this.Injector.Activate(typeof(CommitteeServiceController));
        if (service.SaveCommittee(new ModelStateWrapper(this.ModelState), m.Entity))
        {
          //Sucessfully saved Committee
          return RedirectToAction("Edit", new { Oid = m.Entity.Oid });
        }
      }

      //If not sucessful saved Committee then return to View
      SetViewBagSelectLists(m);
      return View(m);
    }

    public ActionResult Edit(TypedModel<Committee> m)
    {
      if (this.UserController.EffectiveUser.GetClassAccess(m.EntityType).Allows(Access.Update) == false)
      {
        if (this.UserController.EffectiveUser.GetClassAccess(m.EntityType).Allows(Access.Read) == false)
        {
          return RedirectToAction("View", "Error", new
          {
            errorMsg = "You do not have permission to 'Edit' Committee.",
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

      CommitteeServiceController service = (CommitteeServiceController)this.Injector.Activate(typeof(CommitteeServiceController));
      ViewBag.EditCommittee = service.CanUserEditCommittee(m.Entity);
      ViewBag.EditAttachedDocs = UserController.EffectiveUser.GetClassAccess(typeof(AttachedDocument)).Allows(Access.Update);
      UtilsAttachedDocuments.SetViewDataAttachedDocuments(this.DbSession, ViewData, typeof(Committee), m.Entity.Oid);
      SetViewBagSelectLists(m);
      return View(m);
    }

    [HttpPost]
    [ActionName("Edit")]
    public ActionResult EditPost(TypedModel<Committee> m)
    {
      if (this.UserController.EffectiveUser.GetClassAccess(m.EntityType).Allows(Access.Update) == false)
      {
        return RedirectToAction("View", "Error", new
        {
          errorMsg = "You do not have permission to 'Edit' Committee.",
          redirectUrl = Request.UrlReferrer
        });
      }
      ViewBag.PnlView = Enum.GetName(typeof(PageViewType), PageViewType.Edit);

      if (ModelState.IsValid)
      {
        CommitteeServiceController service = (CommitteeServiceController)this.Injector.Activate(typeof(CommitteeServiceController));
        service.SaveCommittee(new ModelStateWrapper(this.ModelState), m.Entity);
      }

      //If not sucessful saved Committee then return to View
      SetViewBagSelectLists(m);
      return View(m);
    }

    public PartialViewResult Search(string controlId = null, bool selectMany = false)
    {
      TypedModel<CommitteeQry> model = new TypedModel<CommitteeQry>();
      model.Init();
      ViewBag.SelectMany = selectMany;
      SetViewOptions();
      SetViewBagSelectLists_ForSearch(controlId);
      return PartialView(model);
    }

    public ActionResult SearchResults_ReadData([DataSourceRequest]DataSourceRequest request, TypedModel<CommitteeQry> filter)
    {
      if (request.PageSize == 0)
      {
        request.PageSize = 10;
      }

      IEnumerable<Committee> teams = GetCommittees(filter);
      IMapper Mapper = AutoMapperConfig.MapperConfiguration.CreateMapper();
      IEnumerable<ModelCommittee> result = Mapper.Map<IEnumerable<ModelCommittee>>(teams);
      return Json(result.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
    }

    /// <summary>
    /// Called for CommitteeMember searches - options for the result list are also set in here.
    /// </summary>
    /// <param name="CommitteeSelect">If True then 'Select Committee' enabled and 'Edit' is disabled in List Results.</param>
    /// <returns></returns>
    public PartialViewResult SearchCommitteeMembers(string controlId = null, bool selectMany = false)
    {
      TypedModel<CommitteeMemberQry> model = new TypedModel<CommitteeMemberQry>();
      model.Init();
      ViewBag.SelectMany = selectMany;
      SetViewOptions();
      SetViewBagSelectLists_ForSearch(controlId);
      return PartialView(model);
    }

    public ActionResult SearchCommitteeMembers_ReadData([DataSourceRequest]DataSourceRequest request, TypedModel<CommitteeMemberQry> filter)
    {
      if (request.PageSize == 0)
      {
        request.PageSize = 10;
      }

      IEnumerable<CommitteeMember> teamMembers = GetCommitteeMembers(filter);
      IMapper Mapper = AutoMapperConfig.MapperConfiguration.CreateMapper();
      IEnumerable<ModelCommitteeMember> result = Mapper.Map<IEnumerable<ModelCommitteeMember>>(teamMembers);
      return Json(result.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
    }

    private void SetViewBagSelectLists_ForSearch(string controlId)
    {
      ViewBag.CommitteeControlId = controlId;
      ViewBag.CommitteeMemberTypes = UtilsSelectLists.CommitteeMemberTypes(this.DbSession);
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

    private IEnumerable<Committee> GetCommittees(TypedModel<CommitteeQry> filter)
    {
      IEnumerable<Committee> result = new List<Committee>();
      if (UserController.EffectiveUser.GetClassAccess(typeof(Committee)).Allows(Access.Read))
      {
        result = filter.Entity.List(this.DbSession).Cast<Committee>();
      }
      return result;
    }

    private IEnumerable<CommitteeMember> GetCommitteeMembers(TypedModel<CommitteeMemberQry> filter)
    {
      IEnumerable<CommitteeMember> result = new List<CommitteeMember>();
      if (UserController.EffectiveUser.GetClassAccess(typeof(CommitteeMember)).Allows(Access.Read))
      {
        result = filter.Entity.List(this.DbSession).Cast<CommitteeMember>();
      }
      return result;
    }

    private IEnumerable<CommitteeMember> GetCommitteeMembers(Guid committeeOid)
    {
      IEnumerable<CommitteeMember> result = this.DbSession.QueryOver<CommitteeMember>().List<CommitteeMember>().Where(x => x.Committee.Oid == committeeOid).OrderBy(x => x.Person.Surname);
      return result;
    }

    private void SetViewBagSelectLists(TypedModel<Committee> m)
    {
      ViewBag.Committees = UtilsSelectLists.CommitteesWhereUserIsAdminOrAdministrator(this.DbSession, this.UserController.EffectiveUser);
      ViewBag.CommitteeMemberTypes = UtilsSelectLists.CommitteeMemberTypes(this.DbSession, true);
      ViewBag.AdminUsers = UtilsSelectLists.Users(this.DbSession, true);

      //ViewBag.CommitteeTitles = UtilsSelectLists.CommitteeTitles(this.DbSession, (m == null ? null : m.Entity.Title));
      //ViewBag.MembershipTypes = UtilsSelectLists.MembershipTypes(this.DbSession);
    }

    public PartialViewResult CommitteeMinuteAjax(Guid? committeeOid = null, Guid? committeeMinuteOid = null, string controlId = null)
    {
      CommitteeServiceController service = (CommitteeServiceController)this.Injector.Activate(typeof(CommitteeServiceController));
      CommitteeMinute committeeMinute = new CommitteeMinute();
      if (committeeMinuteOid.HasValue && committeeMinuteOid != Guid.Empty)
      {
        committeeMinute = this.DbSession.Get<CommitteeMinute>(committeeMinuteOid);
      }
      else
      {
        committeeMinute.Committee = this.DbSession.Get<Committee>(committeeOid);
      }
      ViewBag.EditCommitteeMinute = service.CanUserEditCommittee(committeeMinute.Committee);
      ViewBag.CommitteeMinutesControlId = controlId;
      IMapper Mapper = AutoMapperConfig.MapperConfiguration.CreateMapper();
      ModelCommitteeMinute result = Mapper.Map<ModelCommitteeMinute>(committeeMinute);

      return PartialView(result);
    }

    public ActionResult CommitteeMinuteCreate(Guid committeeOid)
    {
      Committee committee = this.DbSession.Get<Committee>(committeeOid);
      ModelCommitteeMinute m = new ModelCommitteeMinute(committee);
      CommitteeServiceController service = (CommitteeServiceController)this.Injector.Activate(typeof(CommitteeServiceController));
      ViewBag.CreateCommitteeMinute = service.CanUserEditCommittee(committee); ;
      ViewBag.Committees = UtilsSelectLists.CommitteesWhereUserIsAdminOrAdministrator(this.DbSession, this.UserController.EffectiveUser, true);
      return View(m);
    }

    [HttpPost]
    [ActionName("CommitteeMinuteCreate")]
    public ActionResult CommitteeMinuteCreatePost(ModelCommitteeMinute m)
    {
      ViewBag.Committees = UtilsSelectLists.CommitteesWhereUserIsAdminOrAdministrator(this.DbSession, this.UserController.EffectiveUser, true);
      CommitteeServiceController service = (CommitteeServiceController)this.Injector.Activate(typeof(CommitteeServiceController));
      if (ModelState.IsValid)
      {
        if (service.SaveCommitteeMinute(new ModelStateWrapper(this.ModelState), ref m))
        {
          return RedirectToAction("CommitteeMinuteEdit", new { committeeMinuteOid = m.Oid });
        }
      }
      //If not sucessful saved Committee then return to View
      ViewBag.CreateCommitteeMinute = true;
      return View(m);
    }

    public ActionResult CommitteeMinuteEdit(Guid committeeMinuteOid)
    {
      CommitteeServiceController service = (CommitteeServiceController)this.Injector.Activate(typeof(CommitteeServiceController));
      CommitteeMinute committee = this.DbSession.Get<CommitteeMinute>(committeeMinuteOid);
      IMapper Mapper = AutoMapperConfig.MapperConfiguration.CreateMapper();
      ModelCommitteeMinute m = Mapper.Map<ModelCommitteeMinute>(committee);

      ViewBag.EditCommitteeMinute = service.CanUserEditCommittee(committee.Committee);
      ViewBag.Committees = UtilsSelectLists.CommitteesWhereUserIsAdminOrAdministrator(this.DbSession, this.UserController.EffectiveUser, true);
      return View(m);
    }

    [HttpPost]
    [ActionName("CommitteeMinuteEdit")]
    public ActionResult CommitteeMinuteEditPost(ModelCommitteeMinute m)
    {
      ViewBag.Committees = UtilsSelectLists.CommitteesWhereUserIsAdminOrAdministrator(this.DbSession, this.UserController.EffectiveUser, true);
      CommitteeServiceController service = (CommitteeServiceController)this.Injector.Activate(typeof(CommitteeServiceController));
      if (ModelState.IsValid)
      {
        service.SaveCommitteeMinute(new ModelStateWrapper(this.ModelState), ref m);
      }

      ViewBag.EditCommitteeMinute = service.CanUserEditCommittee(m.CommitteeOid);
      ViewBag.Committees = UtilsSelectLists.CommitteesWhereUserIsAdminOrAdministrator(this.DbSession, this.UserController.EffectiveUser, true);
      return View(m);
    }

    [HttpPost]
    [ActionName("CommitteeMinuteAjax")]
    public JsonResult CommitteeMinuteAjaxPost(ModelCommitteeMinute m)
    {
      CommitteeServiceController service = (CommitteeServiceController)this.Injector.Activate(typeof(CommitteeServiceController));
      if (service.SaveCommitteeMinute(new ModelStateWrapper(this.ModelState), ref m))
      {
        ModelJsonResult j = new ModelJsonResult();
        j.success = true;
        return Json(j, JsonRequestBehavior.AllowGet);
      }
      throw new ModelStateException(this.ModelState);
    }

    #region Committee Members

    public JsonResult CommitteeMember_GridRead([DataSourceRequest]DataSourceRequest request, Guid committeeOid)
    {
      if (request.PageSize == 0)
      {
        request.PageSize = 30;
      }

      IEnumerable<CommitteeMember> m = GetCommitteeMembers(committeeOid);
      IMapper Mapper = AutoMapperConfig.MapperConfiguration.CreateMapper();
      IEnumerable<ModelCommitteeMember> result = Mapper.Map<IEnumerable<ModelCommitteeMember>>(m);
      return Json(result.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
    }

    [AcceptVerbs(HttpVerbs.Post)]
    public ActionResult CommitteeMember_Delete([DataSourceRequest] DataSourceRequest request, ModelCommitteeMember product)
    {
      if (this.UserController.EffectiveUser.GetClassAccess(typeof(CommitteeMember)).Allows(Access.Delete) == false)
      {
        return RedirectToAction("View", "Error", new
        {
          errorMsg = "You do not have permission to 'Delete' Committee Members's.",
          redirectUrl = Request.UrlReferrer
        });
      }

      CommitteeMember domainEntity = this.DbSession.Get<CommitteeMember>(product.Oid);
      CommitteeServiceController service = (CommitteeServiceController)this.Injector.Activate(typeof(CommitteeServiceController));
      if (service.DeleteCommitteeMember(new ModelStateWrapper(this.ModelState), ref domainEntity))
      {
        IMapper Mapper = AutoMapperConfig.MapperConfiguration.CreateMapper();
        Mapper.Map<CommitteeMember, ModelCommitteeMember>(domainEntity, product);
        return Json(new[] { product }.ToDataSourceResult(request, ModelState));
      }
      return Json(new[] { product }.ToDataSourceResult(request, ModelState));
      //throw new ModelStateException(this.ModelState);
    }

    /// <summary>
    /// Only the CommitteeMemberType can change here
    /// </summary>
    /// <param name="request"></param>
    /// <param name="product"></param>
    /// <returns></returns>
    [AcceptVerbs(HttpVerbs.Post)]
    public ActionResult CommitteeMember_Update([DataSourceRequest] DataSourceRequest request, ModelCommitteeMember product)
    {
      if (this.UserController.EffectiveUser.GetClassAccess(typeof(CommitteeMember)).Allows(Access.Update) == false)
      {
        return RedirectToAction("View", "Error", new
        {
          errorMsg = "You do not have permission to 'Update' Committee Members's.",
          redirectUrl = Request.UrlReferrer
        });
      }

      CommitteeMember domainEntity = this.DbSession.Get<CommitteeMember>(product.Oid);
      domainEntity.CommitteeMemberType = this.DbSession.Get<CommitteeMemberType>(product.CommitteeMemberTypeOid);
      IMapper Mapper = AutoMapperConfig.MapperConfiguration.CreateMapper();
      Mapper.Map<ModelCommitteeMember, CommitteeMember>(product, domainEntity);
      CommitteeServiceController service = (CommitteeServiceController)this.Injector.Activate(typeof(CommitteeServiceController));
      if (service.SaveCommitteeMember(new ModelStateWrapper(this.ModelState), ref domainEntity))
      {
        Mapper.Map<CommitteeMember, ModelCommitteeMember>(domainEntity, product);
        return Json(new[] { product }.ToDataSourceResult(request, ModelState));
      }
      return Json(new[] { product }.ToDataSourceResult(request, ModelState));
      //throw new ModelStateException(this.ModelState);
    }

    #endregion

    #region Committee Admins Users

    public JsonResult CommitteeAdmin_GridRead([DataSourceRequest]DataSourceRequest request, Guid committeeOid)
    {
      if (request.PageSize == 0)
      {
        request.PageSize = 30;
      }

      IEnumerable<CommitteeAdmin> m = GetCommitteeAdmins(committeeOid);
      IMapper Mapper = AutoMapperConfig.MapperConfiguration.CreateMapper();
      IEnumerable<ModelCommitteeAdmin> result = Mapper.Map<IEnumerable<ModelCommitteeAdmin>>(m);
      return Json(result.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
    }

    [AcceptVerbs(HttpVerbs.Post)]
    public ActionResult CommitteeAdmin_Delete([DataSourceRequest] DataSourceRequest request, ModelCommitteeAdmin product)
    {
      if (this.UserController.EffectiveUser.GetClassAccess(typeof(Committee)).Allows(Access.Delete) == false)
      {
        return RedirectToAction("View", "Error", new
        {
          errorMsg = "You do not have permission to 'Delete' Committee Admin's.",
          redirectUrl = Request.UrlReferrer
        });
      }

      CommitteeAdmin domainEntity = this.DbSession.Get<CommitteeAdmin>(product.Oid);
      CommitteeServiceController service = (CommitteeServiceController)this.Injector.Activate(typeof(CommitteeServiceController));
      if (service.DeleteCommitteeAdminUser(new ModelStateWrapper(this.ModelState), ref domainEntity))
      {
        IMapper Mapper = AutoMapperConfig.MapperConfiguration.CreateMapper();
        Mapper.Map<CommitteeAdmin, ModelCommitteeAdmin>(domainEntity, product);
        return Json(new[] { product }.ToDataSourceResult(request, ModelState));
      }
      return Json(new[] { product }.ToDataSourceResult(request, ModelState));
      //throw new ModelStateException(this.ModelState);
    }

    /// <summary>
    /// Only the CommitteeAdminType can change here
    /// </summary>
    /// <param name="request"></param>
    /// <param name="product"></param>
    /// <returns></returns>
    [AcceptVerbs(HttpVerbs.Post)]
    public ActionResult CommitteeAdmin_Create([DataSourceRequest] DataSourceRequest request, ModelCommitteeAdmin product)
    {
      if (this.UserController.EffectiveUser.GetClassAccess(typeof(CommitteeAdmin)).Allows(Access.Update) == false)
      {
        return RedirectToAction("View", "Error", new
        {
          errorMsg = "You do not have permission to 'Update' Committee Members's.",
          redirectUrl = Request.UrlReferrer
        });
      }

      CommitteeAdmin domainEntity = this.DbSession.Get<CommitteeAdmin>(product.Oid);
      domainEntity.Committee = this.DbSession.Get<Committee>(product.CommitteeOid);
      domainEntity.User = this.DbSession.Get<User>(product.UserOid);
      IMapper Mapper = AutoMapperConfig.MapperConfiguration.CreateMapper();
      Mapper.Map<ModelCommitteeAdmin, CommitteeAdmin>(product, domainEntity);
      CommitteeServiceController service = (CommitteeServiceController)this.Injector.Activate(typeof(CommitteeServiceController));
      if (service.SaveCommitteeAdminUser(new ModelStateWrapper(this.ModelState), ref domainEntity))
      {
        Mapper.Map<CommitteeAdmin, ModelCommitteeAdmin>(domainEntity, product);
        return Json(new[] { product }.ToDataSourceResult(request, ModelState));
      }
      return Json(new[] { product }.ToDataSourceResult(request, ModelState));
      //throw new ModelStateException(this.ModelState);
    }

    /// <summary>
    /// Only the CommitteeAdminType can change here
    /// </summary>
    /// <param name="request"></param>
    /// <param name="product"></param>
    /// <returns></returns>
    [AcceptVerbs(HttpVerbs.Post)]
    public ActionResult CommitteeAdmin_Update([DataSourceRequest] DataSourceRequest request, ModelCommitteeAdmin product)
    {
      if (this.UserController.EffectiveUser.GetClassAccess(typeof(CommitteeAdmin)).Allows(Access.Update) == false)
      {
        return RedirectToAction("View", "Error", new
        {
          errorMsg = "You do not have permission to 'Update' Committee Members's.",
          redirectUrl = Request.UrlReferrer
        });
      }

      CommitteeAdmin domainEntity = this.DbSession.Get<CommitteeAdmin>(product.Oid);
      domainEntity.Committee = this.DbSession.Get<Committee>(product.CommitteeOid);
      domainEntity.User = this.DbSession.Get<User>(product.UserOid);
      IMapper Mapper = AutoMapperConfig.MapperConfiguration.CreateMapper();
      Mapper.Map<ModelCommitteeAdmin, CommitteeAdmin>(product, domainEntity);
      CommitteeServiceController service = (CommitteeServiceController)this.Injector.Activate(typeof(CommitteeServiceController));
      if (service.SaveCommitteeAdminUser(new ModelStateWrapper(this.ModelState), ref domainEntity))
      {
        Mapper.Map<CommitteeAdmin, ModelCommitteeAdmin>(domainEntity, product);
        return Json(new[] { product }.ToDataSourceResult(request, ModelState));
      }
      return Json(new[] { product }.ToDataSourceResult(request, ModelState));
      //throw new ModelStateException(this.ModelState);
    }

    private IEnumerable<CommitteeAdmin> GetCommitteeAdmins(Guid committeeOid)
    {
      IEnumerable<CommitteeAdmin> result = this.DbSession.QueryOver<CommitteeAdmin>().List<CommitteeAdmin>().Where(x => x.Committee.Oid == committeeOid).OrderBy(x => x.User.FullName);
      return result;
    }

    #endregion

    #region Committee Minutes

    private IEnumerable<CommitteeMinute> GetCommitteeMinutes(Guid committeeOid)
    {
      IEnumerable<CommitteeMinute> result = this.DbSession.QueryOver<CommitteeMinute>().List<CommitteeMinute>().Where(x => x.Committee.Oid == committeeOid).OrderBy(x => x.Date);
      return result;
    }

    public JsonResult CommitteeMinute_GridRead([DataSourceRequest]DataSourceRequest request, Guid committeeOid)
    {
      if (request.PageSize == 0)
      {
        request.PageSize = 30;
      }

      IEnumerable<CommitteeMinute> m = GetCommitteeMinutes(committeeOid);
      IMapper Mapper = AutoMapperConfig.MapperConfiguration.CreateMapper();
      IEnumerable<ModelCommitteeMinute> result = Mapper.Map<IEnumerable<ModelCommitteeMinute>>(m);
      return Json(result.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
    }

    [AcceptVerbs(HttpVerbs.Post)]
    public ActionResult CommitteeMinute_Delete([DataSourceRequest] DataSourceRequest request, ModelCommitteeMinute product)
    {
      if (this.UserController.EffectiveUser.GetClassAccess(typeof(CommitteeMinute)).Allows(Access.Delete) == false)
      {
        return RedirectToAction("View", "Error", new
        {
          errorMsg = "You do not have permission to 'Delete' Committee Members's.",
          redirectUrl = Request.UrlReferrer
        });
      }

      CommitteeMinute domainEntity = this.DbSession.Get<CommitteeMinute>(product.Oid);
      CommitteeServiceController service = (CommitteeServiceController)this.Injector.Activate(typeof(CommitteeServiceController));
      if (service.DeleteCommitteeMinute(new ModelStateWrapper(this.ModelState), domainEntity))
      {
        IMapper Mapper = AutoMapperConfig.MapperConfiguration.CreateMapper();
        Mapper.Map<CommitteeMinute, ModelCommitteeMinute>(domainEntity, product);
        return Json(new[] { product }.ToDataSourceResult(request, ModelState));
      }
      return Json(new[] { product }.ToDataSourceResult(request, ModelState));
      //throw new ModelStateException(this.ModelState);
    }

    /// <summary>
    /// Only the CommitteeMinuteType can change here
    /// </summary>
    /// <param name="request"></param>
    /// <param name="product"></param>
    /// <returns></returns>
    [AcceptVerbs(HttpVerbs.Post)]
    public ActionResult CommitteeMinute_Update([DataSourceRequest] DataSourceRequest request, ModelCommitteeMinute product)
    {
      if (this.UserController.EffectiveUser.GetClassAccess(typeof(CommitteeMinute)).Allows(Access.Update) == false)
      {
        return RedirectToAction("View", "Error", new
        {
          errorMsg = "You do not have permission to 'Update' Committee Members's.",
          redirectUrl = Request.UrlReferrer
        });
      }

      CommitteeMinute domainEntity = this.DbSession.Get<CommitteeMinute>(product.Oid);
      domainEntity.Committee = this.DbSession.Get<Committee>(product.CommitteeOid);
      IMapper Mapper = AutoMapperConfig.MapperConfiguration.CreateMapper();
      Mapper.Map<ModelCommitteeMinute, CommitteeMinute>(product, domainEntity);
      CommitteeServiceController service = (CommitteeServiceController)this.Injector.Activate(typeof(CommitteeServiceController));
      if (service.SaveCommitteeMinute(new ModelStateWrapper(this.ModelState), ref domainEntity))
      {
        Mapper.Map<CommitteeMinute, ModelCommitteeMinute>(domainEntity, product);
        return Json(new[] { product }.ToDataSourceResult(request, ModelState));
      }
      return Json(new[] { product }.ToDataSourceResult(request, ModelState));
      //throw new ModelStateException(this.ModelState);
    }

    #endregion

  }
}