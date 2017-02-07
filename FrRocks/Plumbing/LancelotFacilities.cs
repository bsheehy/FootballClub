using Castle.MicroKernel.Facilities;
using Castle.MicroKernel.Registration;
using Mallon.Core.Interfaces;
using Mallon.Core.Logic;
using Mallon.Core.Web.Logic;
using Mallon.Documents;
using Mallon.Documents.Interfaces;
using Mallon.Documents.Logic;


namespace DiamondFireWeb.Gui.Plumbing
{
  public class LancelotFacilities : AbstractFacility
  {
    protected override void Init()
    {
      //Kernel.Register(
      // Component.For<IPersistentController>()
      // .ImplementedBy<PersistentController>()
      // .LifestylePerWebRequest()
      // );

      Kernel.Register(
        Component.For<IUserController>()
        .ImplementedBy<WebUserController>()
        .LifestylePerWebRequest()
        );

      Kernel.Register(
        Component.For<ISessionIdProvider>()
        .ImplementedBy<WebMsSqlSessionIdProvider>()
        .LifestylePerWebRequest()
        );

      Kernel.Register(
        Component.For<IEventLog>()
        .ImplementedBy<EventLogController>()
        .LifestylePerWebRequest()
        );

      Kernel.Register(
        Component.For<Mallon.Core.Web.Logic.MembershipPvd>()
        .ImplementedBy<Mallon.Core.Web.Logic.MembershipPvd>()
        .LifestylePerWebRequest()
        );

      Kernel.Register(
        Component.For<IFileFactory>()
        .ImplementedBy<AutoTypePathFileFactory>()
        .LifestylePerWebRequest()
        );

      Kernel.Register(
       Component.For<IAttachedDocsController>()
       .ImplementedBy<AttachedDocumentsController>()
       .LifestylePerWebRequest()
       );

      //Kernel.Register(
      //  Component.For<IMapper>()
      //  .ImplementedBy<Mallon.Core.Web.Logic.MembershipPvd>()
      //  .LifestylePerWebRequest()
      //  );

    }
  }
}