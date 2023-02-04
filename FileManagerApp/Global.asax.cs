using Autofac;
using Autofac.Integration.Mvc;
using FileManagerData.Interfaces;
using FileManagerData.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using FileManagerBussinessLogic;

namespace FileManagerApp
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<FileManagerService>().As<IFileManagerService>();
            builder.RegisterType<CosmosRepository<FileManagerDTO.Models.File>>().As<ICosmosRepository<FileManagerDTO.Models.File>>();


            builder.RegisterControllers(typeof(MvcApplication).Assembly);
            
            var container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));

            


            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);


        }
    }
}
