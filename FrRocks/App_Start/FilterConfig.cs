using FrRocks.Filters;
using System.Web.Mvc;

namespace FrRocks
{
  public class FilterConfig
  {
    public static void RegisterGlobalFilters(GlobalFilterCollection filters)
    {
      filters.Add(new ApplicationAuthorizeAttribute());
      //filters.Add(new HandleErrorAttribute());
    }
  }
}
