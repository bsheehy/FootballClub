using Mallon.Core.Resources;
using System;
using System.ComponentModel.DataAnnotations;

namespace Club.Domain.Models
{
  public class ModelCommitteeMember
  {
    public virtual Guid Oid { get; set; }

    public virtual int Orev { get; set; }

    [Required]
    public virtual Guid CommitteeMemberTypeOid { get; set; }

    [DisplayName("Member Type")]
    public virtual string CommitteeMemberTypeName { get; set; }

    [Required]
    public virtual Guid CommitteeOid { get; set; }

    public virtual string CommitteeName { get; set; }

    public virtual int? CommitteeYear { get; set; }

    public virtual string CommitteeNameSingleLine { get; set; }

    [Required]
    public virtual Guid PersonOid { get; set; }

    public virtual string PersonForename { get; set; }

    public virtual string PersonSurname { get; set; }

    [DisplayName("DOB")]
    public virtual DateTime? PersonDob { get; set; }

    public virtual int? PersonAge { get; set; }

    public virtual string PersonPhone { get; set; }

    public virtual string PersonMobileNo { get; set; }

    public virtual string PersonEmail { get; set; }

    public virtual string PersonComments { get; set; }

    [DisplayName("Name")]
    public virtual string PersonNameSingleLine { get; set; }

    /// <remarks>
    /// Returns a single line representaion of address.
    /// </remarks>
    /// <returns></returns>
    [DisplayName("Address Details")]
    public virtual string PersonAddressSingleLine { get; set; }

    [DisplayName("Address Details")]
    public virtual string PersonAddressAllDetails { get; set; }

  }
}
