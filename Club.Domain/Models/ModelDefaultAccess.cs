using Mallon.Core.Artifacts;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Club.Domain.Models
{
  public class ModelDefaultAccess
  {

    public Guid Oid { get; set; }
    public Access Access { get; set; }
    public string Name { get; set; }
    public bool Check { get; set; }

    public static ModelDefaultAccess Instance(Access access, Access defaultAccess)
    {
      return new ModelDefaultAccess
      {
        Access = access,
        Name = Enum.GetName(typeof(Access), access),
        Check = defaultAccess.HasFlag(access) ? true : false
      };
    }

    public static IEnumerable<ModelDefaultAccess> Collection(IEnumerable<Access> accessLevels, Access defaultAccess)
    {
      return accessLevels.Select(x => Instance(x, defaultAccess));
    }
  }
}
