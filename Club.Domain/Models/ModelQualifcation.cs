using Mallon.Core.Resources;
using System;
using System.ComponentModel.DataAnnotations;
namespace Club.Domain.Models
{
  public class ModelQualifcation
  {
    public virtual Guid Oid { get; set; }

    public virtual int Orev { get; set; }

    [DisplayName("Qualification Name")]
    public virtual string Name { get; set; }

    [StringLength(1000)]
    [DisplayName("Qualification Description")]
    public virtual string Description { get; set; }

    [DisplayName("Qualification Cost")]
    public virtual double? Cost { get; set; }

  }
}
