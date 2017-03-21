using Club.Domain.Artifacts;
using Mallon.Core.Resources;
using System;
namespace Club.Domain.Models
{
  public class ModelPersonMembershipType
  {

    public ModelPersonMembershipType()
    {

    }

    public ModelPersonMembershipType(MembershipType membershipType)
    {
      this.Date = DateTime.Now.Date;
      this.MembershipTypeOid = membershipType.Oid;
      this.MembershipTypeName = membershipType.NameSingleLine;
      this.Year = membershipType.Year;
    }

    public virtual Guid Oid { get; set; }

    public virtual DateTime? Date { get; set; }

    public virtual bool Active { get; set; }

    #region Person Details

    public virtual Guid PersonOid { get; set; }

    public virtual string PersonNameSingleLine { get; set; }

    public virtual string PersonForename { get; set; }

    public virtual string PersonSurname { get; set; }

    [DisplayName("Address Details")]
    public virtual string PersonAddressSingleLine { get; set; }

    #endregion

    #region MembershipType Details
    public virtual Guid MembershipTypeOid { get; set; }

    public virtual bool MembershipTypeActive { get; set; }

    public virtual string MembershipTypeName { get; set; }

    public virtual int Year { get; set; }

    public virtual DateTime? MembershipTypeStartDate { get; set; }

    public virtual DateTime? MembershipTypeEndDate { get; set; }

    #endregion
  }
}
