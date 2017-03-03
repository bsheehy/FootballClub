========================================================================================
	Windsor Installers
========================================================================================
Windsor uses dedicated classes called installers to feed the container with knowledge about our types (this process iscalled registration). Our installer needs to tell Windsor two things: 
1. how to find controllers in our app - This is easy, as ASP.NET MVC requires by convention, that controllers implement IController interface.

2. how to configure them - Configuration for now will be pretty simple too. First of all, MVC framework requires that we create a new controller instance each time it asks us for one. This is different from Windsor's default which would create just one instance the first time we ask for one, and then reuse it for all subsequent requests.

When working with the container, the first thing you need to do is to register all your components. Windsor uses installers (that is types implementing IWindsorInstaller interface) to encapsulate and partition your registration logic, as well as some helper types like Configuration and FromAssembly to make working with installers a breeze.

Installers are simply types that implement the IWindsorInstaller interface. The interface has a single method called Install. The method gets an instance of the container, which it can then register components with using fluent registration API:

public class RepositoriesInstaller : IWindsorInstaller
{
   public void Install(IWindsorContainer container, IConfigurationStore store)
   {
      container.Register(AllTypes.FromAssemblyNamed("Acme.Crm.Data")
                            .Where(type => type.Name.EndsWith("Repository"))
                            .WithService.DefaultInterface()
                            .Configure(c => c.LifeStyle.PerWebRequest));
   }
}