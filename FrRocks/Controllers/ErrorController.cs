using Elmah;
using FrRocks.Models;
using Mallon.Core.Web.Exceptions;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace FrRocks.Controllers
{
  [AllowAnonymous]
  public class ErrorController : Controller
  {
    /// <summary>
    /// This is used to Log javascript errors to ELMAH. To turn on Javascrip\Ajax error logging (which will normally result
    /// in multiple errors being logged in ELMAH) then set the following in the Layout page:
    ///     window.logErrorAfterAjaxHandled = true;
    /// </summary>
    /// <param name="message"></param>
    [AllowAnonymous]
    [ValidateInput(false)]
    public void LogJavaScriptError(string message)
    {
      ErrorSignal.FromCurrentContext().Raise(new JavaScriptException(message));
    }

    //Throw Error
    public ActionResult GenerateHttpError(HttpStatusCode httpStatusCode)
    {
      throw new HttpException((int)httpStatusCode, "Error");
    }

    //Display Error
    public ActionResult DisplayError(HttpStatusCode httpStatusCode)
    {
      ViewBag.StatusCodeDesc = httpStatusCode.ToString();
      ViewBag.StatusCode = (int)httpStatusCode;
      return View();
    }

    [AllowAnonymous]
    [ValidateInput(false)]
    public new ActionResult View(string errorMsg, string redirectUrl)
    {
      ModelErrorPage errModel = new ModelErrorPage(errorMsg, redirectUrl);
      if (string.IsNullOrEmpty(errModel.RedirectUrl))
      {
        redirectUrl = Url.RouteUrl("Default", ((Route)RouteTable.Routes["Default"]).Defaults);
      }
      return View(errModel);
    }
  }
}
