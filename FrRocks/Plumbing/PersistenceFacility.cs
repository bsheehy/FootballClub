
using Castle.MicroKernel.Facilities;
using Castle.MicroKernel.Registration;
using Mallon.Core.Interfaces;
using Mallon.Core.Logic.Util;
using Mallon.Core.Spatial;
using NHibernate;
using System.Configuration;
using System.Data.SqlClient;


namespace DiamondFireWeb.Gui.Plumbing
{
  /// <summary>
  /// We chose to write a facility (rather than an INstaller) because in order to use NHibernate we need 
  /// to not only register certain components, but also configure and bootstrap the framework. In cases 
  /// like this facility is the recommended option. 
  /// In order to get NHibernate fully up and running, we need to do the following things:
  ///   1. Configure connection to the database and low level database related things.
  ///   2. Create mapping for our classes so that NHibernate knows how to CRUD them.
  ///   3. Expose surface-area NHibernate components via the container so that we can use them in the application.
  /// </summary>
  public class PersistenceFacility : AbstractFacility
  {
    /// <summary>
    /// By UsingFactoryMethod we can specify a method which Windsor should use to obtain the value for us. 
    /// For ISessionFactory we already have all we need. We're telling Windsor to use BuildSessionFactory 
    /// method on the config we got from FluentNHibernate. The Kernel is a property we got from 
    /// AbstractFacility which we can use to register components. It is of type IKernel which we can also 
    /// use for resolving services from the container.
    /// 
    /// To get an ISession instance, we need to first get the ISessionFactory we just registered, and get 
    /// a new session from the factory by calling OpenSession(). For that we're using a different overload 
    /// of UsingFactoryMethod, one that gives us a single object - k (which is of type IKernel). We can use 
    /// this object to pull the ISessionFactory from the container and ask it for the ISession we need. So 
    /// we say - kernel, give me the ISessionFactory and I'll grab the ISession from it (via the OpenSession() 
    /// method).
    /// 
    /// There's one important, although invisible effect of what we just did. By registering the components 
    /// we didn't just tell Windsor how to create them. Windsor will also take care of properly destroying 
    /// the instances for us, thus taking care of managing their full lifetime. In layman terms, Windsor 
    /// will dispose both objects when they are no longer being used. This means it will flush the changes 
    /// we made to ISession to the database for us, and it will clean up the ISessionFactory. And we get all 
    /// of this for free. 
    /// 
    /// NOTE: ISession is not thread safe so we'll be exposed to multiple bugs if multiple requests try to 
    /// access it. Also as we get more and more requests, its internal cache will grow and grow, getting 
    /// slower with every request until we run out of memory. Thus we set lifestyle to PerWebRequest. Now 
    /// Windsor will call our factory method once in every web request where it needs an ISession instance 
    /// and then reuse this instance in the scope of that web request. When a new web request comes along, 
    /// the factory method will be called again, asking ISessionFactory to OpenSession for that new web 
    /// request, and so on.
    /// </summary
    /// 
    protected override void Init()
    {

      Kernel.Register(
         Component.For<ISessionFactory>()
            .UsingFactoryMethod(_ => SpatialDatabaseWebOptomised.NewSessionFactory())
            .LifeStyle.Singleton,
         Component.For<ISession>()
        //.UsingFactoryMethod(k => k.Resolve<ISessionFactory>().OpenSession(new DefaultInterceptor()))
        .UsingFactoryMethod(k =>
        {
          SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["mallon.Default"].ConnectionString);
          con.Open();
          ISession result = k.Resolve<ISessionFactory>().OpenSession(con, new DefaultInterceptor());

          k.Resolve<ISessionIdProvider>().RegisterDbSession(result);
          return result;
        })
        .LifestylePerWebRequest()
        .OnDestroy((k, s) =>
        {
          s.Connection.Close();
        })
      );
    }
  }
}