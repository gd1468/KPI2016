using System;
using MoneyManagement.DomainModel.Commons;

namespace MoneyManagement.DomainModel.Domain
{
    public class Expenditure : BaseEntity
    {
        public decimal Amount { get; set; }
        public DateTime ExpenditureDate { get; set; }
        public string Description { get; set; }
        public Guid? BudgetId { get; set; }
        public virtual Budget Budget { get; set; }
        public Guid AccountId { get; set; }
        public virtual Account Account { get; set; }
        public User User { get; set; }
        public Guid UserId { get; set; }
    }
}
