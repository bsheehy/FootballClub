using Mallon.Core.Artifacts;
using Mallon.Core.Interfaces;
using NHibernate;
using System;

namespace Club.Services.Controllers
{
  public class AdminServiceController
  {
    public bool Save(Role m, ISession session, IValidationDictionary validatonDictionary)
    {
      try
      {
        session.BeginTransaction();
        session.SaveOrUpdate(m);
        session.Flush();
        session.Transaction.Commit();
        return true;
      }
      catch (Exception ex)
      {
        if (session.Transaction.IsActive)
        {
          session.Transaction.Rollback();
        }
        validatonDictionary.AddError("Error:", ex.Message);

        throw;
      }
    }

  }
}
