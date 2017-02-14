using System;
using System.Collections.Generic;

namespace Club.Domain
{
  public static class Constants
  {
    public static List<string> AdministrationRoleNames = new List<string>() { "Administrator", "Super Administrator" };
    public static List<Guid> AdministrationRoleOids = new List<Guid>() { new Guid("058CA795-D577-43AA-958C-769295280D59"), new Guid("058CA795-D577-43AA-958C-769295280D58") };

    public static List<string> SuperAdministrationRoleNames = new List<string>() { "Super Administrator" };
    public static List<Guid> SuperAdministrationRoleOids = new List<Guid>() { new Guid("058CA795-D577-43AA-958C-769295280D58") };


    public static List<string> CommitteOrAboveRoleNames = new List<string>() { "Administrator", "Super Administrator", "Committe Role" };
    public static List<Guid> CommitteOrAboveRoleOids = new List<Guid>() { new Guid("058CA795-D577-43AA-958C-769295280D59"), new Guid("058CA795-D577-43AA-958C-769295280D58"), new Guid("67B00F36-F679-4F3D-82DD-5ABD7FAC03E1") };

  }
}
