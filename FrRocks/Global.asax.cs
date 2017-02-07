using Castle.MicroKernel.Registration;
using Club.Services.Controllers;
using DiamondFireWeb.Gui.Plumbing;
using Mallon.Core.DependencyInjection;
using Mallon.Core.Interfaces;
using System;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace FrRocks
{
  public class MvcApplication : System.Web.HttpApplication
  {
    private static Castle.Windsor.IWindsorContainer container;

    public override void Init()
    {
      base.Init();
      this.PostRequestHandlerExecute += new EventHandler(MvcApplication_PostRequestHandlerExecute);
    }

    protected void Application_Start()
    {
      //AutoMpaper static configuration
      AutoMapperConfig.RegisterMappings();

      //Only use the View Engines that you require: 
      //http://www.robertsindall.co.uk/blog/how-to-improve-mvc-application-performance/
      ViewEngines.Engines.Clear();
      ViewEngines.Engines.Add(new RazorViewEngine());

      //System.Diagnostics.Debugger.Break();
      CoreSpatial.SqlServerTypes.Utilities.LoadNativeAssemblies(Server.MapPath("~/bin"));

      AreaRegistration.RegisterAllAreas();
      FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
      RouteConfig.RegisterRoutes(RouteTable.Routes);
      BundleConfig.RegisterBundles(BundleTable.Bundles);
      //log4net.Config.XmlConfigurator.Configure();
      BootstrapContainer();

      //bool isHttps = false;
      //if (Boolean.TryParse(ConfigurationManager.AppSettings["sheehy.club.Https"], out isHttps))
      //{
      //  AntiForgeryConfig.RequireSsl = isHttps;
      //}
    }

    protected void Application_End()
    {
      container.Dispose();
    }

    /// <summary>
    /// DFW-404 Login Redirect Issue. Fix at: http://jvance.com/blog/2012/09/21/FixFormsAuthentication302.xhtml
    /// </summary>
    //protected void Application_EndRequest()
    //{
    //  var context = new HttpContextWrapper(this.Context);

    //  // If we're an ajax request and forms authentication caused a 302, 
    //  // then we actually need to do a 401
    //  if (FormsAuthentication.IsEnabled
    //    && context.Response.StatusCode == 302
    //    && context.Request.IsAjaxRequest()
    //    && context.Request.IsAuthenticated == false)
    //  {
    //    //context.Response.SuppressFormsAuthenticationRedirect = true;
    //    context.Response.Clear();
    //    context.Response.StatusCode = 401;
    //  }
    //}

    /// <summary>
    /// We're instantiating the WindsorContainer class which is the core class in Windsor. We then call 
    /// its Install method. Reading it from inside out, the FromAssembly class will look for, instantiate
    /// and return all installers in our assembly (installers folder). Then WindsorContainer will call down 
    /// to each of those installers, which in turn will register the components specified by each installer.
    /// 
    /// https://github.com/castleproject/Windsor/blob/master/docs/mvc-tutorial-part-4-putting-it-all-together.md
    /// </summary>
    private static void BootstrapContainer()
    {
      container = new Castle.Windsor.WindsorContainer();
      container.Register(
        Component.For<IInjector>()
        .UsingFactoryMethod(() => new CastleInjector(container)));

      //// Can install in specific Order if we need to
      //container.Install(
      //  new LancelotInstaller(),
      //  new ControllersInstaller(),
      //  new ElamhInstaller(),
      //  new PersistenceInstaller()
      //  );

      container.Install(Castle.Windsor.Installer.FromAssembly.This());

      var controllerFactory = new DiamondFireWeb.Gui.Plumbing.WindsorControllerFactory(container.Kernel);
      container.Kernel.ComponentCreated += new Castle.MicroKernel.ComponentInstanceDelegate(Kernel_ComponentCreated);
      ControllerBuilder.Current.SetControllerFactory(controllerFactory);
    }


    static void Kernel_ComponentCreated(Castle.Core.ComponentModel model, object instance)
    {
      IResumable res = null;
      if ((res = instance as IResumable) != null)
      {
        // Remember the object
        ResumableStore.Instances.Add(res);
        // Resume it from the session state
        ResumableStore.Resume(res);
      }
    }

    void MvcApplication_PostRequestHandlerExecute(object sender, EventArgs e)
    {
      foreach (IResumable instance in ResumableStore.Instances)
      {
        ResumableStore.Pause(instance);
      }
    }

    /// <summary>
    /// Could not get httpErrors from the wenb.config working so had to use customError configuration as per:
    /// http://www.digitallycreated.net/Blog/57/getting-the-correct-http-status-codes-out-of-asp.net-custom-error-pages
    /// 
    /// You can see httpErrors implementation (that did not work) at:
    /// http://dusted.codes/demystifying-aspnet-mvc-5-error-pages-and-error-logging
    /// http://benfoster.io/blog/aspnet-mvc-custom-error-pages
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    //protected void Application_Error(object sender, EventArgs e)
    //{
    //  bool customErrors = true; //on by default
    //  try
    //  {
    //    //allow custom errors to be switched off (false)
    //    customErrors = Convert.ToBoolean(ConfigurationManager.AppSettings["mallon.dfw.CustomErrors"]);
    //  }
    //  catch { }

    //  if (customErrors)
    //    ShowCustomErrorPage(Server.GetLastError());
    //}

    //private void ShowCustomErrorPage(Exception exception)
    //{
    //  var context = new HttpContextWrapper(Context);
    //  if (!context.Request.IsAjaxRequest())
    //  {
    //    HttpException httpException = exception as HttpException;
    //    if (httpException == null)
    //      httpException = new HttpException(500, "Internal Server Error", exception);

    //    Response.Clear();
    //    RouteData routeData = new RouteData();
    //    routeData.Values.Add("controller", "Error");
    //    routeData.Values.Add("action", "DisplayError");
    //    routeData.Values.Add("httpStatusCode", httpException.GetHttpCode());
    //    routeData.Values.Add("fromAppErrorEvent", true);

    //    Server.ClearError();

    //    context.Response.ContentType = "text/html";
    //    IController controller = new ErrorController();
    //    controller.Execute(new RequestContext(new HttpContextWrapper(Context), routeData));
    //  }
    //}
  }
}
