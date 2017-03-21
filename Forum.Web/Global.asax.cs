using Autofac.Integration.Web;
using HibernatingRhinos.Profiler.Appender.EntityFramework;
using System;
using System.Web;
using System.Web.Optimization;
using System.Web.Routing;

namespace Forum.Web
{
    public class Global : HttpApplication, IContainerProviderAccessor
    {
        private static IContainerProvider _containerProvider;

        public IContainerProvider ContainerProvider
        {
            get
            {
                return _containerProvider;
            }
        }

        protected void Application_Start(object sender, EventArgs e)
        {
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            AutofacConfig.Configure(out _containerProvider);

            if (HttpContext.Current.IsDebuggingEnabled)
                EntityFrameworkProfiler.Initialize();
        }
    }
}