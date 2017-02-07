using System;

namespace Club.Services.Models
{
  public class ModelAttachedDocument
  {
    public ModelAttachedDocument()
    {

    }

    public ModelAttachedDocument(Guid appliesToOid, Type appliesTo)
    {
      this.AppliesToOid = appliesToOid;
      this.AppliesTo = appliesTo.FullName;
    }

    public virtual Guid Oid { get; set; }

    public virtual string AppliesTo { get; set; }

    public virtual Guid AppliesToOid { get; set; }

    public virtual DateTime? DateAdded { get; set; }

    public virtual Guid FileOid { get; set; }

    public virtual string FileName { get; set; }
  }
}
