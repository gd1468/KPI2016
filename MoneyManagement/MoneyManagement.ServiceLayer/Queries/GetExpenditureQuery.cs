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
    public class GetExpenditureQuery : IQuery<GetExpenditureQuery.Result>
    {
        public Guid CultureId { get; set; }
        public Guid UserId { get; set; }

        public class Result
        {
            public List<ExpenditurePresentation> ExpenditurePresentations { get; set; }
        }
    }

    public class GetExpenditureQueryHandler : IQueryHandler<GetExpenditureQuery, GetExpenditureQuery.Result>
    {
        private readonly IMoneyManagementContext _db;

        public GetExpenditureQueryHandler(IMoneyManagementContext db)
        {
            _db = db;
        }

        public async Task<GetExpenditureQuery.Result> Execute(GetExpenditureQuery query)
        {
            var accounts = (from account in _db.Accounts.Where(x => x.UserId == query.UserId)
                            join accountTranslation in _db.AccountTranslations.Where(x => x.CultureId == query.CultureId) on
                                account.KeyId equals accountTranslation.TranslationOfId
                                into accountTranslationTemp
                            from accountTranslation in accountTranslationTemp.DefaultIfEmpty()
                            select new
                            {
                                account.KeyId,
                                DisplayName = accountTranslation != null ? "[" + account.ShortName + "] " + accountTranslation.Name : account.ShortName
                            });

            var budgets = (from budget in _db.Budgets.Where(x => x.UserId == query.UserId)
                           join budgetTranslation in _db.BudgetTranslations.Where(x => x.CultureId == query.CultureId) on
                               budget.KeyId equals budgetTranslation.TranslationOfId
                               into budgetTranslationTemp
                           from budgetTranslation in budgetTranslationTemp.DefaultIfEmpty()
                           select new
                           {
                               budget.KeyId,
                               DisplayName = budgetTranslation != null ? "[" + budget.ShortName + "] " + budgetTranslation.Name : budget.ShortName
                           });

            var expenditures = await (from expenditure in _db.Expenditures.Where(x => x.UserId == query.UserId)
                                      join account in accounts on expenditure.AccountId equals account.KeyId
                                      join budget in budgets on expenditure.BudgetId equals budget.KeyId
                                          into budgetTemp
                                      from budget in budgetTemp.DefaultIfEmpty()
                                      select new ExpenditurePresentation
                                      {
                                          KeyId = expenditure.KeyId,
                                          Budget =
                                              budget != null
                                                  ? new ObjectReference { KeyId = budget.KeyId, DisplayName = budget.DisplayName }
                                                  : null,
                                          Account =
                                              account != null
                                                  ? new ObjectReference { KeyId = account.KeyId, DisplayName = account.DisplayName }
                                                  : null,
                                          Amount = expenditure.Amount,
                                          Description = expenditure.Description,
                                          ExpenditureDate = expenditure.ExpenditureDate
                                      }).ToListAsync();

            return new GetExpenditureQuery.Result
            {
                ExpenditurePresentations = expenditures
            };
        }
    }
}
