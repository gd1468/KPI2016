using System;
using System.Collections.Generic;
using System.Linq;
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
    public class DepositToExistingAccountCommandTest
    {
        private Mock<IMoneyManagementContext> _db;
        private DepositToExistingAccountCommandHandler _handler;
        private FakeDbSet<Expenditure> _expenditures;
        [TestInitialize]
        public void Setup()
        {
            _db = new Mock<IMoneyManagementContext>();
            _handler = new DepositToExistingAccountCommandHandler(_db.Object);

            var accounts = new List<Account>
            {
                new Account
                {
                    KeyId = new Guid("714b19c9-7c5b-47f9-9a99-46639db1595d"),
                    UserId = new Guid("05541516-ccf1-41f4-b6ac-b2aa7d807b8c"),
                    Balance = 2000
                }
            };

            _expenditures = new FakeDbSet<Expenditure>();
            _db.Setup(x => x.Accounts).Returns(accounts.ToMockDbSet().Object);
            _db.Setup(x => x.Expenditures).Returns(_expenditures);
        }

        [TestMethod]
        public async Task CreateDepositiveExpenditure()
        {
            var command = new DepositToExistingAccountCommand
            {
                UserId = new Guid("05541516-ccf1-41f4-b6ac-b2aa7d807b8c"),
                CultureId = new Guid("b295e0bc-96e9-41b2-9973-8fed2ce25599"),
                Amount = 1000,
                AccountId = new Guid("714b19c9-7c5b-47f9-9a99-46639db1595d"),
                Description = "Description",
                ExpenditureDate = DateTime.Today
            };
            _db.Setup(x => x.SaveChangesAsync()).ReturnsAsync(1);
            var result = await _handler.Execute(command);

            Assert.IsNotNull(result);

            Assert.AreEqual(result.EffectiveRows, 1);
            Assert.AreEqual(_expenditures.Count, 1);

            var expenditure = _expenditures.First();

            Assert.AreEqual(expenditure.AccountId, new Guid("714b19c9-7c5b-47f9-9a99-46639db1595d"));
            Assert.AreEqual(expenditure.Description, "Description");
            Assert.AreEqual(expenditure.ExpenditureDate, DateTime.Today);
            Assert.AreEqual(expenditure.Amount, 1000);
            Assert.AreEqual(expenditure.UserId, new Guid("05541516-ccf1-41f4-b6ac-b2aa7d807b8c"));
        }

        [TestMethod]
        public async Task DontCreateExpenditureWithoutAccount()
        {
            var command = new DepositToExistingAccountCommand
            {
                UserId = new Guid("05541516-ccf1-41f4-b6ac-b2aa7d807b8c"),
                CultureId = new Guid("b295e0bc-96e9-41b2-9973-8fed2ce25599"),
                Amount = 1000,
                AccountId = Guid.NewGuid(),
                Description = "Description",
                ExpenditureDate = DateTime.Today
            };
            _db.Setup(x => x.SaveChangesAsync()).ReturnsAsync(0);
            var result = await _handler.Execute(command);

            Assert.IsNotNull(result);

            Assert.AreEqual(result.EffectiveRows, 0);
        }
    }
}
