using Club.Domain;
using Club.Domain.Artifacts;
using Mallon.Core.Artifacts;
using Mallon.Core.Interfaces;
using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Club.Services.Controllers
{
  public class TeamServiceController
  {
    #region Properties

    public ISession session { get; set; }
    public IUserController userController { get; set; }

    #endregion

    public bool SaveTeam(IValidationDictionary validatonDictionary, Team entity)
    {
      try
      {
        ValidateTeam(validatonDictionary, entity);

        if (validatonDictionary.IsValid)
        {
          this.session.BeginTransaction();
          this.session.SaveOrUpdate(entity);
          this.session.Transaction.Commit();
          return true;
        }
      }
      catch (Exception)
      {
        if (session.Transaction.IsActive)
        {
          this.session.Transaction.Rollback();
        }
        throw;
      }

      return false;
    }

    /// <summary>
    /// DirecDebit Lotto tickets are never deleted - they are set to inactive
    /// </summary>
    /// <param name="validatonDictionary"></param>
    /// <param name="entity"></param>
    /// <returns></returns>
    public bool DeleteTeam(IValidationDictionary validatonDictionary, Team entity)
    {
      try
      {
        entity.Active = false;
        this.session.BeginTransaction();
        this.session.SaveOrUpdate(entity);
        this.session.Transaction.Commit();
        return true;
      }
      catch (Exception)
      {
        if (session.Transaction.IsActive)
        {
          this.session.Transaction.Rollback();
        }
        throw;
      }
    }

    private void ValidateTeam(IValidationDictionary validatonDictionary, Team entity)
    {
      if (string.IsNullOrEmpty(entity.Name))
      {
        validatonDictionary.AddError("Name", "Please specify the Team Name.");
      }

      if (entity.TimeType == Domain.enumTimeType.Start_And_End_Date)
      {
        if (entity.StartDate.HasValue == false || entity.StartDate == default(DateTime))
        {
          validatonDictionary.AddError("StartDate", "Please enter a valid Start Date.");
        }

        if (entity.EndDate.HasValue == false || entity.EndDate == default(DateTime))
        {
          validatonDictionary.AddError("EndDate", "Please enter a valid End Date.");
        }

        if (entity.StartDate >= entity.EndDate)
        {
          validatonDictionary.AddError("StartDate", "The Start Date must be later than the End Date.");
        }
      }

      if (entity.TimeType == Domain.enumTimeType.Year)
      {
        if (entity.Year > 2400 || entity.Year < 1800)
        {
          validatonDictionary.AddError("Year", "Please enter a valid Year (greater than 1800) for this Team.");
        }
      }
    }

    public IEnumerable<TeamMember> TeamMembersGet(IValidationDictionary validatonDictionary, Guid teamOid, bool? isActive = null)
    {

      IEnumerable<TeamMember> entities = getTeamMembers(validatonDictionary, teamOid);
      return entities;
      //return ViewGridFireCertFee.GetMapping(entities);
    }

    private IEnumerable<TeamMember> getTeamMembers(IValidationDictionary validatonDictionary, Guid teamOid)
    {
      return this.session.QueryOver<TeamMember>().List<TeamMember>().Where(x => x.Team.Oid == teamOid).OrderBy(x => x.Person.NameSingleLine);
    }

    public bool CanUserEditTeam(Team team)
    {
      if (userController.LoggedOnUser.HasRole(userController.LoggedOnUser, Constants.AdministrationRoleOids))
      {
        return true;
      }

      if (team.TeamAdmins.Where(x => x.User.Oid == userController.LoggedOnUser.Oid).Count() > 0)
      {
        return true;
      }
      return false;
    }

    public bool CanUserEditTeam(Guid teamOid)
    {
      Team team = this.session.Get<Team>(teamOid);
      return CanUserEditTeam(team);
    }

    #region Team Admin Users

    public bool SelectTeamAdminUser(IValidationDictionary validatonDictionary, Guid userOid, Guid teamOid)
    {
      User user = this.session.Load<User>(userOid);
      if (user == null)
      {
        validatonDictionary.AddError("User", string.Format("Cannot locate User with Id:{0}", userOid.ToString()));
      }
      Team team = this.session.Load<Team>(teamOid);
      if (team == null)
      {
        validatonDictionary.AddError("Team", string.Format("Cannot locate Team with Id:{0}", teamOid.ToString()));
      }

      TeamAdmin entity = new TeamAdmin();
      entity.User = user;
      entity.Team = team;

      return SaveTeamAdminUser(validatonDictionary, ref entity);
    }

    public bool SaveTeamAdminUser(IValidationDictionary validatonDictionary, ref TeamAdmin entity)
    {
      try
      {
        ValidateTeamAdminUser(validatonDictionary, entity);

        if (validatonDictionary.IsValid)
        {
          //MIGHT NEED TO ADD TeamAdmin TO User AND Team
          this.session.BeginTransaction();
          this.session.SaveOrUpdate(entity);
          this.session.Transaction.Commit();
          return true;
        }
      }
      catch (Exception)
      {
        if (session.Transaction.IsActive)
        {
          this.session.Transaction.Rollback();
        }
        throw;
      }

      return false;
    }

    public bool DeleteTeamAdminUser(IValidationDictionary validatonDictionary, ref TeamAdmin entity)
    {
      try
      {
        if (CanUserEditTeam(entity.Team) == false)
        {
          validatonDictionary.AddError("Team", "You do not have permissions to add Team Admin users.");
        }

        if (validatonDictionary.IsValid)
        {
          this.session.BeginTransaction();
          this.session.Delete(entity);
          this.session.Transaction.Commit();
          return true;
        }
      }
      catch (Exception)
      {
        if (session.Transaction.IsActive)
        {
          this.session.Transaction.Rollback();
        }
        throw;
      }

      return false;
    }

    private void ValidateTeamAdminUser(IValidationDictionary validatonDictionary, TeamAdmin entity)
    {
      if (entity.User == null || entity.User.Oid == Guid.Empty)
      {
        validatonDictionary.AddError("User", "Please specify the User for this entity.");
      }
      if (entity.Team == null || entity.Team.Oid == Guid.Empty)
      {
        validatonDictionary.AddError("Team", "Please specify the Team for this entity.");
      }

      if (CanUserEditTeam(entity.Team) == false)
      {
        validatonDictionary.AddError("Team", "You do not have permissions to add Team Admin users.");
      }

      //No duplicates
      if (entity.Oid == Guid.Empty && this.session.QueryOver<TeamAdmin>().Where(x => x.Team.Oid == entity.Team.Oid && x.User.Oid == entity.User.Oid).RowCount() > 0)
      {
        validatonDictionary.AddError("User", "You can not enter duplicate Admins for Teams.");
      }
    }

    #endregion

    #region Team Members

    public bool CopyTeamMembers(IValidationDictionary validatonDictionary, Guid copyToTeamOid, Guid copyFromTeamOid)
    {
      //List<TeamMember> teamCopyTo = this.session.QueryOver<TeamMember>().List().Where(x => x.Team.Oid == copyToTeamOid).ToList<TeamMember>();
      Team teamCopyTo = this.session.Get<Team>(copyToTeamOid);
      if (teamCopyTo == null)
      {
        validatonDictionary.AddError("teamCopyTo", string.Format("Cannot locate Team with Id:{0}", copyToTeamOid.ToString()));
      }
      List<TeamMember> teamMembersCopyFrom = this.session.QueryOver<TeamMember>().List().Where(x => x.Team.Oid == copyFromTeamOid).ToList<TeamMember>();
      if (teamMembersCopyFrom == null)
      {
        validatonDictionary.AddError("copyFromTeamOid", string.Format("Cannot locate Team with Id:{0}", copyFromTeamOid.ToString()));
      }

      foreach (var item in teamMembersCopyFrom)
      {
        //To copy items from one Objet to another you need to EVICT the items or else they will not be copied BUT moved.
        this.session.Evict(item);
      }

      //Get members that are in teamCopyFrom but not in teamCopyTo
      List<TeamMember> results = teamMembersCopyFrom.Where(p => !teamCopyTo.Members.Any(p2 => p2.Person.Oid == p.Person.Oid)).ToList<TeamMember>();
      if (results.Count > 0)
      {
        try
        {
          this.session.BeginTransaction();
          foreach (TeamMember teamMember in results)
          {
            TeamMember newTeamMember = new TeamMember(teamMember, teamCopyTo);
            this.session.SaveOrUpdate(newTeamMember);
            teamCopyTo.Members.Add(newTeamMember);
          }

          this.session.SaveOrUpdate(teamCopyTo);
          this.session.Transaction.Commit();
          return true;

        }
        catch (Exception)
        {
          if (session.Transaction.IsActive)
          {
            this.session.Transaction.Rollback();
          }
          throw;
        }
      }
      return true;
    }

    public bool SelectTeamMember(IValidationDictionary validatonDictionary, Guid personOid, Guid teamOid)
    {
      Person person = this.session.Load<Person>(personOid);
      if (person == null)
      {
        validatonDictionary.AddError("Person", string.Format("Cannot locate Person with Id:{0}", personOid.ToString()));
      }
      Team team = this.session.Load<Team>(teamOid);
      if (team == null)
      {
        validatonDictionary.AddError("Team", string.Format("Cannot locate Team with Id:{0}", teamOid.ToString()));
      }

      TeamMemberType teamMemberType = this.session.QueryOver<TeamMemberType>().Where(x => x.Default == true).SingleOrDefault();
      if (teamMemberType == null)
      {
        validatonDictionary.AddError("TeamMemberType", "There is no default Team MemberType confgured in the database.");
      }

      TeamMember entity = new TeamMember();
      entity.Person = person;
      entity.Team = team;
      entity.TeamMemberType = teamMemberType;

      return SaveTeamMember(validatonDictionary, ref entity);
    }

    public bool SaveTeamMember(IValidationDictionary validatonDictionary, ref TeamMember entity)
    {
      try
      {
        ValidateTeamMember(validatonDictionary, entity);

        if (validatonDictionary.IsValid)
        {
          //MIGHT NEED TO ADD TeamMember TO Person AND Team
          this.session.BeginTransaction();
          this.session.SaveOrUpdate(entity);
          this.session.Transaction.Commit();
          return true;
        }
      }
      catch (Exception)
      {
        if (session.Transaction.IsActive)
        {
          this.session.Transaction.Rollback();
        }
        throw;
      }

      return false;
    }

    public bool DeleteTeamMember(IValidationDictionary validatonDictionary, ref TeamMember entity)
    {
      try
      {
        if (validatonDictionary.IsValid)
        {
          this.session.BeginTransaction();
          this.session.Delete(entity);
          this.session.Transaction.Commit();
          return true;
        }
      }
      catch (Exception)
      {
        if (session.Transaction.IsActive)
        {
          this.session.Transaction.Rollback();
        }
        throw;
      }

      return false;
    }

    private void ValidateTeamMember(IValidationDictionary validatonDictionary, TeamMember entity)
    {
      if (entity.Person == null || entity.Person.Oid == Guid.Empty)
      {
        validatonDictionary.AddError("Person", "Please specify the Person for this entity.");
      }
      if (entity.Team == null || entity.Team.Oid == Guid.Empty)
      {
        validatonDictionary.AddError("Team", "Please specify the Team for this entity.");
      }
      if (entity.TeamMemberType == null || entity.TeamMemberType.Oid == Guid.Empty)
      {
        validatonDictionary.AddError("TeamMemberType", "Please specify the Team Member Type for this team member.");
      }

      //No duplicates
      if (entity.Oid == Guid.Empty && this.session.QueryOver<TeamMember>().Where(x => x.Team.Oid == entity.Team.Oid && x.Person.Oid == entity.Person.Oid).RowCount() > 0)
      {
        validatonDictionary.AddError("Person", "You can not enter duplicate members for Teams.");
      }
    }

    #endregion
  }
}
