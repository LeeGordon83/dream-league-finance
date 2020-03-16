namespace LG.DLFinance.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addmanager : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("DL.Emails", "ManagerId", "DL.Manager");
            DropIndex("DL.Emails", new[] { "ManagerId" });
            RenameColumn(table: "DL.Emails", name: "ManagerId", newName: "Manager_ManagerId");
            AlterColumn("DL.Emails", "Manager_ManagerId", c => c.Guid());
            CreateIndex("DL.Emails", "Manager_ManagerId");
            AddForeignKey("DL.Emails", "Manager_ManagerId", "DL.Manager", "ManagerId");
        }
        
        public override void Down()
        {
            DropForeignKey("DL.Emails", "Manager_ManagerId", "DL.Manager");
            DropIndex("DL.Emails", new[] { "Manager_ManagerId" });
            AlterColumn("DL.Emails", "Manager_ManagerId", c => c.Guid(nullable: false));
            RenameColumn(table: "DL.Emails", name: "Manager_ManagerId", newName: "ManagerId");
            CreateIndex("DL.Emails", "ManagerId");
            AddForeignKey("DL.Emails", "ManagerId", "DL.Manager", "ManagerId", cascadeDelete: true);
        }
    }
}
