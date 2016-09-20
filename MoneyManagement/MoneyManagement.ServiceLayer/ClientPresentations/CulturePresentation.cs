using System;

namespace MoneyManagement.ServiceLayer.ClientPresentations
{
    public class CulturePresentation
    {
        public Guid KeyId { get; set; }
        public bool IsPrimary { get; set; }
        public string ShortName { get; set; }
    }
}
