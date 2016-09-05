using System.Data.Entity.ModelConfiguration;
using MoneyManagement.DomainModel.Domain;

namespace MoneyManagement.Persistance.Configurations
{
    public class SavingDepositAccountConfiguration:EntityTypeConfiguration<SavingDepositAccount>
    {
        public SavingDepositAccountConfiguration()
        {
            HasMany(x => x.Translations).WithRequired(x=>(SavingDepositAccount) x.TranslationOfEntity).HasForeignKey(x=>x.TranslationOfId);
        }
    }
}
