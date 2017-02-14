using AutoMapper;
using Club.Domain;
using Club.Domain.Models;
using Club.Services.Controllers;
using FrRocks.Models;
using Mallon.Core.Artifacts;
using Mallon.Core.PersistentSupport;
using Mallon.Core.Queries;
using Mallon.Core.Resources;
using Mallon.Core.Web.Models;
using Mallon.Resources.Util;
using NHibernate;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web.Mvc;
//using DiamondFireEncryption;
//using Mallons.DomainFire.Models;
//using DiamondFireWeb.Gui.Models;

namespace FrRocks.Controllers
{
  [DisplayName("User", "Users")]
  // [Authorize(Roles = "Adminstrator, Super Administrator")]
  public class AdminUsersController : ControllerBase
  {
    private const string licenceKey = "mallon.dfw.LicenceKey";
    private const string passCode = "3^AjyK";
    private const string customerName = "mallon.dfw.CustomerName";
    //private bool userLimit = false;
    //private bool userLimitGreater = false;

    public ActionResult Index([BoundModel(DontMerge = true)]TypedModel<Mallon.Core.Artifacts.User> model)
    {
      bool accountWasLocked = model.Entity.AccountLocked;
      model.Merge(Request.Form, Request.QueryString, RouteData.Values);

      string licKey = ConfigurationManager.AppSettings[licenceKey];
      switch (model.ExtraFields["op"])
      {
        case "n":
          model.Init(new Mallon.Core.Artifacts.User());
          break;
        case "s":
          if (ModelState.IsValid)
          {
            //check if hosted
            try
            {
              model.Entity.Email = model.Entity.Login;
              if (Convert.ToBoolean(ConfigurationManager.AppSettings["mallon.HostedInCloud"]))
              {
                //check if account was locked but is now unlocked
                if (accountWasLocked && !model.Entity.AccountLocked)
                {
                  //reset login fails
                  model.Entity.NoOfFailedLogins = 0;
                }
              }
              if (model.Entity.IsActive == true)
              {
                DbSession.SaveOrUpdate(model.Entity);
                DbSession.Flush();
              }
              else
              {
                DbSession.SaveOrUpdate(model.Entity);
                DbSession.Flush();
              }

            }
            catch (Exception e)
            {
              throw e;
            }
          }
          break;
        case "d":
          try
          {
            //Dont allow anyone to delete superUser
            if (model.Entity.HasRole(model.Entity, Constants.SuperAdministrationRoleOids))
            {
              throw new ModelStateException("You cant delete the Super User.");
            }
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
      if (model.Entity.Roles.Count > 0 && model.Entity.Roles.ElementAt(0) == null)
      {
        model.Entity.Roles.RemoveAt(0);
      }

      ModelUser m = null;

      IMapper Mapper = AutoMapperConfig.MapperConfiguration.CreateMapper();
      m = Mapper.Map<ModelUser>(model.Entity);

      // If the logged on user is accessing their admin account disable the 'Active' field 
      // so they can't make themselves 'Inactive'
      ViewBag.CurrentUser = false;
      var current = this.UserController.LoggedOnUser;
      if (current.Login == m.Login)
      {
        //If this is true then 'Active' field with not show
        ViewBag.CurrentUser = true;
      }
      ViewBag.CurrentActiveUsers = this.DbSession.QueryOver<User>().Where(x => x.IsActive == true).List<User>().Count;
      return PartialView(m);
    }

    public void SetInactive(TypedModel<User> model)
    {
      model.Entity.IsActive = false;
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

    public JsonResult GetRoles()
    {
      var current = this.UserController.LoggedOnUser;
      IEnumerable<ModelRole> results;
      IMapper Mapper = AutoMapperConfig.MapperConfiguration.CreateMapper();
      if (this.UserController.LoggedOnUser.HasRole(current, "Super Administrator"))
      {
        var list = DbSession.QueryOver<Role>().List<Role>().OrderBy(x => x.Name);
        results = Mapper.Map<IEnumerable<Role>, IEnumerable<ModelRole>>(list);
      }
      else
      {
        var list = DbSession.QueryOver<Role>().List<Role>().Where(x => x.Name != "Super Administrator").OrderBy(x => x.Name);
        results = Mapper.Map<IEnumerable<Role>, IEnumerable<ModelRole>>(list);
      }

      return Json(results, JsonRequestBehavior.AllowGet);
    }

    public virtual PartialViewResult ListAll(Guid? oid = null)
    {
      // Get a list of all entities of the class
      IQueryOver<User, User> qry =
      DbSession.QueryOver<User>();
      ApplySort(qry);

      IEnumerable<KeyValuePair<string, Guid>> result;

      var current = this.UserController.LoggedOnUser;
      if (this.UserController.LoggedOnUser.HasRole(current, "Super Administrator"))
      {
        result = qry.List()
        .Select(x => new KeyValuePair<string, Guid>(GetText(x), x.Oid));
      }
      else
      {
        result = qry.List()
          .Where(x => x.Login != "dfwSuperAdmin@mallontechnology.com")
          .Select(x => new KeyValuePair<string, Guid>(GetText(x), x.Oid));
      }
      // Return it to the view
      SelectList list = new SelectList(result, "Value", "Key", oid);
      return PartialView(list);
    }

    /// <summary>
    /// Adds the sort ordering to the query object used by <see cref="ListAll"/>.
    /// </summary>
    /// <remarks>
    /// The default implementation uses the <see cref="DisplayPropAttribute"/> applied to the
    /// entity class to decide how to order results.
    /// </remarks>
    /// <param name="qry">QueryOver object used by ListAll().</param>
    protected virtual void ApplySort(IQueryOver<User, User> qry)
    {
      QueryUtil.ApplyDefaultOrder(qry);
    }

    public virtual PartialViewResult Toolbar(string op = "v", Guid? oid = null)
    {
      List<ModelToolbarItem> result = new List<ModelToolbarItem>();

      string oidVal = oid.HasValue && oid.Value != Guid.Empty ? oid.ToString() : null;
      result.Add(new ModelToolbarItem { Text = "New", Image = Url.Content("~/Images/32px/add.png"), Enabled = "vsc".Contains(op), Op = "n", Oid = oidVal });
      result.Add(new ModelToolbarItem { Text = "Cancel", Image = Url.Content("~/Images/32px/error.png"), Enabled = "tn".Contains(op), Op = "c", Oid = oidVal });
      result.Add(new ModelToolbarItem { Text = "Save", Image = Url.Content("~/Images/32px/check.png"), Enabled = "nt".Contains(op), Op = "s", Oid = oidVal });
      result.Add(new ModelToolbarItem { Text = "Delete", Image = Url.Content("~/Images/32px/delete.png"), Enabled = "vc".Contains(op), Op = "d", Oid = oidVal });

      string ctlName = this.GetType().Name;
      ctlName = ctlName.Remove(ctlName.Length - 10, 10);
      result.ForEach(x => x.Controller = ctlName);
      return PartialView(result);
    }

    protected virtual string GetText(object obj)
    {
      if (obj == null)
        return "";
      if (!getTextInit)
      {
        summaryFormat = DisplayPropAttribute.GetSummaryFormat(typeof(User));
      }

      string result = null;
      if (summaryFormat != null)
        result = DisplayHelper.GetDisplayValue(obj, summaryFormat);
      else
        result = obj.ToString();

      return result;
    }

    /// <summary>
    /// Check to see if the number of active users in the database is equal to the 
    /// number of users the customer has paid for. If its true user will not be allowed 
    /// to add anymore active users
    /// </summary>
    /// <returns></returns>
    //private bool ValidateCustomerLicence(Guid userOid)
    //{
    //  string licKey = ConfigurationManager.AppSettings[licenceKey];
    //  string keyData = EncryptionHelper.Decrypt(licKey, passCode);
    //  string[] data = keyData.Split(',');
    //  string cusName = data[0];
    //  int users = Int32.Parse(data[1]);

    //  IList<User> activeUsers = this.DbSession.QueryOver<User>().Where(x => x.IsActive == true).List<User>();
    //  int numOfActive = activeUsers.Count;
    //  var userAlreadyActive = false;
    //  foreach (var u in activeUsers)
    //  {
    //    if (u.Oid == userOid)
    //    {
    //      userAlreadyActive = true;
    //    }
    //  }
    //  if (userAlreadyActive != true)
    //  {
    //    if (numOfActive == users)
    //    {
    //      userLimit = true;
    //      return false;
    //    }

    //    if (numOfActive > users)
    //    {
    //      userLimitGreater = true;
    //      return false;
    //    }
    //  }

    //  return true;
    //}

    private bool getTextInit;
    private string summaryFormat;
  }
}
