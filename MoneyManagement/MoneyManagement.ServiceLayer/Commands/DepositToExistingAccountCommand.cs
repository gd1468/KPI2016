﻿using System;
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
    public class DepositToExistingAccountCommand : ICommand<DepositToExistingAccountCommand.Result>
    {
        public decimal Amount { get; set; }
        public string Description { get; set; }
        public DateTime ExpenditureDate { get; set; }
        public Guid AccountId { get; set; }
        public Guid UserId { get; set; }
        public Guid CultureId { get; set; }
        public class Result
        {
            public List<AccountPresentation> AccountPresentations { get; set; }
        }
    }

    public class DepositToExistingAccountCommandHandler :
        ICommandHandler<DepositToExistingAccountCommand, DepositToExistingAccountCommand.Result>
    {
        private readonly IMoneyManagementContext _db;

        public DepositToExistingAccountCommandHandler(IMoneyManagementContext db)
        {
            _db = db;
        }
        public async Task<DepositToExistingAccountCommand.Result> Execute(DepositToExistingAccountCommand command)
        {
            var expenditure = new ExpenditureIncome
            {
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

            if (account != null) account.Balance += command.Amount;

            _db.Expenditures.Add(expenditure);

            await _db.SaveChangesAsync();

            var listAccount = accounts.Select(x => new AccountPresentation
            {
                KeyId = x.KeyId,
                Balance = x.Balance,
                DisplayName = x.Translations.Any() ? string.Format("[{0}] {1}", x.ShortName, x.Translations.FirstOrDefault(y => y.CultureId == command.CultureId)?.Name) : x.ShortName,
            }).ToList();

            return new DepositToExistingAccountCommand.Result
            {
                AccountPresentations = listAccount
            };
        }
    }
}
