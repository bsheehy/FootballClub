using System;
using System.ComponentModel.DataAnnotations;
using System.Configuration;

namespace Club.Domain.Models
{
  public class DataAnnotationAttributes
  {
    public static string GetRegexPattern(string patternConfigKey)
    {
      bool hostedInWeb = false;
      string regexPattern = ".*"; //anything
      try
      {
        hostedInWeb = Convert.ToBoolean(ConfigurationManager.AppSettings["mallon.HostedInCloud"]);
        if (hostedInWeb)
        {
          regexPattern = ConfigurationManager.AppSettings[patternConfigKey];
        }
      }
      catch { }

      return regexPattern;
    }
  }

  public class ConfigRegularExpressionAttribute : RegularExpressionAttribute
  {
    /// <summary>
    /// Loads Regular Express 'patternConfigKey' from Web.Config. Adds a ErrorMessage also for 'patternConfigKey'
    /// </summary>
    /// <param name="patternConfigKey"></param>
    public ConfigRegularExpressionAttribute(string patternConfigKey)
      : base(DataAnnotationAttributes.GetRegexPattern(patternConfigKey))
    {
      try
      {
        //load error message
        bool hostedInWeb = Convert.ToBoolean(ConfigurationManager.AppSettings["mallon.HostedInCloud"]);
        if (hostedInWeb)
        {
          string patternConfigKeyDesc = String.Format("{0}Desc", patternConfigKey); //e.g. PasswordStrengthDesc
          ErrorMessage = ConfigurationManager.AppSettings[patternConfigKeyDesc];
        }
      }
      catch { }
    }
  }
}
