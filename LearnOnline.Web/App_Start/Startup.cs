using Autofac;
using Autofac.Integration.Mvc;
using Autofac.Integration.WebApi;
using LearnOnline.Data.Infrastructure;
using LearnOnline.Data.Repositories;
using LearnOnline.Model.Models;
using LearnOnline.Service.Services;
using Microsoft.Owin;
using Owin;
using System.Reflection;
using System.Web.Http;
using System.Web.Mvc;

[assembly: OwinStartup(typeof(LearnOnline.Web.App_Start.Startup))]

namespace LearnOnline.Web.App_Start
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigAutofac(app);
        }

        private void ConfigAutofac(IAppBuilder app)
        {
            var builder = new ContainerBuilder();
            builder.RegisterControllers(Assembly.GetExecutingAssembly());
            // Register your Web API controllers.
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly()); //Register WebApi Controllers

            builder.RegisterType<UnitOfWork>().As<IUnitOfWork>().InstancePerRequest();
            builder.RegisterType<DbFactory>().As<IDbFactory>().InstancePerRequest();

            builder.RegisterType<LearnOnlineDbContext>().AsSelf().InstancePerRequest();

            // Repositories
            //builder.RegisterAssemblyTypes(typeof(DistrictRepository).Assembly)
            //    .Where(t => t.Name.EndsWith("Repository"))
            //    .AsImplementedInterfaces().InstancePerRequest();

            //builder.RegisterAssemblyTypes(typeof(ProvinceRepository).Assembly)
            //  .Where(t => t.Name.EndsWith("Repository"))
            //  .AsImplementedInterfaces().InstancePerRequest();

            //builder.RegisterAssemblyTypes(typeof(UserGroupRepository).Assembly)
            //  .Where(t => t.Name.EndsWith("Repository"))
            //  .AsImplementedInterfaces().InstancePerRequest();

            builder.RegisterAssemblyTypes(typeof(UserRepository).Assembly)
              .Where(t => t.Name.EndsWith("Repository"))
              .AsImplementedInterfaces().InstancePerRequest();

            // Services
            builder.RegisterAssemblyTypes(typeof(UserService).Assembly)
               .Where(t => t.Name.EndsWith("Service"))
               .AsImplementedInterfaces().InstancePerRequest();

           // builder.RegisterAssemblyTypes(typeof(DistrictService).Assembly)
           //   .Where(t => t.Name.EndsWith("Service"))
           //   .AsImplementedInterfaces().InstancePerRequest();

           // builder.RegisterAssemblyTypes(typeof(ProvinceService).Assembly)
           //   .Where(t => t.Name.EndsWith("Service"))
           //   .AsImplementedInterfaces().InstancePerRequest();

           // builder.RegisterAssemblyTypes(typeof(UserGroupService).Assembly)
           //.Where(t => t.Name.EndsWith("Service"))
           //.AsImplementedInterfaces().InstancePerRequest();


            Autofac.IContainer container = builder.Build();

            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));

            GlobalConfiguration.Configuration.DependencyResolver = new AutofacWebApiDependencyResolver((IContainer)container); //Set the WebApi DependencyResolver
        }
    }
}