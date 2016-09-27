using System;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using MoneyManagement.Persistance.Interfaces;
using MoneyManagement.ServiceLayer.Interfaces;

namespace MoneyManagement.ServiceLayer.Commands
{
    public class UpdateExpenditureCommand : ICommand<UpdateExpenditureCommand.Result>
    {
        public decimal Amount { get; set; }
        public string Description { get; set; }
        public DateTime ExpenditureDate { get; set; }
        public Guid? BudgetId { get; set; }
        public Guid AccountId { get; set; }
        public Guid UserId { get; set; }
        public Guid CultureId { get; set; }
        public Guid KeyId { get; set; }
        public class Result
        {
            public int EffectiveRows { get; set; }
        }
    }

    public class UpdateExpenditureCommandHandler :
        ICommandHandler<UpdateExpenditureCommand, UpdateExpenditureCommand.Result>
    {
        private readonly IMoneyManagementContext _db;

        public UpdateExpenditureCommandHandler(IMoneyManagementContext db)
        {
            _db = db;
        }

        public async Task<UpdateExpenditureCommand.Result> Execute(UpdateExpenditureCommand command)
        {
            var expenditureModel = await _db.Expenditures.FirstOrDefaultAsync(x => x.KeyId == command.KeyId);
            if (expenditureModel != null)
            {
                expenditureModel.AccountId = command.AccountId;
                var oldAmount = expenditureModel.Amount;
                expenditureModel.Amount = command.Amount;
                expenditureModel.BudgetId = command.BudgetId;
                expenditureModel.Description = command.Description;
                expenditureModel.ExpenditureDate = command.ExpenditureDate;
                expenditureModel.LastTime = DateTime.Now;
                expenditureModel.LastUserId = command.UserId;

                var accounts = await _db.Accounts.Where(x => x.UserId == command.UserId).ToListAsync();

                var account = accounts.FirstOrDefault(x => x.KeyId == command.AccountId);

                if (account?.Balance + oldAmount < command.Amount)
                {
                    throw new Exception("Account doesn't have enough mooney for this expenditure");
                }

                if (account != null) account.Balance = account.Balance + oldAmount - command.Amount;

                var budgets = await _db.Budgets.Where(x => x.UserId == command.UserId).ToListAsync();

                var budget = budgets.FirstOrDefault(x => x.KeyId == command.BudgetId);

                if (budget?.Balance + oldAmount < command.Amount)
                {
                    throw new Exception("Budget doesn't have enough mooney for this expenditure");
                }

                if (budget != null)
                {
                    budget.Balance = budget.Balance + oldAmount - command.Amount;
                    budget.Expensed = budget.Expensed - oldAmount + command.Amount;
                }
            }

            var effectiveRows = await _db.SaveChangesAsync();
            return new UpdateExpenditureCommand.Result
            {
                EffectiveRows = effectiveRows
            };
        }
    }
}
