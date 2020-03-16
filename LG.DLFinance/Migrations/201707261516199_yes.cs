namespace LG.DLFinance.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class yes : DbMigration
    {
        public override void Up()
        {
            AddColumn("DL.Fees", "FeeType", c => c.String());
            AddColumn("DL.Fees", "FeeAmount", c => c.Int(nullable: false));
            DropColumn("DL.Fees", "WeeklyFee");
            DropColumn("DL.Fees", "CupFee");
            DropColumn("DL.Fees", "LeagueCupFee");
            DropColumn("DL.Fees", "LeagueEntryFee");
        }
        
        public override void Down()
        {
            AddColumn("DL.Fees", "LeagueEntryFee", c => c.Int(nullable: false));
            AddColumn("DL.Fees", "LeagueCupFee", c => c.Int(nullable: false));
            AddColumn("DL.Fees", "CupFee", c => c.Int(nullable: false));
            AddColumn("DL.Fees", "WeeklyFee", c => c.Int(nullable: false));
            DropColumn("DL.Fees", "FeeAmount");
            DropColumn("DL.Fees", "FeeType");
        }
    }
}
