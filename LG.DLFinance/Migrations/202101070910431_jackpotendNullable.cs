namespace LG.DLFinance.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class jackpotendNullable : DbMigration
    {
        public override void Up()
        {
            AlterColumn("DL.Jackpot", "JackpotEndWk", c => c.Int());
        }
        
        public override void Down()
        {
            AlterColumn("DL.Jackpot", "JackpotEndWk", c => c.Int(nullable: false));
        }
    }
}
