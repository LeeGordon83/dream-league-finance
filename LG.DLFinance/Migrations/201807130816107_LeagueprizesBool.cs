namespace LG.DLFinance.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class LeagueprizesBool : DbMigration
    {
        public override void Up()
        {
            AddColumn("DL.Prizes", "LeaguePrize", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("DL.Prizes", "LeaguePrize");
        }
    }
}
