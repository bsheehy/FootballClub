using Mallon.Core.Resources;
using System.ComponentModel.DataAnnotations;


namespace FrRocks.Models
{
  public class ModelErrorPage
  {
    public ModelErrorPage(string errorMessage)
    {
      ErrorMessage = errorMessage;
    }

    public ModelErrorPage(string errorMessage, string redirectUrl)
    {
      this.ErrorMessage = errorMessage;
      this.RedirectUrl = redirectUrl;
    }

    [Required]
    [DisplayName("Error Message")]
    public string ErrorMessage { get; set; }

    /// <remarks>
    /// If this is not populated then by default Dimond Fire should redirect to menu screen.
    /// </remarks>
    [DisplayName("Redirect Url")]
    public string RedirectUrl { get; set; }
  }
}
