using System.Threading.Tasks;
using System.Web.Http;
using MoneyManagement.ServiceLayer.ClientPresentations;
using MoneyManagement.ServiceLayer.Interfaces;
using MoneyManagement.ServiceLayer.Queries;

namespace MoneyManagement.Web.Controllers
{
    public class UserController : ApiController
    {
        private readonly IQueryDispatcher _queryDispatcher;
        public UserController(IQueryDispatcher queryDispatcher)
        {
            _queryDispatcher = queryDispatcher;
        }

        public Task<GetUserQuery.Result> Get([FromBody] GetUserQuery userQuery)
        {
            var user = _queryDispatcher.Execute<GetUserQuery, GetUserQuery.Result>(new GetUserQuery
            {
                UserName = userQuery.UserName,
                Password = userQuery.Password
            });
            return user;
        }
    }
}
