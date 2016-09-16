using System;
using System.Threading.Tasks;
using System.Web.Http;
using MoneyManagement.ServiceLayer.Interfaces;
using MoneyManagement.ServiceLayer.Queries;

namespace MoneyManagement.Web.Controllers.Finance
{
    public class ExpenditureApiController : ApiController
    {
        private readonly IQueryDispatcher _queryDispatcher;
        public ExpenditureApiController(IQueryDispatcher queryDispatcher)
        {
            _queryDispatcher = queryDispatcher;
        }
    }
}
