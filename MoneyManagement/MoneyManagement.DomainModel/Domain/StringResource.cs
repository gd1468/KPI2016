using System;
using MoneyManagement.DomainModel.Commons;

namespace MoneyManagement.DomainModel.Domain
{
    public class StringResource:Entity
    {
        public string DefaultValue { get; set; }
        public virtual Resource Resource { get; set; }
        public Guid ResourceId { get; set; }

        public virtual Culture Culture { get; set; }
        public Guid CultureId { get; set; }
    }
}
