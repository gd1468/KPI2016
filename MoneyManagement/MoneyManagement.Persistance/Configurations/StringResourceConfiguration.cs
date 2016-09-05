using System.Data.Entity.ModelConfiguration;
using MoneyManagement.DomainModel.Domain;

namespace MoneyManagement.Persistance.Configurations
{
    public class StringResourceConfiguration:EntityTypeConfiguration<StringResource>
    {
        public StringResourceConfiguration()
        {
            HasRequired(x => x.Culture);
            Property(x => x.CultureId).IsRequired();
        }
    }
}
