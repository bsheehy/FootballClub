using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace FrRocks.Utils
{
  /// <summary>
  /// Build a SelectList for a Enum with Char values
  /// </summary>
  public static class UtilsSelectListEnum
  {
    public static string GetDescription<TEnum>(this TEnum value)
    {
      return value.ToString();
    }

    /// <summary>
    /// Build a select list for an enum
    /// </summary>
    public static SelectList SelectListFor<T>() where T : struct
    {
      Type t = typeof(T);
      return !t.IsEnum ? null
                       : new SelectList(BuildSelectListItems(t), "Value", "Text");
    }

    /// <summary>
    /// Build a select list for an enum
    /// </summary>
    public static SelectList SelectListFor<T>(T Tenum) where T : struct
    {
      Type t = typeof(T);
      return !t.IsEnum ? null
                       : new SelectList(BuildSelectListItems(t), "Value", "Text", Tenum);
    }

    /// <summary>
    /// Build a select list for an enum with a particular value selected 
    /// </summary>
    public static SelectList SelectListFor<T>(T selected, string placeHolder = "") where T : struct
    {
      Type t = typeof(T);
      SelectList result = !t.IsEnum ? null
                       : new SelectList(BuildSelectListItems(t), "Text", "Value", selected.ToString());
      if (string.IsNullOrEmpty(placeHolder) == false)
      {

      }

      return result;
    }

    private static IEnumerable<SelectListItem> BuildSelectListItems(Type t)
    {
      return Enum.GetValues(t)
                 .Cast<Enum>()
                 .Select(e => new SelectListItem { Value = e.ToString(), Text = e.GetDescription() });
    }

    /// <summary>
    /// Converts Enum to a SelectList used in DropDownList
    /// </summary>
    /// <typeparam name="TEnum"></typeparam>
    /// <param name="enumObj"></param>
    /// <returns></returns>
    public static SelectList ToSelectList<TEnum>(this TEnum enumObj)
    {
      var values = (from TEnum e in Enum.GetValues(typeof(TEnum))
                    select new { Value = e, Text = e.ToString() }).ToList();

      return new SelectList(values, "Value", "Text", enumObj);
    }

    public static SelectList ToSelectList<TEnum>()
    {
      var values = (from TEnum e in Enum.GetValues(typeof(TEnum))
                    select new { Value = e, Text = e.ToString() }).ToList();

      return new SelectList(values, "Value", "Text");
    }
  }
}