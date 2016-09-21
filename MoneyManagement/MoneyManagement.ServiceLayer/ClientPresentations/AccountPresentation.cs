using System;

namespace MoneyManagement.ServiceLayer.ClientPresentations
{
    public class AccountPresentation
    {
        public Guid KeyId { get; set; }
        public string DisplayName { get; set; }
        public decimal Balance { get; set; }
    }
}
