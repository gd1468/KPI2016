using System;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using MoneyManagement.Persistance.Interfaces;
using MoneyManagement.ServiceLayer.ClientPresentations;
using MoneyManagement.ServiceLayer.Interfaces;

namespace MoneyManagement.ServiceLayer.Queries
{
    public class GetBudgetByIdQuery : IQuery<GetBudgetByIdQuery.Result>
    {
        public Guid KeyId { get; set; }
        public Guid CultureId { get; set; }
        public class Result
        {
            public BudgetPresentation BudgetPresentation { get; set; }
        }
    }

    public class GetBudgetByIdQueryHandler : IQueryHandler<GetBudgetByIdQuery, GetBudgetByIdQuery.Result>
    {
        private readonly IMoneyManagementContext _db;

        public GetBudgetByIdQueryHandler(IMoneyManagementContext db)
        {
            _db = db;
        }
        public async Task<GetBudgetByIdQuery.Result> Execute(GetBudgetByIdQuery query)
        {
            var budget = await _db.Budgets.FirstOrDefaultAsync(x => x.KeyId == query.KeyId);
            return new GetBudgetByIdQuery.Result
            {
                BudgetPresentation = new BudgetPresentation
                {
                    KeyId = budget.KeyId,
                    DisplayName = budget.Translations.Any(y => y.CultureId == query.CultureId)
                        ? string.Format("[{0}] {1}", budget.ShortName, budget.Translations.FirstOrDefault(y => y.CultureId == query.CultureId)?.Name)
                        : budget.ShortName
                }
            };
        }
    }
}
