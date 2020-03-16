namespace LG.DLFinance.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class jackpot : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "DL.Jackpot",
                c => new
                    {
                        JackpotId = c.Int(nullable: false, identity: true),
                        JackpotStartWk = c.Int(nullable: false),
                        JackpotEndWk = c.Int(nullable: false),
                        JackpotValue = c.Int(nullable: false),
                        Active = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.JackpotId);
            
        }
        
        public override void Down()
        {
            DropTable("DL.Jackpot");
        }
    }
}
