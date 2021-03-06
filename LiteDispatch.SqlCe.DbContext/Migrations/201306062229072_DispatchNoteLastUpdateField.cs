namespace LiteDispatch.SqlCe.DbContext.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DispatchNoteLastUpdateField : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.DispatchNote", "LastUpdate", c => c.DateTime(nullable: false, defaultValue: DateTime.Now));
        }
        
        public override void Down()
        {
            DropColumn("dbo.DispatchNote", "LastUpdate");
        }
    }
}
