namespace LiteDispatch.DbContext.Migrations
{
  using System.Data.Entity.Migrations;
    
    public partial class InitialModel : DbMigration
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
                        TruckReg = c.String(maxLength: 4000),
                        DispatchReference = c.String(maxLength: 4000),
                        CreationDate = c.DateTime(nullable: false),
                        User = c.String(maxLength: 4000),
                        Haulier_Id = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Haulier", t => t.Haulier_Id, cascadeDelete: true)
                .Index(t => t.Haulier_Id);
            
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
            
        }
        
        public override void Down()
        {
            DropIndex("dbo.DispatchLine", new[] { "DispatchNote_Id" });
            DropIndex("dbo.DispatchNote", new[] { "Haulier_Id" });
            DropForeignKey("dbo.DispatchLine", "DispatchNote_Id", "dbo.DispatchNote");
            DropForeignKey("dbo.DispatchNote", "Haulier_Id", "dbo.Haulier");
            DropTable("dbo.DispatchLine");
            DropTable("dbo.DispatchNote");
            DropTable("dbo.Haulier");
        }
    }
}
