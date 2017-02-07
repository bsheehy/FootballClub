using FrRocks.Models;
using System;
using System.Text;
using System.Web.Mvc;

namespace FrRocks.Filters
{
  /// <summary>
  /// This attribute has been added to the BaseController class and deals with throw ModelStateException's.
  /// NOTE: This attribute prevents the ModelStateException raising an error. Thus errors are NOT be caught by the web.config custom errors section: i.e. <httpErrors errorMode="Custom" existingResponse="Replace" defaultResponseMode="Redirect">
  ///       These errors will be dealt with in client javascript via UtilsErrHandler.handleAjaxError()
  /// e.g. FireCertController.CalculateFee()
  /// http://erraticdev.blogspot.co.uk/2010/11/handling-validation-errors-on-ajax.html
  /// </summary>
  [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, AllowMultiple = false)]
  public sealed class HandleModelStateExceptionAttribute : FilterAttribute, IExceptionFilter
  {
    /// <summary>
    /// Called when an exception occurs and processes <see cref="ModelStateException"/> object.
    /// </summary>
    /// <param name="filterContext">Filter context.</param>
    public void OnException(ExceptionContext filterContext)
    {
      if (filterContext == null)
      {
        throw new ArgumentNullException("filterContext");
      }

      if (filterContext.Exception != null && typeof(ModelStateException).IsInstanceOfType(filterContext.Exception) && !filterContext.ExceptionHandled)
      {

        filterContext.ExceptionHandled = true;
        filterContext.HttpContext.Response.TrySkipIisCustomErrors = true;
        filterContext.HttpContext.Response.Clear();
        filterContext.HttpContext.Response.ContentEncoding = Encoding.UTF8;
        filterContext.HttpContext.Response.HeaderEncoding = Encoding.UTF8;

        if (filterContext.HttpContext.Request.Headers["X-Requested-With"] == "XMLHttpRequest" || filterContext.HttpContext.Request.IsAjaxRequest() || (filterContext.Exception as ModelStateException).IsPartialView)
        {
          //http://erraticdev.blogspot.co.uk/2010/11/handling-validation-errors-on-ajax.html
          //filterContext.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;

          if ((filterContext.Exception as ModelStateException).JsonResult != null)
          {
            filterContext.Result = (filterContext.Exception as ModelStateException).JsonResult;
          }
          else
          {
            filterContext.Result = new JsonResult
            {
              JsonRequestBehavior = JsonRequestBehavior.AllowGet,
              Data = new
              {
                //create structure similar to the one build from the ModelState by ToDataSourceResult extension method
                ModelStateErrors = (filterContext.Exception as ModelStateException).Errors,
                ModelStateErrorsAsString = (filterContext.Exception as ModelStateException).Message,
                IsPartialView = (filterContext.Exception as ModelStateException).IsPartialView,
                DisplayKeyInError = (filterContext.Exception as ModelStateException).DisplayKeyInError
              }
            };
          }
        }
        else
        {
          //Normal Request Error
          //filterContext.HttpContext.Response.StatusCode = 400;
          filterContext.Result = new ContentResult
          {
            Content = (filterContext.Exception as ModelStateException).Message,
            ContentEncoding = Encoding.UTF8,
          };
        }
      }
    }
  }
}