using System.Web.Http;
using Autofac;
using Autofac.Integration.Mvc;
using Autofac.Integration.WebApi;
using AutoMapper;
using MoneyManagement.ServiceLayer;

namespace MoneyManagement.Web
{
    public class ContainerConfig
    {
        public static IContainer RegisterDependencies(HttpConfiguration config)
        {
            var builder = Build();

            builder.RegisterWebApiFilterProvider(config);
            builder.RegisterFilterProvider();


            var container = builder.Build();

            Mapper.Initialize(x => x.ConstructServicesUsing(container.Resolve));

            container.RegisterMvcResolver();
            container.RegisterApiResolver(config);

            return container;
        }

        public static ContainerBuilder Build()
        {
            var builder = new ContainerBuilder();

            var baseModule = new BaseModule();
            builder.RegisterModule(baseModule);

            var webModule = new WebModule();
            builder.RegisterModule(webModule);

            var requestModule = new AutofacWebTypesModule();
            builder.RegisterModule(requestModule);

            return builder;
        }
    }
}