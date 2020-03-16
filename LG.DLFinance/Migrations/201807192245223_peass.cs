namespace LG.DLFinance.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class peass : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("DL.Fees", "Transaction_TransactionId", "DL.Transaction");
            DropForeignKey("DL.Prizes", "Transaction_TransactionId", "DL.Transaction");
            DropIndex("DL.Fees", new[] { "Transaction_TransactionId" });
            DropIndex("DL.Prizes", new[] { "Transaction_TransactionId" });
            AddColumn("DL.Transaction", "transactionType", c => c.String());
            AddColumn("DL.Transaction", "ManagerId", c => c.Guid(nullable: false));
            CreateIndex("DL.Transaction", "ManagerId");
            AddForeignKey("DL.Transaction", "ManagerId", "DL.Manager", "ManagerId", cascadeDelete: true);
            DropColumn("DL.Fees", "Transaction_TransactionId");
            DropColumn("DL.Prizes", "Transaction_TransactionId");
            DropColumn("DL.Transaction", "AccountId");
        }
        
        public override void Down()
        {
            AddColumn("DL.Transaction", "AccountId", c => c.Guid(nullable: false));
            AddColumn("DL.Prizes", "Transaction_TransactionId", c => c.Guid());
            AddColumn("DL.Fees", "Transaction_TransactionId", c => c.Guid());
            DropForeignKey("DL.Transaction", "ManagerId", "DL.Manager");
            DropIndex("DL.Transaction", new[] { "ManagerId" });
            DropColumn("DL.Transaction", "ManagerId");
            DropColumn("DL.Transaction", "transactionType");
            CreateIndex("DL.Prizes", "Transaction_TransactionId");
            CreateIndex("DL.Fees", "Transaction_TransactionId");
            AddForeignKey("DL.Prizes", "Transaction_TransactionId", "DL.Transaction", "TransactionId");
            AddForeignKey("DL.Fees", "Transaction_TransactionId", "DL.Transaction", "TransactionId");
        }
    }
}
