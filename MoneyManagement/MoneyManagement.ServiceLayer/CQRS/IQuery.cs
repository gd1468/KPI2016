using System.Threading.Tasks;

namespace MoneyManagement.ServiceLayer.Interfaces
{
    public interface IQuery<TResult>
    {
    }

    public interface IQueryHandler<in TQuery, TResult>
        where TQuery : IQuery<TResult>
    {
        Task<TResult> Execute(TQuery query);
    }

    public interface IQueryDispatcher
    {
        Task<TResult> Execute<TQuery, TResult>(TQuery query)
            where TQuery : IQuery<TResult>;
    }
}
