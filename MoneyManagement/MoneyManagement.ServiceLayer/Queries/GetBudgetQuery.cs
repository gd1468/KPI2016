using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using MoneyManagement.Persistance.Interfaces;
using MoneyManagement.ServiceLayer.ClientPresentations;
using MoneyManagement.ServiceLayer.Interfaces;

namespace MoneyManagement.ServiceLayer.Queries
{
    public class GetBudgetQuery : IQuery<GetBudgetQuery.Result>
    {
        public Guid CultureId { get; set; }
        public Guid UserId { get; set; }
        public class Result
        {
            public List<BudgetPresentation> BudgetPresentations { get; set; }
        }
    }

    public class GetBudgetQueryHandler : IQueryHandler<GetBudgetQuery, GetBudgetQuery.Result>
    {
        private readonly IMoneyManagementContext _db;

        public GetBudgetQueryHandler(IMoneyManagementContext db)
        {
            _db = db;
        }
        public async Task<GetBudgetQuery.Result> Execute([FromUri]GetBudgetQuery query)
        {
            var budgets = _db.Budgets.Where(x => x.UserId == query.UserId).AsEnumerable().Select(x => new BudgetPresentation
            {
                KeyId = x.KeyId,
                DisplayName = x.Translations.Any() ? string.Format("[{0}] {1}", x.ShortName, x.Translations.FirstOrDefault(y => y.CultureId == query.CultureId)?.Name) : x.ShortName,
                Balance = x.Balance,
                EndDate = x.EffectiveTo,
                Expensed = x.Expensed,
                Total = x.Total,
                StartDate = x.EffectiveFrom
            }).ToList();
            return await Task.FromResult(new GetBudgetQuery.Result()
            {
                BudgetPresentations = budgets
            });
        }
    }
}
