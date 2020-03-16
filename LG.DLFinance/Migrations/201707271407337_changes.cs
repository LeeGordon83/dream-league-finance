namespace LG.DLFinance.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changes : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("DL.TransactionType", "TransactionId", "DL.Transaction");
            DropIndex("DL.TransactionType", new[] { "TransactionId" });
            DropPrimaryKey("DL.TransactionType");
            AddColumn("DL.Transaction", "TransType_TransactionTypeId", c => c.Guid());
            AlterColumn("DL.Transaction", "Value", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddPrimaryKey("DL.TransactionType", "TransactionTypeId");
            CreateIndex("DL.Transaction", "TransType_TransactionTypeId");
            AddForeignKey("DL.Transaction", "TransType_TransactionTypeId", "DL.TransactionType", "TransactionTypeId");
            DropColumn("DL.TransactionType", "TransactionId");
            DropColumn("DL.Transaction", "MonthPaid");
        }
        
        public override void Down()
        {
            AddColumn("DL.Transaction", "MonthPaid", c => c.String());
            AddColumn("DL.TransactionType", "TransactionId", c => c.Guid(nullable: false));
            DropForeignKey("DL.Transaction", "TransType_TransactionTypeId", "DL.TransactionType");
            DropIndex("DL.Transaction", new[] { "TransType_TransactionTypeId" });
            DropPrimaryKey("DL.TransactionType");
            AlterColumn("DL.Transaction", "Value", c => c.Int(nullable: false));
            DropColumn("DL.Transaction", "TransType_TransactionTypeId");
            AddPrimaryKey("DL.TransactionType", "TransactionId");
            CreateIndex("DL.TransactionType", "TransactionId");
            AddForeignKey("DL.TransactionType", "TransactionId", "DL.Transaction", "TransactionId");
        }
    }
}
