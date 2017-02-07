using Mallon.Core.Resources;
using System;

namespace Club.Domain.Models
{
  public class ModelPersonQualification
  {
    public virtual Guid Oid { get; set; }

    public virtual int Orev { get; set; }

    [System.ComponentModel.DefaultValue(true)]
    public virtual bool Active { get; set; }

    [DisplayName("Date Taken")]
    public virtual DateTime DateTaken { get; set; }

    public virtual string Result { get; set; }

    public virtual ModelPerson Person { get; set; }

    //public virtual ModelQualifcation Qualification { get; set; }

    public virtual Guid QualificationOid { get; set; }

    public virtual int QualificationOrev { get; set; }

    [DisplayName("Qualification Name")]
    public virtual string QualificationName { get; set; }

    [DisplayName("Qualification Description")]
    public virtual string QualificationDescription { get; set; }

    [DisplayName("Qualification Cost")]
    public virtual double? QualificationCost { get; set; }

  }
}
