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
            public List<AccountPresentation> AccountPresentations { get; set; }
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

            await _db.SaveChangesAsync();

            var accounts = await _db.Accounts.Where(x => x.UserId == command.UserId).ToListAsync();

            var result = accounts.Select(x => new AccountPresentation
            {
                KeyId = x.KeyId,
                Balance = x.Balance,
                DisplayName = x.Translations.Any() ? string.Format("[{0}] {1}", x.ShortName, x.Translations.FirstOrDefault(y => y.CultureId == command.CultureId)?.Name) : x.ShortName
            }).ToList();

            return new SaveAccountCommand.Result
            {
                AccountPresentations = result
            };
        }
    }
}
