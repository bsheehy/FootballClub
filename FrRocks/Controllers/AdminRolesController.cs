using Club.Domain.Artifacts;
using Club.Domain.Models;
using Club.Services.Controllers;
using FrRocks.Models;
using Kendo.Mvc;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Mallon.Core.Artifacts;
using Mallon.Core.Interfaces;
using Mallon.Core.Logic.Controllers;
using Mallon.Core.PersistentSupport;
using Mallon.Core.Resources;
using Mallon.Core.Web.Models;
using Mallon.Resources.Util;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web.Mvc;
using System.Web.UI.WebControls;

namespace FrRocks.Controllers
{
  [DisplayName("Group", "Groups")]
  public class AdminRolesController : AdminControllerBase<Role>
  {
    public IPersistentController PersistentController { get; set; }

    public override PartialViewResult Index(TypedModel<Mallon.Core.Artifacts.Role> model)
    {
      switch (model.ExtraFields["op"])
      {
        case "n":
          model.Init(new Mallon.Core.Artifacts.Role());
          break;
        case "s":
          if (ModelState.IsValid)
          {
            foreach (string key in model.ExtraFields.AllKeys)
            {
              if (key.StartsWith("ClassAccess_"))
              {
                // Get the class access to set the value for
                string path = key.Remove(0, 12);
                ClassAccess ent = model.Entity.ClassAccess.Where(x => x.ClassType.FullName == path).FirstOrDefault();
                if (ent == null)
                {
                  ent = new ClassAccess();
                  ent.ClassType = PersistentHelper.GetType(path);
                  model.Entity.ClassAccess.Add(ent);
                }
                ent.Access = (Access)int.Parse(model.ExtraFields[key]);
              }
            }

            // Update the default access
            string vals = model.ExtraFields["DefaultAccessChk"];
            Access access = Access.None;
            if (vals != null)
            {
              foreach (string val in vals.Split(','))
              {
                int eVal = 0;
                if (int.TryParse(val, out eVal))
                  access |= (Access)eVal;
              }
              model.Entity.DefaultAccess = access;
            }
            DbSession.SaveOrUpdate(model.Entity);
            DbSession.Flush();
          }
          break;
        case "d":
          try
          {
            DbSession.Delete(model.Entity);
            DbSession.Flush();
          }
          catch (NHibernate.ADOException err)
          {
            if (err.InnerException != null && err.InnerException.Message.Contains("DELETE statement conflicted with the REFERENCE constraint"))
            {
              DbSession.Clear();
              SetInactive(model);
            }
          }
          break;
      }
      ViewBag.DefaultAccess = GetDefaultAccess(model);
      return PartialView(model);
    }

    public void SetInactive(TypedModel<Role> model)
    {
      model.Entity.Active = false;
      try
      {
        DbSession.SaveOrUpdate(model.Entity);
        DbSession.Flush();
      }
      catch (Exception ex)
      {
        throw new Exception(ex.Message);
      }
    }

    public ActionResult Search([DataSourceRequest]DataSourceRequest request)
    {
      IEnumerable<ModelRoles> result = GetRoles(request);
      request.Filters.Clear();
      return Json(result.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
    }

    private IEnumerable<ModelRoles> GetRoles(DataSourceRequest request)
    {
      IPcQuery<Role> qry = null;
      Guid val = Guid.Empty;
      FilterDescriptor filter = (FilterDescriptor)request.Filters.Cast<FilterDescriptor>().FirstOrDefault(
        x => x.Value != null && Guid.TryParse(x.Value.ToString(), out val));
      if (filter != null)
        switch (filter.Member)
        {
          case "Roles":
            return ModelRoles.Collection(
              DbSession.QueryOver<Role>()
              .JoinQueryOver(x => x.Name)
                .List());
        }

      if (qry != null)
        return ModelRoles.Collection(PersistentController.ListObjects(qry));
      else
        return ModelRoles.Collection(PersistentController.ListObjects<Role>());
    }

    public PartialViewResult RoleList()
    {
      return PartialView(GetRoles(null));
    }

    public PartialViewResult ClassAccessTree(TypedModel<Role> model)
    {
      ViewBag.Oid = model.Entity.Oid;
      return PartialView(model);
    }

    [HttpPost]
    public ActionResult DeleteClassAccess(Guid oid, Guid role)
    {
      Role model = this.DbSession.Get<Role>(role);
      ClassAccess classAccess = this.DbSession.Get<ClassAccess>(oid);
      model.ClassAccess.Remove(classAccess);
      DbSession.SaveOrUpdate(model);
      DbSession.Flush();

      return Json(new[] { oid, role });
    }

    public JsonResult ClassAccessRead(TypedModel<Role> model)
    {
      IEnumerable<ModelClassAccess> result = ModelClassAccess.Collection(model.Entity.ClassAccess);
      string path = model.ExtraFields["Path"];
      result = result.Where(x => x.ParentPath == path);
      return Json(result, JsonRequestBehavior.AllowGet);
    }


    public IList getClassTypes()
    {
      return ReflectionHelper.GetImplementors(typeof(IPersistent),
        ConfigurationManager.AppSettings["mallon.artifactAssemblies"].Split(';'));
    }


    public PartialViewResult ClassEditor(TypedModel<Role> model)
    {
      ViewBag.RoleOid = model.Entity.Oid;
      IEnumerable<ModelClassAccess> result = GetModelClassAccess(model);
      return PartialView(result);
    }

    public ActionResult SaveClassEditor(IEnumerable<ModelClassAccess> models, Guid oid)
    {
      ViewBag.RoleOid = oid;
      Role role = this.DbSession.Get<Role>(oid);
      ClassAccess temp;

      ModelJsonResult j = new ModelJsonResult();
      try
      {
        if (models != null)
        {
          foreach (ModelClassAccess model in models)
          {
            if (model.Value != null)
            {
              temp = new ClassAccess();

              temp.ClassType = Type.GetType(model.Value);
              temp.Access = role.DefaultAccess;

              //check for duplication
              if (role.ClassAccess.Where(w => w.ClassType == temp.ClassType).Count() == 0)
                role.ClassAccess.Add(temp);
            }
          }
        }

        AdminServiceController service = (AdminServiceController)this.Injector.Activate(typeof(AdminServiceController));
        if (service.Save(role, this.DbSession, new ModelStateWrapper(this.ModelState)))
        {
          j.success = true;
        }
      }
      catch (Exception e)
      {
        j.error = e.Message;
      }

      return Json(j, JsonRequestBehavior.AllowGet);
    }

    private static IEnumerable<ModelClassAccess> GetModelClassAccess(TypedModel<Role> model)
    {
      IList<Type> classes = ReflectionHelper.GetImplementors(typeof(IPersistent),
        ConfigurationManager.AppSettings["mallon.artifactAssemblies"].Split(';')).Cast<Type>().ToList();

      IList<ClassAccess> tempList = new List<ClassAccess>();
      foreach (Type a in classes)
      {
        ClassAccess temp = new ClassAccess();
        temp.ClassType = a;
        tempList.Add(temp);
      }

      IEnumerable<ModelClassAccess> result = ModelClassAccess.Collection(tempList);
      string path = model.ExtraFields["Path"];
      result = result.Where(x => x.ParentPath == path);
      return result;
    }

    public JsonResult GetClassAccessList()
    {
      IList<TreeNode> rootNode = new List<TreeNode>();

      rootNode.Add(new TreeNode() { Text = typeof(Team).Name, Value = typeof(Team).AssemblyQualifiedName });
      rootNode.Add(new TreeNode() { Text = typeof(TeamMemberType).Name, Value = typeof(TeamMemberType).AssemblyQualifiedName });
      rootNode.Add(new TreeNode() { Text = typeof(MembershipType).Name, Value = typeof(MembershipType).AssemblyQualifiedName });
      rootNode.Add(new TreeNode() { Text = typeof(Qualification).Name, Value = typeof(Qualification).AssemblyQualifiedName });

      return Json(rootNode, JsonRequestBehavior.AllowGet);
    }


    public PartialViewResult DefaultAccess(TypedModel<Role> model)
    {
      return PartialView(GetDefaultAccess(model));
    }

    private static IEnumerable<ModelDefaultAccess> GetDefaultAccess(TypedModel<Role> model)
    {
      IEnumerable<Access> accessLevels = Enum.GetValues(typeof(Access)).Cast<Access>().Where(x => x != Access.None && x != Access.All).ToList();
      IEnumerable<ModelDefaultAccess> defaultModel = ModelDefaultAccess.Collection(accessLevels, model.Entity.DefaultAccess);
      return defaultModel;
    }

  }
}
