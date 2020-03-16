namespace LG.DLFinance.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class removeManId : DbMigration
    {
        public override void Up()
        {
            DropColumn("DL.Manager", "ManagerId");
        }
        
        public override void Down()
        {
            AddColumn("DL.Manager", "ManagerId", c => c.Guid(nullable: false));
        }
    }
}
