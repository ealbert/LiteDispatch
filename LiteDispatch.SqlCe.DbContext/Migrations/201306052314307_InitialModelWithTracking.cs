namespace LiteDispatch.SqlCe.DbContext.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialModelWithTracking : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Haulier",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Name = c.String(maxLength: 4000),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.DispatchNote",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        DispatchDate = c.DateTime(nullable: false),
                        DispatchNoteStatus = c.String(maxLength: 4000),
                        TruckReg = c.String(maxLength: 4000),
                        DispatchReference = c.String(maxLength: 4000),
                        CreationDate = c.DateTime(nullable: false),
                        User = c.String(maxLength: 4000),
                        LastTrackingNotification_Id = c.Long(),
                        Haulier_Id = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.TrackingNotification", t => t.LastTrackingNotification_Id)
                .ForeignKey("dbo.Haulier", t => t.Haulier_Id, cascadeDelete: true)
                .Index(t => t.LastTrackingNotification_Id)
                .Index(t => t.Haulier_Id);
            
            CreateTable(
                "dbo.TrackingNotification",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Distance = c.Double(nullable: false),
                        Duration = c.Double(nullable: false),
                        DistanceMetric = c.String(maxLength: 4000),
                        DurationMetric = c.String(maxLength: 4000),
                        Latitude = c.Double(nullable: false),
                        Longitude = c.Double(nullable: false),
                        DispatchNote_Id = c.Long(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.DispatchNote", t => t.DispatchNote_Id)
                .Index(t => t.DispatchNote_Id);
            
            CreateTable(
                "dbo.DispatchLine",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        ProductType = c.String(maxLength: 4000),
                        Product = c.String(maxLength: 4000),
                        Metric = c.String(maxLength: 4000),
                        Quantity = c.Int(nullable: false),
                        ShopId = c.Int(nullable: false),
                        ShopLetter = c.String(maxLength: 4000),
                        Client = c.String(maxLength: 4000),
                        DispatchNote_Id = c.Long(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.DispatchNote", t => t.DispatchNote_Id)
                .Index(t => t.DispatchNote_Id);
            
            CreateTable(
                "dbo.PascalRecords",
                c => new
                    {
                        L = c.Int(nullable: false),
                        R = c.Int(nullable: false),
                        P = c.Int(nullable: false),
                        Value = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => new { t.L, t.R, t.P });
            
        }
        
        public override void Down()
        {
            DropIndex("dbo.DispatchLine", new[] { "DispatchNote_Id" });
            DropIndex("dbo.TrackingNotification", new[] { "DispatchNote_Id" });
            DropIndex("dbo.DispatchNote", new[] { "Haulier_Id" });
            DropIndex("dbo.DispatchNote", new[] { "LastTrackingNotification_Id" });
            DropForeignKey("dbo.DispatchLine", "DispatchNote_Id", "dbo.DispatchNote");
            DropForeignKey("dbo.TrackingNotification", "DispatchNote_Id", "dbo.DispatchNote");
            DropForeignKey("dbo.DispatchNote", "Haulier_Id", "dbo.Haulier");
            DropForeignKey("dbo.DispatchNote", "LastTrackingNotification_Id", "dbo.TrackingNotification");
            DropTable("dbo.PascalRecords");
            DropTable("dbo.DispatchLine");
            DropTable("dbo.TrackingNotification");
            DropTable("dbo.DispatchNote");
            DropTable("dbo.Haulier");
        }
    }
}
