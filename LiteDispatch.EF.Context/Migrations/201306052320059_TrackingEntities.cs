namespace LiteDispatch.DbContext.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TrackingEntities : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TrackingNotification",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Distance = c.Double(nullable: false),
                        Duration = c.Double(nullable: false),
                        DistanceMetric = c.String(),
                        DurationMetric = c.String(),
                        Latitude = c.Double(nullable: false),
                        Longitude = c.Double(nullable: false),
                        DispatchNote_Id = c.Long(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.DispatchNote", t => t.DispatchNote_Id)
                .Index(t => t.DispatchNote_Id);
            
            AddColumn("dbo.DispatchNote", "DispatchNoteStatus", c => c.String());
            AddColumn("dbo.DispatchNote", "LastTrackingNotification_Id", c => c.Long());
            AddForeignKey("dbo.DispatchNote", "LastTrackingNotification_Id", "dbo.TrackingNotification", "Id");
            CreateIndex("dbo.DispatchNote", "LastTrackingNotification_Id");
        }
        
        public override void Down()
        {
            DropIndex("dbo.TrackingNotification", new[] { "DispatchNote_Id" });
            DropIndex("dbo.DispatchNote", new[] { "LastTrackingNotification_Id" });
            DropForeignKey("dbo.TrackingNotification", "DispatchNote_Id", "dbo.DispatchNote");
            DropForeignKey("dbo.DispatchNote", "LastTrackingNotification_Id", "dbo.TrackingNotification");
            DropColumn("dbo.DispatchNote", "LastTrackingNotification_Id");
            DropColumn("dbo.DispatchNote", "DispatchNoteStatus");
            DropTable("dbo.TrackingNotification");
        }
    }
}
