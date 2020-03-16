namespace LG.DLFinance.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changes : DbMigration
    {
        public override void Up()
        {
            DropTable("dbo.ErrorLogs");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.ErrorLogs",
                c => new
                    {
                        ErrorLogId = c.Guid(nullable: false),
                        Error = c.String(),
                    })
                .PrimaryKey(t => t.ErrorLogId);
            
        }
    }
}
