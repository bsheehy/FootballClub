using System;

namespace Club.Domain.Models
{
  public class ModelTeam
  {
    public virtual Guid Oid { get; set; }

    public virtual int Orev { get; set; }

    public virtual string Name { get; set; }

    public virtual int? Year { get; set; }

    public virtual string NameSingleLine { get; set; }

    public virtual enumSex Sex { get; set; }
  }
}
