using System;
using System.Threading.Tasks;
using System.Web.Http;
using MoneyManagement.ServiceLayer.Interfaces;
using MoneyManagement.ServiceLayer.Queries;

namespace MoneyManagement.Web.Controllers.Finance
{
    public class BudgetApiController : ApiController
    {
        private readonly IQueryDispatcher _queryDispatcher;
        public BudgetApiController(IQueryDispatcher queryDispatcher)
        {
            _queryDispatcher = queryDispatcher;
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
    }
}
