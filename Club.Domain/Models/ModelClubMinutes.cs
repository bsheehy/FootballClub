using Mallon.Core.Resources;
using System;
using System.ComponentModel.DataAnnotations;

namespace Club.Domain.Models
{
  public class ModelClubMinutes
  {
    public virtual Guid Oid { get; set; }

    public virtual int Orev { get; set; }

    [DisplayName("Date")]
    public virtual DateTime Date { get; set; }

    [StringLength(short.MaxValue)]
    public virtual string MinutesText { get; set; }

    public virtual ModelCommittee Committee { get; set; }

  }
}
