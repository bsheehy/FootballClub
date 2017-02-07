using Club.Services.Utils;
using FrRocks.Models;
using System;
using System.Collections.Generic;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace FrRocks.Controllers
{
  public class AdminController : ControllerBase
  {
    public ActionResult Index()
    {
      if (Utility.UserIsAdmin(this.UserController) == false)
      {
        throw new HttpException((int)HttpStatusCode.Forbidden, string.Format("'{0}' does not have permission to Access the Administration section.", this.UserController.LoggedOnUser.Login));
      }
      ViewBag.SuperUser = Utility.UserIsSuperAdmin(this.UserController);
      return View();
    }

    public PartialViewResult Toolbar(string op = "v", Guid? oid = null)
    {
      List<ModelToolbarItem> result = new List<ModelToolbarItem>();

      string oidVal = oid.HasValue && oid.Value != Guid.Empty ? oid.ToString() : null;
      result.Add(new ModelToolbarItem { Text = "New", Image = Url.Content("~/Images/32px/add.png"), Enabled = "vsc".Contains(op), Op = "n", Oid = oidVal });
      result.Add(new ModelToolbarItem { Text = "Cancel", Image = Url.Content("~/Images/32px/error.png"), Enabled = "tn".Contains(op), Op = "c", Oid = oidVal });
      result.Add(new ModelToolbarItem { Text = "Save", Image = Url.Content("~/Images/32px/check.png"), Enabled = "nt".Contains(op), Op = "s", Oid = oidVal });
      result.Add(new ModelToolbarItem { Text = "Delete", Image = Url.Content("~/Images/32px/delete.png"), Enabled = "vc".Contains(op), Op = "d", Oid = oidVal });

      result.ForEach(x =>
      {
        x.Controller = "AdminAuthority";
      }
      );
      return PartialView(result);
    }

    public PartialViewResult UserSetup()
    {
      return PartialView();
    }

  }
}