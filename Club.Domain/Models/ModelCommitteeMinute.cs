using Club.Domain.Artifacts;
using Mallon.Core.Resources;
using System;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Club.Domain.Models
{
  public class ModelCommitteeMinute
  {

    public ModelCommitteeMinute()
    {
      this.Date = DateTime.Now.Date;
    }

    public ModelCommitteeMinute(Committee committee)
    {
      this.Date = DateTime.Now.Date;
      this.CommitteeOid = committee.Oid;
      this.CommitteeNameSingleLine = committee.NameSingleLine;
      this.MinutesText = committee.GetCommitteeMinutesHtmlTemplate();
    }

    public virtual Guid Oid { get; set; }

    public virtual int Orev { get; set; }

    [DisplayName("Date")]
    [Required]
    public virtual DateTime? Date { get; set; }

    [StringLength(short.MaxValue)]
    [Required]
    [AllowHtml]
    public virtual string MinutesText { get; set; }

    [Required]
    public virtual Guid CommitteeOid { get; set; }

    [DisplayName("Committee Name")]
    public virtual string CommitteeNameSingleLine { get; set; }
  }
}
