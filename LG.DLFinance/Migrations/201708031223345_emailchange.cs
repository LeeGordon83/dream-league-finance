namespace LG.DLFinance.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class emailchange : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("DL.Emails", "ManagerID", "DL.Manager");
            DropIndex("DL.Emails", new[] { "ManagerID" });
            DropPrimaryKey("DL.Emails");
            AddColumn("DL.Manager", "ManagerId", c => c.Guid(nullable: false));
            AddColumn("DL.Emails", "EmailsId", c => c.Guid(nullable: false));
            AddColumn("DL.Emails", "Manager_AccountId", c => c.Guid());
            AddPrimaryKey("DL.Emails", "EmailsId");
            CreateIndex("DL.Emails", "Manager_AccountId");
            AddForeignKey("DL.Emails", "Manager_AccountId", "DL.Manager", "AccountId");
        }
        
        public override void Down()
        {
            DropForeignKey("DL.Emails", "Manager_AccountId", "DL.Manager");
            DropIndex("DL.Emails", new[] { "Manager_AccountId" });
            DropPrimaryKey("DL.Emails");
            DropColumn("DL.Emails", "Manager_AccountId");
            DropColumn("DL.Emails", "EmailsId");
            DropColumn("DL.Manager", "ManagerId");
            AddPrimaryKey("DL.Emails", "ManagerID");
            CreateIndex("DL.Emails", "ManagerID");
            AddForeignKey("DL.Emails", "ManagerID", "DL.Manager", "AccountId");
        }
    }
}
