using System;
using System.Collections.Generic;
using MoneyManagement.DomainModel.Commons;

namespace MoneyManagement.DomainModel.Domain
{
    public class Resource:Entity
    {
        public string ResourceText { get; set; }
        public virtual List<StringResource> StringResources { get; set; } 
    }
}
