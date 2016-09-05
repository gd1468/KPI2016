using System.Data.Entity.ModelConfiguration;
using MoneyManagement.DomainModel.Domain;

namespace MoneyManagement.Persistance.Configurations
{
    public class BudgetConfiguration : EntityTypeConfiguration<Budget>
    {
        public BudgetConfiguration()
        {
            HasMany(x => x.Translations).WithRequired(x => x.TranslationOfEntity).HasForeignKey(x => x.TranslationOfId);
        }
    }
}
