namespace LiteDispatch.DbContext.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Pascal1 : DbMigration
    {
        public override void Up()
        {
            //CreateTable(
            //    "dbo.PascalRecords",
            //    c => new
            //        {
            //            L = c.Int(nullable: false),
            //            R = c.Int(nullable: false),
            //            P = c.Int(nullable: false),
            //            Value = c.Decimal(nullable: false, precision: 38, scale: 0),
            //        })
            //    .PrimaryKey(t => new { t.L, t.R, t.P });
            
        }
        
        public override void Down()
        {
            DropTable("dbo.PascalRecords");
        }
    }
}
