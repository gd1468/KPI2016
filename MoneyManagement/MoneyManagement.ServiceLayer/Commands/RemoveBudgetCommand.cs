using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using MoneyManagement.Persistance.Interfaces;
using MoneyManagement.ServiceLayer.Interfaces;

namespace MoneyManagement.ServiceLayer.Commands
{
    public class RemoveBudgetCommand : ICommand<RemoveBudgetCommand.Result>
    {
        public List<Guid> BudgetIds { get; set; }
        public class Result
        {
            public int EffectiveRows { get; set; }
        }
    }

    public class RemoveBudgetCommandHandler : ICommandHandler<RemoveBudgetCommand, RemoveBudgetCommand.Result>
    {
        private readonly IMoneyManagementContext _db;

        public RemoveBudgetCommandHandler(IMoneyManagementContext db)
        {
            _db = db;
        }
        public async Task<RemoveBudgetCommand.Result> Execute(RemoveBudgetCommand command)
        {
            if (command?.BudgetIds == null)
            {
                throw new ArgumentNullException("There is no selected budget");
            }

            var deletedBudget = await _db.Budgets.Where(x => command.BudgetIds.Contains(x.KeyId)).ToListAsync();
            deletedBudget.ForEach(x =>
            {
                _db.Budgets.Remove(x);
            });
            var effectiveRows = await _db.SaveChangesAsync();
            return new RemoveBudgetCommand.Result
            {
                EffectiveRows = effectiveRows
            };
        }
    }
}
