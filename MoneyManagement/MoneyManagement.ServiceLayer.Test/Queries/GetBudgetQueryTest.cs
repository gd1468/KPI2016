using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MoneyManagement.DomainModel.Domain;
using MoneyManagement.Persistance.Interfaces;
using MoneyManagement.ServiceLayer.Queries;
using MoneyManagement.ServiceLayer.Test.FakeDb;
using Moq;

namespace MoneyManagement.ServiceLayer.Test.Queries
{
    [TestClass]
    public class GetBudgetQueryTest
    {
        private Mock<IMoneyManagementContext> _db;
        private GetBudgetQueryHandler _handler;

        [TestInitialize]
        public void Setup()
        {
            _db = new Mock<IMoneyManagementContext>();
            _handler = new GetBudgetQueryHandler(_db.Object);

            var budgets = new List<Budget>
            {
                new Budget
                {
                    KeyId = new Guid("714b19c9-7c5b-47f9-9a99-46639db1595d"),
                    UserId = new Guid("05541516-ccf1-41f4-b6ac-b2aa7d807b8c"),
                    Balance = 2000,
                    Expensed = 1000,
                    Total = 3000,
                    EffectiveFrom = DateTime.Today,
                    EffectiveTo = DateTime.Today,
                    ShortName = "LNC",
                    Translations = new List<BudgetTranslation>
                    {
                        new BudgetTranslation
                        {
                            CultureId = new Guid("714b19c9-7c5b-47f9-9a99-46639db1595d"),
                            Name = "Lunch"
                        }
                    }
                }
            };

            _db.Setup(x => x.Budgets).Returns(budgets.ToMockDbSet().Object);
        }

        [TestMethod]
        public async Task GetListBudget()
        {
            var query = new GetBudgetQuery
            {
                UserId = new Guid("05541516-ccf1-41f4-b6ac-b2aa7d807b8c"),
                CultureId = new Guid("714b19c9-7c5b-47f9-9a99-46639db1595d")
            };

            var result = await _handler.Execute(query);

            Assert.IsNotNull(result);
            Assert.AreEqual(result.BudgetPresentations.Count, 1);

            var budget = result.BudgetPresentations.First();
            Assert.AreEqual(budget.KeyId, new Guid("714b19c9-7c5b-47f9-9a99-46639db1595d"));
            Assert.AreEqual(budget.Balance, 2000);
            Assert.AreEqual(budget.DisplayName, "[LNC] Lunch");
            Assert.AreEqual(budget.EndDate, DateTime.Today);
            Assert.AreEqual(budget.StartDate, DateTime.Today);
            Assert.AreEqual(budget.Expensed, 1000);
            Assert.AreEqual(budget.Balance, 2000);
            Assert.AreEqual(budget.Total, 3000);
        }
    }
}
