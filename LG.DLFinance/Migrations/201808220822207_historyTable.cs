namespace LG.DLFinance.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class historyTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Histories", "year", c => c.Int(nullable: false));
            AddColumn("dbo.Histories", "mostMoney", c => c.String());
            AddColumn("dbo.Histories", "mostFivers", c => c.String());
            AddColumn("dbo.Histories", "leastFivers", c => c.String());
            AddColumn("dbo.Histories", "mostWeekly", c => c.String());
            AddColumn("dbo.Histories", "leastWeekly", c => c.String());
            AddColumn("dbo.Histories", "biggestJackpot", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Histories", "biggestJackpot");
            DropColumn("dbo.Histories", "leastWeekly");
            DropColumn("dbo.Histories", "mostWeekly");
            DropColumn("dbo.Histories", "leastFivers");
            DropColumn("dbo.Histories", "mostFivers");
            DropColumn("dbo.Histories", "mostMoney");
            DropColumn("dbo.Histories", "year");
        }
    }
}
