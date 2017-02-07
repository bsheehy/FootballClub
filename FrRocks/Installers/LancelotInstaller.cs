using Castle.MicroKernel.Registration;
using DiamondFireWeb.Gui.Plumbing;

namespace DiamondFireWeb.Gui.Installers
{
  public class LancelotInstaller: IWindsorInstaller
  {
    #region IWindsorInstaller Members

    public void Install(Castle.Windsor.IWindsorContainer container, Castle.MicroKernel.SubSystems.Configuration.IConfigurationStore store)
    {
      container.AddFacility<LancelotFacilities>();
    }

    #endregion
  }
}