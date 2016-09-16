using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using MoneyManagement.Persistance.Interfaces;
using MoneyManagement.ServiceLayer.ClientPresentations;
using MoneyManagement.ServiceLayer.Interfaces;

namespace MoneyManagement.ServiceLayer.Queries
{
    public class GetAccountQuery : IQuery<GetAccountQuery.Result>
    {
        public Guid CultureId { get; set; }
        public class Result
        {
            public List<AccountPresentation> AccountPresentations { get; set; }
        }
    }

    public class GetAccountQueryHandler : IQueryHandler<GetAccountQuery, GetAccountQuery.Result>
    {
        private readonly IMoneyManagementContext _db;

        public GetAccountQueryHandler(IMoneyManagementContext db)
        {
            _db = db;
        }
        public async Task<GetAccountQuery.Result> Execute(GetAccountQuery query)
        {
            var accounts = _db.Accounts.AsEnumerable().Select(x => new AccountPresentation
            {
                KeyId = x.KeyId,
                DisplayName = x.Translations.Any() ? string.Format("[{0}] {1}", x.ShortName, x.Translations.FirstOrDefault(y => y.CultureId == query.CultureId)?.Name) : x.ShortName
            }).ToList();
            return await Task.FromResult(new GetAccountQuery.Result()
            {
                AccountPresentations = accounts
            });
        }
    }
}
