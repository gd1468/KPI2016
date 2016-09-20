namespace MoneyManagement.Persistance.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeExpenditureUserRelationship : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Expenditures", "User_KeyId", "dbo.Users");
            DropIndex("dbo.Expenditures", new[] { "User_KeyId" });
            RenameColumn(table: "dbo.Expenditures", name: "User_KeyId", newName: "UserId");
            AlterColumn("dbo.Expenditures", "UserId", c => c.Guid(nullable: false));
            CreateIndex("dbo.Expenditures", "UserId");
            AddForeignKey("dbo.Expenditures", "UserId", "dbo.Users", "KeyId", cascadeDelete: false);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Expenditures", "UserId", "dbo.Users");
            DropIndex("dbo.Expenditures", new[] { "UserId" });
            AlterColumn("dbo.Expenditures", "UserId", c => c.Guid());
            RenameColumn(table: "dbo.Expenditures", name: "UserId", newName: "User_KeyId");
            CreateIndex("dbo.Expenditures", "User_KeyId");
            AddForeignKey("dbo.Expenditures", "User_KeyId", "dbo.Users", "KeyId");
        }
    }
}
