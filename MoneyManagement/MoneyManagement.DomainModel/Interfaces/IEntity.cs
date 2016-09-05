using System;

namespace MoneyManagement.DomainModel.Interfaces
{
    public interface IEntity
    {
        Guid KeyId { get; set; }
        Guid LastUserId { get; set; }

        Guid CreatedById { get; set; }

        DateTime CreatedOn { get; set; }

        DateTime LastTime { get; set; }

        Byte[] LastModified { get; set; }
    }
}