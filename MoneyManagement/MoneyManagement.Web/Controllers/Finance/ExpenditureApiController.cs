using System;
using System.Threading.Tasks;
using System.Web.Http;
using MoneyManagement.ServiceLayer.Commands;
using MoneyManagement.ServiceLayer.Interfaces;
using MoneyManagement.ServiceLayer.Queries;

namespace MoneyManagement.Web.Controllers.Finance
{
    public class ExpenditureApiController : ApiController
    {
        private readonly ICommandDispatcher _commandDispatcher;
        private readonly IQueryDispatcher _queryDispatcher;

        public ExpenditureApiController(ICommandDispatcher commandDispatcher, IQueryDispatcher queryDispatcher)
        {
            _commandDispatcher = commandDispatcher;
            _queryDispatcher = queryDispatcher;
        }

        public async Task<SaveExpenditureCommand.Result> Post(SaveExpenditureCommand command)
        {
            return await _commandDispatcher.Execute<SaveExpenditureCommand, SaveExpenditureCommand.Result>(command);
        }

        public async Task<DepositToExistingAccountCommand.Result> Put(DepositToExistingAccountCommand command)
        {
            return await _commandDispatcher.Execute<DepositToExistingAccountCommand, DepositToExistingAccountCommand.Result>(command);
        }

        public async Task<GetExpenditureQuery.Result> Get([FromUri]GetExpenditureQuery query)
        {
            return await _queryDispatcher.Execute<GetExpenditureQuery, GetExpenditureQuery.Result>(query);
        }
    }
}
