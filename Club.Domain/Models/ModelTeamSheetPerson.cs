using Mallon.Core.Resources;
using System;
namespace Club.Domain.Models
{
  public class ModelTeamSheetPerson
  {
    public virtual Guid Oid { get; set; }

    public virtual Guid TeamSheetOid { get; set; }

    public virtual Guid TeamSheetTeamOid { get; set; }

    public virtual string TeamSheetTeamName { get; set; }

    public virtual int? TeamSheetTeamYear { get; set; }

    public virtual string TeamSheetTeamNameSingleLine { get; set; }

    public virtual Guid PersonOid { get; set; }

    [DisplayName("DOB")]
    public virtual DateTime? PersonDob { get; set; }

    public virtual int? PersonAge { get; set; }

    public virtual string PersonPhone { get; set; }

    public virtual string PersonMobileNo { get; set; }

    public virtual string PersonEmail { get; set; }

    [DisplayName("Name")]
    public virtual string PersonNameSingleLine { get; set; }

    [DisplayName("Irish Name")]
    public virtual string PersonIrishName { get; set; }

    public virtual Guid? TeamPositionOid { get; set; }

    [DisplayName("Position Name")]
    public virtual string TeamPositionName { get; set; }

    [DisplayName("Position Number")]
    public virtual int? TeamPositionNumber { get; set; }

    [DisplayName("Notes")]
    public virtual string Notes { get; set; }
  }
}
