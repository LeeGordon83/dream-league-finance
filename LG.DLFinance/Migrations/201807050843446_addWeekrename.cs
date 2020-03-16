namespace LG.DLFinance.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addWeekrename : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Weeks", "weekEndDate", c => c.DateTime(nullable: false));
            DropColumn("dbo.Weeks", "weekeEndDate");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Weeks", "weekeEndDate", c => c.DateTime(nullable: false));
            DropColumn("dbo.Weeks", "weekEndDate");
        }
    }
}
