using System.Collections.Generic;
using System.Reflection;
using Autofac;
using Module = Autofac.Module;
using System.Linq;
using MoneyManagement.ServiceLayer.CQRS;
using MoneyManagement.ServiceLayer.Interfaces;
using System;
using MoneyManagement.Persistance;
using MoneyManagement.Persistance.Interfaces;
using MoneyManagement.ServiceLayer.Queries;

namespace MoneyManagement.ServiceLayer
{
    public class BaseModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);
            var assemblies = new[]
            {
                typeof(GetUserQuery).Assembly,
                Assembly.GetCallingAssembly()
            };
            builder.RegisterType<MoneyManagementDbContext>().As<IMoneyManagementContext>();

            RegisterCommandHandlers(builder, assemblies);
            RegisterQueryHandlers(builder, assemblies);
        }

        private void RegisterCommandHandlers(ContainerBuilder builder, IEnumerable<Assembly> assembliesToScan)
        {
            var commandHandlers =
                assembliesToScan.SelectMany(s => s.GetTypes().Where(t => t.GetInterfaces().Any(i =>
                                                                       i.IsGenericType &&
                                                                       (i.GetGenericTypeDefinition() == typeof(ICommandHandler<,>)))
                                                                    )
                                           ).ToArray();

            builder.RegisterTypes(commandHandlers).AsImplementedInterfaces();

            builder.RegisterType<CommandDispatcher>().As<ICommandDispatcher>();
        }

        private void RegisterQueryHandlers(ContainerBuilder builder, IEnumerable<Assembly> assembliesToScan)
        {
            var commandHandlers =
                assembliesToScan.SelectMany(s => s.GetTypes().Where(t => t.GetInterfaces().Any(i =>
                                                                       i.IsGenericType &&
                                                                       (i.GetGenericTypeDefinition() == typeof(IQueryHandler<,>)))
                                                                    )
                                           ).ToArray();

            builder.RegisterTypes(commandHandlers).AsImplementedInterfaces();

            builder.RegisterType<QueryDispatcher>().As<IQueryDispatcher>();
        }
    }
}
