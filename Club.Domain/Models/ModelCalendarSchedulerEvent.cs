using Club.Resources.DataAttributes;
using Kendo.Mvc.UI;
using Mallon.Core.Resources;
using System;
using System.ComponentModel.DataAnnotations;

namespace Club.Domain.Models
{
  public class ModelCalendarSchedulerEvent : ISchedulerEvent
  {

    public ModelCalendarSchedulerEvent()
    {
      this.IsReminder = true;
    }

    /// <summary>
    /// Oid of the ClubCalendar item
    /// </summary>
    public Guid? Oid { get; set; }

    [Required]
    [DisplayName("Event Type")]
    public Guid ClubCalendarEventTypeOid { get; set; }

    [DisplayName("Event Name")]
    public string ClubCalendarEventTypeName { get; set; }

    /// <summary>
    /// The color of the item
    /// </summary>
    public string ClubCalendarEventTypeColorHex { get; set; }

    [DisplayName("Team")]
    public Guid? TeamOid { get; set; }

    [DisplayName("Team Name")]
    public string TeamName { get; set; }

    /// <summary>
    /// Set this to true if the user added this Calendar entry - these will never have Assignemnts 
    /// attached to them. i.e. they will be reminders created and configured by users.
    /// </summary>
    public bool IsReminder { get; set; }

    public virtual string RecurrenceID { get; set; }

    [Required]
    public string Title { get; set; }

    public string Description { get; set; }

    public string Url { get; set; }

    [DateGreaterThan(OtherField = "Start")]
    public DateTime End { get; set; }

    public string EndTimezone { get; set; }

    public bool IsAllDay { get; set; }

    public string RecurrenceException { get; set; }

    public string RecurrenceRule { get; set; }

    public DateTime Start { get; set; }

    public string StartTimezone { get; set; }

  }
}