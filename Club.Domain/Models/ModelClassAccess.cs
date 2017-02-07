using Mallon.Core.Artifacts;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Club.Domain.Models
{
  public class ModelClassAccess
  {
    public Guid Oid { get; set; }
    public string Path { get; set; }
    public string ParentPath { get; set; }
    public bool HasChildren { get; set; }

    public Access Access { get; set; }
    public string Name { get; set; }

    public bool Checked { get; set; }
    public string Assembly { get; set; }

    public string Text { get; set; }
    public string Value { get; set; }

    public static ModelClassAccess Instance(ClassAccess src)
    {
      return new ModelClassAccess
      {
        Oid = src.Oid,
        Access = src.Access,
        Name = src.ClassType == null ? null : src.ClassType.Name,
        Path = src.ClassType == null ? null : src.ClassType.FullName,
        ParentPath = src.ClassType == null ? null : src.ClassType.FullName.Remove(src.ClassType.FullName.LastIndexOf('.')),
        Checked = src.Access != Access.None,
        Assembly = GetAssembly(src),
        Value = src.ClassType == null ? null : src.ClassType.FullName,
        Text = src.ClassType == null ? null : src.ClassType.Name,
      };
    }

    private static string GetAssembly(ClassAccess src)
    {
      string assembly = "";
      if (src.ClassType != null)
      {
        assembly = src.ClassType.AssemblyQualifiedName;
      }
      return assembly;
    }

    public static IEnumerable<ModelClassAccess> Collection(IEnumerable<ClassAccess> accessLevels)
    {
      IList<ModelClassAccess> result = accessLevels.Select(x => Instance(x)).ToList();
      foreach (ClassAccess ca in accessLevels)
      {
        string path = ca.ClassType.FullName;
        path = path.Remove(path.LastIndexOf('.'));
        while (!String.IsNullOrEmpty(path))
        {
          int dotPos = path.LastIndexOf('.');
          string parentPath = dotPos >= 0 ? path.Remove(dotPos) : null;
          if (!result.Any(x => x.Path == path))
          {
            result.Add(new ModelClassAccess
            {
              Path = path,
              ParentPath = parentPath,
              HasChildren = true,
              Name = path.Remove(0, dotPos + 1)
            });
          }
          path = parentPath;
        }
      }

      return result;
    }
  }
}
