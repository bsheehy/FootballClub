using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace FrRocks.Extensions
{
  public static class ModelStateExtensions
  {
    public static IDictionary ToSerializedDictionary(this ModelStateDictionary modelState)
    {
      return modelState.ToDictionary(
          k => k.Key,
          v => v.Value.Errors.Select(x => x.ErrorMessage).ToArray()
      );
    }

    public static string ToSerializedErrString(this ModelStateDictionary modelState, bool includeFieldKey = true)
    {
      StringBuilder sb = new StringBuilder();
      foreach (KeyValuePair<string, ModelState> modelStateDD in modelState)
      {
        string fieldKey = modelStateDD.Key;
        ModelState mState = modelStateDD.Value;

        if (mState.Errors.Count > 0)
        {
          if (string.IsNullOrEmpty(fieldKey) == false && includeFieldKey)
          {
            sb.Append(string.Concat(fieldKey, " : "));
          }
          foreach (ModelError error in mState.Errors)
          {
            sb.Append(string.Concat(error.ErrorMessage, ". "));
          }
          sb.Append(System.Environment.NewLine);
        }
      }
      return sb.ToString();
    }

    public static Dictionary<string, string[]> GetErrorDictionary(this ModelStateDictionary modelState)
    {
      if (!modelState.IsValid)
      {
        var errorList = modelState
                .Where(x => x.Value.Errors.Count > 0)
                .ToDictionary(
                    kvp => kvp.Key,
                    kvp => kvp.Value.Errors.Select(e => e.ErrorMessage).ToArray()
                );
        return errorList;
        //var errorList = modelState.ToDictionary(
        //  kvp => kvp.Key,
        //  kvp => kvp.Value.Errors.Select(e => e.ErrorMessage).ToArray()).Where(m => m.Value.Any());
      }
      return null;
    }
  }
}