namespace LG.DLFinance.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class again : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.Weeks", newName: "Week");
            MoveTable(name: "dbo.Week", newSchema: "DL");
        }
        
        public override void Down()
        {
            MoveTable(name: "DL.Week", newSchema: "dbo");
            RenameTable(name: "dbo.Week", newName: "Weeks");
        }
    }
}
