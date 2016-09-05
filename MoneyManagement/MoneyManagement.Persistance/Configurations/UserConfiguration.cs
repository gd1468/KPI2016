using System.Data.Entity.ModelConfiguration;
using MoneyManagement.DomainModel.Domain;

namespace MoneyManagement.Persistance.Configurations
{
    public class UserConfiguration:EntityTypeConfiguration<User>
    {
        public UserConfiguration()
        {
            HasMany(x => x.Accounts).WithRequired(x => x.User).HasForeignKey(x => x.UserId);
            HasMany(x => x.Budgets).WithRequired(x => x.User).HasForeignKey(x => x.UserId);
            Map(x =>
            {
                x.Properties(y=>new
                {
                    y.AccessLevelId,
                    y.CreatedById,
                    y.CreatedOn,
                    y.KeyId,
                    y.LastModified,
                    y.LastTime,
                    y.LastUserId,
                    y.Password,
                    y.ShortName,
                    y.Username
                });
                x.ToTable("Users");
            });
            Map(x =>
            {
                x.Properties(y => new
                {
                    y.KeyId,
                    y.UserInformation
                });
                x.ToTable("UserDetails");
            });
        }
    }
}
