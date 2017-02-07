using System.Web.Mvc;
using Castle.MicroKernel.Registration;

namespace DiamondFireWeb.Gui.Installers
{
  public class ElamhInstaller: IWindsorInstaller
  {
    #region IWindsorInstaller Members

    public void Install(Castle.Windsor.IWindsorContainer container, Castle.MicroKernel.SubSystems.Configuration.IConfigurationStore store)
    {
      container.Register(Classes.FromAssemblyNamed("Elmah.Mvc")
            .BasedOn<IController>()
            .LifestyleTransient());
    }

    #endregion
  }
}