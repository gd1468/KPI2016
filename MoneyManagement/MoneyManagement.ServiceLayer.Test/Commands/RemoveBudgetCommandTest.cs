using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MoneyManagement.DomainModel.Domain;
using MoneyManagement.Persistance.Interfaces;
using MoneyManagement.ServiceLayer.Commands;
using MoneyManagement.ServiceLayer.Test.FakeDb;
using Moq;

namespace MoneyManagement.ServiceLayer.Test.Commands
{
    [TestClass]
    public class RemoveBudgetCommandTest
    {
        private Mock<IMoneyManagementContext> _db;
        private RemoveBudgetCommandHandler _handler;
        private FakeDbSet<Budget> _budgets;

        [TestInitialize]
        public void Setup()
        {
            _db = new Mock<IMoneyManagementContext>();
            _handler = new RemoveBudgetCommandHandler(_db.Object);

            _budgets = new FakeDbSet<Budget> {new Budget
                {
                    KeyId = new Guid("714b19c9-7c5b-47f9-9a99-46639db1595d"),
                    UserId = new Guid("05541516-ccf1-41f4-b6ac-b2aa7d807b8c"),
                    Balance = 2000
                }};

            _db.Setup(x => x.Budgets).Returns(_budgets);
        }

        [TestMethod]
        public async Task RemoveExistingBudget()
        {
            var command = new RemoveBudgetCommand
            {
                BudgetIds = new List<Guid>
                {
                    new Guid("714b19c9-7c5b-47f9-9a99-46639db1595d")
                }
            };
            _db.Setup(x => x.SaveChangesAsync()).ReturnsAsync(1);

            var result = await _handler.Execute(command);

            Assert.IsNotNull(result);
            Assert.AreEqual(result.EffectiveRows, 1);
            Assert.AreEqual(_budgets.Count, 0);
        }

        [TestMethod]
        public async Task DontRemoveBudget()
        {
            var command = new RemoveBudgetCommand
            {
                BudgetIds = new List<Guid>
                {
                    Guid.NewGuid()
                }
            };
            _db.Setup(x => x.SaveChangesAsync()).ReturnsAsync(0);

            var result = await _handler.Execute(command);

            Assert.IsNotNull(result);
            Assert.AreEqual(result.EffectiveRows, 0);
            Assert.AreEqual(_budgets.Count, 1);
        }
    }
}
