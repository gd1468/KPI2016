using System;
using System.Threading.Tasks;
using MoneyManagement.DomainModel.Domain;
using MoneyManagement.Persistance.Interfaces;
using MoneyManagement.ServiceLayer.Interfaces;

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
        public class Result
        {
            public Boolean IsSuccess { get; set; }
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

            _db.Expenditures.Add(expenditure);
            var result = await _db.SaveChangesAsync();

            return new SaveExpenditureCommand.Result
            {
                IsSuccess = result > 0
            };
        }
    }
}
