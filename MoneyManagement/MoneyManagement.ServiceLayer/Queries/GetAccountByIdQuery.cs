using System;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using MoneyManagement.Persistance.Interfaces;
using MoneyManagement.ServiceLayer.ClientPresentations;
using MoneyManagement.ServiceLayer.Interfaces;

namespace MoneyManagement.ServiceLayer.Queries
{
    public class GetAccountByIdQuery : IQuery<GetAccountByIdQuery.Result>
    {
        public Guid KeyId { get; set; }
        public Guid CultureId { get; set; }
        public class Result
        {
            public AccountPresentation AccountPresentation { get; set; }
        }
    }

    public class GetAccountByIdQueryHanler : IQueryHandler<GetAccountByIdQuery, GetAccountByIdQuery.Result>
    {
        private readonly IMoneyManagementContext _db;

        public GetAccountByIdQueryHanler(IMoneyManagementContext db)
        {
            _db = db;
        }

        public async Task<GetAccountByIdQuery.Result> Execute(GetAccountByIdQuery query)
        {
            var account = await _db.Accounts.FirstOrDefaultAsync(x => x.KeyId == query.KeyId);
            return new GetAccountByIdQuery.Result
            {
                AccountPresentation = new AccountPresentation
                {
                    KeyId = account.KeyId,
                    DisplayName =
                        account.Translations.Any(y => y.CultureId == query.CultureId)
                            ? string.Format("[{0}] {1}", account.ShortName, account.Translations.FirstOrDefault(y => y.CultureId == query.CultureId)?.Name)
                            : account.ShortName
                }
            };
        }
    }
}
