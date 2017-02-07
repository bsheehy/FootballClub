using AutoMapper;
using Club.Domain;
using Club.Domain.Artifacts;
using Club.Domain.Models;
using Mallon.Core.Artifacts;
using Mallon.Core.Interfaces;
using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace Club.Services.Controllers
{
  public class CommitteeServiceController
  {
    #region Properties

    public ISession session { get; set; }
    public IUserController userController { get; set; }

    #endregion

    public bool SaveCommitteeMemberType(IValidationDictionary validatonDictionary, CommitteeMemberType entity)
    {
      try
      {
        ValidateCommitteeMemberType(validatonDictionary, entity);

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

    public bool DeleteCommitteeMemberType(IValidationDictionary validatonDictionary, CommitteeMemberType entity)
    {
      try
      {
        //entity.Active = false;
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

    private void ValidateCommitteeMemberType(IValidationDictionary validatonDictionary, CommitteeMemberType entity)
    {
      if (string.IsNullOrEmpty(entity.Name))
      {
        validatonDictionary.AddError("Name", "Please specify the Committee Member Type Name.");
      }

      //No duplicates
      if (entity.Oid == Guid.Empty && this.session.QueryOver<CommitteeMemberType>().Where(x => x.Name.Equals(entity.Name)).RowCount() > 0)
      {
        validatonDictionary.AddError("Name", "You can not enter duplicate names's for CommitteeMemberType.");
      }
    }

    public bool SaveCommittee(IValidationDictionary validatonDictionary, Committee entity)
    {
      try
      {
        ValidateCommittee(validatonDictionary, entity);

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

    public bool DeleteCommittee(IValidationDictionary validatonDictionary, Committee entity)
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

    private void ValidateCommittee(IValidationDictionary validatonDictionary, Committee entity)
    {
      if (string.IsNullOrEmpty(entity.Name))
      {
        validatonDictionary.AddError("Name", "Please specify the Committee Name.");
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

    public IEnumerable<CommitteeMember> GetCommitteeMembers(IValidationDictionary validatonDictionary, Guid committeeOid)
    {

      IEnumerable<CommitteeMember> entities = getCommitteeMembers(validatonDictionary, committeeOid);
      return entities;
      //return ViewGridFireCertFee.GetMapping(entities);
    }

    private IEnumerable<CommitteeMember> getCommitteeMembers(IValidationDictionary validatonDictionary, Guid committeeOid)
    {
      return this.session.QueryOver<CommitteeMember>().List<CommitteeMember>().Where(x => x.Committee.Oid == committeeOid).OrderBy(x => x.Committee.Year);
    }

    /// <summary>
    /// Only members of the CommitteAdmins or an administrator can CREATE or UPDATE minutes for the committee
    /// </summary>
    /// <param name="committee"></param>
    /// <returns></returns>
    public bool CanUserEditCommittee(Committee committee)
    {
      if (userController.LoggedOnUser.HasRole(userController.LoggedOnUser, Constants.AdministrationRoleOids))
      {
        return true;
      }

      if (committee.CommitteeAdmins.Where(x => x.User.Oid == userController.LoggedOnUser.Oid).Count() > 0)
      {
        return true;
      }
      return false;
    }

    public bool CanUserEditCommittee(Guid committeeOid)
    {
      Committee team = this.session.Get<Committee>(committeeOid);
      return CanUserEditCommittee(team);
    }

    #region Committee Members

    public bool SelectCommitteeMember(IValidationDictionary validatonDictionary, Guid personOid, Guid committeeOid)
    {
      Person person = this.session.Load<Person>(personOid);
      if (person == null)
      {
        validatonDictionary.AddError("Person", string.Format("Cannot locate Person with Id:{0}", personOid.ToString()));
      }
      Committee committee = this.session.Load<Committee>(committeeOid);
      if (committee == null)
      {
        validatonDictionary.AddError("Committee", string.Format("Cannot locate Committee with Id:{0}", committeeOid.ToString()));
      }

      CommitteeMemberType committeeMemberType = this.session.QueryOver<CommitteeMemberType>().Where(x => x.Default == true).SingleOrDefault();
      if (committeeMemberType == null)
      {
        validatonDictionary.AddError("CommitteeMemberType", "There is no default Committee MemberType confgured in the database.");
      }

      CommitteeMember entity = new CommitteeMember();
      entity.Person = person;
      entity.Committee = committee;
      entity.CommitteeMemberType = committeeMemberType;

      return SaveCommitteeMember(validatonDictionary, ref entity);
    }

    public bool SaveCommitteeMember(IValidationDictionary validatonDictionary, ref CommitteeMember entity)
    {
      try
      {
        ValidateCommitteeMember(validatonDictionary, entity);

        if (validatonDictionary.IsValid)
        {
          //MIGHT NEED TO ADD CommitteeMember TO Person AND CommitteeMemberType
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

    public bool DeleteCommitteeMember(IValidationDictionary validatonDictionary, ref CommitteeMember entity)
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

    private void ValidateCommitteeMember(IValidationDictionary validatonDictionary, CommitteeMember entity)
    {
      if (entity.Person == null || entity.Person.Oid == Guid.Empty)
      {
        validatonDictionary.AddError("PersonOid", "Please specify the Person for this entity.");
      }
      if (entity.CommitteeMemberType == null || entity.CommitteeMemberType.Oid == Guid.Empty)
      {
        validatonDictionary.AddError("CommitteeMemberType", "Please specify the CommitteeMemberType for this entity.");
      }
      if (entity.Committee == null || entity.Committee.Oid == Guid.Empty)
      {
        validatonDictionary.AddError("Committee", "Please specify the Committee for this entity.");
      }

      //No Membership duplicates
      if (this.session.QueryOver<CommitteeMember>().Where(x => x.CommitteeMemberType.Oid == entity.CommitteeMemberType.Oid && x.Person.Oid == entity.Person.Oid).RowCount() > 0)
      {
        validatonDictionary.AddError("CommitteeMember", "You can not enter duplicate people in a Committee.");
      }
    }

    #endregion

    #region Committee Admin Users

    public bool SelectCommitteeAdminUser(IValidationDictionary validatonDictionary, Guid userOid, Guid committeeOid)
    {
      User user = this.session.Load<User>(userOid);
      if (user == null)
      {
        validatonDictionary.AddError("User", string.Format("Cannot locate User with Id:{0}", userOid.ToString()));
      }
      Committee committee = this.session.Load<Committee>(committeeOid);
      if (committee == null)
      {
        validatonDictionary.AddError("Committee", string.Format("Cannot locate Committee with Id:{0}", committeeOid.ToString()));
      }

      CommitteeAdmin entity = new CommitteeAdmin();
      entity.User = user;
      entity.Committee = committee;

      return SaveCommitteeAdminUser(validatonDictionary, ref entity);
    }

    public bool SaveCommitteeAdminUser(IValidationDictionary validatonDictionary, ref CommitteeAdmin entity)
    {
      try
      {
        ValidateCommitteeAdminUser(validatonDictionary, entity);

        if (validatonDictionary.IsValid)
        {
          //MIGHT NEED TO ADD CommitteeAdmin TO User AND Committee
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

    public bool DeleteCommitteeAdminUser(IValidationDictionary validatonDictionary, ref CommitteeAdmin entity)
    {
      try
      {
        if (CanUserEditCommittee(entity.Committee) == false)
        {
          validatonDictionary.AddError("Committee", "You do not have permissions to add Committee Admin users.");
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

    private void ValidateCommitteeAdminUser(IValidationDictionary validatonDictionary, CommitteeAdmin entity)
    {
      if (entity.User == null || entity.User.Oid == Guid.Empty)
      {
        validatonDictionary.AddError("User", "Please specify the User for this entity.");
      }
      if (entity.Committee == null || entity.Committee.Oid == Guid.Empty)
      {
        validatonDictionary.AddError("Committee", "Please specify the Committee for this entity.");
      }

      //Only User who is an Admin or a Committee Admin can edit the Committee
      if (CanUserEditCommittee(entity.Committee) == false)
      {
        validatonDictionary.AddError("Committee", "You do not have permissions to add Committee Admin users.");
      }

      //No duplicates
      if (entity.Oid == Guid.Empty && this.session.QueryOver<CommitteeAdmin>().Where(x => x.Committee.Oid == entity.Committee.Oid && x.User.Oid == entity.User.Oid).RowCount() > 0)
      {
        validatonDictionary.AddError("User", "You can not enter duplicate Admins for Committees.");
      }
    }

    #endregion

    #region Committee Minutes

    public bool SaveCommitteeMinute(IValidationDictionary validatonDictionary, ref ModelCommitteeMinute entity)
    {
      try
      {
        CommitteeMinute committeeMinute = new CommitteeMinute();
        if (entity.Oid != Guid.Empty)
        {
          committeeMinute = this.session.Get<CommitteeMinute>(entity.Oid);
        }

        committeeMinute.Committee = this.session.Get<Committee>(entity.CommitteeOid);
        committeeMinute.Date = entity.Date.Value;
        committeeMinute.MinutesText = HttpUtility.HtmlDecode(entity.MinutesText); ;

        ValidateCommitteeMinute(validatonDictionary, committeeMinute);

        if (validatonDictionary.IsValid)
        {
          this.session.BeginTransaction();
          this.session.SaveOrUpdate(committeeMinute);
          this.session.Transaction.Commit();


          IMapper Mapper = AutoMapperConfig.MapperConfiguration.CreateMapper();
          entity = Mapper.Map<ModelCommitteeMinute>(committeeMinute);

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

    public bool SaveCommitteeMinute(IValidationDictionary validatonDictionary, ref CommitteeMinute entity)
    {
      try
      {
        ValidateCommitteeMinute(validatonDictionary, entity);

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

    public bool DeleteCommitteeMinute(IValidationDictionary validatonDictionary, CommitteeMinute entity)
    {
      try
      {
        ValidateCommitteeMinute(validatonDictionary, entity);

        if (validatonDictionary.IsValid)
        {
          //entity.Active = false;
          //this.session.BeginTransaction();
          //this.session.SaveOrUpdate(entity);
          //this.session.Transaction.Commit();
          //return true;
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

    private void ValidateCommitteeMinute(IValidationDictionary validatonDictionary, CommitteeMinute entity)
    {
      if (string.IsNullOrEmpty(entity.MinutesText))
      {
        validatonDictionary.AddError("MinutesText", "Please add the Committee Minutes Text.");
      }

      if (CanUserEditCommittee(entity.Committee) == false)
      {
        validatonDictionary.AddError("Committee", "You do not have permissions to add Committee Minutes.");
      }
    }

    #endregion
  }
}
