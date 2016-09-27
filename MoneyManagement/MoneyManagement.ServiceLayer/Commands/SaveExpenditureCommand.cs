using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MoneyManagement.DomainModel.Domain;
using MoneyManagement.Persistance.Interfaces;
using MoneyManagement.ServiceLayer.Interfaces;
using System.Data.Entity;
using System.Linq;
using MoneyManagement.ServiceLayer.ClientPresentations;

namespace MoneyManagement.ServiceLayer.Commands
{
    public class SaveExpenditureCommand : ICommand<SaveExpenditureCommand.Result>
    {
        public decimal Amount { get; set; }
        public string Description { get; set; }
        public DateTime ExpenditureDate { get; set; }
        public Guid BudgetId { get; set; }
        public Guid AccountId { get; set; }
        public Guid UserId { get; set; }
        public Guid CultureId { get; set; }
        public class Result
        {
            public int EffectiveRows { get; set; }
        }
    }

    public class SaveExpenditureCommandHandler : ICommandHandler<SaveExpenditureCommand, SaveExpenditureCommand.Result>
    {
        private readonly IMoneyManagementContext _db;

        public SaveExpenditureCommandHandler(IMoneyManagementContext db)
        {
            _db = db;
        }

        public async Task<SaveExpenditureCommand.Result> Execute(SaveExpenditureCommand command)
        {
            var expenditure = new Expenditure
            {
                BudgetId = command.BudgetId,
                Amount = command.Amount,
                AccountId = command.AccountId,
                CreatedOn = DateTime.Now,
                ExpenditureDate = command.ExpenditureDate,
                Description = command.Description,
                LastTime = DateTime.Now,
                CreatedById = command.UserId,
                LastUserId = command.UserId,
                UserId = command.UserId
            };

            var accounts = await _db.Accounts.Where(x => x.UserId == command.UserId).ToListAsync();

            var account = accounts.FirstOrDefault(x => x.KeyId == command.AccountId);

            if (account?.Balance < command.Amount)
            {
                throw new Exception("Account doesn't have enough mooney for this expenditure");
            }

            if (account != null) account.Balance -= command.Amount;

            var budgets = await _db.Budgets.Where(x => x.UserId == command.UserId).ToListAsync();

            var budget = budgets.FirstOrDefault(x => x.KeyId == command.BudgetId);

            if (budget?.Balance < command.Amount)
            {
                throw new Exception("Budget doesn't have enough mooney for this expenditure");
            }

            if (budget != null)
            {
                budget.Balance -= command.Amount;
                budget.Expensed += command.Amount;
            }

            _db.Expenditures.Add(expenditure);
            var effectiveRows = await _db.SaveChangesAsync();

            return new SaveExpenditureCommand.Result
            {
                EffectiveRows = effectiveRows
            };
        }
    }
}
