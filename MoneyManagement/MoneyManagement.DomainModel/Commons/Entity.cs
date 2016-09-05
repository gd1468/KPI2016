using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MoneyManagement.DomainModel.Interfaces;

namespace MoneyManagement.DomainModel.Commons
{
    public abstract class Entity:IEntity
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public Guid KeyId { get; set; }

        public Guid LastUserId { get; set; }

        public Guid CreatedById { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime LastTime { get; set; }

        [Timestamp]
        public virtual Byte[] LastModified { get; set; }
    }
}
