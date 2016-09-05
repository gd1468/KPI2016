namespace MoneyManagement.DomainModel.Commons
{
    public class Translation<TEntity>:Translation where TEntity:Entity
    {
        public TEntity TranslationOfEntity { get; set; }
    }
}
