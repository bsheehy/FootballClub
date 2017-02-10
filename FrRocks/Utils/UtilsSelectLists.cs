
using Club.Domain;
using Club.Domain.Artifacts;
using Mallon.Core.Artifacts;
using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
namespace FrRocks.Utils
{
  public static class UtilsSelectLists
  {
    public static SelectList PersonTitles(ISession session, PersonTitle title = null)
    {
      var entities = session.QueryOver<PersonTitle>().List<PersonTitle>().Where(x => x.Active == true).OrderBy(x => x.Description);
      SelectList result;
      if (title == null)
      {
        result = new SelectList(entities, "Oid", "Description");
      }
      else
      {
        result = new SelectList(entities, "Oid", "Description", title.Oid);
      }
      return result;
    }

    public static SelectList MembershipTypes(ISession session, bool? activeOnly = null)
    {

      List<MembershipType> entities;
      if (activeOnly.HasValue)
      {
        entities = session.QueryOver<MembershipType>().List<MembershipType>().Where(x => x.Active == activeOnly.Value).OrderBy(x => x.StartDate).ToList<MembershipType>();
      }
      else
      {
        entities = session.QueryOver<MembershipType>().List<MembershipType>().OrderBy(x => x.StartDate).ToList<MembershipType>();
      }

      SelectList result;
      if (entities.Count > 0)
      {
        result = new SelectList(entities, "Oid", "NameSingleLine", entities[0]);
      }
      else
      {
        result = new SelectList(entities, "Oid", "NameSingleLine");
      }
      return result;
    }

    public static SelectList MembershipTypesForAdmin(ISession session, User user, bool? activeOnly = null, int? yearLimit = null)
    {
      if (user.HasRole(user, Constants.AddPersonMembershipRoleNames) == false)
      {
        return null;
      }

      List<MembershipType> entities;
      if (activeOnly.HasValue)
      {
        entities = session.QueryOver<MembershipType>().List<MembershipType>().Where(x => x.Active == activeOnly.Value).OrderBy(x => x.StartDate).ToList<MembershipType>();
      }
      else
      {
        entities = session.QueryOver<MembershipType>().List<MembershipType>().OrderBy(x => x.StartDate).ToList<MembershipType>();
      }


      if (yearLimit.HasValue && yearLimit.Value > 1800)
      {
        entities = entities.Where(x => x.Year == yearLimit).ToList();
      }

      SelectList result;
      if (entities.Count > 0)
      {
        result = new SelectList(entities, "Oid", "NameSingleLine", entities[0]);
      }
      else
      {
        result = new SelectList(entities, "Oid", "NameSingleLine");
      }


      return result;
    }

    public static SelectList Qualifications(ISession session, bool? activeOnly = null)
    {

      List<Qualification> entities;
      if (activeOnly.HasValue)
      {
        entities = session.QueryOver<Qualification>().List<Qualification>().Where(x => x.Active == activeOnly.Value).OrderBy(x => x.Name).ToList<Qualification>();
      }
      else
      {
        entities = session.QueryOver<Qualification>().List<Qualification>().OrderBy(x => x.Name).ToList<Qualification>();
      }

      SelectList result;
      if (entities.Count > 0)
      {
        result = new SelectList(entities, "Oid", "Name", entities[0]);
      }
      else
      {
        result = new SelectList(entities, "Oid", "Name");
      }
      return result;
    }

    public static SelectList Teams(ISession session, bool? activeOnly = null)
    {

      List<Team> entities;
      if (activeOnly.HasValue)
      {
        entities = session.QueryOver<Team>().List<Team>().Where(x => x.Active == activeOnly.Value).ToList<Team>();
      }
      else
      {
        entities = session.QueryOver<Team>().List<Team>().ToList<Team>();
      }

      SelectList result;
      if (entities.Count > 0)
      {
        result = new SelectList(entities, "Oid", "NameSingleLine", entities[0]);
      }
      else
      {
        result = new SelectList(entities, "Oid", "NameSingleLine");
      }
      return result;
    }

    public static SelectList TeamsWhereUserIsAdminOrAdministrator(ISession session, User user, bool? activeOnly = null, int? yearLimit = null)
    {

      List<Team> entities;
      if (activeOnly.HasValue)
      {
        if (user.HasRole(user, Constants.AdministrationRoleOids))
        {
          entities = session.QueryOver<Team>().Where(x => x.Active == activeOnly).List<Team>().ToList<Team>();
        }
        else
        {
          entities = session.QueryOver<Team>().Where(x => x.Active == activeOnly).Inner.JoinQueryOver<TeamAdmin>(u => u.TeamAdmins).Where(x => x.User.Oid == user.Oid).List<Team>().ToList<Team>();
        }
      }
      else
      {
        if (user.HasRole(user, Constants.AdministrationRoleOids))
        {
          entities = session.QueryOver<Team>().List<Team>().ToList<Team>();
        }
        else
        {
          entities = session.QueryOver<Team>().Inner.JoinQueryOver<TeamAdmin>(u => u.TeamAdmins).Where(x => x.User.Oid == user.Oid).List<Team>().ToList<Team>();
        }
      }


      if (yearLimit.HasValue && yearLimit.Value > 1800)
      {
        entities = entities.Where(x => x.Year == yearLimit).ToList();
      }


      SelectList result;
      if (entities.Count > 0)
      {
        result = new SelectList(entities, "Oid", "NameSingleLine", entities[0]);
      }
      else
      {
        result = new SelectList(entities, "Oid", "NameSingleLine");
      }
      return result;
    }

    public static SelectList LottoDrawsWithoutResults(ISession session, int? yearLimit = null)
    {
      List<Lotto> entities = session.QueryOver<Lotto>().Where(x => x.LottoResult == null || x.LottoResult.Oid == Guid.Empty).List<Lotto>().ToList<Lotto>();
      if (yearLimit.HasValue && yearLimit.Value > 1800)
      {
        entities = entities.Where(x => x.LottoDrawDate.Value.Date.Year == yearLimit).ToList();
      }

      SelectList result;
      if (entities.Count > 0)
      {
        result = new SelectList(entities, "Oid", "LottoDrawDate", entities[0]);
      }
      else
      {
        result = new SelectList(entities, "Oid", "LottoDrawDate");
      }
      return result;
    }

    public static SelectList TeamMemberTypes(ISession session, bool? activeOnly = null)
    {

      List<TeamMemberType> entities;
      if (activeOnly.HasValue)
      {
        entities = session.QueryOver<TeamMemberType>().List<TeamMemberType>().Where(x => x.Active == activeOnly.Value).ToList<TeamMemberType>();
      }
      else
      {
        entities = session.QueryOver<TeamMemberType>().List<TeamMemberType>().ToList<TeamMemberType>();
      }

      SelectList result;
      if (entities.Count > 0)
      {
        result = new SelectList(entities, "Oid", "Name", entities[0]);
      }
      else
      {
        result = new SelectList(entities, "Oid", "Name");
      }
      return result;
    }

    public static SelectList Managers(ISession session, bool? activeOnly = null)
    {

      List<TeamMemberType> entities;
      if (activeOnly.HasValue)
      {
        entities = session.QueryOver<TeamMemberType>().List<TeamMemberType>().Where(x => x.Active == activeOnly.Value && x.AdminMember == true).ToList<TeamMemberType>();
      }
      else
      {
        entities = session.QueryOver<TeamMemberType>().List<TeamMemberType>().Where(x => x.AdminMember == true).ToList<TeamMemberType>();
      }

      SelectList result;
      if (entities.Count > 0)
      {
        result = new SelectList(entities, "Oid", "Name", entities[0]);
      }
      else
      {
        result = new SelectList(entities, "Oid", "Name");
      }
      return result;
    }

    public static SelectList Committees(ISession session, bool? activeOnly = null)
    {

      List<Committee> entities;
      if (activeOnly.HasValue)
      {
        entities = session.QueryOver<Committee>().List<Committee>().Where(x => x.Active == activeOnly.Value).ToList<Committee>();
      }
      else
      {
        entities = session.QueryOver<Committee>().List<Committee>().ToList<Committee>();
      }

      SelectList result;
      if (entities.Count > 0)
      {
        result = new SelectList(entities, "Oid", "NameSingleLine", entities[0]);
      }
      else
      {
        result = new SelectList(entities, "Oid", "NameSingleLine");
      }
      return result;
    }

    public static SelectList CommitteesWhereUserIsAdminOrAdministrator(ISession session, User user, bool? activeOnly = null, int? yearLimit = null)
    {

      List<Committee> entities;
      if (activeOnly.HasValue)
      {
        if (user.HasRole(user, Constants.AdministrationRoleOids))
        {
          entities = session.QueryOver<Committee>().Where(x => x.Active == activeOnly).List<Committee>().ToList<Committee>();
        }
        else
        {
          entities = session.QueryOver<Committee>().Where(x => x.Active == activeOnly).Inner.JoinQueryOver<CommitteeAdmin>(u => u.CommitteeAdmins).Where(x => x.User.Oid == user.Oid).List<Committee>().ToList<Committee>();
        }
      }
      else
      {
        if (user.HasRole(user, Constants.AdministrationRoleOids))
        {
          entities = session.QueryOver<Committee>().List<Committee>().ToList<Committee>();
        }
        else
        {
          entities = session.QueryOver<Committee>().Inner.JoinQueryOver<CommitteeAdmin>(u => u.CommitteeAdmins).Where(x => x.User.Oid == user.Oid).List<Committee>().ToList<Committee>();
        }
      }


      if (yearLimit.HasValue && yearLimit.Value > 1800)
      {
        entities = entities.Where(x => x.Year == yearLimit).ToList();
      }


      SelectList result;
      if (entities.Count > 0)
      {
        result = new SelectList(entities, "Oid", "NameSingleLine", entities[0]);
      }
      else
      {
        result = new SelectList(entities, "Oid", "NameSingleLine");
      }
      return result;
    }

    public static SelectList CommitteeMemberTypes(ISession session, bool? activeOnly = null)
    {

      List<CommitteeMemberType> entities;
      if (activeOnly.HasValue)
      {
        entities = session.QueryOver<CommitteeMemberType>().List<CommitteeMemberType>().Where(x => x.Active == activeOnly.Value).ToList<CommitteeMemberType>();
      }
      else
      {
        entities = session.QueryOver<CommitteeMemberType>().List<CommitteeMemberType>().ToList<CommitteeMemberType>();
      }

      SelectList result;
      if (entities.Count > 0)
      {
        result = new SelectList(entities, "Oid", "Name", entities[0]);
      }
      else
      {
        result = new SelectList(entities, "Oid", "Name");
      }
      return result;
    }

    public static SelectList Users(ISession session, bool? isActiveOnly = null)
    {

      List<User> entities;
      if (isActiveOnly.HasValue)
      {
        entities = session.QueryOver<User>().List<User>().Where(x => x.IsActive == isActiveOnly.Value).OrderBy(x => x.FullName).ToList<User>();
      }
      else
      {
        entities = session.QueryOver<User>().List<User>().OrderBy(x => x.FullName).ToList<User>();
      }

      SelectList result;
      if (entities.Count > 0)
      {
        result = new SelectList(entities, "Oid", "FullName", entities[0]);
      }
      else
      {
        result = new SelectList(entities, "Oid", "FullName");
      }
      return result;
    }

    public static SelectList YesNo()
    {
      var yesNo = new[]
      {
        new SelectListItem { Text = "Yes", Value = "True" },
        new SelectListItem { Text = "No", Value = "False" }
      };

      SelectList result = new SelectList(yesNo, "Value", "Text");

      return result;
    }

    public static SelectList QueryMaxRecords()
    {
      var maxRecords = new[]
      {
        new SelectListItem { Text = "200", Value = "200"},
        new SelectListItem { Text = "500", Value = "500" },
        new SelectListItem { Text = "1000", Value = "1000" },
        new SelectListItem { Text = "5000", Value = "5000" },
        new SelectListItem { Text = "10000", Value = "10000" },
      };

      SelectList result = new SelectList(maxRecords, "Value", "Text");

      return result;
    }

    public static SelectList TimeTypes(int? val)
    {
      if (val.HasValue)
      {

      }
      else
      {

      }

      var maxRecords = new[]
      {
        new SelectListItem { Text = "200", Value = "200"},
        new SelectListItem { Text = "500", Value = "500" },
        new SelectListItem { Text = "1000", Value = "1000" },
        new SelectListItem { Text = "5000", Value = "5000" },
        new SelectListItem { Text = "10000", Value = "10000" },
      };

      SelectList result = new SelectList(maxRecords, "Value", "Text");

      return result;
    }
  }
}