using System;
using System.Threading.Tasks;
using System.Web.Http;
using MoneyManagement.ServiceLayer.Interfaces;

namespace MoneyManagement.Web.Controllers.Finance
{
    public class ExpenditureController : ApiController
    {
        private readonly IQueryDispatcher _queryDispatcher;
        public ExpenditureController(IQueryDispatcher queryDispatcher)
        {
            _queryDispatcher = queryDispatcher;
        }

        [HttpGet]
        public Task<Account> Account(Guid id)
        {
            return _queryDispatcher.Execute<GetAccountByIdQuery>(new GetAccountByIdQuery
            {

            });
        }
    }
}
