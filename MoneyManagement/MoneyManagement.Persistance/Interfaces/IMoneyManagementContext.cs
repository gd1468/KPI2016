using System.Data.Entity;
using MoneyManagement.DomainModel.Commons;
using MoneyManagement.DomainModel.Domain;

namespace MoneyManagement.Persistance.Interfaces
{
    interface IMoneyManagementContext:IDbContext
    {
        IDbSet<User> Users { get; set; }
        IDbSet<Account> Accounts { get; set; }
        IDbSet<AccountTranslation> AccountTranslations { get; set; }
        IDbSet<Expenditure> Expenditures { get; set; }
        IDbSet<Budget> Budgets { get; set; }
        IDbSet<BudgetTranslation> BudgetTranslations { get; set; }
        IDbSet<Culture> Cultures { get; set; }
        IDbSet<AccessLevel> AccessLevels { get; set; }
        IDbSet<Resource> Resources { get; set; }
        IDbSet<StringResource> StringResources { get; set; }
    }
}
