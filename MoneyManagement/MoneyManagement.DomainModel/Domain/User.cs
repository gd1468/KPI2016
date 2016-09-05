using System.Collections.Generic;
using MoneyManagement.DomainModel.Commons;
using MoneyManagement.DomainModel.Complex_types;

namespace MoneyManagement.DomainModel.Domain
{
    public class User : BaseEntity
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public UserInformation UserInformation { get; set; }
        public int AccessLevelId { get; set; }
        public virtual AccessLevel AccessLevel { get; set; }
        public virtual List<Account> Accounts { get; set; }
        public virtual List<Budget> Budgets { get; set; }
        public virtual List<Expenditure> Expenditures { get; set; }
    }
}
