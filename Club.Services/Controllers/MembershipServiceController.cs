using Club.Domain;
using Club.Domain.Artifacts;
using Mallon.Core.Interfaces;
using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;


namespace Club.Services.Controllers
{
  public class MembershipServiceController
  {
    #region Properties

    public ISession session { get; set; }
    public IUserController userController { get; set; }

    #endregion

    public bool SaveMembershipType(IValidationDictionary validatonDictionary, MembershipType entity)
    {
      try
      {
        ValidateMembershipType(validatonDictionary, entity);

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
    public bool DeleteMembership(IValidationDictionary validatonDictionary, MembershipType entity)
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

    private void ValidateMembershipType(IValidationDictionary validatonDictionary, MembershipType entity)
    {
      if (string.IsNullOrEmpty(entity.Name))
      {
        validatonDictionary.AddError("Name", "Please specify the MembershipType Name.");
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
          validatonDictionary.AddError("Year", "Please enter a valid Year for this membership (greater than 1800).");
        }
      }
    }

    public IEnumerable<PersonMembershipType> PersonMembershipsGet(IValidationDictionary validatonDictionary, Guid personOid, bool? isActive = null)
    {

      IEnumerable<PersonMembershipType> entities = getPersonMemberships(validatonDictionary, personOid);
      return entities;
      //return ViewGridFireCertFee.GetMapping(entities);
    }

    private IEnumerable<PersonMembershipType> getPersonMemberships(IValidationDictionary validatonDictionary, Guid personOid, bool? isActive = null)
    {
      if (isActive.HasValue)
      {
        return this.session.QueryOver<PersonMembershipType>().List<PersonMembershipType>().Where(x => x.Person.Oid == personOid && x.Active == isActive.Value).OrderBy(x => x.MembershipType.StartDate);
      }
      else
      {
        return this.session.QueryOver<PersonMembershipType>().List<PersonMembershipType>().Where(x => x.Person.Oid == personOid).OrderBy(x => x.MembershipType.StartDate);
      }
    }

    public bool PersonMembershipSave(IValidationDictionary validatonDictionary, ref PersonMembershipType entity)
    {
      try
      {
        ValidatePersonMembership(validatonDictionary, entity);

        if (validatonDictionary.IsValid)
        {
          //MIGHT NEED TO ADD PersonMembershipType TO Person AND MembershipType
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
    /// Person must be a member of Committe Role or higher to delete Person Membership
    /// </summary>
    /// <param name="validatonDictionary"></param>
    /// <param name="entity"></param>
    /// <returns></returns>
    public bool PersonMembershipDelete(IValidationDictionary validatonDictionary, ref PersonMembershipType entity)
    {
      try
      {
        if (userController.LoggedOnUser.HasRole(userController.LoggedOnUser, Constants.CommitteOrAboveRoleNames) == false)
        {
          validatonDictionary.AddError("Permissions", "User must be in Committee Role or higher to delete Membership Details.");
        }
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

    private void ValidatePersonMembership(IValidationDictionary validatonDictionary, PersonMembershipType entity)
    {
      if (entity.Person == null || entity.Person.Oid == Guid.Empty)
      {
        validatonDictionary.AddError("PersonOid", "Please specify the Person for this entity.");
      }
      if (entity.MembershipType == null || entity.MembershipType.Oid == Guid.Empty)
      {
        validatonDictionary.AddError("MembershipOid", "Please specify the MembershipType for this entity.");
      }

      //No Membership duplicates
      if (entity.Oid == Guid.Empty && this.session.QueryOver<PersonMembershipType>().Where(x => x.MembershipType.Oid == entity.MembershipType.Oid && x.Person.Oid == entity.Person.Oid).RowCount() > 0)
      {
        validatonDictionary.AddError("MembershipOid", "You can not enter duplicate memberships.");
      }

      if (userController.LoggedOnUser.HasRole(userController.LoggedOnUser, Constants.CommitteOrAboveRoleNames) == false)
      {
        validatonDictionary.AddError("Permissions", "User must be in Committee Role or higher to delete Membership Details.");
      }
      //No Year membership duplicates
    }

    /// <summary>
    /// You must be a member sof the Administrator group or the Committee Group to add a persons Membership 
    /// </summary>
    /// <returns></returns>
    public bool CanUserEditMembership()
    {
      if (userController.LoggedOnUser.HasRole(userController.LoggedOnUser, Constants.CommitteOrAboveRoleNames))
      {
        return true;
      }
      return false;
    }

    public bool SelectMembershipMember(IValidationDictionary validatonDictionary, Guid personOid, Guid membershipTypeOid)
    {
      Person person = this.session.Load<Person>(personOid);
      if (person == null)
      {
        validatonDictionary.AddError("Person", string.Format("Cannot locate Person with Id:{0}", personOid.ToString()));
      }
      MembershipType membershipType = this.session.Load<MembershipType>(membershipTypeOid);
      if (membershipType == null)
      {
        validatonDictionary.AddError("membershipType", string.Format("Cannot locate membershipType with Id:{0}", membershipTypeOid.ToString()));
      }


      PersonMembershipType entity = new PersonMembershipType();
      entity.Person = person;
      entity.MembershipType = membershipType;
      entity.Date = DateTime.Now;

      return SaveMembershipTypeMember(validatonDictionary, ref entity);
    }

    public bool SaveMembershipTypeMember(IValidationDictionary validatonDictionary, ref PersonMembershipType entity)
    {
      try
      {
        ValidateMembershipTypeMember(validatonDictionary, entity);

        if (validatonDictionary.IsValid)
        {
          //MIGHT NEED TO ADD membershipTypeMember TO Person AND membershipType
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

    public bool DeletemembershipTypeMember(IValidationDictionary validatonDictionary, ref PersonMembershipType entity)
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

    private void ValidateMembershipTypeMember(IValidationDictionary validatonDictionary, PersonMembershipType entity)
    {
      if (entity.Person == null || entity.Person.Oid == Guid.Empty)
      {
        validatonDictionary.AddError("Person", "Please specify the Person for this entity.");
      }
      if (entity.MembershipType == null || entity.MembershipType.Oid == Guid.Empty)
      {
        validatonDictionary.AddError("membershipType", "Please specify the membershipType for this entity.");
      }

      //No duplicates
      if (entity.Oid == Guid.Empty && this.session.QueryOver<PersonMembershipType>().Where(x => x.MembershipType.Oid == entity.MembershipType.Oid && x.Person.Oid == entity.Person.Oid).RowCount() > 0)
      {
        validatonDictionary.AddError("Person", "You can not enter duplicate members for a Membership Type.");
      }
    }

  }
}
