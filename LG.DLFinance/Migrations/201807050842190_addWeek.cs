namespace LG.DLFinance.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addWeek : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("DL.Fees", "FrequencyId", "DL.Frequency");
            DropForeignKey("DL.Transaction", "SeasonId", "DL.Season");
            DropForeignKey("DL.Transaction", "TransType_TransactionTypeId", "DL.TransactionType");
            DropForeignKey("DL.Manager", "Season_SeasonId", "DL.Season");
            DropIndex("DL.Fees", new[] { "FrequencyId" });
            DropIndex("DL.Transaction", new[] { "SeasonId" });
            DropIndex("DL.Transaction", new[] { "TransType_TransactionTypeId" });
            DropIndex("DL.Manager", new[] { "Season_SeasonId" });
            CreateTable(
                "dbo.Weeks",
                c => new
                    {
                        weekNo = c.Int(nullable: false, identity: true),
                        weekStartDate = c.DateTime(nullable: false),
                        weekeEndDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.weekNo);
            
            AddColumn("DL.TransactionType", "Transaction_TransactionId", c => c.Guid());
            CreateIndex("DL.Transaction", "WeekNo");
            CreateIndex("DL.TransactionType", "Transaction_TransactionId");
            AddForeignKey("DL.TransactionType", "Transaction_TransactionId", "DL.Transaction", "TransactionId");
            AddForeignKey("DL.Transaction", "WeekNo", "dbo.Weeks", "weekNo", cascadeDelete: true);
            DropColumn("DL.Manager", "Season_SeasonId");
            DropColumn("DL.Fees", "FrequencyId");
            DropColumn("DL.Transaction", "SeasonId");
            DropColumn("DL.Transaction", "TransType_TransactionTypeId");
            DropTable("DL.Frequency");
            DropTable("DL.Season");
        }
        
        public override void Down()
        {
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
                "DL.Frequency",
                c => new
                    {
                        FrequencyId = c.Guid(nullable: false),
                        FrequencyName = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.FrequencyId);
            
            AddColumn("DL.Transaction", "TransType_TransactionTypeId", c => c.Guid());
            AddColumn("DL.Transaction", "SeasonId", c => c.Guid(nullable: false));
            AddColumn("DL.Fees", "FrequencyId", c => c.Guid(nullable: false));
            AddColumn("DL.Manager", "Season_SeasonId", c => c.Guid());
            DropForeignKey("DL.Transaction", "WeekNo", "dbo.Weeks");
            DropForeignKey("DL.TransactionType", "Transaction_TransactionId", "DL.Transaction");
            DropIndex("DL.TransactionType", new[] { "Transaction_TransactionId" });
            DropIndex("DL.Transaction", new[] { "WeekNo" });
            DropColumn("DL.TransactionType", "Transaction_TransactionId");
            DropTable("dbo.Weeks");
            CreateIndex("DL.Manager", "Season_SeasonId");
            CreateIndex("DL.Transaction", "TransType_TransactionTypeId");
            CreateIndex("DL.Transaction", "SeasonId");
            CreateIndex("DL.Fees", "FrequencyId");
            AddForeignKey("DL.Manager", "Season_SeasonId", "DL.Season", "SeasonId");
            AddForeignKey("DL.Transaction", "TransType_TransactionTypeId", "DL.TransactionType", "TransactionTypeId");
            AddForeignKey("DL.Transaction", "SeasonId", "DL.Season", "SeasonId", cascadeDelete: true);
            AddForeignKey("DL.Fees", "FrequencyId", "DL.Frequency", "FrequencyId", cascadeDelete: true);
        }
    }
}
