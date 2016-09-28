using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MoneyManagement.DomainModel.Domain;
using MoneyManagement.Persistance.Interfaces;
using MoneyManagement.ServiceLayer.Queries;
using MoneyManagement.ServiceLayer.Test.FakeDb;
using Moq;
using System.Threading.Tasks;

namespace MoneyManagement.ServiceLayer.Test.Queries
{
    [TestClass]
    public class GetAccountQueryTest
    {
        private Mock<IMoneyManagementContext> _db;
        private GetAccountQueryHandler _handler;

        [TestInitialize]
        public void Setup()
        {
            _db = new Mock<IMoneyManagementContext>();
            _handler = new GetAccountQueryHandler(_db.Object);

            var accounts = new List<Account>
            {
                new Account
                {
                    KeyId = new Guid("714b19c9-7c5b-47f9-9a99-46639db1595d"),
                    UserId = new Guid("05541516-ccf1-41f4-b6ac-b2aa7d807b8c"),
                    Balance = 2000,
                    ShortName = "BNK",
                    Translations = new List<AccountTranslation>
                    {
                        new AccountTranslation
                        {
                            CultureId = new Guid("714b19c9-7c5b-47f9-9a99-46639db1595d"),
                            Name = "Bank"
                        }
                    }

                }
            };

            _db.Setup(x => x.Accounts).Returns(accounts.ToMockDbSet().Object);
        }

        [TestMethod]
        public async Task GetListAccount()
        {
            var query = new GetAccountQuery
            {
                UserId = new Guid("05541516-ccf1-41f4-b6ac-b2aa7d807b8c"),
                CultureId = new Guid("714b19c9-7c5b-47f9-9a99-46639db1595d")
            };

            var result = await _handler.Execute(query);

            Assert.IsNotNull(result);
            Assert.AreEqual(result.AccountPresentations.Count, 1);

            var account = result.AccountPresentations.First();
            Assert.AreEqual(account.KeyId, new Guid("714b19c9-7c5b-47f9-9a99-46639db1595d"));
            Assert.AreEqual(account.Balance,2000);
            Assert.AreEqual(account.DisplayName,"[BNK] Bank");
        }
    }
}
