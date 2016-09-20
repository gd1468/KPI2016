using System;
using System.Collections.Generic;
using MoneyManagement.DomainModel.Commons;

namespace MoneyManagement.DomainModel.Domain
{
    public class Account : NameEntity<AccountTranslation, Account>
    {
        public decimal Balance { get; set; }
        public Guid UserId { get; set; }
        public virtual User User { get; set; }
        public virtual List<Expenditure> Expenditures { get; set; }
    }
}
