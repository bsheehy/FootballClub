using FrRocks.Filters;
using Mallon.Core.Artifacts;
using Mallon.Core.Interfaces;
using Mallon.Core.PersistentSupport;
using Mallon.Core.Web.Attributes;
using Mallon.Core.Web.Controllers;
using NHibernate;
using System;

namespace FrRocks.Controllers
{
  /// <summary>
  /// Provides a base class for all controllers, for common functionality.
  /// </summary>
  [NoCacheAttribute]
  [ApplicationAuthorizeAttribute]
  [HandleModelStateException]
  public abstract class ControllerBase : InjectingController
  {
    public ISession DbSession { get; set; }
    public IUserController UserController { get; set; }

    /// <summary>
    /// This is the Category attribute of the domain artifact that this controller acts on 
    /// e.g. FireCert, Administration etc..
    /// </summary>
    public string Category { get; set; }

    public Access PageUserView { get; set; }

    /// <summary>
    /// Gets the logged in users Access to a specific Category
    /// </summary>
    public void SetPageUserView(string Category)
    {
      if (string.IsNullOrEmpty(this.Category))
      {
        if (UserController.EffectiveUser != null)
        {
          // Decide which controls to display
          this.PageUserView = this.UserController.EffectiveUser.GetCategoryAccess(this.Category);
          ViewData["PageUserAccess"] = Enum.GetName(this.PageUserView.GetType(), this.PageUserView);
          return;
        }
      }
      this.PageUserView = Access.All;
    }

    /// <summary>
    /// Gets the logged in users Access to a specific Category
    /// </summary>
    public void SetPageUserView(Type t)
    {
      CategoryAttribute MyAttribute = (CategoryAttribute)Attribute.GetCustomAttribute(t, typeof(CategoryAttribute));
      if (MyAttribute == null)
      {
        SetPageUserView(MyAttribute.Name);
      }
    }
  }
}