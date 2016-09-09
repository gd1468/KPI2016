using System;
using System.Management.Instrumentation;
using System.Web.Mvc;
using MoneyManagement.ServiceLayer.Interfaces;

namespace MoneyManagement.ServiceLayer.CQRS
{
    public class CommandDispatcher : ICommandDispatcher
    {
        private readonly IDependencyResolver _resolver;

        public CommandDispatcher(IDependencyResolver resolver)
        {
            _resolver = resolver;
        }

        public void Execute<TCommand>(TCommand command)
            where TCommand : ICommand
        {
            if (command == null)
            {
                throw new ArgumentNullException("command");
            }

            var handler = _resolver.GetService<ICommandHandler<TCommand>>();

            if (handler == null)
            {
                throw new InstanceNotFoundException("Command Handler Not Found");
            }

            handler.Execute(command);
        }
    }
}
