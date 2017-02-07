using System;
using System.Collections.Generic;
using System.Linq;

namespace Club.Services.Utils
{
  public static class DictionaryExtensions
  {
    /// <summary>
    /// Merges a dictionary against an array of other dictionaries.
    /// </summary>
    /// <typeparam name="TResult">The type of the resulting dictionary.</typeparam>
    /// <typeparam name="TKey">The type of the key in the resulting dictionary.</typeparam>
    /// <typeparam name="TValue">The type of the value in the resulting dictionary.</typeparam>
    /// <param name="source">The source dictionary.</param>
    /// <param name="mergeBehavior">A delegate returning the merged value. (Parameters in order: The current key, The current value, The previous value)</param>
    /// <param name="mergers">Dictionaries to merge against.</param>
    /// <returns>The merged dictionary.</returns>
    public static TResult MergeLeft<TResult, TKey, TValue>(
        this TResult source,
        Func<TKey, TValue, TValue, TValue> mergeBehavior,
        params IDictionary<TKey, TValue>[] mergers)
        where TResult : IDictionary<TKey, TValue>, new()
    {
      var result = new TResult();
      var sources = new List<IDictionary<TKey, TValue>> { source }
          .Concat(mergers);

      foreach (var kv in sources.SelectMany(src => src))
      {
        TValue previousValue;
        result.TryGetValue(kv.Key, out previousValue);
        result[kv.Key] = mergeBehavior(kv.Key, kv.Value, previousValue);
      }

      return result;
    }


    public static void AddHtmlAttribute(this System.Collections.Generic.IDictionary<string, object> htmlAttributes, string key, string value, bool overWrite = false)
    {
      if (htmlAttributes.ContainsKey(key))
      {
        if (overWrite)
        {
          htmlAttributes[key] = value;
        }
        else
        {
          htmlAttributes[key] = string.Concat(htmlAttributes[key].ToString(), " ", value);
        }
      }
      else
      {
        htmlAttributes.Add(key, value);
      }
    }

  }
}