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
  public class MembershipController : ControllerBase
  {
    public ActionResult Create([BoundModel(DontLoad = true, DontMerge = true)]TypedModel<MembershipType> m, bool hideLayout = false)
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
    public ActionResult CreatePost(TypedModel<MembershipType> m)
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
        MembershipServiceController service = (MembershipServiceController)this.Injector.Activate(typeof(MembershipServiceController));
        if (service.SaveMembershipType(new ModelStateWrapper(this.ModelState), m.Entity))
        {
          //Sucessfully saved MembershipType
          return RedirectToAction("Edit", new { Oid = m.Entity.Oid });
        }
      }

      //If not sucessful saved MembershipType then return to View
      SetViewBagSelectLists(m);
      return View(m);
    }

    public ActionResult Edit(TypedModel<MembershipType> m)
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

      SetViewBagSelectLists(m);
      return View(m);
    }

    [HttpPost]
    [ActionName("Edit")]
    public ActionResult EditPost(TypedModel<MembershipType> m)
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
        MembershipServiceController service = (MembershipServiceController)this.Injector.Activate(typeof(MembershipServiceController));
        service.SaveMembershipType(new ModelStateWrapper(this.ModelState), m.Entity);
      }

      //If not sucessful saved MembershipType then return to View
      SetViewBagSelectLists(m);
      return View(m);
    }

    /// <summary>
    /// Called for MembershipType searches - options for the result list are also set in here.
    /// </summary>
    /// <param name="MembershipSelect">If True then 'Select MembershipType' enabled and 'Edit' is disabled in List Results.</param>
    /// <returns></returns>
    public PartialViewResult Search(string controlId = null)
    {
      TypedModel<PersonMembershipQry> model = new TypedModel<PersonMembershipQry>();
      model.Init();
      SetViewOptions_Membership();
      SetViewBagSelectLists_ForSearch(controlId);
      return PartialView(model);
    }

    public ActionResult SearchResults_ReadData([DataSourceRequest]DataSourceRequest request, TypedModel<PersonMembershipQry> filter)
    {
      if (request.PageSize == 0)
      {
        request.PageSize = 30;
      }

      IEnumerable<PersonMembershipType> people = GetPersonMembership(filter);
      IMapper Mapper = AutoMapperConfig.MapperConfiguration.CreateMapper();
      IEnumerable<ModelPersonMembershipType> result = Mapper.Map<IEnumerable<ModelPersonMembershipType>>(people);
      return Json(result.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
    }

    public JsonResult PersonMembership_GridRead([DataSourceRequest]DataSourceRequest request, Guid personOid)
    {
      if (request.PageSize == 0)
      {
        request.PageSize = 30;
      }

      MembershipServiceController service = (MembershipServiceController)this.Injector.Activate(typeof(MembershipServiceController));
      IEnumerable<PersonMembershipType> m = service.PersonMembershipsGet(new ModelStateWrapper(this.ModelState), personOid, true);

      IMapper Mapper = AutoMapperConfig.MapperConfiguration.CreateMapper();
      IEnumerable<ModelPersonMembershipType> result = Mapper.Map<IEnumerable<ModelPersonMembershipType>>(m);
      return Json(result.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
    }

    [AcceptVerbs(HttpVerbs.Post)]
    public ActionResult PersonMembership_Update([DataSourceRequest] DataSourceRequest request, ModelPersonMembershipType product)
    {
      if (this.UserController.EffectiveUser.GetClassAccess(typeof(PersonMembershipType)).Allows(Access.Update) == false)
      {
        return RedirectToAction("View", "Error", new
        {
          errorMsg = "You do not have permission to 'Update' PersonMembershipType's.",
          redirectUrl = Request.UrlReferrer
        });
      }

      if (product != null && ModelState.IsValid)
      {
        PersonMembershipType domainEntity = this.DbSession.Get<PersonMembershipType>(product.Oid);
        domainEntity.MembershipType = this.DbSession.Get<MembershipType>(product.MembershipTypeOid);
        IMapper Mapper = AutoMapperConfig.MapperConfiguration.CreateMapper();
        Mapper.Map<ModelPersonMembershipType, PersonMembershipType>(product, domainEntity);

        MembershipServiceController service = (MembershipServiceController)this.Injector.Activate(typeof(MembershipServiceController));
        if (service.PersonMembershipSave(new ModelStateWrapper(this.ModelState), ref domainEntity))
        {
          Mapper.Map<PersonMembershipType, ModelPersonMembershipType>(domainEntity, product);
          return Json(new[] { product }.ToDataSourceResult(request, ModelState));
        }
      }
      return Json(new[] { product }.ToDataSourceResult(request, ModelState));
      //throw new ModelStateException(this.ModelState);
    }

    [AcceptVerbs(HttpVerbs.Post)]
    public ActionResult PersonMembership_Delete([DataSourceRequest] DataSourceRequest request, ModelPersonMembershipType product)
    {
      if (this.UserController.EffectiveUser.GetClassAccess(typeof(PersonMembershipType)).Allows(Access.Delete) == false)
      {
        return RedirectToAction("View", "Error", new
        {
          errorMsg = "You do not have permission to 'Delete' PersonMembershipType's.",
          redirectUrl = Request.UrlReferrer
        });
      }

      PersonMembershipType domainEntity = this.DbSession.Get<PersonMembershipType>(product.Oid);
      MembershipServiceController service = (MembershipServiceController)this.Injector.Activate(typeof(MembershipServiceController));
      if (service.PersonMembershipDelete(new ModelStateWrapper(this.ModelState), ref domainEntity))
      {
        IMapper Mapper = AutoMapperConfig.MapperConfiguration.CreateMapper();
        Mapper.Map<PersonMembershipType, ModelPersonMembershipType>(domainEntity, product);
        return Json(new[] { product }.ToDataSourceResult(request, ModelState));
      }
      return Json(new[] { product }.ToDataSourceResult(request, ModelState));
      //throw new ModelStateException(this.ModelState);
    }

    [AcceptVerbs(HttpVerbs.Post)]
    public ActionResult PersonMembership_Create([DataSourceRequest] DataSourceRequest request, ModelPersonMembershipType product, Guid personOid)
    {
      if (this.UserController.EffectiveUser.GetClassAccess(typeof(PersonMembershipType)).Allows(Access.Create) == false)
      {
        return RedirectToAction("View", "Error", new
        {
          errorMsg = "You do not have permission to 'Create' PersonMembershipType's.",
          redirectUrl = Request.UrlReferrer
        });
      }
      //product.PersonOid = personOid;

      PersonMembershipType domainEntity = new PersonMembershipType();
      IMapper Mapper = AutoMapperConfig.MapperConfiguration.CreateMapper();
      Mapper.Map<ModelPersonMembershipType, PersonMembershipType>(product, domainEntity);
      domainEntity.Person = this.DbSession.Get<Person>(personOid);
      domainEntity.MembershipType = this.DbSession.Get<MembershipType>(product.MembershipTypeOid);

      MembershipServiceController service = (MembershipServiceController)this.Injector.Activate(typeof(MembershipServiceController));
      if (service.PersonMembershipSave(new ModelStateWrapper(this.ModelState), ref domainEntity))
      {
        Mapper.Map<PersonMembershipType, ModelPersonMembershipType>(domainEntity, product);
        return Json(new[] { product }.ToDataSourceResult(request, ModelState));
      }
      return Json(new[] { product }.ToDataSourceResult(request, ModelState));
    }

    public ActionResult AddPersonMembership(Guid membershipTypeOid)
    {
      TypedModel<PersonMembershipQry> model = new TypedModel<PersonMembershipQry>();
      model.Init();
      model.Entity.aq_MembershipOid = membershipTypeOid;
      SetViewOptions_Membership();
      SetViewBagSelectLists_ForSearch(null);
      //ViewBag.MembershipTypes = UtilsSelectLists.MembershipTypesForAdmin(this.DbSession, this.UserController.EffectiveUser, true);
      //MembershipType membershipType = this.DbSession.Get<MembershipType>(membershipTypeOid);
      //ModelPersonMembershipType m = new ModelPersonMembershipType(membershipType);
      MembershipServiceController service = (MembershipServiceController)this.Injector.Activate(typeof(MembershipServiceController));
      ViewBag.AddPersonMembership = service.CanUserEditMembership();
      return View(model);
    }

    private void SetViewBagSelectLists_ForSearch(string controlId)
    {
      ViewBag.MembershipControlId = controlId;
      ViewBag.MaxRecords = UtilsSelectLists.QueryMaxRecords();
      ViewBag.MembershipTypes = UtilsSelectLists.MembershipTypes(this.DbSession);
    }

    private void SetViewOptions_Membership()
    {
      bool editMembership = UserController.EffectiveUser.GetClassAccess(typeof(MembershipType)).Allows(Access.Update);
      ViewBag.MembershipEdit = editMembership;
      if (editMembership)
      {
        ViewBag.PnlView = Utility.setPnlView("Edit", UserController, typeof(MembershipType));
      }
      else
      {
        ViewBag.PnlView = Utility.setPnlView("ReadOnly", UserController, typeof(MembershipType));
      }
    }

    private void SetViewOptions_PersonMembership()
    {
      bool editMembership = UserController.EffectiveUser.GetClassAccess(typeof(PersonMembershipType)).Allows(Access.Update);
      ViewBag.MembershipEdit = editMembership;
      if (editMembership)
      {
        ViewBag.PnlView = Utility.setPnlView("Edit", UserController, typeof(PersonMembershipType));
      }
      else
      {
        ViewBag.PnlView = Utility.setPnlView("ReadOnly", UserController, typeof(PersonMembershipType));
      }

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

    private IEnumerable<PersonMembershipType> GetPersonMembership(TypedModel<PersonMembershipQry> filter)
    {
      IEnumerable<PersonMembershipType> result = new List<PersonMembershipType>();
      if (UserController.EffectiveUser.GetClassAccess(typeof(PersonMembershipType)).Allows(Access.Read))
      {
        result = filter.Entity.List(this.DbSession).Cast<PersonMembershipType>();
      }
      return result;
    }

    private void SetViewBagSelectLists(TypedModel<MembershipType> m)
    {

    }
  }
}