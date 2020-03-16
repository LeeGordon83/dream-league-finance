namespace LG.DLFinance.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class newbuild : DbMigration
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
                        ManagerID = c.Guid(nullable: false),
                        EmailAddress = c.String(),
                        Primary = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ManagerID)
                .ForeignKey("DL.Manager", t => t.ManagerID)
                .Index(t => t.ManagerID);
            
            CreateTable(
                "DL.Fees",
                c => new
                    {
                        FeesId = c.Guid(nullable: false),
                        WeeklyFee = c.Int(nullable: false),
                        CupFee = c.Int(nullable: false),
                        LeagueCupFee = c.Int(nullable: false),
                        LeagueEntryFee = c.Int(nullable: false),
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
                        Value = c.Int(nullable: false),
                        WeekNo = c.Int(nullable: false),
                        MonthPaid = c.String(),
                        TransactionDate = c.DateTime(nullable: false),
                        Notes = c.String(),
                        AccountId = c.Guid(nullable: false),
                        SeasonId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.TransactionId)
                .ForeignKey("DL.Account", t => t.AccountId, cascadeDelete: true)
                .ForeignKey("DL.Season", t => t.SeasonId, cascadeDelete: true)
                .Index(t => t.AccountId)
                .Index(t => t.SeasonId);
            
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
                "DL.TransactionType",
                c => new
                    {
                        TransactionId = c.Guid(nullable: false),
                        TransactionTypeId = c.Guid(nullable: false),
                        TransactionName = c.String(),
                        Active = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.TransactionId)
                .ForeignKey("DL.Transaction", t => t.TransactionId)
                .Index(t => t.TransactionId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("DL.TransactionType", "TransactionId", "DL.Transaction");
            DropForeignKey("DL.Manager", "Season_SeasonId", "DL.Season");
            DropForeignKey("DL.Manager", "AccountId", "DL.Account");
            DropForeignKey("DL.Jackpot", "AccountId", "DL.Account");
            DropForeignKey("DL.Transaction", "SeasonId", "DL.Season");
            DropForeignKey("DL.Transaction", "AccountId", "DL.Account");
            DropForeignKey("DL.Fees", "FrequencyId", "DL.Frequency");
            DropForeignKey("DL.Emails", "ManagerID", "DL.Manager");
            DropIndex("DL.TransactionType", new[] { "TransactionId" });
            DropIndex("DL.Manager", new[] { "Season_SeasonId" });
            DropIndex("DL.Manager", new[] { "AccountId" });
            DropIndex("DL.Jackpot", new[] { "AccountId" });
            DropIndex("DL.Transaction", new[] { "SeasonId" });
            DropIndex("DL.Transaction", new[] { "AccountId" });
            DropIndex("DL.Fees", new[] { "FrequencyId" });
            DropIndex("DL.Emails", new[] { "ManagerID" });
            DropTable("DL.TransactionType");
            DropTable("DL.Manager");
            DropTable("DL.Jackpot");
            DropTable("DL.Transaction");
            DropTable("DL.Season");
            DropTable("DL.Frequency");
            DropTable("DL.Fees");
            DropTable("DL.Emails");
            DropTable("DL.Account");
        }
    }
}
