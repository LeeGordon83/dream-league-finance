namespace LG.DLFinance.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class removeacc : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("DL.Transaction", "AccountId", "DL.Account");
            DropForeignKey("DL.Manager", "AccountId", "DL.Account");
            DropForeignKey("DL.Jackpot", "AccountId", "DL.Account");
            DropForeignKey("DL.Emails", "Manager_AccountId", "DL.Manager");
            DropIndex("DL.Emails", new[] { "Manager_AccountId" });
            DropIndex("DL.Transaction", new[] { "AccountId" });
            DropIndex("DL.Manager", new[] { "AccountId" });
            DropIndex("DL.Jackpot", new[] { "AccountId" });
            DropColumn("DL.Emails", "ManagerId");
            RenameColumn(table: "DL.Emails", name: "Manager_AccountId", newName: "ManagerId");
            DropPrimaryKey("DL.Manager");
            AddColumn("DL.Manager", "Balance", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("DL.Manager", "Active", c => c.Boolean(nullable: false));
            AlterColumn("DL.Emails", "ManagerId", c => c.Guid(nullable: false));
            AddPrimaryKey("DL.Manager", "ManagerId");
            CreateIndex("DL.Emails", "ManagerId");
            AddForeignKey("DL.Emails", "ManagerId", "DL.Manager", "ManagerId", cascadeDelete: true);
            DropColumn("DL.Manager", "AccountId");
            DropTable("DL.Account");
            DropTable("DL.Jackpot");
        }
        
        public override void Down()
        {
            CreateTable(
                "DL.Jackpot",
                c => new
                    {
                        AccountId = c.Guid(nullable: false),
                        JackpotStartWk = c.Int(nullable: false),
                        JackpotEndWk = c.Int(nullable: false),
                        JackpotValue = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.AccountId);
            
            CreateTable(
                "DL.Account",
                c => new
                    {
                        AccountId = c.Guid(nullable: false),
                        Balance = c.Int(nullable: false),
                        Active = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.AccountId);
            
            AddColumn("DL.Manager", "AccountId", c => c.Guid(nullable: false));
            DropForeignKey("DL.Emails", "ManagerId", "DL.Manager");
            DropIndex("DL.Emails", new[] { "ManagerId" });
            DropPrimaryKey("DL.Manager");
            AlterColumn("DL.Emails", "ManagerId", c => c.Guid());
            DropColumn("DL.Manager", "Active");
            DropColumn("DL.Manager", "Balance");
            AddPrimaryKey("DL.Manager", "AccountId");
            RenameColumn(table: "DL.Emails", name: "ManagerId", newName: "Manager_AccountId");
            AddColumn("DL.Emails", "ManagerId", c => c.Guid(nullable: false));
            CreateIndex("DL.Jackpot", "AccountId");
            CreateIndex("DL.Manager", "AccountId");
            CreateIndex("DL.Transaction", "AccountId");
            CreateIndex("DL.Emails", "Manager_AccountId");
            AddForeignKey("DL.Emails", "Manager_AccountId", "DL.Manager", "AccountId");
            AddForeignKey("DL.Jackpot", "AccountId", "DL.Account", "AccountId");
            AddForeignKey("DL.Manager", "AccountId", "DL.Account", "AccountId");
            AddForeignKey("DL.Transaction", "AccountId", "DL.Account", "AccountId", cascadeDelete: true);
        }
    }
}
