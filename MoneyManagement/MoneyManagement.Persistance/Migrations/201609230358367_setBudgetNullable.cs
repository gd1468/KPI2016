namespace MoneyManagement.Persistance.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class setBudgetNullable : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Expenditures", new[] { "BudgetId" });
            AlterColumn("dbo.Expenditures", "BudgetId", c => c.Guid());
            CreateIndex("dbo.Expenditures", "BudgetId");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Expenditures", new[] { "BudgetId" });
            AlterColumn("dbo.Expenditures", "BudgetId", c => c.Guid(nullable: false));
            CreateIndex("dbo.Expenditures", "BudgetId");
        }
    }
}
