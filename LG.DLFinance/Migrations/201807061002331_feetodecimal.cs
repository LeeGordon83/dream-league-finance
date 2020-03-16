namespace LG.DLFinance.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class feetodecimal : DbMigration
    {
        public override void Up()
        {
            AlterColumn("DL.Fees", "FeeAmount", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            AlterColumn("DL.Fees", "FeeAmount", c => c.Int(nullable: false));
        }
    }
}
