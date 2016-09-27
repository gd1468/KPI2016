using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using MoneyManagement.DomainModel.Domain;
using MoneyManagement.Persistance.Interfaces;
using MoneyManagement.ServiceLayer.ClientPresentations;
using MoneyManagement.ServiceLayer.Interfaces;

namespace MoneyManagement.ServiceLayer.Commands
{
    public class SaveBudgetCommand : ICommand<SaveBudgetCommand.Result>
    {
        public decimal Total { get; set; }
        public decimal Balance { get; set; }
        public decimal Expensed { get; set; }
        public string Name { get; set; }
        public string ShortName { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public Guid UserId { get; set; }
        public Guid CultureId { get; set; }
        public class Result
        {
            public int EffectiveRows { get; set; }
        }
    }

    public class SaveBudgetCommandHandler : ICommandHandler<SaveBudgetCommand, SaveBudgetCommand.Result>
    {
        private readonly IMoneyManagementContext _db;

        public SaveBudgetCommandHandler(IMoneyManagementContext db)
        {
            _db = db;
        }

        public async Task<SaveBudgetCommand.Result> Execute(SaveBudgetCommand command)
        {
            var budget = new Budget
            {
                Balance = command.Balance,
                CreatedById = command.UserId,
                CreatedOn = DateTime.Now,
                LastTime = DateTime.Now,
                LastUserId = command.UserId,
                UserId = command.UserId,
                ShortName = command.ShortName,
                EffectiveFrom = command.StartDate,
                EffectiveTo = command.EndDate,
                Expensed = command.Expensed,
                Total = command.Total
            };

            var addedBudget = _db.Budgets.Add(budget);
            var translation = new BudgetTranslation
            {
                CultureId = command.CultureId,
                CreatedOn = DateTime.Now,
                CreatedById = command.UserId,
                LastTime = DateTime.Now,
                LastUserId = command.UserId,
                Name = command.Name,
                TranslationOfId = addedBudget.KeyId
            };
            _db.BudgetTranslations.Add(translation);

            var effectiveRows = await _db.SaveChangesAsync();

            return new SaveBudgetCommand.Result
            {
                EffectiveRows = effectiveRows
            };
        }
    }
}
