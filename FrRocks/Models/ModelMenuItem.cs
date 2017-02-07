using System;

namespace FrRocks.Models
{
  public class ModelMenuItem
  {
    public ModelMenuItem()
    {
    }

    public virtual Guid Oid { get; set; }

    public virtual Guid ParentOid { get; set; }

    /// <summary>
    /// This is the Menu text value
    /// </summary>
    public virtual string Text { get; set; }

    public virtual string Category { get; set; }

    public virtual string Action { get; set; }

    public virtual string Controller { get; set; }

    public virtual string ParentCategory { get; set; }
  }
}