namespace MoneyManagement.ServiceLayer.Interfaces
{
    public interface ICommand
    {
    }

    public interface ICommandHandler<in TCommand> where TCommand: ICommand
    {
        void Execute(TCommand command);
    }

    public interface ICommandDispatcher
    {
        void Execute<TCommand>(TCommand command)
            where TCommand : ICommand;
    }
}
