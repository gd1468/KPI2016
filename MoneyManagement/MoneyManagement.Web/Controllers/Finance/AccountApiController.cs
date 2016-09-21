using System;
using System.Threading.Tasks;
using System.Web.Http;
using MoneyManagement.ServiceLayer.Commands;
using MoneyManagement.ServiceLayer.Interfaces;
using MoneyManagement.ServiceLayer.Queries;

namespace MoneyManagement.Web.Controllers.Finance
{
    public class AccountApiController : ApiController
    {
        private readonly IQueryDispatcher _queryDispatcher;
        private readonly ICommandDispatcher _commandDispatcher;
        public AccountApiController(IQueryDispatcher queryDispatcher, ICommandDispatcher commandDispatcher)
        {
            _queryDispatcher = queryDispatcher;
            _commandDispatcher = commandDispatcher;
        }

        [HttpGet]
        public async Task<GetAccountByIdQuery.Result> Account(Guid id)
        {
            return await _queryDispatcher.Execute<GetAccountByIdQuery, GetAccountByIdQuery.Result>(new GetAccountByIdQuery
            {
                KeyId = id
            });
        }

        [HttpGet]
        public async Task<GetAccountQuery.Result> Account([FromUri]GetAccountQuery query)
        {
            return await _queryDispatcher.Execute<GetAccountQuery, GetAccountQuery.Result>(query);
        }

        [HttpPost]
        public async Task<SaveAccountCommand.Result> Post([FromBody]SaveAccountCommand command)
        {
            return await _commandDispatcher.Execute<SaveAccountCommand, SaveAccountCommand.Result>(command);
        }
    }
}
