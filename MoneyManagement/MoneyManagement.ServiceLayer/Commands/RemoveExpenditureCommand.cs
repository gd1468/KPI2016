using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MoneyManagement.Persistance.Interfaces;
using MoneyManagement.ServiceLayer.Interfaces;
using System.Data.Entity;

namespace MoneyManagement.ServiceLayer.Commands
{
    public class RemoveExpenditureCommand : ICommand<RemoveExpenditureCommand.Result>
    {
        public List<Guid> ExpenditureIds { get; set; }
        public Guid UserId { get; set; }
        public Guid CultureId { get; set; }
        public class Result
        {
            public int EffectiveRows { get; set; }
        }
    }

    public class RemoveExpenditureCommandHandler :
        ICommandHandler<RemoveExpenditureCommand, RemoveExpenditureCommand.Result>
    {
        private IMoneyManagementContext _db;

        public RemoveExpenditureCommandHandler(IMoneyManagementContext db)
        {
            _db = db;
        }

        public async Task<RemoveExpenditureCommand.Result> Execute(RemoveExpenditureCommand command)
        {
            if (command?.ExpenditureIds == null)
            {
                throw new ArgumentNullException("There is no selected expenditure");
            }

            var deletedExpenditures = await _db.Expenditures.Where(x => command.ExpenditureIds.Contains(x.KeyId)).ToListAsync();
            deletedExpenditures.ForEach(x =>
            {
                if (x.BudgetId != null)
                {
                    x.Account.Balance += x.Amount;
                    x.Budget.Balance += x.Amount;
                    x.Budget.Expensed -= x.Amount;
                }
                else
                {
                    if (x.Account.Balance - x.Amount < 0)
                    {
                        throw new AggregateException("Account balance cannot be less than 0");
                    }
                    x.Account.Balance -= x.Amount;
                }

                _db.Expenditures.Remove(x);
            });
            var effectiveRows = await _db.SaveChangesAsync();
            return new RemoveExpenditureCommand.Result
            {
                EffectiveRows = effectiveRows
            };
        }
    }
}
