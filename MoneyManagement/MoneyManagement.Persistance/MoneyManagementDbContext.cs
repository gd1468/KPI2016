using System;
using System.Data.Entity;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MoneyManagement.DomainModel.Commons;
using MoneyManagement.DomainModel.Domain;
using MoneyManagement.Persistance.Configurations;
using MoneyManagement.Persistance.Interfaces;

namespace MoneyManagement.Persistance
{
    public class MoneyManagementDbContext : DbContext, IMoneyManagementContext
    {
        public IDbSet<BudgetTranslation> BudgetTranslations { get; set; }

        public IDbSet<Resource> Resources { get; set; }

        public IDbSet<StringResource> StringResources { get; set; }

        public IDbSet<AccessLevel> AccessLevels { get; set; }

        public IDbSet<Account> Accounts { get; set; }

        public IDbSet<AccountTranslation> AccountTranslations { get; set; }

        public IDbSet<Budget> Budgets { get; set; }

        public IDbSet<Culture> Cultures { get; set; }

        public IDbSet<Expenditure> Expenditures { get; set; }

        public IDbSet<User> Users { get; set; }


        public MoneyManagementDbContext(string databaseName)
            : base(databaseName)
        {
            Database.SetInitializer(new DropCreateDatabaseIfModelChanges<MoneyManagementDbContext>());
        }

        public MoneyManagementDbContext()
            : base("name=MoneyManagementDbContext")
        {
            Database.SetInitializer(new DropCreateDatabaseIfModelChanges<MoneyManagementDbContext>());
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Configurations.Add(new AccountConfiguration());
            modelBuilder.Configurations.Add(new BudgetConfiguration());
            modelBuilder.Configurations.Add(new ExpenditureConfiguration());
            modelBuilder.Configurations.Add(new ResourceConfiguration());
            modelBuilder.Configurations.Add(new StringResourceConfiguration());
            modelBuilder.Configurations.Add(new UserConfiguration());
        }
    }
}
