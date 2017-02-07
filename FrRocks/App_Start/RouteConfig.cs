using System.Web.Mvc;
using System.Web.Routing;

namespace FrRocks
{
  public class RouteConfig
  {
    public static void RegisterRoutes(RouteCollection routes)
    {
      routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

      routes.IgnoreRoute("{*favicon}", new { favicon = @"(.*/)?favicon.([iI][cC][oO]|[gG][iI][fF])(/.*)?" });

      routes.MapRoute(
          name: "Default",
          url: "{controller}/{action}/{Oid}",
          defaults: new { controller = "Home", action = "Index", Oid = UrlParameter.Optional }
      );
    }
  }
}
