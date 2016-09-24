using System;

namespace MoneyManagement.ServiceLayer.ClientPresentations
{
    public class ExpenditurePresentation
    {
        public Guid KeyId { get; set; }
        public decimal Amount { get; set; }
        public ObjectReference Budget { get; set; }
        public ObjectReference Account { get; set; }
        public DateTime ExpenditureDate { get; set; }
        public string Description { get; set; }

        public bool IsIncome => Budget == null;
    }
}
