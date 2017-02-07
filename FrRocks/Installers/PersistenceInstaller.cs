using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using DiamondFireWeb.Gui.Plumbing;

namespace DiamondFireWeb.Gui.Installers
{
  public class PersistenceInstaller : IWindsorInstaller
  {
    public void Install(IWindsorContainer container, IConfigurationStore store)
    {
      container.AddFacility<PersistenceFacility>();
    }
  }
}