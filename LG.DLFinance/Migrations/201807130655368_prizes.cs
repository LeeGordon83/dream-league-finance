namespace LG.DLFinance.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class prizes : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("DL.TransactionType", "Transaction_TransactionId", "DL.Transaction");
            DropIndex("DL.TransactionType", new[] { "Transaction_TransactionId" });
            CreateTable(
                "DL.Prizes",
                c => new
                    {
                        PrizesId = c.Guid(nullable: false),
                        PrizeType = c.String(),
                        PrizeAmount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Transaction_TransactionId = c.Guid(),
                    })
                .PrimaryKey(t => t.PrizesId)
                .ForeignKey("DL.Transaction", t => t.Transaction_TransactionId)
                .Index(t => t.Transaction_TransactionId);
            
            AddColumn("DL.Fees", "Transaction_TransactionId", c => c.Guid());
            CreateIndex("DL.Fees", "Transaction_TransactionId");
            AddForeignKey("DL.Fees", "Transaction_TransactionId", "DL.Transaction", "TransactionId");
            DropTable("DL.TransactionType");
        }
        
        public override void Down()
        {
            CreateTable(
                "DL.TransactionType",
                c => new
                    {
                        TransactionTypeId = c.Guid(nullable: false),
                        TransactionName = c.String(),
                        Active = c.Boolean(nullable: false),
                        Transaction_TransactionId = c.Guid(),
                    })
                .PrimaryKey(t => t.TransactionTypeId);
            
            DropForeignKey("DL.Prizes", "Transaction_TransactionId", "DL.Transaction");
            DropForeignKey("DL.Fees", "Transaction_TransactionId", "DL.Transaction");
            DropIndex("DL.Prizes", new[] { "Transaction_TransactionId" });
            DropIndex("DL.Fees", new[] { "Transaction_TransactionId" });
            DropColumn("DL.Fees", "Transaction_TransactionId");
            DropTable("DL.Prizes");
            CreateIndex("DL.TransactionType", "Transaction_TransactionId");
            AddForeignKey("DL.TransactionType", "Transaction_TransactionId", "DL.Transaction", "TransactionId");
        }
    }
}
