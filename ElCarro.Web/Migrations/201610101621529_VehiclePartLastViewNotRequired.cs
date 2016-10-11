namespace ElCarro.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class VehiclePartLastViewNotRequired : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.VehicleParts", "Last_View", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.VehicleParts", "Last_View", c => c.DateTime(nullable: false));
        }
    }
}
