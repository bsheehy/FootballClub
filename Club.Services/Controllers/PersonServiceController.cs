using Club.Domain.Artifacts;
using Mallon.Core.Interfaces;
using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Club.Services.Controllers
{
  public class PersonServiceController
  {
    #region Properties

    public ISession session { get; set; }
    public IUserController userController { get; set; }

    #endregion

    //public IEnumerable<Person> GetPersons(IValidationDictionary validatonDictionary, Guid personOid, bool? isActive = null)
    //{
    //  IEnumerable<Person> entities = getPersons(validatonDictionary, personOid, isActive);
    //  return entities;
    //  //return ViewGridFireCertFee.GetMapping(entities);
    //}

    //private IEnumerable<Person> getPersons(IValidationDictionary validatonDictionary, Guid personOid, bool? isActive = null)
    //{
    //  if (isActive.HasValue)
    //  {
    //    return this.session.QueryOver<Person>().List<Person>().Where(x => x.Person.Oid == personOid && x.Active == isActive.Value);
    //  }
    //  else
    //  {
    //    return this.session.QueryOver<Person>().List<Person>().Where(x => x.Person.Oid == personOid);
    //  }
    //}

    public bool SavePerson(IValidationDictionary validatonDictionary, Person entity)
    {
      try
      {
        ValidatePerson(validatonDictionary, entity);

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
    public bool DeletePerson(IValidationDictionary validatonDictionary, Person entity)
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


    #region Person Guardians

    public IEnumerable<PersonGuardian> GetPersonGuardians(IValidationDictionary validatonDictionary, Guid personOid)
    {
      IEnumerable<PersonGuardian> entities = this.session.QueryOver<PersonGuardian>().List<PersonGuardian>().Where(x => x.Person.Oid == personOid);
      return entities;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="validatonDictionary"></param>
    /// <param name="entity"></param>
    /// <returns></returns>
    public bool DeletePersonGuardian(IValidationDictionary validatonDictionary, ref PersonGuardian entity)
    {
      try
      {
        this.session.BeginTransaction();
        this.session.Delete(entity);
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

    public bool SavePersonGuardian(IValidationDictionary validatonDictionary, Guid personOid, Guid personGuardianOid)
    {
      Person person = this.session.Load<Person>(personOid);
      if (person == null)
      {
        validatonDictionary.AddError("Person", string.Format("Cannot locate Person with Id:{0}", personOid.ToString()));
      }
      Person guardain = this.session.Load<Person>(personGuardianOid);
      if (guardain == null)
      {
        validatonDictionary.AddError("Guardain", string.Format("Cannot locate Guardian Person with Id:{0}", personGuardianOid.ToString()));
      }

      if (validatonDictionary.IsValid)
      {
        PersonGuardian model = new PersonGuardian();
        model.Person = person;
        model.Guardian = guardain;
        session.SaveOrUpdate(model);
        session.Flush();
        return true;
      }
      else
      {
        return false;
      }
    }

    #endregion

    #region Person Qualifications

    public IEnumerable<PersonQualification> GetQualifications(IValidationDictionary validatonDictionary, Guid personOid, bool? isActive = null)
    {
      IEnumerable<PersonQualification> entities = GetPersonQualifications(validatonDictionary, personOid);
      return entities;
    }

    private IEnumerable<PersonQualification> GetPersonQualifications(IValidationDictionary validatonDictionary, Guid personOid, bool? isActive = null)
    {
      if (isActive.HasValue)
      {
        return this.session.QueryOver<PersonQualification>().List<PersonQualification>().Where(x => x.Person.Oid == personOid && x.Active == isActive.Value).OrderBy(x => x.Qualification.Name);
      }
      else
      {
        return this.session.QueryOver<PersonQualification>().List<PersonQualification>().Where(x => x.Person.Oid == personOid).OrderBy(x => x.Qualification.Name);
      }
    }

    public bool SavePersonQualification(IValidationDictionary validatonDictionary, ref PersonQualification entity)
    {
      try
      {
        ValidatePersonQualification(validatonDictionary, entity);

        if (validatonDictionary.IsValid)
        {
          //MIGHT NEED TO ADD PersonQualification TO Person AND QualificationType
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
        entity.Active = false;
        if (validatonDictionary.IsValid)
        {
          this.session.BeginTransaction();
          //this.session.SaveOrUpdate(entity);
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

      //No Qualification duplicates (YES YOU CAN!!)
      //if (this.session.QueryOver<PersonQualificationType>().Where(x => x.QualificationType.Oid == entity.QualificationType.Oid && x.Person.Oid == entity.Person.Oid).RowCount() > 0)
      //{
      //  validatonDictionary.AddError("QualificationOid", "You can not enter duplicate Qualifications.");
      //}
    }
    #endregion


    private void ValidatePerson(IValidationDictionary validatonDictionary, Person entity)
    {
      if (string.IsNullOrEmpty(entity.Forename))
      {
        validatonDictionary.AddError("Forename", "Please specify the Forename.");
      }
      if (string.IsNullOrEmpty(entity.Surname))
      {
        validatonDictionary.AddError("Surname", "Please specify the Surname.");
      }
    }
  }
}
