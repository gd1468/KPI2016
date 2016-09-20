namespace MoneyManagement.Persistance.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeExpenditureRelationship : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Expenditures", "AccountId");
            RenameColumn(table: "dbo.Expenditures", name: "Account_KeyId", newName: "AccountId");
            RenameIndex(table: "dbo.Expenditures", name: "IX_Account_KeyId", newName: "IX_AccountId");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.Expenditures", name: "IX_AccountId", newName: "IX_Account_KeyId");
            RenameColumn(table: "dbo.Expenditures", name: "AccountId", newName: "Account_KeyId");
            AddColumn("dbo.Expenditures", "AccountId", c => c.Guid(nullable: false));
        }
    }
}
