using Club.Domain.Artifacts;
using Mallon.Core.Interfaces;
using NHibernate;
using System;

namespace Club.Services.Controllers
{
  public class ClubDetailsServiceController
  {
    #region Properties

    public ISession session { get; set; }
    public IUserController userController { get; set; }

    #endregion

    public bool SaveClubDetails(IValidationDictionary validatonDictionary, ClubDetails entity)
    {
      try
      {
        ValidateClubDetails(validatonDictionary, entity);

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

    private void ValidateClubDetails(IValidationDictionary validatonDictionary, ClubDetails entity)
    {
      if (string.IsNullOrEmpty(entity.Name))
      {
        validatonDictionary.AddError("Name", "Please specify the ClubDetails Name.");
      }
    }
  }
}
