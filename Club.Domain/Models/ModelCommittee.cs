using System;

namespace Club.Domain.Models
{
  public class ModelCommittee
  {
    public virtual Guid Oid { get; set; }

    public virtual int Orev { get; set; }

    public virtual string Name { get; set; }

    public virtual int? Year { get; set; }

    public virtual string NameSingleLine { get; set; }

  }
}
