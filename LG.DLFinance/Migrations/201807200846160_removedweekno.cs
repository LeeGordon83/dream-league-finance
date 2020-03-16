namespace LG.DLFinance.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class removedweekno : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("DL.Transaction", "WeekNo", "DL.Week");
            DropIndex("DL.Transaction", new[] { "WeekNo" });
            RenameColumn(table: "DL.Transaction", name: "WeekNo", newName: "Week_weekNo");
            AlterColumn("DL.Transaction", "Week_weekNo", c => c.Int());
            CreateIndex("DL.Transaction", "Week_weekNo");
            AddForeignKey("DL.Transaction", "Week_weekNo", "DL.Week", "weekNo");
        }
        
        public override void Down()
        {
            DropForeignKey("DL.Transaction", "Week_weekNo", "DL.Week");
            DropIndex("DL.Transaction", new[] { "Week_weekNo" });
            AlterColumn("DL.Transaction", "Week_weekNo", c => c.Int(nullable: false));
            RenameColumn(table: "DL.Transaction", name: "Week_weekNo", newName: "WeekNo");
            CreateIndex("DL.Transaction", "WeekNo");
            AddForeignKey("DL.Transaction", "WeekNo", "DL.Week", "weekNo", cascadeDelete: true);
        }
    }
}
