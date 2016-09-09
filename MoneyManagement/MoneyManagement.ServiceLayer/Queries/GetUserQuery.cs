using System;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using MoneyManagement.Persistance.Interfaces;
using MoneyManagement.ServiceLayer.ClientPresentations;
using MoneyManagement.ServiceLayer.Interfaces;

namespace MoneyManagement.ServiceLayer.Queries
{
    public class GetUserQuery : IQuery<GetUserQuery.Result>
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public class Result
        {
            public UserPresentation User { get; set; }
        }
    }

    public class GetUserQueryHandler : IQueryHandler<GetUserQuery, GetUserQuery.Result>
    {
        private readonly IMoneyManagementContext _db;
        public GetUserQueryHandler(IMoneyManagementContext db)
        {
            _db = db;
        }

        public async Task<GetUserQuery.Result> Execute(GetUserQuery query)
        {
            var user = await _db.Users.Where(x => x.Username.Equals(query.UserName) && x.Password.Equals(query.Password)).FirstOrDefaultAsync();
            return new GetUserQuery.Result
            {
                User = new UserPresentation
                {
                    UserName = user?.Username
                }
            };
        }
    }
}
