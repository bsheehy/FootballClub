using Club.Domain;
using Mallon.Core.Artifacts;
using Mallon.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Configuration;

namespace Club.Services.Utils
{
  public static class Utility
  {
    private const string appSettingsTheme = "sheehy.club.Theme";
    private static Guid clubOid = new Guid("15AC9F29-85AB-4ECE-A5FB-DAD23D8D946E");
    public static Guid GetClubId()
    {
      return clubOid;
    }

    public static string GetTheme()
    {
      string theme = ConfigurationManager.AppSettings[appSettingsTheme];
      //If theme not set
      if (string.IsNullOrEmpty(theme) == true)
      {
        //Get default theme: bootstrap
        theme = "bootstrap";
      }

      return theme;
    }

    public static string setPnlView(string pnlView, IUserController userController, Type entityType)
    {
      Access access = userController.EffectiveUser.GetClassAccess(entityType);
      if (string.IsNullOrEmpty(pnlView))
      {
        if (access.Allows(Access.Update))
        {
          pnlView = "Edit";
        }
        else if (access.Allows(Access.Read))
        {
          pnlView = "ReadOnly";
        }
        else
        {
          pnlView = "NoAccess";
        }
      }
      else
      {
        if (!pnlView.Equals("Create", StringComparison.CurrentCultureIgnoreCase)
        && !pnlView.Equals("Edit", StringComparison.CurrentCultureIgnoreCase)
        && !pnlView.Equals("ReadOnly", StringComparison.CurrentCultureIgnoreCase)
        && !pnlView.Equals("NoAccess", StringComparison.CurrentCultureIgnoreCase)
        && !pnlView.Equals("Delete", StringComparison.CurrentCultureIgnoreCase))
        {
          pnlView = "ReadOnly";
        }
      }
      return pnlView;
    }

    /// <summary>
    /// Returns True if the Logged in User is in one of the following roles:
    ///   1. Super Administrator
    ///   2. Administrator
    /// </summary>
    /// <param name="userController"></param>
    /// <returns></returns>
    public static bool UserIsAdmin(IUserController userController)
    {
      /// <summary>
      /// Predfined list of Roles that are allowed to access the Administration section
      /// </summary>
      return userController.LoggedOnUser.HasRole(userController.LoggedOnUser, Constants.AdministrationRoleOids);
    }


    public static bool UserIsSuperAdmin(IUserController userController)
    {
      /// <summary>
      /// Predfined list of Roles that are allowed to access the Administration section
      /// </summary>
      return userController.LoggedOnUser.HasRole(userController.LoggedOnUser, Constants.SuperAdministrationRoleOids);
    }

    public static bool UserIsCommitteeOrAbove(IUserController userController)
    {
      /// <summary>
      /// Predfined list of Roles that are allowed to access the Administration section
      /// </summary>
      return userController.LoggedOnUser.HasRole(userController.LoggedOnUser, Constants.SuperAdministrationRoleOids);
    }


    public static string WelcomeUserMessage(IUserController userController)
    {
      DateTime now = DateTime.Now;

      if (now.Hour < 12)
      {
        return string.Concat("Good Morning ", userController.EffectiveUser.FullName);
      }
      else if (now.Hour < 17)
      {
        return string.Concat("Good Afternoon ", userController.EffectiveUser.FullName);
      }
      else
      {
        return string.Concat("Good Evening ", userController.EffectiveUser.FullName);
      }
    }

    public static PageViewType routesSetPnlViewEnum(string pnlView, Guid? oid)
    {
      if (string.IsNullOrEmpty(pnlView))
      {
        if (oid == null || oid == Guid.Empty)
        {
          return PageViewType.Create;
        }
        else
        {
          return PageViewType.ReadOnly;
        }
      }
      if (!pnlView.Equals("Create", StringComparison.CurrentCultureIgnoreCase)
        && !pnlView.Equals("Edit", StringComparison.CurrentCultureIgnoreCase)
        && !pnlView.Equals("ReadOnly", StringComparison.CurrentCultureIgnoreCase)
        && !pnlView.Equals("NoAccess", StringComparison.CurrentCultureIgnoreCase)
        && !pnlView.Equals("Delete", StringComparison.CurrentCultureIgnoreCase))
      {
        return PageViewType.Create;
      }

      if (pnlView.Equals("ReadOnly", StringComparison.CurrentCultureIgnoreCase))
      {
        return PageViewType.ReadOnly;
      }

      if (pnlView.Equals("Delete", StringComparison.CurrentCultureIgnoreCase))
      {
        return PageViewType.Delete;
      }

      if (pnlView.Equals("Create", StringComparison.CurrentCultureIgnoreCase)
        || pnlView.Equals("Edit", StringComparison.CurrentCultureIgnoreCase))
      {
        if (oid == null || oid == Guid.Empty)
        {
          return PageViewType.Create;
        }
        else
        {
          return PageViewType.Edit;
        }
      }
      return PageViewType.ReadOnly;
    }

    public static IDictionary<string, object> MergeDictionaries(IDictionary<string, object> first, IDictionary<string, object> second)
    {
      if (first == null && second == null) return new Dictionary<string, object>();
      if (second == null) return first;
      if (first == null) return second;

      foreach (var item in second)
        if (!first.ContainsKey(item.Key))
          first.Add(item.Key, item.Value);
      return first;

      //return first.ToList().ForEach(x => second[x.Key] = x.Value);
    }
  }
}