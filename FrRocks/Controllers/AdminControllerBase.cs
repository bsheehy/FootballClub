using Club.Services.Utils;
using FrRocks.Filters;
using FrRocks.Models;
using FrRocks.Utils;
using Mallon.Core.Interfaces;
using Mallon.Core.PersistentSupport;
using Mallon.Core.Queries;
using Mallon.Core.Web.Controllers;
using Mallon.Core.Web.Models;
using Mallon.Resources.Util;
using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace FrRocks.Controllers
{
  [HandleModelStateException]
  [ApplicationAuthorizeAttribute]
  public abstract class AdminControllerBase<T> : InjectingController where T : class, IPersistent, new()
  {
    public ISession DbSession { get; set; }
    public IUserController UserController { get; set; }

    public virtual PartialViewResult Index(TypedModel<T> model)
    {
      if (Utility.UserIsAdmin(this.UserController) == false)
      {
        throw new HttpException((int)HttpStatusCode.Forbidden, string.Format("'{0}' does not have permission to Access the Administration section.", this.UserController.LoggedOnUser.Login));
      }

      switch (model.ExtraFields["op"])
      {
        case "n":
          model.Init(new T());
          break;
        case "s":
          if (ModelState.IsValid)
          {
            SaveEntity(model);
          }
          break;
        case "d":
          try
          {
            DeleteEntity(model);
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
      return PartialView(model);

    }

    public virtual void SetInactive(TypedModel<T> model)
    {

    }

    public virtual void SaveEntity(TypedModel<T> model)
    {
      DbSession.SaveOrUpdate(model.Entity);
      DbSession.Flush();
    }

    public virtual void DeleteEntity(TypedModel<T> model)
    {
      DbSession.Delete(model.Entity);
      DbSession.Flush();
    }


    public virtual PartialViewResult ListAll(Guid? oid = null)
    {
      // Get a list of all entities of the class
      IQueryOver<T, T> qry =
      DbSession.QueryOver<T>();
      ApplySort(qry);


      IEnumerable<KeyValuePair<string, Guid>> result = qry
        .List()
        .Select(x => new KeyValuePair<string, Guid>(GetText(x), x.Oid));

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
    protected virtual void ApplySort(IQueryOver<T, T> qry)
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
        summaryFormat = DisplayPropAttribute.GetSummaryFormat(typeof(T));
      }

      string result = null;
      if (summaryFormat != null)
        result = DisplayHelper.GetDisplayValue(obj, summaryFormat);
      else
        result = obj.ToString();

      return result;
    }


    private bool getTextInit;
    private string summaryFormat;

  }
}