using Club.Domain;
using Club.Domain.Artifacts;
using Mallon.Core.Interfaces;
using NHibernate;
using System;

namespace Club.Services.Controllers
{
  public class QualificationServiceController
  {
    #region Properties

    public ISession session { get; set; }
    public IUserController userController { get; set; }

    #endregion

    public bool SaveQualification(IValidationDictionary validatonDictionary, Qualification entity)
    {
      try
      {
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
    public bool DeleteQualification(IValidationDictionary validatonDictionary, Qualification entity)
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

    public bool CanUserEditQualification()
    {
      if (userController.LoggedOnUser.HasRole(userController.LoggedOnUser, Constants.AdministrationRoleOids))
      {
        return true;
      }

      return false;
    }


    #region Qualification Members

    public bool SelectPersonQualification(IValidationDictionary validatonDictionary, Guid personOid, Guid qualificationOid)
    {
      Person person = this.session.Load<Person>(personOid);
      if (person == null)
      {
        validatonDictionary.AddError("Person", string.Format("Cannot locate Person with Id:{0}", personOid.ToString()));
      }
      Qualification qualification = this.session.Load<Qualification>(qualificationOid);
      if (qualification == null)
      {
        validatonDictionary.AddError("Qualification", string.Format("Cannot locate Qualification with Id:{0}", qualificationOid.ToString()));
      }

      PersonQualification entity = new PersonQualification();
      entity.Person = person;
      entity.Qualification = qualification;

      return SavePersonQualification(validatonDictionary, ref entity);
    }

    public bool SavePersonQualification(IValidationDictionary validatonDictionary, ref PersonQualification entity)
    {
      try
      {
        ValidatePersonQualification(validatonDictionary, entity);

        if (validatonDictionary.IsValid)
        {
          //MIGHT NEED TO ADD PersonQualification TO Person AND Qualification
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

    public bool DeletePersonQualification(IValidationDictionary validatonDictionary, ref PersonQualification entity)
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

    private void ValidatePersonQualification(IValidationDictionary validatonDictionary, PersonQualification entity)
    {
      if (entity.Person == null || entity.Person.Oid == Guid.Empty)
      {
        validatonDictionary.AddError("Person", "Please specify the Person for this entity.");
      }

      if (entity.Qualification == null || entity.Qualification.Oid == Guid.Empty)
      {
        validatonDictionary.AddError("Qualification", "Please specify the Qualification for this entity.");
      }

      //No duplicates
      if (entity.Oid == Guid.Empty && this.session.QueryOver<PersonQualification>().Where(x => x.Qualification.Oid == entity.Qualification.Oid && x.Person.Oid == entity.Person.Oid).RowCount() > 0)
      {
        validatonDictionary.AddError("Person", "You can not enter duplicate Qualifications for People.");
      }
    }

    #endregion
  }
}
