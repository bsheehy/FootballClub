using System;

namespace DiamondFireWeb.Gui.Plumbing
{
   [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
   public class DoNotMapAttribute : Attribute
   {
   }
}