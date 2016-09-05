using System;
using MoneyManagement.DomainModel.Interfaces;

namespace MoneyManagement.DomainModel.Commons
{
    public class Translation:Entity,ITranslation
    {
        public Culture Culture { get; set; }
        public Guid CultureId { get; set; }
        public Guid TranslationOfId { get; set; }
        public string Name { get; set; }
    }
}
