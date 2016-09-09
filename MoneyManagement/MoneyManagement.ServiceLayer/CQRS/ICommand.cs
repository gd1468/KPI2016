using System.Threading.Tasks;

namespace MoneyManagement.ServiceLayer.Interfaces
{
    public interface ICommand<TResult>
    {
    }

    public interface ICommandHandler<in TCommand, TResult>
        where TCommand : ICommand<TResult>
    {
        Task<TResult> Execute(TCommand command);
    }

    public interface ICommandDispatcher
    {
        Task<TResult> Execute<TCommand, TResult>(TCommand query)
            where TCommand : ICommand<TResult>;
    }
}
