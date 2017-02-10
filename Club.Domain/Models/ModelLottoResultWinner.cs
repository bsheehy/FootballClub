using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Club.Domain.Models
{
  public class ModelLottoResultWinner
  {
    public virtual Guid Oid { get; set; }

    public virtual int Orev { get; set; }

    public virtual Guid PersonOid { get; set; }

    [DisplayName("Forename")]
    public virtual string PersonForename { get; set; }

    [DisplayName("Surname")]
    public virtual string PersonSurname { get; set; }

    public virtual string PersonPhone { get; set; }

    public virtual string PersonMobileNo { get; set; }

    public virtual string PersonComments { get; set; }

    [DisplayName("Person Name")]
    public virtual string PersonNameSingleLine { get; set; }

    [DisplayName("Person Address")]
    public virtual string PersonAddressSingleLine { get; set; }

    /// <remarks>
    /// Returns a single line representaion of Person.
    /// </remarks>
    /// <returns></returns>
    [DisplayName("Person Details")]
    public virtual string SingleLine { get; set; }

    //[Required]
    //public virtual ModelPerson Person { get; set; }

    [Required]
    public virtual int No1 { get; set; }

    [Required]
    public virtual int No2 { get; set; }

    [Required]
    public virtual int No3 { get; set; }

    [Required]
    public virtual int No4 { get; set; }

    [DefaultValue(true)]
    public virtual bool Active { get; set; }

    [Required]
    public virtual DateTime StartDate { get; set; }

    public virtual DateTime? EndDate { get; set; }

    public virtual int Matches { get; set; }

    public virtual string Message { get; set; }
  }
}
