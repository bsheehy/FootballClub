using Mallon.Core.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Configuration;

namespace Club.Domain.Models
{
  public class ModelUser
  {
    public ModelUser()
    {
      this.Roles = new List<ModelRole>();
    }

    [DisplayName("Oid")]
    public Guid Oid { get; set; }

    [Required(ErrorMessage = "A Login is required")]
    [DisplayName("Login")]
    public string Login { get; set; }

    //password
    [DisplayName("Password")]
    [DataType(DataType.Password)]
    [ConfigRegularExpressionAttribute("mallon.PasswordStrength")]
    public string Password { get; set; }

    //confirm password must match Password
    [Required(ErrorMessage = "Confirm Password is required")]
    [DataType(DataType.Password)]
    [DisplayName("Confirm Password")]
    [System.ComponentModel.DataAnnotations.Compare("Password", ErrorMessage = "Password and Confirm Password must match")]
    public string ConfirmPassword { get; set; }

    [Required(ErrorMessage = "Full Name is required")]
    [DisplayName("Full Name")]
    public string FullName { get; set; }

    [DisplayName("Email")]
    [DataType(DataType.EmailAddress)]
    public string Email { get; set; }

    [DisplayName("Account Locked")]
    public bool AccountLocked { get; set; }

    [DisplayName("No of Invalid Logins")]
    public int NoOfFailedLogins { get; set; }

    [DisplayName("HostedInCloud")]
    public bool HostedInCloud { get { return getHostedInCloud(); } }

    [DisplayName("Active")]
    public bool IsActive { get; set; }

    public IList<ModelRole> Roles { get; set; }

    private static bool getHostedInCloud()
    {
      try
      {
        return Convert.ToBoolean(ConfigurationManager.AppSettings["mallon.HostedInCloud"]);
      }
      catch
      {
        return false;
      }
    }
  }
}