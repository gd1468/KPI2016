using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using MoneyManagement.Persistance.Interfaces;
using MoneyManagement.ServiceLayer.ClientPresentations;
using MoneyManagement.ServiceLayer.Interfaces;

namespace MoneyManagement.ServiceLayer.Commands
{
    public class RemoveAccountCommand : ICommand<RemoveAccountCommand.Result>
    {
        public List<Guid> AccountIds { get; set; }
        public class Result
        {
            public int EffectiveRows { get; set; }
        }
    }

    public class RemoveAccountCommandHandler : ICommandHandler<RemoveAccountCommand, RemoveAccountCommand.Result>
    {
        private readonly IMoneyManagementContext _db;

        public RemoveAccountCommandHandler(IMoneyManagementContext db)
        {
            _db = db;
        }

        public async Task<RemoveAccountCommand.Result> Execute(RemoveAccountCommand command)
        {
            if (command?.AccountIds == null)
            {
                throw new ArgumentNullException("There is no selected account");
            }

            var deletedAccount = await _db.Accounts.Where(x => command.AccountIds.Contains(x.KeyId)).ToListAsync();
            deletedAccount.ForEach(x =>
            {
                _db.Accounts.Remove(x);
            });
            var effectiveRows = await _db.SaveChangesAsync();
            return new RemoveAccountCommand.Result
            {
                EffectiveRows = effectiveRows
            };
        }
    }
}
