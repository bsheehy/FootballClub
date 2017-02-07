using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using System.Web.Http.Controllers;
using System.Web.Mvc;

namespace DiamondFireWeb.Gui.Installers
{
  /// <summary>
  /// The installer uses the container parameter of the Install method to Register controllers using 
  /// Windsor's Fluent Registration API. This is the recommended way of working with Windsor as it is 
  /// the most terse, and the most flexible. Every time we add a new controller type into our application 
  /// (and big apps can have hundreds of them) it will be automatically registered, we don't need to do 
  /// anything else other than making sure we follow the conventions we established.
  /// </summary>
  public class ControllersInstaller : IWindsorInstaller
  {
    public void Install(IWindsorContainer container, IConfigurationStore store)
    {
      //Classes static class is the entry point to the registration and it will look for public, 
      //non-abstract classes FromThisAssembly, that is the assembly containing the installer (that 
      //is our MVC assembly, which is where, indeed, the controllers live).

      //We don't want just any type from the assembly though. The BasedOn filters the classes even 
      //further to just those that implement IController. The method is named BasedOn rather than 
      //Implements because it can be used for base classes just as well as interfaces, and it even handle 
      //open generic types (but let's not worry about that just yet).

      //The last part controls a very important aspect of working with Windsor - instance lifestyle. 
      //Transient is what MVC framework expects that is, a new instance should be provided by Windsor 
      //every time it is needed, and it's the caller's responsibility to tell Windsor when that instance 
      //is no longer needed and can be discarded (That's what the ReleaseController method on our controller 
      //factory is for).
      container.Register(Classes.FromThisAssembly()
         .BasedOn<IController>()
         .LifestyleTransient());

      //Need to install the shared MVC Controller from the AuditTrail.Mvc project
      //container.Register(Classes.FromAssemblyNamed("AuditTrail.Mvc")
      //    .BasedOn<IController>()
      //    .LifestyleTransient());


      //WebApi controllers
      container.Register(Classes.FromThisAssembly()
         .BasedOn<IHttpController>()
         .LifestyleTransient());
    }
  }
}