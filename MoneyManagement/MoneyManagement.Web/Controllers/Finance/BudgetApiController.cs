using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using MoneyManagement.ServiceLayer.ClientPresentations;
using MoneyManagement.ServiceLayer.Commands;
using MoneyManagement.ServiceLayer.Interfaces;
using MoneyManagement.ServiceLayer.Queries;

namespace MoneyManagement.Web.Controllers.Finance
{
    public class BudgetApiController : ApiController
    {
        private readonly IQueryDispatcher _queryDispatcher;
        private readonly ICommandDispatcher _commandDispatcher;

        public BudgetApiController(IQueryDispatcher queryDispatcher, ICommandDispatcher commandDispatcher)
        {
            _queryDispatcher = queryDispatcher;
            _commandDispatcher = commandDispatcher;
        }

        [HttpGet]
        public async Task<GetBudgetByIdQuery.Result> Budget(Guid id)
        {
            return await _queryDispatcher.Execute<GetBudgetByIdQuery, GetBudgetByIdQuery.Result>(new GetBudgetByIdQuery
            {
                KeyId = id
            });
        }

        [HttpGet]
        public async Task<GetBudgetQuery.Result> Budget([FromUri]GetBudgetQuery query)
        {
            return await _queryDispatcher.Execute<GetBudgetQuery, GetBudgetQuery.Result>(query);
        }

        public async Task<GetBudgetQuery.Result> Post([FromBody]SaveBudgetCommand command)
        {
            await _commandDispatcher.Execute<SaveBudgetCommand, SaveBudgetCommand.Result>(command);
            var budgets = await _queryDispatcher.Execute<GetBudgetQuery, GetBudgetQuery.Result>(new GetBudgetQuery
            {
                CultureId = command.CultureId,
                UserId = command.UserId
            });
            return budgets;
        }

        public async Task<RemoveBudgetCommand.Result> Delete([FromUri]RemoveBudgetCommand command)
        {
            return await _commandDispatcher.Execute<RemoveBudgetCommand, RemoveBudgetCommand.Result>(command);
        }
    }
}
