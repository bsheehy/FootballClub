using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Castle.MicroKernel;

namespace DiamondFireWeb.Gui.Plumbing
{
  /// <summary>
  /// http://docs.castleproject.org/Windsor.Windsor-tutorial-part-two-plugging-Windsor-in.ashx
  /// http://matheusandcode.wordpress.com/2012/03/14/configuring-castle-windsor-in-asp-net-mvc/
  /// 
  /// Here we inherit from the DefaultControllerFactory which is what ASP.Net MVC uses out of the box 
  /// to create instances of controllers every time you request one such as when browsing to a page on 
  /// your web site.Then on our custom controller factory we are using Castle Windsor to resolve components. 
  /// We give it a controller type and then ask it to try to find an implementation in our code. If can’t 
  /// find anything we tell it to return a 404 error otherwise we ask it to create the object for us and r
  /// eturn it.We also tap into the ReleaseController method that gets called once the controller goes out 
  /// of scope and is no longer needed. We make sure that it gets removed from the IoC container to make 
  /// double sure that it doesn’t keep references to objects that have been released from memory.
  /// 
  /// The controller factory provides the MVC runtime with new instances of the controller type for each 
  /// request, and when the request ends it releases the controller. To do it we use IKernel provided by 
  /// Windsor, which is an interface that we use to have Windsor resolve services for us, and 
  /// also tell it to release them when we're done using them. 
  /// </summary>
  public class WindsorControllerFactory : DefaultControllerFactory
  {
    private readonly IKernel kernel;

    public WindsorControllerFactory(IKernel kernel)
    {
      this.kernel = kernel;
    }

    public override void ReleaseController(IController controller)
    {
      kernel.ReleaseComponent(controller);
    }

    protected override IController GetControllerInstance(RequestContext requestContext, Type controllerType)
    {
        if (controllerType == null)
        {
            throw new HttpException(404, string.Format("The controller for path '{0}' could not be found.", requestContext.HttpContext.Request.Path));
        }

        return (IController)kernel.Resolve(controllerType);
    }
  }
}