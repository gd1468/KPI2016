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
    public class SaveAccountCommand : ICommand<SaveAccountCommand.Result>
    {
        public decimal Amount { get; set; }
        public string Name { get; set; }
        public string ShortName { get; set; }
        public Guid UserId { get; set; }
        public Guid CultureId { get; set; }
        public class Result
        {
            public int EffectiveRows { get; set; }
        }
    }

    public class SaveAccountCommandHandler : ICommandHandler<SaveAccountCommand, SaveAccountCommand.Result>
    {
        private readonly IMoneyManagementContext _db;

        public SaveAccountCommandHandler(IMoneyManagementContext db)
        {
            _db = db;
        }

        public async Task<SaveAccountCommand.Result> Execute(SaveAccountCommand command)
        {
            var account = new Account
            {
                Balance = command.Amount,
                CreatedById = command.UserId,
                CreatedOn = DateTime.Now,
                LastTime = DateTime.Now,
                LastUserId = command.UserId,
                UserId = command.UserId,
                ShortName = command.ShortName
            };

            var addedAccount = _db.Accounts.Add(account);
            var translation = new AccountTranslation
            {
                CultureId = command.CultureId,
                CreatedOn = DateTime.Now,
                CreatedById = command.UserId,
                LastTime = DateTime.Now,
                LastUserId = command.UserId,
                Name = command.Name,
                TranslationOfId = addedAccount.KeyId
            };
            _db.AccountTranslations.Add(translation);

            var effectiveRows = await _db.SaveChangesAsync();

            return new SaveAccountCommand.Result
            {
                EffectiveRows = effectiveRows
            };
        }
    }
}
