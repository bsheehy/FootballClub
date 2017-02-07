using Club.Domain.Artifacts;
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
  public class ClubMinutesController : ControllerBase
  {
    /// <summary>
    /// You can only create Club Minuites from within a Committee 
    /// </summary>
    /// <param name="m"></param>
    /// <param name="hideLayout"></param>
    /// <returns></returns>
    public ActionResult Create([BoundModel(DontLoad = true, DontMerge = true)]TypedModel<CommitteeMinute> m, Guid committeeOid)
    {
      if (this.UserController.EffectiveUser.GetClassAccess(m.EntityType).Allows(Access.Create) == false)
      {
        return RedirectToAction("View", "Error", new
        {
          errorMsg = "You do not have permission to 'Create' ClubMinutes.",
          redirectUrl = Request.UrlReferrer
        });
      }
      Committee c = this.DbSession.Get<Committee>(committeeOid);
      ClubMinutesServiceController service = (ClubMinutesServiceController)this.Injector.Activate(typeof(ClubMinutesServiceController));
      //Only members of the CommitteAdmins or an administrator can CREATE or UPDATE minutes for the committee
      if (service.CanUserEditClubMinutes(c) == false)
      {
        return RedirectToAction("View", "Error", new
        {
          errorMsg = "You do not have permission to 'Create' Committee Minutes for this Committee.",
          redirectUrl = Request.UrlReferrer
        });
      }
      m.Entity.Committee = c;
      //ViewBag.HideLayout = hideLayout;
      SetViewBagSelectLists(m);
      return View(m);
    }

    [HttpPost]
    [ActionName("Create")]
    public ActionResult CreatePost(TypedModel<CommitteeMinute> m)
    {
      if (this.UserController.EffectiveUser.GetClassAccess(m.EntityType).Allows(Access.Create) == false)
      {
        return RedirectToAction("View", "Error", new
        {
          errorMsg = "You do not have permission to 'Create' ClubMinutes.",
          redirectUrl = Request.UrlReferrer
        });
      }

      ClubMinutesServiceController service = (ClubMinutesServiceController)this.Injector.Activate(typeof(ClubMinutesServiceController));
      //Only members of the CommitteAdmins or an administrator can CREATE or UPDATE minutes for the committee
      if (service.CanUserEditClubMinutes(m.Entity.Committee) == false)
      {
        ModelState.AddModelError("Committee", "You do not have permission to 'Create' Committee Minutes for this Committee.");
      }


      if (ModelState.IsValid)
      {
        if (service.SaveClubMinutes(new ModelStateWrapper(this.ModelState), m.Entity))
        {
          //Sucessfully saved ClubMinutes
          return RedirectToAction("Edit", new { Oid = m.Entity.Oid });
        }
      }

      //If not sucessful saved ClubMinutes then return to View
      SetViewBagSelectLists(m);
      return View(m);
    }

    public ActionResult Edit(TypedModel<CommitteeMinute> m)
    {
      if (this.UserController.EffectiveUser.GetClassAccess(m.EntityType).Allows(Access.Update) == false)
      {
        if (this.UserController.EffectiveUser.GetClassAccess(m.EntityType).Allows(Access.Read) == false)
        {
          return RedirectToAction("View", "Error", new
          {
            errorMsg = "You do not have permission to 'Edit' ClubMinutes.",
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

      ClubMinutesServiceController service = (ClubMinutesServiceController)this.Injector.Activate(typeof(ClubMinutesServiceController));
      //Only members of the CommitteAdmins or an administrator can CREATE or UPDATE minutes for the committee
      if (service.CanUserEditClubMinutes(m.Entity.Committee) == false)
      {
        return RedirectToAction("View", "Error", new
        {
          errorMsg = "You do not have permission to 'Create' Committee Minutes for this Committee.",
          redirectUrl = Request.UrlReferrer
        });
      }

      ViewBag.EditClubMinutes = service.CanUserEditClubMinutes(m.Entity.Committee);
      ViewBag.EditAttachedDocs = UserController.EffectiveUser.GetClassAccess(typeof(AttachedDocument)).Allows(Access.Update);
      UtilsAttachedDocuments.SetViewDataAttachedDocuments(this.DbSession, ViewData, typeof(CommitteeMinute), m.Entity.Oid);
      SetViewBagSelectLists(m);
      return View(m);
    }

    [HttpPost]
    [ActionName("Edit")]
    public ActionResult EditPost(TypedModel<CommitteeMinute> m)
    {
      if (this.UserController.EffectiveUser.GetClassAccess(m.EntityType).Allows(Access.Update) == false)
      {
        return RedirectToAction("View", "Error", new
        {
          errorMsg = "You do not have permission to 'Edit' ClubMinutes.",
          redirectUrl = Request.UrlReferrer
        });
      }
      ViewBag.PnlView = Enum.GetName(typeof(PageViewType), PageViewType.Edit);

      if (ModelState.IsValid)
      {
        ClubMinutesServiceController service = (ClubMinutesServiceController)this.Injector.Activate(typeof(ClubMinutesServiceController));
        //Only members of the CommitteAdmins or an administrator can CREATE or UPDATE minutes for the committee
        if (service.CanUserEditClubMinutes(m.Entity.Committee) == false)
        {
          return RedirectToAction("View", "Error", new
          {
            errorMsg = "You do not have permission to 'Create' Committee Minutes for this Committee.",
            redirectUrl = Request.UrlReferrer
          });
        }

        service.SaveClubMinutes(new ModelStateWrapper(this.ModelState), m.Entity);
      }

      //If not sucessful saved ClubMinutes then return to View
      SetViewBagSelectLists(m);
      return View(m);
    }

    //public ActionResult Create([BoundModel(DontLoad = true, DontMerge = true)]TypedModel<ClubMinutes> m, Guid committeeOid, bool hideLayout = false, string controlId = "")
    //{
    //  if (this.UserController.EffectiveUser.GetClassAccess(m.EntityType).Allows(Access.Create) == false)
    //  {
    //    return RedirectToAction("View", "Error", new
    //    {
    //      errorMsg = "You do not have permission to 'Create' ClubMinutes.",
    //      redirectUrl = Request.UrlReferrer
    //    });
    //  }
    //  Committee c = this.DbSession.Get<Committee>(committeeOid);
    //  ClubMinutesServiceController service = (ClubMinutesServiceController)this.Injector.Activate(typeof(ClubMinutesServiceController));
    //  //Only members of the CommitteAdmins or an administrator can CREATE or UPDATE minutes for the committee
    //  if (service.CanUserEditClubMinutes(c) == false)
    //  {
    //    return RedirectToAction("View", "Error", new
    //    {
    //      errorMsg = "You do not have permission to 'Create' Committee Minutes for this Committee.",
    //      redirectUrl = Request.UrlReferrer
    //    });
    //  }
    //  ViewBag.HideLayout = hideLayout;
    //  m.Entity.Committee = c;
    //  //ViewBag.HideLayout = hideLayout;
    //  SetViewBagSelectLists(m);
    //  return View(m);
    //}

    public PartialViewResult Search(string controlId = null, bool selectMany = false)
    {
      TypedModel<ClubMinutesQry> model = new TypedModel<ClubMinutesQry>();
      model.Init();
      ViewBag.SelectMany = selectMany;
      SetViewOptions();
      SetViewBagSelectLists_ForSearch(controlId);
      return PartialView(model);
    }

    public ActionResult SearchResults_ReadData([DataSourceRequest]DataSourceRequest request, TypedModel<ClubMinutesQry> filter)
    {
      if (request.PageSize == 0)
      {
        request.PageSize = 10;
      }

      IEnumerable<CommitteeMinute> result = GetClubMinutes(filter);
      //IMapper Mapper = AutoMapperConfig.MapperConfiguration.CreateMapper();
      //IEnumerable<ModelClubMinutes> result = Mapper.Map<IEnumerable<ModelClubMinutes>>(teams);
      return Json(result.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
    }

    private void SetViewBagSelectLists_ForSearch(string controlId)
    {
      ViewBag.ClubMinutesControlId = controlId;
      //ViewBag.ClubMinutesMemberTypes = UtilsSelectLists.ClubMinutesMemberTypes(this.DbSession);
      //ViewBag.ClubMinutess = UtilsSelectLists.ClubMinutess(this.DbSession);
      ViewBag.MaxRecords = UtilsSelectLists.QueryMaxRecords();
    }

    private void SetViewOptions()
    {
      bool editClubMinutes = UserController.EffectiveUser.GetClassAccess(typeof(CommitteeMinute)).Allows(Access.Update);
      ViewBag.ClubMinutesEdit = editClubMinutes;
      if (editClubMinutes)
      {
        ViewBag.PnlView = Utility.setPnlView("Edit", UserController, typeof(CommitteeMinute));
      }
      else
      {
        ViewBag.PnlView = Utility.setPnlView("ReadOnly", UserController, typeof(CommitteeMinute));
      }

      bool editPerson = UserController.EffectiveUser.GetClassAccess(typeof(Person)).Allows(Access.Update);
      ViewBag.PersonEdit = editPerson;
    }

    private void SetViewBagSelectLists(TypedModel<CommitteeMinute> m)
    {
      ViewBag.Committees = UtilsSelectLists.Committees(this.DbSession);
      //ViewBag.ClubMinutess = UtilsSelectLists.ClubMinutess(this.DbSession);
      //ViewBag.ClubMinutesMemberTypes = UtilsSelectLists.ClubMinutesMemberTypes(this.DbSession, true);
      //ViewBag.AdminUsers = UtilsSelectLists.Users(this.DbSession, true);

      //ViewBag.ClubMinutesTitles = UtilsSelectLists.ClubMinutesTitles(this.DbSession, (m == null ? null : m.Entity.Title));
      //ViewBag.MembershipTypes = UtilsSelectLists.MembershipTypes(this.DbSession);
    }

    private IEnumerable<CommitteeMinute> GetClubMinutes(TypedModel<ClubMinutesQry> filter)
    {
      IEnumerable<CommitteeMinute> result = new List<CommitteeMinute>();
      if (UserController.EffectiveUser.GetClassAccess(typeof(CommitteeMinute)).Allows(Access.Read))
      {
        result = filter.Entity.List(this.DbSession).Cast<CommitteeMinute>();
      }
      return result;
    }

  }
}