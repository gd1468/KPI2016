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
        public ExpenditureApiController(ICommandDispatcher commandDispatcher)
        {
            _commandDispatcher = commandDispatcher;
        }

        public async Task<SaveExpenditureCommand.Result> Post(SaveExpenditureCommand command)
        {
            return await _commandDispatcher.Execute<SaveExpenditureCommand, SaveExpenditureCommand.Result>(command);
        }
    }
}
