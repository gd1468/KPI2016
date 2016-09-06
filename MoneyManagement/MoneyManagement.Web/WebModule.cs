using Autofac;
using Autofac.Integration.Mvc;
using Autofac.Integration.WebApi;
using MoneyManagement.ServiceLayer;

namespace MoneyManagement.Web
{
    public class WebModule : BaseModule
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);
            RegisterAspNetDependencies(builder);
        }

        private void RegisterAspNetDependencies(ContainerBuilder builder)
        {
            builder.RegisterApiControllers(typeof(WebApiApplication).Assembly).InstancePerRequest();
            builder.RegisterControllers(typeof(WebApiApplication).Assembly).InstancePerRequest();
        }
    }
}