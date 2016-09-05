using System.Data.Entity.ModelConfiguration;
using MoneyManagement.DomainModel.Domain;

namespace MoneyManagement.Persistance.Configurations
{
    public class ResourceConfiguration:EntityTypeConfiguration<Resource>
    {
        public ResourceConfiguration()
        {
            HasMany(x => x.StringResources).WithRequired(x => x.Resource).HasForeignKey(x => x.ResourceId);
        }
    }
}
