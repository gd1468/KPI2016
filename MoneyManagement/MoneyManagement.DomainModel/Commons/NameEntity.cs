using System.Collections.Generic;

namespace MoneyManagement.DomainModel.Commons
{
    public abstract class NameEntity<TTranslationEntity, TEntity>:BaseEntity where TTranslationEntity : Translation<TEntity>, new() where TEntity : Entity
    {
        private List<TTranslationEntity> _translation;
        public virtual List<TTranslationEntity> Translations
        {
            get { return _translation ?? new List<TTranslationEntity>(); }
            set { _translation = value ?? new List<TTranslationEntity>(); }
        }

        public string PrimaryName { get; set; }
        public string SecondName { get; set; }
    }
}
