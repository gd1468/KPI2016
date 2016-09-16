using System;
using System.Threading.Tasks;
using System.Web.Http;
using MoneyManagement.ServiceLayer.Interfaces;
using MoneyManagement.ServiceLayer.Queries;

namespace MoneyManagement.Web.Controllers.Finance
{
    public class AccountApiController : ApiController
    {
        private readonly IQueryDispatcher _queryDispatcher;
        public AccountApiController(IQueryDispatcher queryDispatcher)
        {
            _queryDispatcher = queryDispatcher;
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
        public async Task<GetAccountQuery.Result> Account()
        {
            return await _queryDispatcher.Execute<GetAccountQuery, GetAccountQuery.Result>(new GetAccountQuery());
        }
    }
}
