using Mallon.Core.Resources;
using System;
using System.ComponentModel.DataAnnotations;

namespace Club.Domain.Models
{
  public class ModelPersonGuardian
  {
    public virtual Guid Oid { get; set; }

    public virtual int Orev { get; set; }

    [Required]
    public virtual Guid PersonOid { get; set; }

    [DisplayName("Person Phone")]
    public virtual string PersonPhone { get; set; }

    [DisplayName("Person Mobile")]
    public virtual string PersonMobileNo { get; set; }

    [DisplayName("Person Name")]
    public virtual string PersonNameSingleLine { get; set; }

    /// <remarks>
    /// Returns a single line representaion of Person.
    /// </remarks>
    /// <returns></returns>
    [DisplayName("Person Details")]
    public virtual string PersonSingleLine { get; set; }

    /// <remarks>
    /// Returns a single line representaion of address.
    /// </remarks>
    /// <returns></returns>
    [DisplayName("Person Address")]
    public virtual string PersonAddressSingleLine { get; set; }

    [DisplayName("DOB")]
    public virtual DateTime? PersonDob { get; set; }

    public virtual int? PersonAge { get; set; }

    [Required]
    public virtual Guid GuardianOid { get; set; }

    [DisplayName("Guardian Phone")]
    public virtual string GuardianPhone { get; set; }

    [DisplayName("Guardian Mobile")]
    public virtual string GuardianMobileNo { get; set; }

    [DisplayName("Guardian Name")]
    public virtual string GuardianNameSingleLine { get; set; }

    /// <remarks>
    /// Returns a single line representaion of Person.
    /// </remarks>
    /// <returns></returns>
    [DisplayName("Guardian Details")]
    public virtual string GuardianSingleLine { get; set; }

    /// <remarks>
    /// Returns a single line representaion of address.
    /// </remarks>
    /// <returns></returns>
    [DisplayName("Guardian Address")]
    public virtual string GuardianAddressSingleLine { get; set; }

  }
}
