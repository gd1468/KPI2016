using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MoneyManagement.DomainModel.Domain;
using MoneyManagement.Persistance.Interfaces;
using MoneyManagement.ServiceLayer.Commands;
using MoneyManagement.ServiceLayer.Test.FakeDb;
using Moq;
using System.Threading.Tasks;

namespace MoneyManagement.ServiceLayer.Test.Commands
{
    [TestClass]
    public class RemoveExpenditureCommandTest
    {
        private Mock<IMoneyManagementContext> _db;
        private RemoveExpenditureCommandHandler _handler;
        private FakeDbSet<Expenditure> _expenditures;
        private FakeDbSet<Account> _accounts;
        private FakeDbSet<Budget> _budgets;

        [TestInitialize]
        public void Setup()
        {
            _db = new Mock<IMoneyManagementContext>();
            _handler = new RemoveExpenditureCommandHandler(_db.Object);

            _accounts = new FakeDbSet<Account>
            {
                new Account
                    {
                        KeyId = new Guid("083f4637-ebff-4b31-a6fe-fe2810a03c61"),
                        Balance = 1000
                    }
            };
            _db.Setup(x => x.Accounts).Returns(_accounts);

            _budgets = new FakeDbSet<Budget>
            {
                new Budget
                    {
                        KeyId = new Guid("6f3ebfab-ef49-41ed-9775-1ac8d2ab99d2"),
                        Expensed = 1000,
                        Total = 2000,
                        Balance = 1000
                    }
            };
            _expenditures = new FakeDbSet<Expenditure> {new Expenditure
                {
                    KeyId = new Guid("714b19c9-7c5b-47f9-9a99-46639db1595d"),
                    UserId = new Guid("05541516-ccf1-41f4-b6ac-b2aa7d807b8c"),
                    Account = _accounts.First(),
                    Budget = _budgets.First(),
                    BudgetId = _budgets.First().KeyId,
                    Amount = 1000
                }};

            _db.Setup(x => x.Expenditures).Returns(_expenditures);
        }

        [TestMethod]
        public async Task RemoveExpenseExpenditure()
        {
            var command = new RemoveExpenditureCommand
            {
                UserId = new Guid("05541516-ccf1-41f4-b6ac-b2aa7d807b8c"),
                ExpenditureIds = new List<Guid>
                {
                    new Guid("714b19c9-7c5b-47f9-9a99-46639db1595d")
                }
            };

            _db.Setup(x => x.SaveChangesAsync()).ReturnsAsync(1);

            var result = await _handler.Execute(command);

            Assert.IsNotNull(result);
            Assert.AreEqual(result.EffectiveRows, 1);

            var account = _accounts.First();
            Assert.AreEqual(account.Balance, 2000);

            var budget = _budgets.First();
            Assert.AreEqual(budget.Expensed, 0);
            Assert.AreEqual(budget.Balance, budget.Total);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException), "There is no selected expenditure")]
        public async Task ThrowException()
        {
            var command = new RemoveExpenditureCommand
            {
                UserId = new Guid("05541516-ccf1-41f4-b6ac-b2aa7d807b8c"),
            };

            _db.Setup(x => x.SaveChangesAsync()).ReturnsAsync(1);

            var result = await _handler.Execute(command);
        }
    }
}
