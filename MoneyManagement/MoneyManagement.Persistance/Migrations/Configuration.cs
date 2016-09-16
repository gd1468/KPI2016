using System.Collections.Generic;
using MoneyManagement.DomainModel.Commons;
using MoneyManagement.DomainModel.Complex_types;
using MoneyManagement.DomainModel.Domain;

namespace MoneyManagement.Persistance.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<MoneyManagement.Persistance.MoneyManagementDbContext>
    {
        private readonly List<User> _users = new List<User>();
        private readonly List<Account> _accounts = new List<Account>();
        private readonly List<Budget> _budgets = new List<Budget>();
        private readonly List<Culture> _cultures = new List<Culture>();
        private readonly List<AccountTranslation> _accountTranslations = new List<AccountTranslation>();
        private List<BudgetTranslation> _budgetTranslations = new List<BudgetTranslation>();

        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(MoneyManagement.Persistance.MoneyManagementDbContext context)
        {
            AddUser(context);
            AddCulture(context);
            AddAccount(context);
            AddAccountTranslation(context);
            AddBudget(context);
        }

        private void AddUser(MoneyManagement.Persistance.MoneyManagementDbContext context)
        {
            _users.AddRange(new List<User>
            {
                new User
                {
                    Username = "dainguyen",
                    Password = "#123",
                    CreatedById = Guid.NewGuid(),
                    CreatedOn = DateTime.Now,
                    LastTime = DateTime.Now,
                    LastUserId = Guid.NewGuid(),
                    ShortName = "Dai Nguyen",
                    UserInformation = new UserInformation
                    {
                        Address = new Address
                        {
                            AddressNumber = "1126",
                            Street = "Quang Trung"
                        },
                        FirstName = "Dai",
                        LastName = "Nguyen Huynh Gia"
                    }
                }
            });
            foreach (var user in _users)
            {
                context.Users.AddOrUpdate(x => x.KeyId, user);
            }

        }

        private void AddAccount(MoneyManagementDbContext context)
        {
            _accounts.AddRange(new List<Account>
            {
                new Account
                {
                    ShortName = "ATM",
                    CreatedById = _users[0].KeyId,
                    CreatedOn = DateTime.Now,
                    LastTime = DateTime.Now,
                    LastUserId = _users[0].KeyId,
                    Balance = 888888,
                    UserId = _users[0].KeyId
                }
            });

            foreach (var account in _accounts)
            {
                context.Accounts.AddOrUpdate(x => x.KeyId, account);
            }
        }

        private void AddAccountTranslation(MoneyManagementDbContext context)
        {
            _accountTranslations.AddRange(new List<AccountTranslation>
            {
                new AccountTranslation
                {
                    Name = "ATM",
                    TranslationOfId = _accounts[0].KeyId,
                    CreatedById = _users[0].KeyId,
                    CreatedOn = DateTime.Now,
                    LastTime = DateTime.Now,
                    LastUserId = _users[0].KeyId,
                    CultureId = _cultures[0].KeyId
                }
            });

            foreach (var accountTranslations in _accountTranslations)
            {
                context.AccountTranslations.AddOrUpdate(x => x.KeyId, accountTranslations);
            }
        }

        private void AddBudget(MoneyManagementDbContext context)
        {
            _budgets.AddRange(new List<Budget>
            {
                new Budget
                {
                    LastTime = DateTime.Now,
                    CreatedById = _users[0].KeyId,
                    Balance = 300000,
                    CreatedOn = DateTime.Now,
                    EffectiveFrom = DateTime.Now,
                    EffectiveTo = DateTime.Now.AddMonths(1),
                    Expensed = 0,
                    LastUserId = _users[0].KeyId,
                    ShortName = "Lunch",
                    Total = 300000,
                    UserId = _users[0].KeyId
                }
            });

            foreach (var budget in _budgets)
            {
                context.Budgets.AddOrUpdate(x => x.KeyId, budget);
            }
        }

        private void AddBudgetTranslation(MoneyManagementDbContext context)
        {
            _budgetTranslations.AddRange(new List<BudgetTranslation>
            {
                new BudgetTranslation
                {
                    Name = "ATM",
                    TranslationOfId = _accounts[0].KeyId,
                    CreatedById = _users[0].KeyId,
                    CreatedOn = DateTime.Now,
                    LastTime = DateTime.Now,
                    LastUserId = _users[0].KeyId,
                    CultureId = _cultures[0].KeyId
                }
            });

            foreach (var budgetTranslation in _budgetTranslations)
            {
                context.BudgetTranslations.AddOrUpdate(x => x.KeyId, budgetTranslation);
            }
        }

        private void AddCulture(MoneyManagementDbContext context)
        {
            _cultures.AddRange(new List<Culture>
            {
                new Culture
                {
                    LastTime = DateTime.Now,
                    LastUserId = _users[0].KeyId,
                    CreatedById = _users[0].KeyId,
                    CreatedOn = DateTime.Now,
                    IsPrimary = true,
                    ShortName = "en-AU"
                }
            });

            foreach (var culture in _cultures)
            {
                context.Cultures.AddOrUpdate(x => x.KeyId, culture);
            }
        }
    }
}
