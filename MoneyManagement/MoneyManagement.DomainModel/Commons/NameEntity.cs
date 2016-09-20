using System.Collections.Generic;

namespace MoneyManagement.DomainModel.Commons
{
    public abstract class NameEntity<TTranslationEntity, TEntity> : BaseEntity where TTranslationEntity : Translation<TEntity>, new() where TEntity : Entity
    {
        public virtual List<TTranslationEntity> Translations { get; set; }

        public string PrimaryName { get; set; }
        public string SecondName { get; set; }
    }
}
