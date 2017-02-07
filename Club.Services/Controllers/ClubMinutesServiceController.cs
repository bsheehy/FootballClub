using Club.Domain;
using Club.Domain.Artifacts;
using Mallon.Core.Interfaces;
using NHibernate;
using System;
using System.Linq;

namespace Club.Services.Controllers
{
  public class ClubMinutesServiceController
  {
    #region Properties

    public ISession session { get; set; }
    public IUserController userController { get; set; }

    #endregion

    public bool SaveClubMinutes(IValidationDictionary validatonDictionary, CommitteeMinute entity)
    {
      try
      {
        ValidateClubMinutes(validatonDictionary, entity);

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
    public bool DeleteClubMinutes(IValidationDictionary validatonDictionary, CommitteeMinute entity)
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

    private void ValidateClubMinutes(IValidationDictionary validatonDictionary, CommitteeMinute entity)
    {
      if (string.IsNullOrEmpty(entity.MinutesText))
      {
        validatonDictionary.AddError("MinutesText", "Please add the Committee Minutes Text.");
      }
    }

    /// <summary>
    /// Only members of the CommitteAdmins or an administrator can CREATE or UPDATE minutes for the committee
    /// </summary>
    /// <param name="committee"></param>
    /// <returns></returns>
    public bool CanUserEditClubMinutes(Committee committee)
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

    public bool CanUserEditClubMinutes(Guid committeeOid)
    {
      Committee team = this.session.Get<Committee>(committeeOid);
      return CanUserEditClubMinutes(team);
    }
  }
}
