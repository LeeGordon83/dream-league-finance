namespace LG.DLFinance.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class historyrename : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.Histories", newName: "History");
            MoveTable(name: "dbo.History", newSchema: "DL");
        }
        
        public override void Down()
        {
            MoveTable(name: "DL.History", newSchema: "dbo");
            RenameTable(name: "dbo.History", newName: "Histories");
        }
    }
}
