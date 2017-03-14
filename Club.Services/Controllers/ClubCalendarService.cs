using AutoMapper;
using Club.Domain.Artifacts;
using Club.Domain.Extensions;
using Club.Domain.Models;
using Club.Services.Utils;
using Mallon.Core.Interfaces;
using NHibernate;
using System;

namespace Club.Services.Controllers
{
  public class ClubCalendarService
  {
    #region Properties

    public ISession session { get; set; }
    public IUserController userController { get; set; }

    #endregion

    public bool SaveClubCalendar(IValidationDictionary validatonDictionary, ref ModelCalendarSchedulerEvent entity)
    {
      ClubCalendar clubCalendar = entity.ConvertToClubCalendar(this.session);
      bool result = SaveClubCalendar(validatonDictionary, ref clubCalendar);
      if (result == true)
      {
        IMapper Mapper = AutoMapperConfig.MapperConfiguration.CreateMapper();
        entity = Mapper.Map<ModelCalendarSchedulerEvent>(clubCalendar);
      }
      return result;
    }

    public bool SaveClubCalendar(IValidationDictionary validatonDictionary, ref ClubCalendar entity)
    {
      try
      {
        ValidateClubCalendar(validatonDictionary, entity);

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

    public bool DeleteClubCalendar(IValidationDictionary validatonDictionary, Guid clubCalendarOid)
    {
      try
      {
        ClubCalendar clubCalendar = this.session.Get<ClubCalendar>(clubCalendarOid);
        this.session.BeginTransaction();
        this.session.Delete(clubCalendar);
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

    private void ValidateClubCalendar(IValidationDictionary validatonDictionary, ClubCalendar entity)
    {

      if (CanUserEditClubCalendar() == false)
      {
        validatonDictionary.AddError("Permissions", "Only an Administrator can edit or add items to the Club Calendar.");
      }

      if (entity.ClubCalendarEventType == null || entity.ClubCalendarEventType.Oid == Guid.Empty)
      {
        validatonDictionary.AddError("ClubCalendarEventType", "Please specify the Event Type.");
      }

      if (string.IsNullOrEmpty(entity.Title))
      {
        validatonDictionary.AddError("Title", "Please add the Title.");
        //string title = entity.ClubCalendarEventType.Name;
        //if (entity.Team != null && entity.Team.Oid != Guid.Empty)
        //{
        //  title += " " + entity.Team.NameSingleLine;
        //}
      }
    }

    /// <summary>
    /// User can edit and add ClubCalendar items if they are member of Administrator.
    /// </summary>
    /// <returns></returns>
    public bool CanUserEditClubCalendar()
    {
      return Utility.UserIsAdmin(this.userController);
    }
  }
}
