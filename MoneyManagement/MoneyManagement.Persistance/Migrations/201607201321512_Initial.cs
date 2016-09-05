namespace MoneyManagement.Persistance.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AccessLevels",
                c => new
                    {
                        KeyId = c.Guid(nullable: false, identity: true),
                        AllowAdd = c.Boolean(nullable: false),
                        AllowSave = c.Boolean(nullable: false),
                        AllowRead = c.Boolean(nullable: false),
                        AllowDelete = c.Boolean(nullable: false),
                        ShortName = c.String(),
                        LastUserId = c.Guid(nullable: false),
                        CreatedById = c.Guid(nullable: false),
                        CreatedOn = c.DateTime(nullable: false),
                        LastTime = c.DateTime(nullable: false),
                        LastModified = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                    })
                .PrimaryKey(t => t.KeyId);
            
            CreateTable(
                "dbo.Accounts",
                c => new
                    {
                        KeyId = c.Guid(nullable: false, identity: true),
                        Balance = c.Decimal(nullable: false, precision: 18, scale: 2),
                        UserId = c.Guid(nullable: false),
                        PrimaryName = c.String(),
                        SecondName = c.String(),
                        ShortName = c.String(),
                        LastUserId = c.Guid(nullable: false),
                        CreatedById = c.Guid(nullable: false),
                        CreatedOn = c.DateTime(nullable: false),
                        LastTime = c.DateTime(nullable: false),
                        LastModified = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                        StartDate = c.DateTime(),
                        InterestRate = c.Decimal(precision: 18, scale: 2),
                        Term = c.Int(),
                        Note = c.String(),
                        TransferMoneyToAccountId = c.Guid(),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.KeyId)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AccountTranslations",
                c => new
                    {
                        KeyId = c.Guid(nullable: false, identity: true),
                        CultureId = c.Guid(nullable: false),
                        TranslationOfId = c.Guid(nullable: false),
                        Name = c.String(),
                        LastUserId = c.Guid(nullable: false),
                        CreatedById = c.Guid(nullable: false),
                        CreatedOn = c.DateTime(nullable: false),
                        LastTime = c.DateTime(nullable: false),
                        LastModified = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                        Culture_KeyId = c.Guid(),
                    })
                .PrimaryKey(t => t.KeyId)
                .ForeignKey("dbo.Cultures", t => t.Culture_KeyId)
                .ForeignKey("dbo.Accounts", t => t.TranslationOfId, cascadeDelete: true)
                .Index(t => t.TranslationOfId)
                .Index(t => t.Culture_KeyId);
            
            CreateTable(
                "dbo.Cultures",
                c => new
                    {
                        KeyId = c.Guid(nullable: false, identity: true),
                        IsPrimary = c.Boolean(nullable: false),
                        ShortName = c.String(),
                        LastUserId = c.Guid(nullable: false),
                        CreatedById = c.Guid(nullable: false),
                        CreatedOn = c.DateTime(nullable: false),
                        LastTime = c.DateTime(nullable: false),
                        LastModified = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                    })
                .PrimaryKey(t => t.KeyId);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        KeyId = c.Guid(nullable: false, identity: true),
                        Username = c.String(),
                        Password = c.String(),
                        AccessLevelId = c.Int(nullable: false),
                        ShortName = c.String(),
                        LastUserId = c.Guid(nullable: false),
                        CreatedById = c.Guid(nullable: false),
                        CreatedOn = c.DateTime(nullable: false),
                        LastTime = c.DateTime(nullable: false),
                        LastModified = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                        AccessLevel_KeyId = c.Guid(),
                    })
                .PrimaryKey(t => t.KeyId)
                .ForeignKey("dbo.AccessLevels", t => t.AccessLevel_KeyId)
                .Index(t => t.AccessLevel_KeyId);
            
            CreateTable(
                "dbo.Budgets",
                c => new
                    {
                        KeyId = c.Guid(nullable: false, identity: true),
                        Total = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Expensed = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Balance = c.Decimal(nullable: false, precision: 18, scale: 2),
                        EffectiveFrom = c.DateTime(nullable: false),
                        EffectiveTo = c.DateTime(nullable: false),
                        UserId = c.Guid(nullable: false),
                        PrimaryName = c.String(),
                        SecondName = c.String(),
                        ShortName = c.String(),
                        LastUserId = c.Guid(nullable: false),
                        CreatedById = c.Guid(nullable: false),
                        CreatedOn = c.DateTime(nullable: false),
                        LastTime = c.DateTime(nullable: false),
                        LastModified = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                    })
                .PrimaryKey(t => t.KeyId)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Expenditures",
                c => new
                    {
                        KeyId = c.Guid(nullable: false, identity: true),
                        Amount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        ExpenditureDate = c.DateTime(nullable: false),
                        Description = c.String(),
                        BudgetId = c.Guid(nullable: false),
                        AccountId = c.Guid(nullable: false),
                        ShortName = c.String(),
                        LastUserId = c.Guid(nullable: false),
                        CreatedById = c.Guid(nullable: false),
                        CreatedOn = c.DateTime(nullable: false),
                        LastTime = c.DateTime(nullable: false),
                        LastModified = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                        Account_KeyId = c.Guid(nullable: false),
                        User_KeyId = c.Guid(),
                    })
                .PrimaryKey(t => t.KeyId)
                .ForeignKey("dbo.Accounts", t => t.Account_KeyId, cascadeDelete: true)
                .ForeignKey("dbo.Budgets", t => t.BudgetId)
                .ForeignKey("dbo.Users", t => t.User_KeyId)
                .Index(t => t.BudgetId)
                .Index(t => t.Account_KeyId)
                .Index(t => t.User_KeyId);
            
            CreateTable(
                "dbo.BudgetTranslations",
                c => new
                    {
                        KeyId = c.Guid(nullable: false, identity: true),
                        CultureId = c.Guid(nullable: false),
                        TranslationOfId = c.Guid(nullable: false),
                        Name = c.String(),
                        LastUserId = c.Guid(nullable: false),
                        CreatedById = c.Guid(nullable: false),
                        CreatedOn = c.DateTime(nullable: false),
                        LastTime = c.DateTime(nullable: false),
                        LastModified = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                        Culture_KeyId = c.Guid(),
                    })
                .PrimaryKey(t => t.KeyId)
                .ForeignKey("dbo.Cultures", t => t.Culture_KeyId)
                .ForeignKey("dbo.Budgets", t => t.TranslationOfId, cascadeDelete: true)
                .Index(t => t.TranslationOfId)
                .Index(t => t.Culture_KeyId);
            
            CreateTable(
                "dbo.Resources",
                c => new
                    {
                        KeyId = c.Guid(nullable: false, identity: true),
                        ResourceText = c.String(),
                        LastUserId = c.Guid(nullable: false),
                        CreatedById = c.Guid(nullable: false),
                        CreatedOn = c.DateTime(nullable: false),
                        LastTime = c.DateTime(nullable: false),
                        LastModified = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                    })
                .PrimaryKey(t => t.KeyId);
            
            CreateTable(
                "dbo.StringResources",
                c => new
                    {
                        KeyId = c.Guid(nullable: false, identity: true),
                        DefaultValue = c.String(),
                        ResourceId = c.Guid(nullable: false),
                        CultureId = c.Guid(nullable: false),
                        LastUserId = c.Guid(nullable: false),
                        CreatedById = c.Guid(nullable: false),
                        CreatedOn = c.DateTime(nullable: false),
                        LastTime = c.DateTime(nullable: false),
                        LastModified = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                        Culture_KeyId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.KeyId)
                .ForeignKey("dbo.Cultures", t => t.Culture_KeyId, cascadeDelete: true)
                .ForeignKey("dbo.Resources", t => t.ResourceId, cascadeDelete: true)
                .Index(t => t.ResourceId)
                .Index(t => t.Culture_KeyId);
            
            CreateTable(
                "dbo.UserDetails",
                c => new
                    {
                        KeyId = c.Guid(nullable: false),
                        FirstName = c.String(),
                        LastName = c.String(),
                        FullName = c.String(),
                        Street = c.String(),
                        AddressNumber = c.String(),
                    })
                .PrimaryKey(t => t.KeyId)
                .ForeignKey("dbo.Users", t => t.KeyId)
                .Index(t => t.KeyId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserDetails", "KeyId", "dbo.Users");
            DropForeignKey("dbo.StringResources", "ResourceId", "dbo.Resources");
            DropForeignKey("dbo.StringResources", "Culture_KeyId", "dbo.Cultures");
            DropForeignKey("dbo.Expenditures", "User_KeyId", "dbo.Users");
            DropForeignKey("dbo.Budgets", "UserId", "dbo.Users");
            DropForeignKey("dbo.BudgetTranslations", "TranslationOfId", "dbo.Budgets");
            DropForeignKey("dbo.BudgetTranslations", "Culture_KeyId", "dbo.Cultures");
            DropForeignKey("dbo.Expenditures", "BudgetId", "dbo.Budgets");
            DropForeignKey("dbo.Expenditures", "Account_KeyId", "dbo.Accounts");
            DropForeignKey("dbo.Accounts", "UserId", "dbo.Users");
            DropForeignKey("dbo.Users", "AccessLevel_KeyId", "dbo.AccessLevels");
            DropForeignKey("dbo.AccountTranslations", "TranslationOfId", "dbo.Accounts");
            DropForeignKey("dbo.AccountTranslations", "Culture_KeyId", "dbo.Cultures");
            DropIndex("dbo.UserDetails", new[] { "KeyId" });
            DropIndex("dbo.StringResources", new[] { "Culture_KeyId" });
            DropIndex("dbo.StringResources", new[] { "ResourceId" });
            DropIndex("dbo.BudgetTranslations", new[] { "Culture_KeyId" });
            DropIndex("dbo.BudgetTranslations", new[] { "TranslationOfId" });
            DropIndex("dbo.Expenditures", new[] { "User_KeyId" });
            DropIndex("dbo.Expenditures", new[] { "Account_KeyId" });
            DropIndex("dbo.Expenditures", new[] { "BudgetId" });
            DropIndex("dbo.Budgets", new[] { "UserId" });
            DropIndex("dbo.Users", new[] { "AccessLevel_KeyId" });
            DropIndex("dbo.AccountTranslations", new[] { "Culture_KeyId" });
            DropIndex("dbo.AccountTranslations", new[] { "TranslationOfId" });
            DropIndex("dbo.Accounts", new[] { "UserId" });
            DropTable("dbo.UserDetails");
            DropTable("dbo.StringResources");
            DropTable("dbo.Resources");
            DropTable("dbo.BudgetTranslations");
            DropTable("dbo.Expenditures");
            DropTable("dbo.Budgets");
            DropTable("dbo.Users");
            DropTable("dbo.Cultures");
            DropTable("dbo.AccountTranslations");
            DropTable("dbo.Accounts");
            DropTable("dbo.AccessLevels");
        }
    }
}
