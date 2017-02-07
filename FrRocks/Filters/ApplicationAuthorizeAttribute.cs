
using FrRocks.Models;
using System.Net;
using System.Web.Mvc;
using System.Web.Security;
namespace FrRocks.Filters
{
  /// <summary>
  /// This overrides the default AuthorizeAttribute in order to deal with Unauthorized Ajax requests see JIRA: DFW-404
  /// Got help via solutions on:
  /// http://www.codeproject.com/Articles/655086/Handling-authentication-specific-issues-for-AJAX-c
  /// https://www.simple-talk.com/dotnet/asp-net/thoughts-on-asp-net-mvc-authorization-and-security/
  /// etc..
  /// </summary>
  public class ApplicationAuthorizeAttribute : System.Web.Mvc.AuthorizeAttribute
  {
    protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
    {
      var httpContext = filterContext.HttpContext;
      var request = httpContext.Request;
      var response = httpContext.Response;
      var user = httpContext.User;

      if (request.IsAjaxRequest())
      {
        if (user.Identity.IsAuthenticated == false)
        {
          ModelJsonResult j = new ModelJsonResult();
          j.success = false;
          j.url = FormsAuthentication.LoginUrl;
          filterContext.Result = new JsonResult() { JsonRequestBehavior = JsonRequestBehavior.AllowGet, Data = j };
          return;
        }
        else
        {
          response.StatusCode = (int)HttpStatusCode.Forbidden;
        }
        response.SuppressFormsAuthenticationRedirect = true;
        response.End();
      }

      base.HandleUnauthorizedRequest(filterContext);
    }
  }
}