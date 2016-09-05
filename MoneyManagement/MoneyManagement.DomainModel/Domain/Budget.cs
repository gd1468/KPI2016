using System;
using System.Collections.Generic;
using MoneyManagement.DomainModel.Commons;

namespace MoneyManagement.DomainModel.Domain
{
    public class Budget : NameEntity<BudgetTranslation, Budget>
    {
        public decimal Total { get; set; }
        public decimal Expensed { get; set; }
        public decimal Balance { get; set; }
        public DateTime EffectiveFrom { get; set; }
        public DateTime EffectiveTo { get; set; }
        public Guid UserId { get; set; }
        public virtual User User { get; set; }
        public virtual List<Expenditure> Expenditures { get; set; }
    }
}
