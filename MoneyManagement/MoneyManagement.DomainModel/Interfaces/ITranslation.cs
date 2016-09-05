using System;
using MoneyManagement.DomainModel.Commons;

namespace MoneyManagement.DomainModel.Interfaces
{
    public interface ITranslation
    {
        Culture Culture { get; set; }
        Guid CultureId { get; set; }
        Guid TranslationOfId { get; set; }
    }
}