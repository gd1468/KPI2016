using MoneyManagement.DomainModel.Commons;

namespace MoneyManagement.DomainModel.Domain
{
    public class AccessLevel:BaseEntity
    {
        public bool AllowAdd { get; set; }
        public bool AllowSave { get; set; }
        public bool AllowRead { get; set; }
        public bool AllowDelete { get; set; }
    }
}
