using Club.Domain.Artifacts;
using Club.Domain.Models;
using NHibernate;
using System;

namespace Club.Domain.Extensions
{
  public static class ExtsConvertTo
  {
    public static ClubCalendar ConvertToClubCalendar(this ModelCalendarSchedulerEvent modelCalendarSchedulerEvent, ISession session)
    {
      ClubCalendar result = new ClubCalendar();
      if (modelCalendarSchedulerEvent.Oid.HasValue && modelCalendarSchedulerEvent.Oid != Guid.Empty)
      {
        result = session.Get<ClubCalendar>(modelCalendarSchedulerEvent.Oid);
      }
      if (modelCalendarSchedulerEvent.TeamOid.HasValue && modelCalendarSchedulerEvent.TeamOid != Guid.Empty)
      {
        result.Team = session.Get<Team>(modelCalendarSchedulerEvent.TeamOid);
      }
      else
      {
        result.Team = null;
      }
      result.ClubCalendarEventType = session.Get<ClubCalendarEventType>(modelCalendarSchedulerEvent.ClubCalendarEventTypeOid);

      result.Description = modelCalendarSchedulerEvent.Description;
      result.End = modelCalendarSchedulerEvent.End;
      result.IsAllDay = modelCalendarSchedulerEvent.IsAllDay;
      result.IsReminder = modelCalendarSchedulerEvent.IsReminder;
      result.RecurrenceException = modelCalendarSchedulerEvent.RecurrenceException;
      result.RecurrenceID = modelCalendarSchedulerEvent.RecurrenceID;
      result.RecurrenceRule = modelCalendarSchedulerEvent.RecurrenceRule;
      result.Start = modelCalendarSchedulerEvent.Start;
      result.Title = modelCalendarSchedulerEvent.Title;
      result.Url = modelCalendarSchedulerEvent.Url;
      return result;
    }
  }
}
