namespace LG.DLFinance.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class linkweek : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("DL.Transaction", "Week_weekNo", "DL.Week");
            DropIndex("DL.Transaction", new[] { "Week_weekNo" });
            RenameColumn(table: "DL.Transaction", name: "Week_weekNo", newName: "WeekId");
            AlterColumn("DL.Transaction", "WeekId", c => c.Int(nullable: false));
            CreateIndex("DL.Transaction", "WeekId");
            AddForeignKey("DL.Transaction", "WeekId", "DL.Week", "weekNo", cascadeDelete: false);
        }
        
        public override void Down()
        {
            DropForeignKey("DL.Transaction", "WeekId", "DL.Week");
            DropIndex("DL.Transaction", new[] { "WeekId" });
            AlterColumn("DL.Transaction", "WeekId", c => c.Int());
            RenameColumn(table: "DL.Transaction", name: "WeekId", newName: "Week_weekNo");
            CreateIndex("DL.Transaction", "Week_weekNo");
            AddForeignKey("DL.Transaction", "Week_weekNo", "DL.Week", "weekNo");
        }
    }
}
