using System.Threading.Tasks;
using System.Web.Http;
using MoneyManagement.ServiceLayer.Interfaces;
using MoneyManagement.ServiceLayer.Queries;

namespace MoneyManagement.Web.Controllers
{
    public class CultureApiController : ApiController
    {
        private readonly IQueryDispatcher _queryDispatcher;
        public CultureApiController(IQueryDispatcher queryDispatcher)
        {
            _queryDispatcher = queryDispatcher;
        }

        public async Task<GetCultureQuery.Result> Get()
        {
            return await _queryDispatcher.Execute<GetCultureQuery, GetCultureQuery.Result>(new GetCultureQuery());
        }
    }
}
