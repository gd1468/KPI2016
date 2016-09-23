using System;

namespace MoneyManagement.ServiceLayer.ClientPresentations
{
    public class BudgetPresentation
    {
        public Guid KeyId { get; set; }
        public string DisplayName { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public decimal Total { get; set; }
        public decimal Balance { get; set; }
        public decimal Expensed { get; set; }
    }
}
