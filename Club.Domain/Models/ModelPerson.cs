using Mallon.Core.Resources;
using System;
using System.ComponentModel.DataAnnotations;

namespace Club.Domain.Models
{
  public class ModelPerson
  {
    public virtual Guid Oid { get; set; }

    public virtual int Orev { get; set; }

    public virtual string TitleOid { get; set; }

    public virtual string TitleDescription { get; set; }

    [Required]
    public virtual string Forename { get; set; }

    [Required]
    public virtual string Surname { get; set; }

    [DisplayName("DOB")]
    public virtual DateTime? Dob { get; set; }

    public virtual int? Age { get; set; }

    public virtual string Phone { get; set; }

    public virtual string MobileNo { get; set; }

    public virtual string Email { get; set; }

    public virtual string Comments { get; set; }

    [DisplayName("Name")]
    public virtual string NameSingleLine { get; set; }

    [DisplayName("Irish Name")]
    public virtual string IrishName { get; set; }

    /// <remarks>
    /// Returns a single line representaion of Person.
    /// </remarks>
    /// <returns></returns>
    [DisplayName("Person Details")]
    public virtual string SingleLine { get; set; }

    [StringLength(200)]
    [DisplayName("Name or Number")]
    public virtual string AddressNumber { get; set; }

    [StringLength(200)]
    [DisplayName("Street")]
    public virtual string AddressStreet { get; set; }

    [StringLength(200)]
    [DisplayName("Town")]
    public virtual string AddressTown { get; set; }

    [StringLength(200)]
    [DisplayName("Locality")]
    public virtual string AddressCounty { get; set; }

    [StringLength(200)]
    [DisplayName("County")]
    public virtual string AddressCountry { get; set; }

    [DisplayName("Post Code")]
    public virtual string AddressPostCode { get; set; }

    /// <summary>
    /// Stores the X coordinate.
    /// </summary>
    [DisplayName("X Longitude")]
    public virtual double? AddressXlngCoord { get; set; }

    /// <summary>
    /// Stores the Y coordinate.
    /// </summary>
    [DisplayName("Y Latitude")]
    public virtual double? AddressYlatCoord { get; set; }

    /// <remarks>
    /// Returns a single line representaion of address.
    /// </remarks>
    /// <returns></returns>
    [DisplayName("Address Details")]
    public virtual string AddressSingleLine { get; set; }

    [DisplayName("Address Details")]
    public virtual string AddressAllDetails { get; set; }

    public virtual bool HasDirectDebits { get; set; }
  }
}
