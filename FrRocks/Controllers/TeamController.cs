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
  public class TeamController : ControllerBase
  {
    public ActionResult Create([BoundModel(DontLoad = true, DontMerge = true)]TypedModel<Team> m, bool hideLayout = false)
    {
      if (this.UserController.EffectiveUser.GetClassAccess(m.EntityType).Allows(Access.Create) == false)
      {
        return RedirectToAction("View", "Error", new
        {
          errorMsg = "You do not have permission to 'Create' Team.",
          redirectUrl = Request.UrlReferrer
        });
      }

      //ViewBag.HideLayout = hideLayout;
      SetViewBagSelectLists(m);
      return View(m);
    }

    [HttpPost]
    [ActionName("Create")]
    public ActionResult CreatePost(TypedModel<Team> m)
    {
      if (this.UserController.EffectiveUser.GetClassAccess(m.EntityType).Allows(Access.Create) == false)
      {
        return RedirectToAction("View", "Error", new
        {
          errorMsg = "You do not have permission to 'Create' Team.",
          redirectUrl = Request.UrlReferrer
        });
      }

      if (ModelState.IsValid)
      {
        TeamServiceController service = (TeamServiceController)this.Injector.Activate(typeof(TeamServiceController));
        if (service.SaveTeam(new ModelStateWrapper(this.ModelState), m.Entity))
        {
          //Sucessfully saved Team
          return RedirectToAction("Edit", new { Oid = m.Entity.Oid });
        }
      }

      //If not sucessful saved Team then return to View
      SetViewBagSelectLists(m);
      return View(m);
    }

    public ActionResult Edit(TypedModel<Team> m)
    {
      if (this.UserController.EffectiveUser.GetClassAccess(m.EntityType).Allows(Access.Update) == false)
      {
        if (this.UserController.EffectiveUser.GetClassAccess(m.EntityType).Allows(Access.Read) == false)
        {
          return RedirectToAction("View", "Error", new
          {
            errorMsg = "You do not have permission to 'Edit' Team.",
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

      TeamServiceController service = (TeamServiceController)this.Injector.Activate(typeof(TeamServiceController));
      ViewBag.EditTeam = service.CanUserEditTeam(m.Entity);
      ViewBag.EditAttachedDocs = UserController.EffectiveUser.GetClassAccess(typeof(AttachedDocument)).Allows(Access.Update);
      UtilsAttachedDocuments.SetViewDataAttachedDocuments(this.DbSession, ViewData, typeof(Team), m.Entity.Oid);
      SetViewBagSelectLists(m);
      return View(m);
    }

    [HttpPost]
    [ActionName("Edit")]
    public ActionResult EditPost(TypedModel<Team> m)
    {
      if (this.UserController.EffectiveUser.GetClassAccess(m.EntityType).Allows(Access.Update) == false)
      {
        return RedirectToAction("View", "Error", new
        {
          errorMsg = "You do not have permission to 'Edit' Team.",
          redirectUrl = Request.UrlReferrer
        });
      }
      ViewBag.PnlView = Enum.GetName(typeof(PageViewType), PageViewType.Edit);

      TeamServiceController service = (TeamServiceController)this.Injector.Activate(typeof(TeamServiceController));
      if (ModelState.IsValid)
      {
        service.SaveTeam(new ModelStateWrapper(this.ModelState), m.Entity);
      }

      //If not sucessful saved Team then return to View
      ViewBag.EditTeam = service.CanUserEditTeam(m.Entity);
      ViewBag.EditAttachedDocs = UserController.EffectiveUser.GetClassAccess(typeof(AttachedDocument)).Allows(Access.Update);
      UtilsAttachedDocuments.SetViewDataAttachedDocuments(this.DbSession, ViewData, typeof(Team), m.Entity.Oid);
      SetViewBagSelectLists(m);
      return View(m);
    }

    public PartialViewResult Search(string controlId = null, bool selectMany = false)
    {
      TypedModel<TeamQry> model = new TypedModel<TeamQry>();
      model.Init();
      ViewBag.SelectMany = selectMany;
      SetViewOptions();
      SetViewBagSelectLists_ForSearch(controlId);
      return PartialView(model);
    }

    #region Team Members

    public JsonResult TeamMember_GridRead([DataSourceRequest]DataSourceRequest request, Guid teamOid)
    {
      if (request.PageSize == 0)
      {
        request.PageSize = 30;
      }

      IEnumerable<TeamMember> m = GetTeamMembers(teamOid);
      IMapper Mapper = AutoMapperConfig.MapperConfiguration.CreateMapper();
      IEnumerable<ModelTeamMember> result = Mapper.Map<IEnumerable<ModelTeamMember>>(m);
      return Json(result.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
    }

    [AcceptVerbs(HttpVerbs.Post)]
    public ActionResult TeamMember_Delete([DataSourceRequest] DataSourceRequest request, ModelTeamMember product)
    {
      if (this.UserController.EffectiveUser.GetClassAccess(typeof(TeamMember)).Allows(Access.Delete) == false)
      {
        return RedirectToAction("View", "Error", new
        {
          errorMsg = "You do not have permission to 'Delete' Team Members's.",
          redirectUrl = Request.UrlReferrer
        });
      }

      TeamMember domainEntity = this.DbSession.Get<TeamMember>(product.Oid);
      TeamServiceController service = (TeamServiceController)this.Injector.Activate(typeof(TeamServiceController));
      if (service.DeleteTeamMember(new ModelStateWrapper(this.ModelState), ref domainEntity))
      {
        IMapper Mapper = AutoMapperConfig.MapperConfiguration.CreateMapper();
        Mapper.Map<TeamMember, ModelTeamMember>(domainEntity, product);
        return Json(new[] { product }.ToDataSourceResult(request, ModelState));
      }
      return Json(new[] { product }.ToDataSourceResult(request, ModelState));
      //throw new ModelStateException(this.ModelState);
    }

    /// <summary>
    /// Only the TeamMemberType can change here
    /// </summary>
    /// <param name="request"></param>
    /// <param name="product"></param>
    /// <returns></returns>
    [AcceptVerbs(HttpVerbs.Post)]
    public ActionResult TeamMember_Update([DataSourceRequest] DataSourceRequest request, ModelTeamMember product)
    {
      TeamServiceController service = (TeamServiceController)this.Injector.Activate(typeof(TeamServiceController));
      if (this.UserController.EffectiveUser.GetClassAccess(typeof(TeamMember)).Allows(Access.Update) == false)
      {
        return RedirectToAction("View", "Error", new
        {
          errorMsg = "You do not have permission to 'Update' Team Members's.",
          redirectUrl = Request.UrlReferrer
        });
      }

      TeamMember domainEntity = this.DbSession.Get<TeamMember>(product.Oid);
      domainEntity.TeamMemberType = this.DbSession.Get<TeamMemberType>(product.TeamMemberTypeOid);
      IMapper Mapper = AutoMapperConfig.MapperConfiguration.CreateMapper();
      Mapper.Map<ModelTeamMember, TeamMember>(product, domainEntity);
      if (service.SaveTeamMember(new ModelStateWrapper(this.ModelState), ref domainEntity))
      {
        Mapper.Map<TeamMember, ModelTeamMember>(domainEntity, product);
        return Json(new[] { product }.ToDataSourceResult(request, ModelState));
      }
      return Json(new[] { product }.ToDataSourceResult(request, ModelState));
      //throw new ModelStateException(this.ModelState);
    }

    #endregion

    #region Team Admins Users

    public JsonResult TeamAdmin_GridRead([DataSourceRequest]DataSourceRequest request, Guid teamOid)
    {
      if (request.PageSize == 0)
      {
        request.PageSize = 30;
      }

      IEnumerable<TeamAdmin> m = GetTeamAdmins(teamOid);
      IMapper Mapper = AutoMapperConfig.MapperConfiguration.CreateMapper();
      IEnumerable<ModelTeamAdmin> result = Mapper.Map<IEnumerable<ModelTeamAdmin>>(m);
      return Json(result.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
    }

    [AcceptVerbs(HttpVerbs.Post)]
    public ActionResult TeamAdmin_Delete([DataSourceRequest] DataSourceRequest request, ModelTeamAdmin product)
    {
      if (this.UserController.EffectiveUser.GetClassAccess(typeof(Team)).Allows(Access.Delete) == false)
      {
        return RedirectToAction("View", "Error", new
        {
          errorMsg = "You do not have permission to 'Delete' Team Admin's.",
          redirectUrl = Request.UrlReferrer
        });
      }

      TeamAdmin domainEntity = this.DbSession.Get<TeamAdmin>(product.Oid);
      TeamServiceController service = (TeamServiceController)this.Injector.Activate(typeof(TeamServiceController));
      if (service.DeleteTeamAdminUser(new ModelStateWrapper(this.ModelState), ref domainEntity))
      {
        IMapper Mapper = AutoMapperConfig.MapperConfiguration.CreateMapper();
        Mapper.Map<TeamAdmin, ModelTeamAdmin>(domainEntity, product);
        return Json(new[] { product }.ToDataSourceResult(request, ModelState));
      }
      return Json(new[] { product }.ToDataSourceResult(request, ModelState));
      //throw new ModelStateException(this.ModelState);
    }

    /// <summary>
    /// Only the TeamAdminType can change here
    /// </summary>
    /// <param name="request"></param>
    /// <param name="product"></param>
    /// <returns></returns>
    [AcceptVerbs(HttpVerbs.Post)]
    public ActionResult TeamAdmin_Create([DataSourceRequest] DataSourceRequest request, ModelTeamAdmin product)
    {
      if (this.UserController.EffectiveUser.GetClassAccess(typeof(TeamAdmin)).Allows(Access.Update) == false)
      {
        return RedirectToAction("View", "Error", new
        {
          errorMsg = "You do not have permission to 'Update' Team Members's.",
          redirectUrl = Request.UrlReferrer
        });
      }

      TeamAdmin domainEntity = this.DbSession.Get<TeamAdmin>(product.Oid);
      domainEntity.Team = this.DbSession.Get<Team>(product.TeamOid);
      domainEntity.User = this.DbSession.Get<User>(product.UserOid);
      IMapper Mapper = AutoMapperConfig.MapperConfiguration.CreateMapper();
      Mapper.Map<ModelTeamAdmin, TeamAdmin>(product, domainEntity);
      TeamServiceController service = (TeamServiceController)this.Injector.Activate(typeof(TeamServiceController));
      if (service.SaveTeamAdminUser(new ModelStateWrapper(this.ModelState), ref domainEntity))
      {
        Mapper.Map<TeamAdmin, ModelTeamAdmin>(domainEntity, product);
        return Json(new[] { product }.ToDataSourceResult(request, ModelState));
      }
      return Json(new[] { product }.ToDataSourceResult(request, ModelState));
      //throw new ModelStateException(this.ModelState);
    }

    /// <summary>
    /// Only the TeamAdminType can change here
    /// </summary>
    /// <param name="request"></param>
    /// <param name="product"></param>
    /// <returns></returns>
    [AcceptVerbs(HttpVerbs.Post)]
    public ActionResult TeamAdmin_Update([DataSourceRequest] DataSourceRequest request, ModelTeamAdmin product)
    {
      if (this.UserController.EffectiveUser.GetClassAccess(typeof(TeamAdmin)).Allows(Access.Update) == false)
      {
        return RedirectToAction("View", "Error", new
        {
          errorMsg = "You do not have permission to 'Update' Team Members's.",
          redirectUrl = Request.UrlReferrer
        });
      }

      TeamAdmin domainEntity = this.DbSession.Get<TeamAdmin>(product.Oid);
      domainEntity.Team = this.DbSession.Get<Team>(product.TeamOid);
      domainEntity.User = this.DbSession.Get<User>(product.UserOid);
      IMapper Mapper = AutoMapperConfig.MapperConfiguration.CreateMapper();
      Mapper.Map<ModelTeamAdmin, TeamAdmin>(product, domainEntity);
      TeamServiceController service = (TeamServiceController)this.Injector.Activate(typeof(TeamServiceController));
      if (service.SaveTeamAdminUser(new ModelStateWrapper(this.ModelState), ref domainEntity))
      {
        Mapper.Map<TeamAdmin, ModelTeamAdmin>(domainEntity, product);
        return Json(new[] { product }.ToDataSourceResult(request, ModelState));
      }
      return Json(new[] { product }.ToDataSourceResult(request, ModelState));
      //throw new ModelStateException(this.ModelState);
    }

    #endregion

    public ActionResult SearchResults_ReadData([DataSourceRequest]DataSourceRequest request, TypedModel<TeamQry> filter)
    {
      if (request.PageSize == 0)
      {
        request.PageSize = 30;
      }

      IEnumerable<Team> teams = GetTeams(filter);
      IMapper Mapper = AutoMapperConfig.MapperConfiguration.CreateMapper();
      IEnumerable<ModelTeam> result = Mapper.Map<IEnumerable<ModelTeam>>(teams);
      return Json(result.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
    }

    /// <summary>
    /// Called for TeamMember searches - options for the result list are also set in here.
    /// </summary>
    /// <param name="TeamSelect">If True then 'Select Team' enabled and 'Edit' is disabled in List Results.</param>
    /// <returns></returns>
    public PartialViewResult SearchTeamMembers(string controlId = null, bool selectMany = false)
    {
      TypedModel<TeamMemberQry> model = new TypedModel<TeamMemberQry>();
      model.Init();
      ViewBag.SelectMany = selectMany;
      SetViewOptions();
      SetViewBagSelectLists_ForSearch(controlId);
      return PartialView(model);
    }

    public ActionResult SearchTeamMembers_ReadData([DataSourceRequest]DataSourceRequest request, TypedModel<TeamMemberQry> filter)
    {
      if (request.PageSize == 0)
      {
        request.PageSize = 30;
      }

      IEnumerable<TeamMember> teamMembers = GetTeamMembers(filter);
      IMapper Mapper = AutoMapperConfig.MapperConfiguration.CreateMapper();
      IEnumerable<ModelTeamMember> result = Mapper.Map<IEnumerable<ModelTeamMember>>(teamMembers);
      return Json(result.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
    }

    [HttpPost]
    public JsonResult CopyTeamMembers(Guid copyToTeamOid, Guid copyFromTeamOid)
    {
      TeamServiceController service = (TeamServiceController)this.Injector.Activate(typeof(TeamServiceController));
      if (service.CopyTeamMembers(new ModelStateWrapper(this.ModelState), copyToTeamOid, copyFromTeamOid))
      {
        ModelJsonResult j = new ModelJsonResult();
        j.success = true;
        return Json(j, JsonRequestBehavior.AllowGet);
      }
      throw new ModelStateException(this.ModelState);
    }

    private void SetViewBagSelectLists_ForSearch(string controlId)
    {
      ViewBag.TeamControlId = controlId;
      ViewBag.TeamMemberTypes = UtilsSelectLists.TeamMemberTypes(this.DbSession);
      ViewBag.Teams = UtilsSelectLists.Teams(this.DbSession);
      ViewBag.MaxRecords = UtilsSelectLists.QueryMaxRecords();
    }

    private void SetViewOptions()
    {
      bool editTeam = UserController.EffectiveUser.GetClassAccess(typeof(Team)).Allows(Access.Update);
      ViewBag.TeamEdit = editTeam;
      if (editTeam)
      {
        ViewBag.PnlView = Utility.setPnlView("Edit", UserController, typeof(Team));
      }
      else
      {
        ViewBag.PnlView = Utility.setPnlView("ReadOnly", UserController, typeof(Team));
      }

      bool editPerson = UserController.EffectiveUser.GetClassAccess(typeof(Person)).Allows(Access.Update);
      ViewBag.PersonEdit = editPerson;
    }

    private IEnumerable<Team> GetTeams(TypedModel<TeamQry> filter)
    {
      IEnumerable<Team> result = new List<Team>();
      if (UserController.EffectiveUser.GetClassAccess(typeof(Team)).Allows(Access.Read))
      {
        result = filter.Entity.List(this.DbSession).Cast<Team>();
      }
      return result;
    }

    private IEnumerable<TeamMember> GetTeamMembers(TypedModel<TeamMemberQry> filter)
    {
      IEnumerable<TeamMember> result = new List<TeamMember>();
      if (UserController.EffectiveUser.GetClassAccess(typeof(TeamMember)).Allows(Access.Read))
      {
        result = filter.Entity.List(this.DbSession).Cast<TeamMember>();
      }
      return result;
    }

    private IEnumerable<TeamMember> GetTeamMembers(Guid teamOid)
    {
      IEnumerable<TeamMember> result = this.DbSession.QueryOver<TeamMember>().List<TeamMember>().Where(x => x.Team.Oid == teamOid).OrderBy(x => x.Person.Surname);
      return result;
    }

    private IEnumerable<TeamAdmin> GetTeamAdmins(Guid teamOid)
    {
      IEnumerable<TeamAdmin> result = this.DbSession.QueryOver<TeamAdmin>().List<TeamAdmin>().Where(x => x.Team.Oid == teamOid).OrderBy(x => x.User.FullName);
      return result;
    }

    private void SetViewBagSelectLists(TypedModel<Team> m)
    {
      ViewBag.Teams = UtilsSelectLists.Teams(this.DbSession);
      ViewBag.TeamMemberTypes = UtilsSelectLists.TeamMemberTypes(this.DbSession, true);
      ViewBag.AdminUsers = UtilsSelectLists.Users(this.DbSession, true);

      //ViewBag.TeamTitles = UtilsSelectLists.TeamTitles(this.DbSession, (m == null ? null : m.Entity.Title));
      //ViewBag.MembershipTypes = UtilsSelectLists.MembershipTypes(this.DbSession);
    }
  }
}