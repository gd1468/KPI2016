using System;

namespace MoneyManagement.DomainModel.Domain
{
    public class SavingDepositAccount:Account
    {
        public DateTime StartDate { get; set; }
        public decimal InterestRate { get; set; }
        public int Term { get; set; }
        public string Note { get; set; }
        public Guid TransferMoneyToAccountId { get; set; }
    }
}
