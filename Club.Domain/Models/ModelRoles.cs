using Mallon.Core.Artifacts;
using Mallon.Core.Resources;
using System;
using System.Collections.Generic;
using System.Linq;


namespace Club.Domain.Models
{
  public class ModelRoles
  {
    public ModelRoles()
    {
      //this.Access= new List<Access>();
    }

    [DisplayName("Oid")]
    public Guid Oid { get; set; }

    [DisplayName("Name")]
    public string Name { get; set; }

    [DisplayName("Description")]
    public string Description { get; set; }

    [DisplayName("ClassName")]
    public string ClassName { get; set; }

    public static IEnumerable<ModelRoles> Collection(IEnumerable<Role> src)
    {
      return src.Select(x => Instance(x));
    }

    public static ModelRoles Instance(Role src)
    {
      return new ModelRoles
      {
        Oid = src.Oid,
        Name = src.Name,
        Description = src.Description,
      };
    }
  }
}