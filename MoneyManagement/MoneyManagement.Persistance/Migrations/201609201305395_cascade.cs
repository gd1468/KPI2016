namespace MoneyManagement.Persistance.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class cascade : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Expenditures", "AccountId", "dbo.Accounts");
            DropForeignKey("dbo.Expenditures", "UserId", "dbo.Users");
            AddForeignKey("dbo.Expenditures", "AccountId", "dbo.Accounts", "KeyId");
            AddForeignKey("dbo.Expenditures", "UserId", "dbo.Users", "KeyId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Expenditures", "UserId", "dbo.Users");
            DropForeignKey("dbo.Expenditures", "AccountId", "dbo.Accounts");
            AddForeignKey("dbo.Expenditures", "UserId", "dbo.Users", "KeyId", cascadeDelete: true);
            AddForeignKey("dbo.Expenditures", "AccountId", "dbo.Accounts", "KeyId", cascadeDelete: true);
        }
    }
}
