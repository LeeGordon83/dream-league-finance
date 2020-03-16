namespace LG.DLFinance.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class cupbool : DbMigration
    {
        public override void Up()
        {
            AddColumn("DL.Prizes", "CupPrize", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("DL.Prizes", "CupPrize");
        }
    }
}
