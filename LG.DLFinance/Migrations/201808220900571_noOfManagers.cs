namespace LG.DLFinance.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class noOfManagers : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Histories", "numberOfManagers", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Histories", "numberOfManagers");
        }
    }
}
