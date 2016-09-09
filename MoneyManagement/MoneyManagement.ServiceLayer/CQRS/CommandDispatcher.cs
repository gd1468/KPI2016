using System;
using System.Management.Instrumentation;
using System.Threading.Tasks;
using System.Web.Mvc;
using Autofac;
using MoneyManagement.ServiceLayer.Interfaces;

namespace MoneyManagement.ServiceLayer.CQRS
{
    public class CommandDispatcher : ICommandDispatcher
    {
        private readonly IComponentContext _resolver;

        public CommandDispatcher(IComponentContext resolver)
        {
            _resolver = resolver;
        }

        public Task<TResult> Execute<TCommand, TResult>(TCommand command)
            where TCommand : ICommand<TResult>
        {
            if (command == null)
            {
                throw new ArgumentNullException("command");
            }

            var handler = _resolver.Resolve<ICommandHandler<TCommand, TResult>>();

            if (handler == null)
            {
                throw new InstanceNotFoundException("Command Handler Not Found");
            }

            return handler.Execute(command);
        }
    }
}
