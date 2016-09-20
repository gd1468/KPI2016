using System.Data.Entity.ModelConfiguration;
using MoneyManagement.DomainModel.Domain;

namespace MoneyManagement.Persistance.Configurations
{
    public class ExpenditureConfiguration : EntityTypeConfiguration<Expenditure>
    {
        public ExpenditureConfiguration()
        {
            HasRequired(x => x.Budget).WithMany(x => x.Expenditures).HasForeignKey(x => x.BudgetId).WillCascadeOnDelete(false);
            HasRequired(x => x.Account).WithMany(x => x.Expenditures).HasForeignKey(x => x.AccountId).WillCascadeOnDelete(false);
            HasRequired(x => x.User).WithMany(x => x.Expenditures).HasForeignKey(x => x.UserId).WillCascadeOnDelete(false);
            Property(x => x.Amount).IsRequired();
        }
    }
}
