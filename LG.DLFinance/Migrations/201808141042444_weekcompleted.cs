namespace LG.DLFinance.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class weekcompleted : DbMigration
    {
        public override void Up()
        {
            AddColumn("DL.Week", "WeekCompleted", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("DL.Week", "WeekCompleted");
        }
    }
}
