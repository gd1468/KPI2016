namespace MoneyManagement.Persistance.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveDepositAccount : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Expenditures", "AccountId", "dbo.Accounts");
            DropForeignKey("dbo.Expenditures", "BudgetId", "dbo.Budgets");
            DropForeignKey("dbo.Expenditures", "UserId", "dbo.Users");
            AddColumn("dbo.Expenditures", "Discriminator", c => c.String(nullable: false, maxLength: 128));
            AddForeignKey("dbo.Expenditures", "AccountId", "dbo.Accounts", "KeyId", cascadeDelete: false);
            AddForeignKey("dbo.Expenditures", "BudgetId", "dbo.Budgets", "KeyId", cascadeDelete: false);
            AddForeignKey("dbo.Expenditures", "UserId", "dbo.Users", "KeyId", cascadeDelete: false);
            DropColumn("dbo.Accounts", "StartDate");
            DropColumn("dbo.Accounts", "InterestRate");
            DropColumn("dbo.Accounts", "Term");
            DropColumn("dbo.Accounts", "Note");
            DropColumn("dbo.Accounts", "TransferMoneyToAccountId");
            DropColumn("dbo.Accounts", "Discriminator");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Accounts", "Discriminator", c => c.String(nullable: false, maxLength: 128));
            AddColumn("dbo.Accounts", "TransferMoneyToAccountId", c => c.Guid());
            AddColumn("dbo.Accounts", "Note", c => c.String());
            AddColumn("dbo.Accounts", "Term", c => c.Int());
            AddColumn("dbo.Accounts", "InterestRate", c => c.Decimal(precision: 18, scale: 2));
            AddColumn("dbo.Accounts", "StartDate", c => c.DateTime());
            DropForeignKey("dbo.Expenditures", "UserId", "dbo.Users");
            DropForeignKey("dbo.Expenditures", "BudgetId", "dbo.Budgets");
            DropForeignKey("dbo.Expenditures", "AccountId", "dbo.Accounts");
            DropColumn("dbo.Expenditures", "Discriminator");
            AddForeignKey("dbo.Expenditures", "UserId", "dbo.Users", "KeyId");
            AddForeignKey("dbo.Expenditures", "BudgetId", "dbo.Budgets", "KeyId");
            AddForeignKey("dbo.Expenditures", "AccountId", "dbo.Accounts", "KeyId");
        }
    }
}
