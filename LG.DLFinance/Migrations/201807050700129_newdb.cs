namespace LG.DLFinance.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class newdb : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "DL.Account",
                c => new
                    {
                        AccountId = c.Guid(nullable: false),
                        Balance = c.Int(nullable: false),
                        Active = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.AccountId);
            
            CreateTable(
                "DL.Emails",
                c => new
                    {
                        EmailsId = c.Guid(nullable: false),
                        ManagerId = c.Guid(nullable: false),
                        EmailAddress = c.String(),
                        Primary = c.Boolean(nullable: false),
                        Manager_AccountId = c.Guid(),
                    })
                .PrimaryKey(t => t.EmailsId)
                .ForeignKey("DL.Manager", t => t.Manager_AccountId)
                .Index(t => t.Manager_AccountId);
            
            CreateTable(
                "DL.Fees",
                c => new
                    {
                        FeesId = c.Guid(nullable: false),
                        FeeType = c.String(),
                        FeeAmount = c.Int(nullable: false),
                        FrequencyId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.FeesId)
                .ForeignKey("DL.Frequency", t => t.FrequencyId, cascadeDelete: true)
                .Index(t => t.FrequencyId);
            
            CreateTable(
                "DL.Frequency",
                c => new
                    {
                        FrequencyId = c.Guid(nullable: false),
                        FrequencyName = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.FrequencyId);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "DL.Season",
                c => new
                    {
                        SeasonId = c.Guid(nullable: false),
                        SeasonYear = c.String(),
                        SeasonActive = c.Boolean(nullable: false),
                        SeasonStart = c.DateTime(nullable: false),
                        SeasonEnd = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.SeasonId);
            
            CreateTable(
                "DL.Transaction",
                c => new
                    {
                        TransactionId = c.Guid(nullable: false),
                        Value = c.Decimal(nullable: false, precision: 18, scale: 2),
                        WeekNo = c.Int(nullable: false),
                        TransactionDate = c.DateTime(nullable: false),
                        Notes = c.String(),
                        AccountId = c.Guid(nullable: false),
                        SeasonId = c.Guid(nullable: false),
                        TransType_TransactionTypeId = c.Guid(),
                    })
                .PrimaryKey(t => t.TransactionId)
                .ForeignKey("DL.Account", t => t.AccountId, cascadeDelete: true)
                .ForeignKey("DL.Season", t => t.SeasonId, cascadeDelete: true)
                .ForeignKey("DL.TransactionType", t => t.TransType_TransactionTypeId)
                .Index(t => t.AccountId)
                .Index(t => t.SeasonId)
                .Index(t => t.TransType_TransactionTypeId);
            
            CreateTable(
                "DL.TransactionType",
                c => new
                    {
                        TransactionTypeId = c.Guid(nullable: false),
                        TransactionName = c.String(),
                        Active = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.TransactionTypeId);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "DL.Manager",
                c => new
                    {
                        AccountId = c.Guid(nullable: false),
                        Season_SeasonId = c.Guid(),
                        ManagerId = c.Guid(nullable: false),
                        ManagerName = c.String(),
                    })
                .PrimaryKey(t => t.AccountId)
                .ForeignKey("DL.Account", t => t.AccountId)
                .ForeignKey("DL.Season", t => t.Season_SeasonId)
                .Index(t => t.AccountId)
                .Index(t => t.Season_SeasonId);
            
            CreateTable(
                "DL.Jackpot",
                c => new
                    {
                        AccountId = c.Guid(nullable: false),
                        JackpotStartWk = c.Int(nullable: false),
                        JackpotEndWk = c.Int(nullable: false),
                        JackpotValue = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.AccountId)
                .ForeignKey("DL.Account", t => t.AccountId)
                .Index(t => t.AccountId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("DL.Jackpot", "AccountId", "DL.Account");
            DropForeignKey("DL.Manager", "Season_SeasonId", "DL.Season");
            DropForeignKey("DL.Manager", "AccountId", "DL.Account");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("DL.Transaction", "TransType_TransactionTypeId", "DL.TransactionType");
            DropForeignKey("DL.Transaction", "SeasonId", "DL.Season");
            DropForeignKey("DL.Transaction", "AccountId", "DL.Account");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("DL.Fees", "FrequencyId", "DL.Frequency");
            DropForeignKey("DL.Emails", "Manager_AccountId", "DL.Manager");
            DropIndex("DL.Jackpot", new[] { "AccountId" });
            DropIndex("DL.Manager", new[] { "Season_SeasonId" });
            DropIndex("DL.Manager", new[] { "AccountId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("DL.Transaction", new[] { "TransType_TransactionTypeId" });
            DropIndex("DL.Transaction", new[] { "SeasonId" });
            DropIndex("DL.Transaction", new[] { "AccountId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("DL.Fees", new[] { "FrequencyId" });
            DropIndex("DL.Emails", new[] { "Manager_AccountId" });
            DropTable("DL.Jackpot");
            DropTable("DL.Manager");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("DL.TransactionType");
            DropTable("DL.Transaction");
            DropTable("DL.Season");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetRoles");
            DropTable("DL.Frequency");
            DropTable("DL.Fees");
            DropTable("DL.Emails");
            DropTable("DL.Account");
        }
    }
}
