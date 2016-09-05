using System.Data.Entity.ModelConfiguration;
using MoneyManagement.DomainModel.Domain;

namespace MoneyManagement.Persistance.Configurations
{
    public class AccountConfiguration:EntityTypeConfiguration<Account>
    {
        public AccountConfiguration()
        {
            HasMany(x => x.Translations).WithRequired(x => x.TranslationOfEntity).HasForeignKey(x => x.TranslationOfId);
        }
    }
}
