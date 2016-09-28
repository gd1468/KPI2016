using System;
using System.Collections.Generic;
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
    public class RemoveAccountCommandTest
    {
        private Mock<IMoneyManagementContext> _db;
        private RemoveAccountCommandHandler _handler;
        private FakeDbSet<Account> _accounts;

        [TestInitialize]
        public void Setup()
        {
            _db = new Mock<IMoneyManagementContext>();
            _handler = new RemoveAccountCommandHandler(_db.Object);

            _accounts = new FakeDbSet<Account> {new Account
                {
                    KeyId = new Guid("714b19c9-7c5b-47f9-9a99-46639db1595d"),
                    UserId = new Guid("05541516-ccf1-41f4-b6ac-b2aa7d807b8c"),
                    Balance = 2000
                }};

            _db.Setup(x => x.Accounts).Returns(_accounts);
        }

        [TestMethod]
        public async Task RemoveExistingAccount()
        {
            var command = new RemoveAccountCommand
            {
                AccountIds = new List<Guid>
                {
                    new Guid("714b19c9-7c5b-47f9-9a99-46639db1595d")
                }
            };
            _db.Setup(x => x.SaveChangesAsync()).ReturnsAsync(1);

            var result = await _handler.Execute(command);

            Assert.IsNotNull(result);
            Assert.AreEqual(result.EffectiveRows, 1);
            Assert.AreEqual(_accounts.Count, 0);
        }

        [TestMethod]
        public async Task DontRemoveAccount()
        {
            var command = new RemoveAccountCommand
            {
                AccountIds = new List<Guid>
                {
                    Guid.NewGuid()
                }
            };
            _db.Setup(x => x.SaveChangesAsync()).ReturnsAsync(0);

            var result = await _handler.Execute(command);

            Assert.IsNotNull(result);
            Assert.AreEqual(result.EffectiveRows, 0);
            Assert.AreEqual(_accounts.Count, 1);
        }
    }
}
