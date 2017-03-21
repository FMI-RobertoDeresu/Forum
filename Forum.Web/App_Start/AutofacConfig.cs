using Autofac;
using Autofac.Integration.Web;
using Forum.Domain.Contracts;
using Forum.Framework;
using Forum.Framework.Infrastructure;
using Forum.Framework.Repositorires;
using Forum.Service.Services.Entity;
using System.Linq;

namespace Forum.Web
{
    public static class AutofacConfig
    {
        public static void Configure(out IContainerProvider containerProvider)
        {
            var builder = new ContainerBuilder();

            builder.RegisterType<UnitOfWork>().As<IUnitOfWork>().InstancePerRequest();
            builder.RegisterType<DbFactory>().As<IDbDactory>().InstancePerRequest();

            builder.RegisterAssemblyTypes(typeof(CategoryRepository).Assembly)
                .Where(x => x.Name.EndsWith("Repository"))
                .AsImplementedInterfaces().InstancePerRequest();

            builder.RegisterAssemblyTypes(typeof(CategoryService).Assembly)
                .Where(x => x.Name.EndsWith("Service"))
                .AsImplementedInterfaces().InstancePerRequest();

            builder.Register(x => SecurityContext.Current);

            containerProvider = new ContainerProvider(builder.Build());
        }
    }
}