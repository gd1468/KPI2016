using System.Web.Http;
using System.Web.Mvc;
using Autofac.Integration.Mvc;
using Autofac.Integration.WebApi;
using Autofac;

namespace MoneyManagement.Web
{
    public static class RegistrationExtensions
    {
        public static void RegisterApiResolver(this IContainer container, HttpConfiguration config)
        {
            var apiResolver = new AutofacWebApiDependencyResolver(container);
            config.DependencyResolver = apiResolver;
        }

        public static void RegisterMvcResolver(this IContainer container)
        {
            var mvcResolver = new AutofacDependencyResolver(container);
            DependencyResolver.SetResolver(mvcResolver);
        }
    }
}